using Lowcode.Domain.Services;
using Lowcode.Framework.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt.Services
{
    public class ExtractDbService : IExtractDbService
    {

        private Data.DomainContext context;

        public ExtractDbService()
        {
            context = new Data.DomainContext();
        }

        public List<string> GetTablesName()
        {
            var result = new List<string>();
            var sqlCommandString = @"SELECT TABLE_NAME
                                    FROM INFORMATION_SCHEMA.TABLES
                                    WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG=@DBName";
            var queryResult = context.ExecDataTable(sqlCommandString, CommandType.Text, new SqlParameter[] { new SqlParameter("@DBName", context.GetDatabaseName()) });
            result = queryResult.AsEnumerable().Select(c => c["TABLE_NAME"].ToString()).ToList();
            return result;
        }

        public List<ColumnInfo> GetColumnsByTable(string tableName)
        {

            var sqlCommandString = @"select COLUMN_NAME as ColumnName, DATA_TYPE as DataType, CHARACTER_MAXIMUM_LENGTH as [MaxLength],      
                                      CONVERT(bit, (CASE IS_NULLABLE
														WHEN 'Yes' THEN 1
														WHEN 'No' THEN 0
													END)
													 ) as IsNullable 
                                    from INFORMATION_SCHEMA.COLUMNS
                                    where TABLE_NAME=@TableName";
            var queryResult = context.ExecDataTable(sqlCommandString, CommandType.Text, new SqlParameter[] { new SqlParameter("@TableName", tableName) });
            var result = queryResult.ToList<ColumnInfo>();
            return result;
        }

        public string GetPrimaryKey(string tableName)
        {
            var sqlCommandString = @"SELECT 
                                        c.name AS column_name,
                                        i.name AS index_name,
                                        c.is_identity
                                    FROM sys.indexes i
                                        inner join sys.index_columns ic  ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                                        inner join sys.columns c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                                    WHERE i.is_primary_key = 1
                                        and i.object_ID = OBJECT_ID(@TableName);";
            var queryResult = context.ExecDataTable(sqlCommandString, CommandType.Text, new SqlParameter[] { new SqlParameter("@TableName", tableName) });
            var result = queryResult.AsEnumerable().Select(c => c["column_name"].ToString()).FirstOrDefault();
            return result;

        }

        public TableInfo GetTableInfo(string tableName)
        {
            var primarykey = GetPrimaryKey(tableName);
            var result = new TableInfo()
            {
                TableName = tableName,
                PrimaryKey = primarykey,
                ProjectName = string.Empty
            };

            result.ColumnInfos = GetColumnsByTable(tableName);
            return result;
        }

        public DataTable ExecQuery(string query, List<ParamInfo> paramaters)
        {
            DataSet dtSet = new DataSet();
            SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand command = new SqlCommand(query, sqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            using (command)
            {
                command.CommandType = CommandType.Text;
                if (paramaters != null)
                {
                    foreach (var para in paramaters)
                    {
                        command.Parameters.Add(new SqlParameter(para.Name, para.Value));
                    }
                }

                adapter.Fill(dtSet);
            }
            return dtSet.Tables[0];
        }

        public DynamicQueryResult ExecQuery(DynamicQueryPath model)
        {
            var query = "SELECT ";
            var pagingQuery = "";
            if (model.PageIndex.HasValue && model.PageSize.HasValue)
            {
                query += "totalCount = COUNT(*) OVER(),";
                pagingQuery = " OFFSET @PageSize * (@PageNumber - 1) ROWS FETCH NEXT @PageSize ROWS ONLY";
                model.Paramaters.Add(new ParamInfo() { Name = "@PageSize", Value = model.PageSize });
                model.Paramaters.Add(new ParamInfo() { Name = "@PageNumber", Value = model.PageIndex });
            }
            query += model.Select;
            if (model.OrderPaths.Count > 0)
            {
                var order = " Order By " + model.OrderPaths[0].Name + " " + (model.OrderPaths[0].Order == OrderType.ASC ? "ASC " : "DESC ");
                for (int i = 1; i < model.OrderPaths.Count; i++)
                {
                    order += " AND " + model.OrderPaths[i].Name + " " + (model.OrderPaths[i].Order == OrderType.ASC ? "ASC " : "DESC ");
                }
                query += order;
            }
            else
            {
                query += " Order By Id desc ";
            }
            if (!string.IsNullOrWhiteSpace(model.WhereCondition))
                query += (" where " + model.WhereCondition);
            query += pagingQuery;
            var res = ExecQuery(query, model.Paramaters);
            var result = new DynamicQueryResult()
            {
                List = res,
                Paging = new Framework.Data.PagingInfo(model.PageSize.HasValue ? model.PageSize.Value : 0, model.PageIndex.HasValue ? model.PageIndex.Value : 1)
            };
            if (!string.IsNullOrWhiteSpace(pagingQuery))
            {
                if (res.Rows.Count == 0)
                {
                    result.Paging.RowsCount = 0;
                }
                else
                {
                    result.Paging.RowsCount = Convert.ToInt32(res.Rows[0]["totalCount"]);
                }
            }
            return result;
        }

        public ActionResultType<DataTable> DynamicInsertUpdate(ObjectInfoModel model)
        {
            DataTable tbResult = null;
            var res = new ActionResultType<DataTable>() { Data = null, Success = false };
            var tableInfo = GetTableInfo(model.TableName);
            var columnNames = tableInfo.ColumnInfos.Select(c => c.ColumnName.ToLower());
            var idField = model.Fields.FirstOrDefault(c => c.FieldName.ToLower() == "id");
            var mappingFields = model.Fields.Where(c => columnNames.Contains(c.FieldName.ToLower())
            && string.Compare(c.FieldName, "Id", true) != 0).ToList();
            if (!(idField != null && idField.FieldValue.ToString() != "0"))
            {
                var command = $"insert into [{model.TableName}] ";
                var insertColumns = new List<string>();
                var valueParams = new List<string>();
                var paramaters = new List<SqlParameter>();
                foreach (var field in mappingFields)
                {
                    insertColumns.Add($"[{field.FieldName}]");
                    valueParams.Add($"@{field.FieldName}");
                    paramaters.Add(new SqlParameter($"@{field.FieldName}", field.FieldValue));
                }
                command += "(" + (string.Join(" , ", insertColumns)) + " ) ";
                command += $"values({string.Join(", ", valueParams)})";
                command += "    SELECT CAST(scope_identity() AS int);";
                var newId = context.ExecScalar(command, paramaters);
                tbResult = context.ExecDataTable($"select * from [{model.TableName}] where id=@id", CommandType.Text, new SqlParameter("@id", newId));
            }
            else
            {
                var command = $"update [{model.TableName}] set ";
                var listSetValues = new List<string>();
                var paramaters = new List<SqlParameter>();
                foreach (var field in mappingFields)
                {
                    listSetValues.Add($"[{field.FieldName}]=@{field.FieldName}");
                    paramaters.Add(new SqlParameter($"@{field.FieldName}", field.FieldValue));
                }
                paramaters.Add(new SqlParameter("@Id", idField.FieldValue));

                command += (string.Join(" , ", listSetValues));
                command += $" where id=@id";
                context.ExecScalar(command, paramaters);
                tbResult = context.ExecDataTable($"select * from [{model.TableName}] where id=@id", CommandType.Text, new SqlParameter("@id", idField.FieldValue));
            }
            return new ActionResultType<DataTable>() { Success = true, Data = tbResult };
        }
    }
}
