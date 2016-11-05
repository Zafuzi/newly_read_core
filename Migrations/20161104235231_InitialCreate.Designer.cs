using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NewlyReadCore.SQLite;

namespace newly_read_core.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20161104235231_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("provider_name");

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
