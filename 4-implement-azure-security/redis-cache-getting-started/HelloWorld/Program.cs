using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            string redisCacheName = ConfigurationManager.AppSettings["redisCacheName"];
            string redisCachePassword = ConfigurationManager.AppSettings["redisCachePassword"];

            if (string.IsNullOrWhiteSpace(redisCacheName) || string.IsNullOrWhiteSpace(redisCachePassword) ||
                redisCacheName.StartsWith("TODO") || redisCachePassword.StartsWith("TODO"))
            {

                Console.WriteLine("Please update your Redis Cache credentials in App.config");
                Console.ReadKey();
                return;
            }

            GettingStarted.Run();
            //RedisString.Run();
            //GeneralConcepts.Run();
            //RedisTags.Run();
            //RedisLists.Run();
            //RedisSortedSets.Run();
            //ConnectionOptions.Run();
            //KeySpaceNotifications.Run();
        }
    }
}
