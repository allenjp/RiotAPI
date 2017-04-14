using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiotAPI.Models
{
    public class SeedData
    {
        // see here for info:
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql

        public static void InitializeSummoners(IServiceProvider serviceProvider)
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

        public static async Task InitializeChampionsAsync(IServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new RiotAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<RiotAPIContext>>()))
                {
                    if (context.Champion.Any())
                    {
                        return; // Champions have been seeded
                    }
                    else
                    {
                        List<Champion> champ_list = await _get_champions_from_riotAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<List<Champion>> _get_champions_from_riotAsync()
        {
            List<Champion> champion_list = new List<Champion>();

            try
            {
                string api_url = "https://na1.api.riotgames.com/lol/static-data/v3/champions?champData=image,tags&api_key=3a0fbaee-bea5-48fe-bcc6-0581cf9407e7";

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(api_url))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    if (result != null)
                    {
                        JObject jo = JObject.Parse(result);
                        JToken champ_data = jo["data"];

                        foreach(JToken champ in champ_data)
                        {
                            var champ_sub = champ.First;
                            int id = Int32.Parse(champ_sub["id"].ToString());
                            string name = champ_sub["name"].ToString();
                            List<String> roles = champ_sub["tags"].ToString().Split(',').ToList();

                            Champion c = new Champion(id, name, roles);

                            champion_list.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return champion_list;
        }
    }
}
