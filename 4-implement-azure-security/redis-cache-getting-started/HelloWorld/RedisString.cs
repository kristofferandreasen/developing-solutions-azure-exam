using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld
{
    public static class RedisString
    {
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();

            // Demo Setup
            DemoSetup(cache);

            //Single SET/GET
            cache.StringSet("i", 1);
            Console.WriteLine("Current Value=" + cache.StringGet("i"));

            //Object
            var contact2 = new Contact() { Id = 1, EmailAddress = "pranavra@microsoft.com", Name = "pranav" };
            cache.StringSet("p", JsonConvert.SerializeObject(contact2));
            var result1 = JsonConvert.DeserializeObject<Contact>(cache.StringGet("p"));
            Console.WriteLine(result1.EmailAddress);

            // Key Expiration
            cache.StringSet("iexp1", 1, TimeSpan.FromSeconds(1));
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Key has expired. Value =" + cache.StringGet("iexp1"));
            
        }
        public class Contact
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete("i");
            cache.KeyDelete("p");
            cache.KeyDelete("iexp1");
        }

    }
}
