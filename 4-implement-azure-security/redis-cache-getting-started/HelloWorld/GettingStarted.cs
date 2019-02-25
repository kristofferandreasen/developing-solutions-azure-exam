using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public static class GettingStarted
    {
        public static void Run()
        {
           IDatabase cache = Connection.GetDatabase();
            
            // Demo Setup
            DemoSetup(cache);
            
            // App Usage
            var itemKey = "itemkey";

            string itemValue = cache.StringGet(itemKey);
            if(itemValue == null)
            {
                // Get From Persistent storage and store in Cache.
                itemValue = GetFromPersistentStorage(itemKey);
                cache.StringSet(itemKey, itemValue);
            }

            Console.WriteLine("Value retrieved =" + cache.StringGet(itemKey));

        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            // Replace these values with the values from your Azure Redis Cache instance.
            // For more information, see http://aka.ms/ConnectToTheAzureRedisCache
            string redisCacheName = ConfigurationManager.AppSettings["redisCacheName"];
            string redisCachePassword = ConfigurationManager.AppSettings["redisCachePassword"];
            return ConnectionMultiplexer.Connect(redisCacheName + ",ssl=true,abortConnect=false,password=" + redisCachePassword);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete("itemkey");
            cache.KeyDelete("i");
        }

        private static string GetFromPersistentStorage(string itemKey)
        {
            return DateTime.Now.ToString();
        }
    }
}
