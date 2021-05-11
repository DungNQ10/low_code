using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Api.Helpers;

using Lowcode.Framework.Data;
using Microsoft.AspNetCore.Mvc;
using Lowcode.Domain.Services;
using Lowcode.Domain.Services.DynamicFormFacade;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Lowcode.Api.Helpers;
namespace Lowcode.Api.Modules.DynamicForms
{
    public class DynamicFormController : Controller
    {
        private readonly IDynamicFormService _dynamicformService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DynamicFormService"></param>
        public DynamicFormController(IDynamicFormService DynamicFormService)
        {
            _dynamicformService = DynamicFormService;
        }
        /// <summary>
        /// get list
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<PagedList<DynamicFormModel>>))]
        [HttpPost]
        [Route("api/v1/DynamicForm/list")]
		//[Authorize]
        public async Task< IActionResult> List([FromBody]FilterDynamicFormModel filter)
        {
            var res = await Task.Run(()=> _dynamicformService.FillterModel(filter));
            return Json(new ResultBase<PagedList<DynamicFormModel>>() { success = true, data = new PagedList<DynamicFormModel>() { list = res.Data, pager = filter.Paging } });
        }
		/// <summary>
        /// get list for admi
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<PagedList<DynamicFormModel>>))]
        [HttpPost]
        [Route("api/v1/DynamicForm/list_admin")]
		//[Authorize]
        public async Task< IActionResult> ListAdmin([FromBody]FilterDynamicFormModel filter)
        {
            var res = _dynamicformService.FillterModel(filter);
            return Json(new ResultBase<PagedList<DynamicFormModel>>() { success = true, data = new PagedList<DynamicFormModel>() { list = res.Data, pager = filter.Paging } });
        }

		/// <summary>
        /// get all item
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<DynamicFormModel>>))]
        [HttpPost]
        [Route("api/v1/DynamicForm/getAll")]
		//[Authorize]
        public async Task< IActionResult> GetAll()
        {
            var res = _dynamicformService.GetAll();
            return Json(res);
        }

        /// <summary>
        /// Save
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpPost]
        [Route("api/v1/DynamicForm/update")]
		//[Authorize]
        public async Task<IActionResult> update([FromBody]DynamicFormModel model)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<PagedList<DynamicFormModel>>() { success = false, message = ModelState.GetErrorsMessage() });
		   var res = _dynamicformService.Save(model);
           
            return Json(res);

        }

		// <summary>
        /// Save
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpPost]
        [Route("api/v1/DynamicForm/add")]
		//[Authorize]
        public async Task<IActionResult> add([FromBody]DynamicFormModel model)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<PagedList<DynamicFormModel>>() { success = false, message = ModelState.GetErrorsMessage() });
            var res = _dynamicformService.Save(model);
            return Json(res);

        }


        /// <summary>
        /// Delete
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpDelete]
        [Route("api/v1/DynamicForm/delete")]
		//[Authorize]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var res = _dynamicformService.Delete(id);
            return Json(res);
        }
        /// <summary>
        /// Get by Id
        /// </summary>
        [HttpGet("api/v1/DynamicForm/get_by_id")]
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<DynamicFormModel>))]
        //[Authorize]
		public async Task<IActionResult> GetById(int id)
        {
            var res = _dynamicformService.GetById(id);
           return Json(res);
        }
    }
}