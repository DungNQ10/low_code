using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowcode.Api.Configuration
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Api server path
        /// </summary>
        public string ApiServer { set; get; }

        /// <summary>
        /// Authentication server path
        /// </summary>
        public string AuthenticationServer { set; get; }

        /// <summary>
        /// Application error mode
        /// </summary>
        public ErrorMode CustomErrorMode { set; get; }

        /// <summary>
        /// List client app redirect uri
        /// </summary>
        public string[] ClientAppRedirectUri { set; get; }
        /// <summary>
        /// RedisDbNumber
        /// </summary>
        public int RedisDbNumber { get; set; }
        public string MPPApi { get; set; }
        public string HashidsSalt { get; set; }
        public string UploadFolder { get; set; }
    }

    /// <summary>
    /// Api error displaying mode
    /// </summary>
    public enum ErrorMode
    {
        /// <summary>
        /// Do not show any error full stack trace for client
        /// </summary>
        On,

        /// <summary>
        /// Show full stack error for all client
        /// </summary>
        Off,

        /// <summary>
        /// Only show stack trace for local request
        /// </summary>
        RemoteOnly
    }
}
