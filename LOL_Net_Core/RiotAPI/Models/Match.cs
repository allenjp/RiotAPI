using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class Match
    {
        private readonly RiotAPIContext _context;

        public int ID { get; set; }
        public Summoner Summoner { get; set; }
        public Champion Champ { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public double KDA { get; set; }
        public int CreepScore { get; set; }
        public double CsPerMin { get; set; }
        public bool Win { get; set; }
        public string Lane { get; set; }
        public GameType GameType { get; set; }

        public Match()
        {

        }
        public Match(String id, string sum_name, int champ_id, int kills, int deaths, int assists, int cs, bool win, string lane, String gt_name, int gameLength, RiotAPIContext context = null)
        {
            _context = context;

            Task<Summoner> sum =  _get_summoner_from_nameAsync(sum_name, _context);
            Task<GameType> gt =  _get_gametype_from_name(gt_name, _context);
            Task<Champion> champ =  _get_champion_from_idAsync(champ_id, _context);

            double kda = _calculate_kda(kills, deaths, assists);
            double cs_per_min = _calculate_cs_per_min(cs, gameLength);

            ID = Int32.Parse(id);
            Summoner = sum.Result;
            Champ = champ.Result;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
            KDA = kda;
            CreepScore = cs;
            CsPerMin = cs_per_min;
            Win = win;
            Lane = lane;
            GameType = gt.Result;
        }

        

        private async Task<Summoner> _get_summoner_from_nameAsync(string summoner_name, RiotAPIContext context)
        {
            var s = await _context.Summoner.SingleOrDefaultAsync(m => m.Name == summoner_name);
            return s;
        }

        private async Task<GameType> _get_gametype_from_name(String gametype_name, RiotAPIContext context)
        {
            var gt = await _context.GameType.SingleOrDefaultAsync(m => m.TypeName == gametype_name);
            return gt;
        }

        private async Task<Champion> _get_champion_from_idAsync(int champ_id, RiotAPIContext context)
        {
            var c = await _context.Champion.SingleOrDefaultAsync(m => m.ID == champ_id);
            return c;
        }

        private double _calculate_kda(int k, int d, int a)
        {
            double result = (k + a) / d;
            return result;
        }

        private double _calculate_cs_per_min(int cs, int gameLength)
        {
            return cs / gameLength;
        }
    }
}
