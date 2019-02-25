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
     * http://redis.io/topics/data-types-intro#sets 
     */

    public static class RedisTags
    {
        public static List<BlogPost> posts;
        public static List<string[]> tags;
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();

            // Demo Setup
            DemoSetup(cache);

            // Tags are easily represented as Redis Sets
            foreach (BlogPost post in posts)
            {
                string redisKey = string.Format(CultureInfo.InvariantCulture,
                    "blog:posts:{0}:tags", post.Id);
                // Add tags to the blog post in redis
                cache.SetAdd(
                    redisKey, post.Tags.Select(s => (RedisValue)s).ToArray());

                // Now do the inverse so we can figure how which blog posts have a given tag.
                foreach (var tag in post.Tags)
                {
                    cache.SetAdd(string.Format(CultureInfo.InvariantCulture,
                        "tag:{0}:blog:posts", tag), post.Id);
                }
            }

            // Show the tags for blog post #1
            Console.WriteLine("Show the tags for blog post #1");
            foreach (var value in cache.SetMembers("blog:posts:1:tags"))
            {
                Console.WriteLine(value);
            }


            // Show the tags in common for blog posts #1 and #2
            Console.WriteLine("Show the tags in common for blog posts #1 and #2");
            foreach (var value in cache.SetCombine(SetOperation.Intersect, new RedisKey[] { "blog:posts:1:tags", "blog:posts:2:tags" }))
            {
                Console.WriteLine(value);
            }

            // Show the ids of the blog posts that have the tag "iot".
            Console.WriteLine("Show the ids of the blog posts that have the tag iot");
            foreach (var value in cache.SetMembers("tag:iot:blog:posts"))
            {
                Console.WriteLine(value);
            }
        }

        private static void DemoSetup(IDatabase cache)
        {
            // Using tags to cross-correlate cached items
            RedisTags.tags = new List<string[]>()
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

            RedisTags.posts = new List<BlogPost>();
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
