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
     * Leaderboard scenarios.
     * http://redis.io/topics/data-types-intro#sorted-sets
     *  
     */

    public static class RedisSortedSets
    {
        public static List<BlogPost> posts;
        public static List<string[]> tags;
        public static string redisKey = "blog:post_rankings";
        public static void Run()
        {
            IDatabase cache = Helper.Connection.GetDatabase();

            DemoSetup(cache);

            // Rate 5 posts
            for (int i = 0; i < 5; i++)
            {
                BlogPost blogPost = posts.Find(p => p.Id == i); 
                cache.SortedSetAdd(redisKey, blogPost.Title,blogPost.Score); 
            }

            Console.WriteLine("SortedSetRangeByRankWithScores");
            foreach (var post in cache.SortedSetRangeByRankWithScores(redisKey))
            {
                Console.WriteLine(post);
            }

            Console.WriteLine("SortedSetRangeByRankWithScores Descending");
            foreach (var post in cache.SortedSetRangeByRankWithScores(
                               redisKey, 0, 9, Order.Descending))
            {
                Console.WriteLine(post);
            }

            Console.WriteLine("Blog posts with scores between 3000 and 8000");
            foreach (var post in cache.SortedSetRangeByScoreWithScores(
                                           redisKey, 3000,8000))
            {
                Console.WriteLine(post);
            }

        }

        private static void DemoSetup(IDatabase cache)
        {
            cache.KeyDelete(RedisSortedSets.redisKey);
            // Using tags to cross-correlate cached items
            RedisSortedSets.tags = new List<string[]>()
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

            RedisSortedSets.posts = new List<BlogPost>();
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
