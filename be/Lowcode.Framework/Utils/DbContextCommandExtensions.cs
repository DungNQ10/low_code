using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Lowcode.Framework.Utils
{
    public static class DbContextCommandExtensions
    {

        /// <summary>
        /// Execute store procedure return dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="commandType"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static DataSet ExecDataSet(this DbContext context, string procName, CommandType commandType = CommandType.StoredProcedure, params Object[] paramaters) 
        {

            
                DataSet dtSet = new DataSet();
                SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand command = new SqlCommand(procName, sqlConn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                using (command)
                {
                    command.CommandType = commandType;
                    if (paramaters != null)
                    {
                        foreach (var para in paramaters)
                        {
                            command.Parameters.Add(para);
                        }
                    }

                    adapter.Fill(dtSet);
                }
                return dtSet;
           

        }

        /// <summary>
        /// exec nonquery using for insert, update, delete
        /// </summary>
        /// <typeparam name="T">db context</typeparam>
        /// <param name="sqlCommand"> </param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static int ExecCommand(this DbContext context, string sqlCommand, params Object[] paramaters)
        {
         

                SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand command = new SqlCommand(sqlCommand, sqlConn);
                using (command)
                {
                    sqlConn.Open();
                    command.CommandType = CommandType.Text;
                    if (paramaters != null)
                    {
                        foreach (var para in paramaters)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    var result = command.ExecuteNonQuery();
                    return result;

                }

          
        }
        /// <summary>
        /// exec nonquery using for insert, update, delete
        /// </summary>
        /// <typeparam name="T">db context</typeparam>
        /// <param name="sqlCommand"> </param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static int ExecCommandProc(this DbContext context, string sqlCommand, CommandType commandType, params Object[] paramaters) 
        {
          

                SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand command = new SqlCommand(sqlCommand, sqlConn);
                using (command)
                {
                    sqlConn.Open();
                    command.CommandType = commandType;
                    if (paramaters != null)
                    {
                        foreach (var para in paramaters)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    var result = command.ExecuteNonQuery();
                    return result;

                }

        }

        /// <summary>
        /// Execute procedure return datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="commandType"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static DataTable ExecDataTable( this DbContext context, string procName, CommandType commandType = CommandType.StoredProcedure, params Object[] paramaters) 
        {
            try
            {
                var dataSet = context.ExecDataSet(procName, commandType, paramaters);
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
               
                return new DataTable();

            }
        }

        /// <summary>
        /// Exec procedure return entity
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="commandType"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static IList<T> ExecEntity< T>(this DbContext context, string procName, CommandType commandType = CommandType.StoredProcedure, params Object[] paramaters )
            where T : new()
        {
            DataTable tb = context.ExecDataTable(procName, commandType, paramaters);
            return tb.ToList<T>();
        }

        /// <summary>
        /// Execute query return int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="commandType"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static object ExecScalar(this DbContext context, string sqlCommand, object[] paramaters = null) 
        {
          
                SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
                var result = new object();
                try
                {
                    sqlConn.Open();
                    SqlCommand command = new SqlCommand(sqlCommand, sqlConn);

                    using (command)
                    {
                        command.CommandType = CommandType.Text;
                        if (paramaters != null)
                        {
                            foreach (var para in paramaters)
                            {
                                command.Parameters.Add(para);
                            }
                        }
                        result = command.ExecuteScalar();
                    }

                }
                catch (Exception ex)
                {
                 

                }
                finally
                {
                    sqlConn.Close();
                }
                return result;
            }


        /// <summary>
        /// Execute query return int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="commandType"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        public static object ExecScalar(this DbContext context, string sqlCommand, List<SqlParameter> paramaters )
        {

            SqlConnection sqlConn = (SqlConnection)context.Database.GetDbConnection();
            var result = new object();
            try
            {
                sqlConn.Open();
                SqlCommand command = new SqlCommand(sqlCommand, sqlConn);

                using (command)
                {
                    command.CommandType = CommandType.Text;
                    if (paramaters != null)
                    {
                        foreach (var para in paramaters)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    result = command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }
        public static string GetDatabaseName(this DbContext context)
        {
            return context.Database.GetDbConnection().Database;

        }
    }

  
    }


