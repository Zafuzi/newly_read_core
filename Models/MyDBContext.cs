using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace NewlyReadCore.SQLite
{
    public class MyDBContext : DbContext
    {
        public DbSet<Source> Sources { get; set; }
        public DbSet<Article> Articles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./storage.db");
        }
    }

    public class Source
    {
        public int id { get; set; }
        public string sourceID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public string urlsToLogos { get; set; }
        public string sortBysAvailable { get; set; }

    }

    public class Article
    {
        public long id { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string publishedAt { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public string category { get; set; }
        public System.DateTime timestamp { get; set; }
        public string jsonstring {get; set;}
    }
}
