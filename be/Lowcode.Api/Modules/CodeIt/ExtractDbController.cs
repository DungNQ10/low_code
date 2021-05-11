using Lowcode.Api.Modules;
using Lowcode.Domain.Modules.CodeIt;
using Lowcode.Domain.Modules.CodeIt.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Lowcode.Api.Modules.CodeIt
{
    public class ExtractDbController: Controller
    {
        private IExtractDbService extractDbService;
        public ExtractDbController(IExtractDbService extractDbService)
        {
            this.extractDbService = extractDbService;
        }

        [HttpPost("api/v1/extractdb/tables")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<string>>))]
        public async Task<IActionResult> GetTables()
        {
            var res =  extractDbService.GetTablesName();
            return Json(new ResultBase<List<string>>() { success = true, data = res });
        }

        [HttpPost("api/v1/extractdb/tableInfo")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<TableInfo>))]
        public async Task<IActionResult> GetTableInfo(string tableName)
        {
            var res = extractDbService.GetTableInfo(tableName);
            return Json(new ResultBase<TableInfo>() { success = true, data = res });
        }

        [HttpPost("api/v1/extractdb/dynamicquery")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<DataTable>))]
        public async Task<IActionResult> DynamicQuery([FromBody] DynamicQueryModel model)
        {
            var res = extractDbService.ExecQuery(model.Query,model.Paramaters);
            return Json(new ResultBase<DataTable>() { success = true, data = res });
        }

        [HttpPost("api/v1/extractdb/dynamicQueryWithPaging")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<DynamicQueryResult>))]
        public async Task<IActionResult> WithPaging([FromBody] DynamicQueryPath model)
        {
            var res = extractDbService.ExecQuery(model);
            return Json(new ResultBase<DynamicQueryResult>() { success = true, data = res });
        }

        [HttpPost("api/v1/extractdb/dynamicsave")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<DataTable>))]
        public async Task<IActionResult> DynamicSave([FromBody] ObjectInfoModel model)
        {
            var res = extractDbService.DynamicInsertUpdate(model);
            return Json(new ResultBase<DataTable>() { success = true, data = res.Data });
        }
    }
}
