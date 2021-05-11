using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lowcode.Framework.Caching
{
    public class RedisConnection
    {
        static RedisConnection()
        {
            lazyConnection = new Lazy<StackExchange.Redis.ConnectionMultiplexer>(() =>
            {
                return StackExchange.Redis.ConnectionMultiplexer.Connect("localhost");
            });
        }

        private static Lazy<StackExchange.Redis.ConnectionMultiplexer> lazyConnection;

        public StackExchange.Redis.ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
