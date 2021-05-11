using Microsoft.AspNetCore.Mvc;

namespace Lowcode.Authentication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        ///     About system info
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [HttpGet]
        public string Index()
        {
            return "WGD PMS AUTHORIZATION SERVER";
        }

        [Route("/Error")]
        [HttpGet]
        public string Error()
        {
            return "INTERNAL SERVER ERROR. PLEASE CONTACT WITH ADMIN";
        }
    }
}