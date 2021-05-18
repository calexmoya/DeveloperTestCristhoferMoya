using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using DeveloperTestCristhoferMoya.Models;
using DeveloperTestCristhoferMoya.Models.Files;
using Newtonsoft.Json;

namespace DeveloperTestCristhoferMoya.Controllers
{

    public class CovidStatsController : Controller
    {
        DataCovidAPI CovidAPI = DataCovidAPI.getInstance();
        GenerateFiles files = GenerateFiles.getInstance();
        public async Task<ActionResult> TopCasesCovid()
        {
            ViewBag.regionsList = await CovidAPI.GetRegionsCombo();
            return View("TopCasesCovid");
        }
        public async Task<ActionResult> TableTopRegion()
        {
            var model = await CovidAPI.GetCasesWorld();
            if (Session != null) {
                Session.Add("infoExport", model);
                Session.Add("region", true);
            }
            
            return View("TableTopRegion",model);
        }
        public async Task<ActionResult> TableTopProvince(string iso)
        {
            var model = await CovidAPI.GetCasesRegion(iso);
            if (Session != null)
            {
                Session.Add("infoExport", model);
                Session.Add("region", false);
            }
            
            return View("TableTopProvince",model);
        }
        public ActionResult GetTableTopTen(string iso)
        {
            Session.Clear();
            if (iso == "WWW")
            {
                return RedirectToAction("TableTopRegion");
            }
            else {
                return RedirectToAction("TableTopProvince",new {iso=iso});
            }
            
        }
        [HttpPost]
        public FileContentResult DownloadCSV()
        {
            if ((Boolean)Session["region"])
            {
                var list = (List<TopRegionWorld>)Session["infoExport"];
                var newList = list.Select(l => new { RANK = l.rank, REGION = l.name, CASES = l.confirmed, DEATHS = l.deaths });
                string csv = files.GenerateToCSV(newList);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Top10WorldCovidCases.csv");
            }
            else {
                var list = (List<CasesRegion>)Session["infoExport"];
                var newList = list.Select(l => new { RANK = l.rank, PROVINCE = l.provinceName, CASES = l.confirmed, DEATHS = l.deaths, REGION=l.regionName });
                string csv = files.GenerateToCSV(newList);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Top10CovidCases[" + list.First().region.name+"].csv");
            }
            
        }
        [HttpPost]
        public FileContentResult DownloadJSON()
        {
            if ((Boolean)Session["region"])
            {
                var list = (List<TopRegionWorld>)Session["infoExport"];
                var newList = list.Select(l => new { rank = l.rank, region = l.name, cases = l.confirmed, deaths = l.deaths });
                var json = JsonConvert.SerializeObject(newList);
                return File(new System.Text.UTF8Encoding().GetBytes(json), "application/json", "Top10WorldCovidCases.json");
            }
            else
            {
                var list = (List<CasesRegion>)Session["infoExport"];
                var newList = list.Select(l => new { rank = l.rank, province = l.provinceName, cases = l.confirmed, deaths = l.deaths, region=l.regionName });
                var json = JsonConvert.SerializeObject(newList);
                return File(new System.Text.UTF8Encoding().GetBytes(json), "application/json", "Top10CovidCases[" + list.First().region.name + "].json");
            }

        }
        [HttpPost]
        public FileContentResult DownloadXML()
        {
            if ((Boolean)Session["region"])
            {
                var list = (List<TopRegionWorld>)Session["infoExport"];
                var xml = files.GenerateToXML<List<TopRegionWorld>>(list);
                return File(new System.Text.UTF8Encoding().GetBytes(xml), "text/xml", "Top10WorldCovidCases.xml");
                
            }
            else
            {
                var list = (List<CasesRegion>)Session["infoExport"];
                var xml = files.GenerateToXML<List<CasesRegion>>(list);
                return File(new System.Text.UTF8Encoding().GetBytes(xml), "text/xml", "Top10CovidCases[" + list.First().region.name + "].xml");
            }

        }
    }
}