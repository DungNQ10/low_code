using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lowcode.Framework.Caching
{
    public class CacheManager
    {
        private static ConnectionMultiplexer connectionMultiplexer;
        private static IDatabase database;
        private static CacheManager _instance;
        private CacheManager()
        {
            ////use locally redis installation
            //var connectionString = string.Format("{0}:{1}", "127.0.0.1", 6379);

            //connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            //database = connectionMultiplexer.GetDatabase();
        }
        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CacheManager();
                return _instance;
            }
            set { }
        }

        public bool Set(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return database.StringSet(key, value, expiry);
        }


        public bool Set<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            return false;

            var obj = JsonConvert.SerializeObject(value);
            return database.StringSet(key, obj, expiry);
        }

        public string Get(string key)
        {
            return null;

            return database.StringGet(key);
        }

        public T Get<T>(string key)
        {
            return default(T);

            var result = database.StringGet(key);
            if (result.HasValue)
                return JsonConvert.DeserializeObject<T>(result);
            return default(T);
        }

        public bool Delete(string key)
        {
            return database.KeyDelete(key);
        }
    }
}
