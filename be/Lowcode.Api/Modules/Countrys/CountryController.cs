using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowcode.Api.Helpers;

using Lowcode.Framework.Data;
using Microsoft.AspNetCore.Mvc;
using Lowcode.Domain.Services;
using Lowcode.Domain.Services.CountryFacade;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Lowcode.Api.Helpers;
namespace Lowcode.Api.Modules.Countrys
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CountryService"></param>
        public CountryController(ICountryService CountryService)
        {
            _countryService = CountryService;
        }
        /// <summary>
        /// get list
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<PagedList<CountryModel>>))]
        [HttpPost]
        [Route("api/v1/Country/list")]
		[Authorize]
        public async Task< IActionResult> List([FromBody]FilterCountryModel filter)
        {
            var res = _countryService.FillterModel(filter);
            return Json(new ResultBase<PagedList<CountryModel>>() { success = true, data = new PagedList<CountryModel>() { list = res.Data, pager = filter.Paging } });
        }
		/// <summary>
        /// get list for admi
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<PagedList<CountryModel>>))]
        [HttpPost]
        [Route("api/v1/Country/list_admin")]
		//[Authorize]
        public async Task< IActionResult> ListAdmin([FromBody]FilterCountryModel filter)
        {
            var res = _countryService.FillterModel(filter);
            return Json(new ResultBase<PagedList<CountryModel>>() { success = true, data = new PagedList<CountryModel>() { list = res.Data, pager = filter.Paging } });
        }

		/// <summary>
        /// get all item
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<List<CountryModel>>))]
        [HttpPost]
        [Route("api/v1/Country/getAll")]
		[Authorize]
        public async Task< IActionResult> GetAll()
        {
            var res = _countryService.GetAll();
            return Json(res);
        }

        /// <summary>
        /// Save
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpPost]
        [Route("api/v1/Country/update")]
		[Authorize]
        public async Task<IActionResult> update([FromBody]CountryModel model)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<PagedList<CountryModel>>() { success = false, message = ModelState.GetErrorsMessage() });
           
		   var res = _countryService.Save(model);
           
            return Json(res);

        }

		// <summary>
        /// Save
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpPost]
        [Route("api/v1/Country/add")]
		[Authorize]
        public async Task<IActionResult> add([FromBody]CountryModel model)
        {
            if (!ModelState.IsValid)
                return Json(new ResultBase<PagedList<CountryModel>>() { success = false, message = ModelState.GetErrorsMessage() });
		    var res = _countryService.Save(model);
            return Json(res);

        }


        /// <summary>
        /// Delete
        /// </summary>
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<bool>))]
        [HttpDelete]
        [Route("api/v1/Country/delete")]
		[Authorize]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            var res = _countryService.Delete(id);
            return Json(res);
        }
        /// <summary>
        /// Get by Id
        /// </summary>
        [HttpGet("api/v1/Country/get_by_id")]
		[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(ResultBase<CountryModel>))]
        [Authorize]
		public async Task<IActionResult> GetById(int id)
        {
            var res = _countryService.GetById(id);
           return Json(res);
        }
    }
}