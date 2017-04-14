using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class Champion
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PrimeRole { get; set; }
        public string SecondRole { get; set; }

        public Champion()
        {

        }
        public Champion(int id, string name, List<String> roles)
        {
            ID = id;
            Name = name;
            PrimeRole = roles[0];
            SecondRole = roles[1];
        }

        
    }
}
