using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class Summoner
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Real Name")]
        [DataType(DataType.Text)]
        public string IrlName { get; set; }
        public string Rank { get; set; }
    }
}
