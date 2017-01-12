using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using Newtonsoft.Json;
using Aylien.TextApi;
using RestSharp;
using NewlyReadCore.Models;


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
        public static Dictionary<string, string> ExtractHtmlFromURL(string url)
        {
            //Aylien Article Extraction
            var client = new RestClient("https://api.aylien.com/api/v1/extract");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-AYLIEN-TextAPI-Application-Key","d2756216e77576be81b60c178ec7b060");
            request.AddHeader("X-AYLIEN-TextAPI-Application-ID","86883b47");
            request.AddParameter("best_image", "true");
            request.AddParameter("url", url);

            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string
            // async with deserialization
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var response = client.Execute<ExtractedArticle>(request);
            //Console.WriteLine(response.Data.title);
            dict["title"] = response.Data.title;
            //Console.WriteLine(response.Data.author);
            dict["author"] = response.Data.author;
            //Console.WriteLine(response.Data.article);
            dict["article"] = response.Data.article;
            dict["publishDate"] = response.Data.publishDate;
            dict["image"] = response.Data.image;
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