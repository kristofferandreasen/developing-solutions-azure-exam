using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public static class GeneralConcepts
    {
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();

            // Demo Setup
            DemoSetup(cache);

            //String SET
            cache.StringSet("i1", 1);
            cache.StringSet("i2", 2);

            // Pipeling
            #region Pipelining
            var task1 = cache.StringGetAsync("i1");
            var task2 = cache.StringGetAsync("i2");

            var customer1 = cache.Wait(task1);
            var customer2 = cache.Wait(task2);
            #endregion

            #region Atomic
            Console.WriteLine("######Atomic######");
            
            cache.StringSet("i3", 99);
            Console.WriteLine("old=" + 99);
           
            long oldValue = cache.StringIncrement("i3");
            Console.WriteLine("new=" + oldValue);

            string resultGetSet = cache.StringGetSet("i3", 0);
            Console.WriteLine("GETSET=" + resultGetSet);
            Console.WriteLine("GETSET=" + cache.StringGet("i3"));

            #endregion


            #region MGET/MSET
            Console.WriteLine("######MGET/MSET######");
            var keysAndValues =
            new List<KeyValuePair<RedisKey, RedisValue>>()
                {
                    new KeyValuePair<RedisKey, RedisValue>("i:1", "value1"),
                    new KeyValuePair<RedisKey, RedisValue>("i:2", "value2"),
                    new KeyValuePair<RedisKey, RedisValue>("i:3", "value3")
                };

            // Store the list of key/value pairs in the cache
            cache.StringSet(keysAndValues.ToArray());

            // Find all values that match a list of keys
            RedisKey[] keys = { "i:1", "i:2", "i:3" };
            RedisValue[] values = null;
            values = cache.StringGet(keys);
            
            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Transaction
            Console.WriteLine("######Transaction######");
            ITransaction transaction = cache.CreateTransaction();
            
            var tx1 = transaction.StringIncrementAsync("i:t1");
            var tx2 = transaction.StringDecrementAsync("i:t2");
            bool result = transaction.Execute();
            
            Console.WriteLine("Transaction {0}", result ? "succeeded" : "failed");
            Console.WriteLine("Result of increment: {0}", tx1.Result);
            Console.WriteLine("Result of decrement: {0}", tx2.Result);
            #endregion


        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete("i1");
            cache.KeyDelete("i2");
            cache.KeyDelete("i3");
            cache.KeyDelete("i:1");
            cache.KeyDelete("i:2");
            cache.KeyDelete("i:3");
            cache.KeyDelete("i:t1");
            cache.KeyDelete("i:t2");
        }
    }

}
