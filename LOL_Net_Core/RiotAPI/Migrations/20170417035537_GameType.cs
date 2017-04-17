using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RiotAPI.Migrations
{
    public partial class GameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PrimeRole = table.Column<string>(nullable: true),
                    SecondRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Summoner",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summoner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<int>(nullable: false),
                    ChampID = table.Column<int>(nullable: true),
                    CreepScore = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    GameTypeID = table.Column<int>(nullable: true),
                    KDA = table.Column<double>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    Lane = table.Column<string>(nullable: true),
                    SummonerID = table.Column<int>(nullable: true),
                    Win = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Match_Champion_ChampID",
                        column: x => x.ChampID,
                        principalTable: "Champion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_GameType_GameTypeID",
                        column: x => x.GameTypeID,
                        principalTable: "GameType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Summoner_SummonerID",
                        column: x => x.SummonerID,
                        principalTable: "Summoner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_ChampID",
                table: "Match",
                column: "ChampID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_GameTypeID",
                table: "Match",
                column: "GameTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_SummonerID",
                table: "Match",
                column: "SummonerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "GameType");
        }
    }
}
