using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static Dictionary<string, List<string>> ExtractHtmlFromURL(string url)
        {
            string i = "";
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
                    i = result;
                }
            });
            TimeSpan ts = TimeSpan.FromMilliseconds(3000);
            if (!t.Wait(ts))
            {
                Console.WriteLine("The timeout interval elapsed.");
            }
            else
            {
                //Console.WriteLine("\n JSON: " + i + "\n");
            }

            // load snippet
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(i);

            // extract hrefs
            List<string> hrefTags = new List<string>();
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>(){
                ["title"] = new List<string>(),
                ["text"] = new List<string>(),
                ["extra"] = new List<string>()
            };
            hrefTags = new List<string>();
 
            foreach (HtmlNode link in htmlSnippet.DocumentNode.Descendants())
            {   
                Console.WriteLine(link.Name);
                switch(link.Name){
                    case "h1":
                        dict["title"].Add(link.InnerText);
                        break;
                    
                    case "p":
                        dict["text"].Add(link.InnerText);
                        break;
                    
                    default:
                        dict["extra"].Add(link.InnerHtml);
                        break;
                }
                
            }
            return dict;
        }
        
        // I shoudl really refactor this to work better with each page. (As in the home page.)
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