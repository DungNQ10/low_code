using Lowcode.Framework.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lowcode.Framework.Caching
{
    public static class CacheKeyHelper
    {
        //public static string GetChecksum(this object obj)
        //{
        //    var json = JsonConvert.SerializeObject(obj);

        //    return EncryptUtil.EncryptMD5(json);
        //}

        public static string GetChecksum(object obj, Type type, string action)
        {
            //var json = JsonConvert.SerializeObject(obj);
            //json = json + "_" + type.FullName + "_" + action;
            //return EncryptUtil.EncryptMD5(json);

            var key = CreateCacheKey(obj);//, type = type, action = action });

            key += "_" + type.FullName + "_" + action;

            return key;
        }

        public static string CreateCacheKey(this object obj, string propName = null)
        {
            var sb = new StringBuilder();
            if (obj.GetType().IsValueType || obj is string)
                sb.AppendFormat("{0}_{1}|", propName, obj);
            else
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType))
                    {
                        var get = prop.GetGetMethod();
                        if (!get.IsStatic && get.GetParameters().Length == 0)
                        {
                            var collection = (IEnumerable<object>)get.Invoke(obj, null);
                            if (collection != null)
                                foreach (var o in collection)
                                    sb.Append(o.CreateCacheKey(prop.Name));
                        }
                    }
                    else
                        sb.AppendFormat("{0}{1}_{2}|", propName, prop.Name, prop.GetValue(obj, null));

                }
            return sb.ToString();
        }
    }
}
