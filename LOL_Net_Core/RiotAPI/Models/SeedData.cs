using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Champion] ON");

                            context.Champion.AddRange(champ_list);
                            context.SaveChanges();

                            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT[dbo].[Champion] OFF");

                            transaction.Commit();
                        }
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
                        champion_list = _parse_champion_data_string(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return champion_list;
        }

        private static List<Champion> _parse_champion_data_string(String input)
        {
            List<Champion> champion_list = new List<Champion>();

            JObject jo = JObject.Parse(input);
            JToken champ_data = jo["data"];

            foreach (JToken champ in champ_data)
            {
                var champ_sub = champ.First;
                int id = Int32.Parse(champ_sub["id"].ToString());
                string name = champ_sub["name"].ToString();

                String roles_str = champ_sub["tags"].ToString();
                List<String> roles_list = _get_champ_roles(roles_str);
                Champion c = new Champion(id, name, roles_list);

                champion_list.Add(c);
            }
            return champion_list;
        }

        private static List<String> _get_champ_roles(String roles_str)
        {
            List<String> roles_list = new List<string>();

            //if (roles_str.Contains(','))
            //{
            //    roles_list = roles_str.Split(',').ToList();
            //}
            //else
            //{
            //    roles_list.Add(roles_str);
            //    roles_list.Add("");
            //}

            var reg = new Regex("\".*?\"");
            var matches = reg.Matches(roles_str);
            foreach(var item in matches)
            {
                roles_list.Add(item.ToString().TrimStart('"').TrimEnd('"'));
            }

            if (roles_list.Count < 2)
            {
                roles_list.Add("");
            }

            return roles_list;
        }
    }
}
