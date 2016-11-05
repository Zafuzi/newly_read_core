using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using NewlyReadCore.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace NewlyReadCore{
    public class ReadAPI : Controller{
        private static MyDBContext db = new MyDBContext();

        // Allows the client to retrieve the full list of sources from the DB in JSON format
        public string GetSources() {
            string result = JsonConvert.SerializeObject("[{message: There was an error obtaining the source material}]");
            if (db.Sources != null) {
                result = JsonConvert.SerializeObject(db.Sources);
            }
            return result;
        }

        // Allows the client to retrieve the full list of articles from the DB in JSON format
        public string GetArticles() {
            string result = JsonConvert.SerializeObject("[{message: There was an error obtaining the source material}]");
            if (db.Articles != null) {
                result = JsonConvert.SerializeObject(db.Articles);
            }
            return result;
        }

        public static string ExtractHtmlFromURL(string url){
            //  This method will be called after migrating to the latest version.
            var client = new RestClient("http://api.embed.ly/1/");
            var request = new RestRequest();
            request.Resource = "extract?key=08ad220089e14298a88f0810a73ce70a&url=" + url;
            Console.WriteLine("\n URL REQUESTED: " + url + "\n");
            request.AddHeader("ContentType", "application/json");

            // execute the request
            // TODO add a fallback method to handle request failure.
            var restResponse = client.Execute(request);
            var content = restResponse.Content;
            return JObject.Parse(content).ToString();
        }

        public static List<Article> GetArticlesByCategory(string category, int length = 20){
            var db = new MyDBContext();

            var providers = db.Articles.Where(i => i.category == category).GroupBy(p => p.provider_name)
                            .Select(group => new { id = group.Key, articles = group.Take(length).OrderBy(p => Guid.NewGuid()).ToList() })
                            .ToList();
            
            List<Article> articles = new List<Article>();

            foreach(var provider in providers){
                foreach(var article in provider.articles){
                    articles.Add(article);
                }
            }
            ArrayTools.Shuffle(articles);
            
            return articles;
        }
    }
}