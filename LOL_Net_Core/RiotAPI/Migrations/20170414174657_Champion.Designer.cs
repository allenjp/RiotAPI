using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RiotAPI.Models;

namespace RiotAPI.Migrations
{
    [DbContext(typeof(RiotAPIContext))]
    [Migration("20170414174657_Champion")]
    partial class Champion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RiotAPI.Models.Champion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PrimeRole");

                    b.Property<string>("SecondRole");

                    b.HasKey("ID");

                    b.ToTable("Champion");
                });

            modelBuilder.Entity("RiotAPI.Models.Summoner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IrlName");

                    b.Property<string>("Name");

                    b.Property<string>("Rank");

                    b.HasKey("ID");

                    b.ToTable("Summoner");
                });
        }
    }
}
