using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NewlyReadCore.SQLite;

namespace dtest.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("NewlyReadCore.SQLite.Article", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("author");

                    b.Property<string>("category");

                    b.Property<string>("description");

                    b.Property<string>("jsonstring");

                    b.Property<string>("publishedAt");

                    b.Property<DateTime>("timestamp");

                    b.Property<string>("title");

                    b.Property<string>("url");

                    b.Property<string>("urlToImage");

                    b.HasKey("id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("NewlyReadCore.SQLite.Source", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("category");

                    b.Property<string>("country");

                    b.Property<string>("description");

                    b.Property<string>("language");

                    b.Property<string>("name");

                    b.Property<string>("sortBysAvailable");

                    b.Property<string>("sourceID");

                    b.Property<string>("url");

                    b.Property<string>("urlsToLogos");

                    b.HasKey("id");

                    b.ToTable("Sources");
                });
        }
    }
}
