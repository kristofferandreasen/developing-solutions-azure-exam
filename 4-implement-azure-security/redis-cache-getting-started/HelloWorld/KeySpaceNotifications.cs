using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    /// <summary>
    /// More info about Key space notifications http://redis.io/topics/notifications
    /// In this sample we have set KEA as the value.
    /// </summary>
    class KeySpaceNotifications
    {
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();
            ISubscriber subscriber = Helper.Connection.GetSubscriber();
            // Demo Setup
            DemoSetup(cache);

            //Single SET/GET
            // Add a key/ value to Redis using a different client

            while (true)
            {
                subscriber.Subscribe("__keyspace@0__:*", (channel, value) =>
                {
                    Console.WriteLine("Notification raised=" + value);
                }
                );
            }
        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete("i");
        }
    }
}
