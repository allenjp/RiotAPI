using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class Match
    {
        public int ID { get; set; }
        public Summoner Summoner { get; set; }
        public Champion Champ { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public double KDA { get; set; }
        public int CreepScore { get; set; }
        public bool Win { get; set; }
        public string Lane { get; set; }
        public GameType GameType { get; set; }

        public Match(int id, Summoner sum, Champion champ, int kills, int deaths, int assists, int cs, bool win, string lane, GameType gt)
        {
            double kda = _calculate_kda(kills, deaths, assists);

            ID = id;
            Summoner = sum;
            Champ = champ;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            KDA = kda;
            CreepScore = cs;
            Win = win;
            Lane = lane;
            GameType = gt;
        }

        public Match()
        {
        }

        private double _calculate_kda(int k, int d, int a)
        {
            double result = (k + a) / d;

            return result;
        }
    }
}
