using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NPC.PMS.Framework.Utils
{
    public class UrlHelper
    {
        private static object _sync = new object();
        /// <summary>
        /// Lấy host url của ứng dụng web. VD: http://vnsolution.com.vn/home.aspx 
        /// --> Phương thức trả về: http://vnsolution.com.vn
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentHostUrl()
        {
            lock (_sync)
            {
                HttpRequest request = HttpContext.Current.Request;

                string Port = request.ServerVariables["SERVER_PORT"];

                if (Port == null || Port == "80" || Port == "443")

                    Port = "";

                else

                    Port = ":" + Port;

                string Protocol = request.ServerVariables["SERVER_PORT_SECURE"];

                if (Protocol == null || Protocol == "0")

                    Protocol = "http://";

                else

                    Protocol = "https://";

                string sBasePath = Protocol + request.ServerVariables["SERVER_NAME"] + Port + request.ApplicationPath;

                if (sBasePath.EndsWith("/"))
                    sBasePath = sBasePath.Substring(0, sBasePath.LastIndexOf("/"));

                HttpContext.Current.Trace.Write("Root Url", sBasePath);

                return sBasePath;
            }
        }

        /// <summary>
        /// Lấy host url của ứng dụng web. VD: http://vnsolution.com.vn/home.aspx 
        /// --> Phương thức trả về: http://vnsolution.com.vn
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentHostUrl(string requestPath)
        {
            lock (_sync)
            {
                Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

                string url = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

                return url;
            }
        }

        public static string GetCurrentDomainName()
        {
            //lock (_sync)
            {
                if (HttpContext.Current.Items["^__CURRENTHOST__$"] == null)
                {
                    //HttpContext.Current.Response.Write("<br/>" +HttpContext.Current.Request.Url.AbsoluteUri);
                    Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

                    string url = uri.Host;
                    //HttpContext.Current.Response.Write("<br/>" + url);
                    if (uri.Port != 80)
                    {
                        url += ":" + uri.Port;
                    }
                    HttpContext.Current.Items["^__CURRENTHOST__$"] = url;
                    return url;
                }
                else
                {
                    return HttpContext.Current.Items["^__CURRENTHOST__$"].ToString();
                }
            }
        }
    }
}
