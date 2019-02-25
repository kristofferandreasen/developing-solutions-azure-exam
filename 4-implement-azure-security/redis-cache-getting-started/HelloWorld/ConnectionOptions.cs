using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    // Single Instance for app. - Reuse ConnectionMultiplexer
    // Timeout options
    // Key Expiration options
    // How should the multiplexer be configured.
    public static class ConnectionOptions
    {
        public static void Run()
        {
            
            IDatabase cache = Connection.GetDatabase();

            // Demo Setup
            DemoSetup(cache);
            //String
            cache.StringSet("i", 1);
            Console.WriteLine("Current Value=" + cache.StringGet("i"));
            
        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete("i");
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions config = new ConfigurationOptions();
            config.EndPoints.Add("pranavracachedemo1.redis.cache.windows.net");
            config.Password = "QBLdCV+Ov0Q6Es/Te/45iTTH05UuHFjh+GhHFxxTaCQ=";
            config.Ssl = true;
            config.AbortOnConnectFail = false;
            config.ConnectRetry = 5;
            config.ConnectTimeout = 1000;
            return ConnectionMultiplexer.Connect(config);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
