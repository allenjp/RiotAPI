using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class SeedData
    {
        // see here for info:
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RiotAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<RiotAPIContext>>()))
            {
                // Look for any Summoners
                if (context.Summoner.Any())
                {
                    return; // DB has been seeded
                }

                context.Summoner.AddRange(
                    new Summoner
                    {
                        Name = "jpallen",
                        IrlName = "Jeff",
                        Rank = "Silver"
                    },

                    new Summoner
                    {
                        Name = "Long Tim",
                        IrlName = "Tim",
                        Rank = "Gold"
                    },

                    new Summoner
                    {
                        Name = "SCIENCE HOMEWORK",
                        IrlName = "Jake",
                        Rank = "Diamond"
                    },

                    new Summoner
                    {
                        Name = "Flirtbot",
                        IrlName = "Jack",
                        Rank = "Gold"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
