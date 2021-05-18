using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeveloperTestCristhoferMoya.Models
{
    public class DataCovidAPI
    {
        //Singleton 
        private static DataCovidAPI instance;
        private DataCovidAPI() { }
        public static DataCovidAPI getInstance()
        {
            if (instance == null)
            {
                instance = new DataCovidAPI();
            }
            return instance;
        }
        //End Singleton
        public async Task<List<Region>> GetRegions()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
                Headers =
                {
                    { "x-rapidapi-key", ConfigurationManager.AppSettings["x-rapidapi-key"] },
                    { "x-rapidapi-host", ConfigurationManager.AppSettings["x-rapidapi-host"] },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                
                var info = await response.Content.ReadAsStringAsync();
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var listRegions = JsonConvert.DeserializeObject<RegionH>(info, serializerSettings).data.OrderBy(a=>a.name).ToList();
                listRegions.Insert(0, new Region("WWW", "World"));


                return listRegions;
                //return new List<Region>();

            }
        }

        public async Task<List<CasesRegion>> GetCasesRegion(string iso)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?iso=" + iso),
                Headers =
                {
                    { "x-rapidapi-key", ConfigurationManager.AppSettings["x-rapidapi-key"] },
                    { "x-rapidapi-host", ConfigurationManager.AppSettings["x-rapidapi-host"] },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var info = await response.Content.ReadAsStringAsync();
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var listCasesRegion = JsonConvert.DeserializeObject<CasesRegionH>(info, serializerSettings);
                var Top10CasesRegion = new List<CasesRegion>();
                var rankCont = 1;
                foreach (var item in listCasesRegion.data.OrderByDescending(c => c.confirmed).Take(10))
                {
                    item.rank = rankCont;
                    item.provinceName = item.region.province;
                    item.regionName = item.region.name;
                    Top10CasesRegion.Add(item);
                    rankCont++;
                }
                return Top10CasesRegion;
            }
        }
        public async Task<List<TopRegionWorld>> GetCasesWorld()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports"),
                Headers =
                {
                    { "x-rapidapi-key", ConfigurationManager.AppSettings["x-rapidapi-key"] },
                    { "x-rapidapi-host", ConfigurationManager.AppSettings["x-rapidapi-host"] },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var info = await response.Content.ReadAsStringAsync();
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                //Get all data by api
                var listCasesRegion = JsonConvert.DeserializeObject<CasesRegionH>(info, serializerSettings);

                /*In this code, group by name of regions and sum confirmed cases and deaths. 
                 * This result save to in list, later this grouped list change to order by descending 
                 * and add position in the rank finally a new object is done and add to return top 10 list*/
                var Top10CasesRegion = new List<TopRegionWorld>();
                var rankCont = 1;
                var listGroup = listCasesRegion.data.GroupBy(d => d.region.name).Select(r => new
                {
                    rank = 0,
                    name = r.Key,
                    confirmed = r.Sum(c => c.confirmed),
                    deaths = r.Sum(c => c.deaths),
                    last_update = r.First().last_update
                });
                foreach (var item in listGroup.OrderByDescending(g => g.confirmed).Take(10))
                {
                    var reg = new TopRegionWorld() {rank=rankCont, name=item.name, confirmed=item.confirmed,
                    deaths=item.deaths, last_update=item.last_update};

                    Top10CasesRegion.Add(reg);
                    rankCont++;
                }
                return Top10CasesRegion;
            }
        }

        public async Task<List<SelectListItem>> GetRegionsCombo()
        {

            var regs = await GetRegions();

            List<SelectListItem> regionsList = regs.ToList().ConvertAll(c =>
            {
                return new SelectListItem()
                {
                    Text = c.name,
                    Value = c.iso
                };
            });
            return regionsList;
        }
    }
}