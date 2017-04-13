using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class Match
    {
        public int ID { get; set; }
        public Summoner SummonerID { get; set; }
        public Champion ChampID { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public double KDA { get; set; }
        public int CreepScore { get; set; }
        public bool Win { get; set; }
        public string Lane { get; set; }
    }
}
