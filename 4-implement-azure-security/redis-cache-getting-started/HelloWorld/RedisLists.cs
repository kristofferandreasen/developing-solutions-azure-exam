using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    /*
     http://redis.io/topics/data-types-intro#lists
     * What is it?
     * LinkedLists
     * Scenario: Add items to a long list in a fast manner in order.
     * Insert time constant at beginning or end
     * Lookup time is constant.
     * eg. last 5 tweets, inter process communication.
     * */

    public static class RedisLists
    {
        public static List<BlogPost> posts;
        public static List<string[]> tags;
        public static string redisKey = "blog:recent_posts";
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();

            DemoSetup(cache);

            // Add 5 posts as read
            for (int i = 0; i < 5; i++)
            {
                BlogPost blogPost = posts.Find(p => p.Id == i); // reference to the blog post that has just been read
                cache.ListLeftPushAsync(RedisLists.redisKey, blogPost.Title); // push the blog post onto the list 
            }

            // Access the last 5 read posts
            Console.WriteLine("Access the last 5 read posts");
            foreach (string postTitle in cache.ListRange(RedisLists.redisKey, 0, 5))
            {
                Console.WriteLine(postTitle);
            }

            // Always show only last 5 read posts.
            Console.WriteLine("Always show only last 5 read posts.");
            cache.ListTrim(RedisLists.redisKey, 0, 5);
            foreach (string postTitle in cache.ListRange(RedisLists.redisKey, 0, 5))
            {
                Console.WriteLine(postTitle);
            }
        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete(RedisLists.redisKey);
            // Using tags to cross-correlate cached items
            RedisLists.tags = new List<string[]>()
            {
                new string[] { "iot","csharp" },
                new string[] { "iot","azure","csharp" },
                new string[] { "csharp","git","big data" },
                new string[] { "iot","git","database" },
                new string[] { "database","git" },
                new string[] { "csharp","database" },
                new string[] { "iot" },
                new string[] { "iot","database","git" },
                new string[] { "azure","database","big data","git","csharp" },
                new string[] { "azure" }
            };

            RedisLists.posts = new List<BlogPost>();
            int blogKey = 0;
            int blogPostId = 0;
            int numberOfPosts = 20;
            Random random = new Random();
            for (int i = 0; i < numberOfPosts; i++)
            {
                blogPostId = blogKey++;
                posts.Add(new BlogPost(
                    blogPostId,               // Blog post ID
                    string.Format(CultureInfo.InvariantCulture, "Blog Post #{0}",
                        blogPostId),          // Blog post title
                    random.Next(100, 10000),  // Ranking score
                    tags[i % tags.Count]));   // Tags – assigned from a collection 
                // in the tags list
            }

            foreach (BlogPost post in posts)
            {
                string redisKey = string.Format(CultureInfo.InvariantCulture,
                    "blog:posts:{0}:tags", post.Id);
                // Add tags to the blog post in redis
                cache.KeyDelete(redisKey);

                // Now do the inverse so we can figure how which blog posts have a given tag.
                foreach (var tag in post.Tags)
                {
                    cache.KeyDelete(string.Format(CultureInfo.InvariantCulture,
                        "tag:{0}:blog:posts", tag));
                }
            }
        }
    }
}
