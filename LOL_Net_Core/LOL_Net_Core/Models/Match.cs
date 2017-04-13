using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiotApi.Models
{
    // My summoner ID: 60657014
    // Jake: 49326823

    public class Match
    {
        public int Id { get; set; }
        //public ICollection<Summoner> Summoners { get; set; }
        public int Deaths { get; set; }
    }
}
