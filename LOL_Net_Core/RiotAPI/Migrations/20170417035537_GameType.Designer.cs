using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RiotAPI.Models;

namespace RiotAPI.Migrations
{
    [DbContext(typeof(RiotAPIContext))]
    [Migration("20170417035537_GameType")]
    partial class GameType
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

            modelBuilder.Entity("RiotAPI.Models.GameType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName");

                    b.HasKey("ID");

                    b.ToTable("GameType");
                });

            modelBuilder.Entity("RiotAPI.Models.Match", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assists");

                    b.Property<int?>("ChampID");

                    b.Property<int>("CreepScore");

                    b.Property<int>("Deaths");

                    b.Property<int?>("GameTypeID");

                    b.Property<double>("KDA");

                    b.Property<int>("Kills");

                    b.Property<string>("Lane");

                    b.Property<int?>("SummonerID");

                    b.Property<bool>("Win");

                    b.HasKey("ID");

                    b.HasIndex("ChampID");

                    b.HasIndex("GameTypeID");

                    b.HasIndex("SummonerID");

                    b.ToTable("Match");
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

            modelBuilder.Entity("RiotAPI.Models.Match", b =>
                {
                    b.HasOne("RiotAPI.Models.Champion", "Champ")
                        .WithMany()
                        .HasForeignKey("ChampID");

                    b.HasOne("RiotAPI.Models.GameType", "GameType")
                        .WithMany()
                        .HasForeignKey("GameTypeID");

                    b.HasOne("RiotAPI.Models.Summoner", "Summoner")
                        .WithMany()
                        .HasForeignKey("SummonerID");
                });
        }
    }
}
