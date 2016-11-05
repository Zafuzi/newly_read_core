using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NewlyReadCore.SQLite;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace NewlyReadCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            reseedSources();
            var sourcesTimer = new System.Threading.Timer((e) =>
               {
                   Console.WriteLine("\n\n Reseeding Sources \n\n");
                   reseedSources();
               }, null, TimeSpan.Zero, TimeSpan.FromDays(7));

            var articleTimer = new System.Threading.Timer((e) =>
                {
                    Console.WriteLine("\n\n Reseeding Articles \n\n");
                    reseedArticles();
                }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        private static void reseedArticles()
        {
            var db = new MyDBContext();
            seedArticles(db);
        }

        private static void reseedSources()
        {
            var db = new MyDBContext();
            if (db.Sources != null)
            {
                // TODO rewrite these to not replace items that already exist, also two seperate timers, one that checks for new sources every week and the other that checks frequently for new articles. Also deleting items should be dynamic, as in it is only deleted after a set period of time and new articles are added if they don't already exist.
                foreach (var source in db.Sources)
                {
                    db.Sources.Remove(source);
                }
                db.SaveChanges();
            }
            seedSources(db);
        }

        // Make a request to newsapi.org
        private static JObject newsRequest(string resource)
        {
            //  This method will be called after migrating to the latest version.
            var client = new RestClient("https://newsapi.org/v1");
            var request = new RestRequest();
            request.Resource = resource;
            request.AddHeader("ContentType", "application/json");

            // execute the request
            // TODO add a fallback method to handle request failure.
            var restResponse = client.Execute(request);
            var content = restResponse.Content;

            return JObject.Parse(content);
        }

        // Seed the sources DB
        private static void seedSources(MyDBContext context)
        {
            JObject data = newsRequest("sources/?language=en");
            JToken[] source = data.GetValue("sources").Children().ToArray();
            if (context.Sources == null)
            {
                return;
            }
            else
            {

                foreach (var key in source)
                {
                    if (key == null)
                    {
                        return;
                    }
                    else
                    {
                        context.Sources.Add(
                            new Source
                            {
                                sourceID = key.Children().ElementAt(0).Last().ToString(),
                                name = key.Children().ElementAt(1).Last().ToString(),
                                description = key.Children().ElementAt(2).Last().ToString(),
                                url = key.Children().ElementAt(3).Last().ToString(),
                                category = key.Children().ElementAt(4).Last().ToString(),
                                language = key.Children().ElementAt(5).Last().ToString(),
                                country = key.Children().ElementAt(6).Last().ToString(),
                                urlsToLogos = key.Children().ElementAt(7).ToString(),
                                sortBysAvailable = key.Children().ElementAt(8).ToString()
                            });
                        context.SaveChanges();
                    }
                }
            }
        }

        // Seed the articles DB
        private static void seedArticles(MyDBContext context)
        {
            var storedSources = context.Sources.ToList();
            // Add each article for each source to server
            string apiKey = "ccfdc66609fc4b7b87258020b85d4380";

            foreach (var source in storedSources)
            {
                JObject data = newsRequest("articles/?source=" + source.sourceID + "&apikey=" + apiKey);
                JToken[] articles = data.GetValue("articles").Children().ToArray();
                if (articles == null)
                {
                    Console.WriteLine("\n\n NewsAPI did not reutrn any articles \n\n");
                    return;
                }
                foreach (var article in articles)
                {
                    if (context.Articles != null)
                    {
                        // If the article exists already, skip this step
                        var length = context.Articles.Where(i => i.url == article.Children().ElementAt(3).Last().ToString()).Count();
                        if (length > 0)
                        {
                            continue;
                        }
                        else
                        {
                            // If it doesn't exist yet, try to add it.
                            try
                            {
                                context.Articles.Add(new Article
                                {
                                    author = article.Children().ElementAt(0).Last().ToString(),
                                    title = article.Children().ElementAt(1).Last().ToString(),
                                    description = article.Children().ElementAt(2).Last().ToString(),
                                    url = article.Children().ElementAt(3).Last().ToString(),
                                    category = source.category,
                                    urlToImage = article.Children().ElementAt(4).Last().ToString(),
                                    publishedAt = article.Children().ElementAt(5).Last().ToString(),
                                    timestamp = DateTime.Now,
                                    provider_name = source.sourceID
                                });
                                context.SaveChanges();
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Error while adding article: " + article.ToString());
                            }
                        }
                    }

                }

                context.SaveChanges();
            }
            Console.WriteLine("Articles Updated: " + DateTime.Now);
        }
    }
}