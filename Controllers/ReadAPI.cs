using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using Newtonsoft.Json;


namespace NewlyReadCore
{
    public class ReadAPI : Controller
    {
        private static MyDBContext db = new MyDBContext();

        // Allows the client to retrieve the full list of sources from the DB in JSON format
        public string GetSources()
        {
            string result = JsonConvert.SerializeObject("[{message: There was an error obtaining the source material}]");
            if (db.Sources != null)
            {
                result = JsonConvert.SerializeObject(db.Sources);
            }
            return result;
        }

        // Allows the client to retrieve the full list of articles from the DB in JSON format
        public string GetArticles()
        {
            string result = JsonConvert.SerializeObject("[{message: There was an error obtaining the source material}]");
            if (db.Articles != null)
            {
                result = JsonConvert.SerializeObject(db.Articles);
            }
            return result;
        }

        // Get a quick summary of two articles from each category, this goes on the front page.
        public static List<Article> SummarizeLatestArticles(int length = 2)
        {
            var db = new MyDBContext();
            var providers = db.Articles.GroupBy(p => p.provider_name)
                    .Select(group => new { id = group.Key, articles = group.OrderByDescending(p => p.timestamp).Take(length).ToList() })
                    .ToList();
            List<Article> articles = new List<Article>();
            foreach (var provider in providers)
            {
                foreach (var article in provider.articles)
                {
                    articles.Add(article);
                }
            }
            return articles.OrderByDescending(p => p.timestamp).ToList();
        }

        // Extract HTML from a specified url
        public static Dictionary<string, HtmlNodeCollection> ExtractHtmlFromURL(string url)
        {
            string html = "";
            Task t = Task.Run(async () =>
            {
                Console.WriteLine(url);
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(url))

                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();
                    html = result;
                }
            });
            // This sucks, I have to wait becuase I need to go grab the website and then return all of the html.
            // If I don't wait than the extraction will try to do it's job with and empty string.
            TimeSpan ts = TimeSpan.FromMilliseconds(3000);
            if (!t.Wait(ts))
            {
                Console.WriteLine("The timeout interval elapsed.");
            }
            else
            {
                //Console.WriteLine("\n JSON: " + i + "\n");
            }

            var document = new HtmlDocument();
            document.LoadHtml(html);
            Console.WriteLine("Length of array before processing: " + document.DocumentNode.Descendants().ToArray().Length + "\n");

            HtmlNode bodyContent = document.DocumentNode.SelectSingleNode("//body");

            Dictionary<string, HtmlNodeCollection> dict = new Dictionary<string, HtmlNodeCollection>();
            dict["title"] = bodyContent.SelectNodes("//h1");
            dict["content"] = bodyContent.SelectNodes("//p");
            dict["links"] = bodyContent.SelectNodes("//a");

            foreach(var node in dict["title"])
            {
                node.Attributes.Remove("class");
                node.Attributes.Remove("id");
                node.Attributes.Remove("style");
            }
            foreach(var node in dict["content"])
            {
                node.Attributes.Remove("class");
                node.Attributes.Remove("id");
                node.Attributes.Remove("style");
            }
            foreach(var node in dict["links"])
            {
                node.Attributes.Remove("class");
                node.Attributes.Remove("id");
                node.Attributes.Remove("style");
            }

            return dict;
        }

        // Gets the most recent 20 articles for the specified category.
        public static List<Article> GetArticlesByCategory(string category, int length = 20)
        {
            var db = new MyDBContext();
            var providers = db.Articles.Where(i => i.category.Contains(category)).GroupBy(p => p.provider_name)
                    .Select(group => new { id = group.Key, articles = group.OrderByDescending(p => p.timestamp).Take(length).ToList() })
                    .ToList();
            List<Article> articles = new List<Article>();
            foreach (var provider in providers)
            {
                foreach (var article in provider.articles)
                {
                    articles.Add(article);
                }
            }
            return articles.OrderByDescending(p => p.timestamp).ToList();
        }
    }
}