using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiotAPI.Models;

namespace RiotAPI.Models
{
    public class RiotAPIContext : DbContext
    {
        public RiotAPIContext (DbContextOptions<RiotAPIContext> options)
            : base(options)
        {
        }

        public DbSet<RiotAPI.Models.Summoner> Summoner { get; set; }
        public DbSet<RiotAPI.Models.Champion> Champion { get; set; }
        public DbSet<RiotAPI.Models.Match> Match { get; set; }
    }
}
