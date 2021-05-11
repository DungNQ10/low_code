using Lowcode.Domain.Data;
using Lowcode.Domain.Services;
using Lowcode.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lowcode.Domain.Modules.CodeIt.Services
{
    public interface IExtractDbService
    {
        List<string> GetTablesName();
        TableInfo GetTableInfo(string tableName);
        DataTable ExecQuery(string query, List<ParamInfo> paramaters);
        DynamicQueryResult ExecQuery(DynamicQueryPath model);
        ActionResultType< DataTable> DynamicInsertUpdate(ObjectInfoModel model);
    }
}
