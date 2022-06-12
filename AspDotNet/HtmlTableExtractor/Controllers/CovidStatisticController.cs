using HtmlTableExtractor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HtmlTableExtractor.Controllers
{
    public class CovidStatisticController : Controller
    {
        private const int PAGE_SIZE = 10;

        private List<CovidStatistic> GetData()
        {
            List<List<string>> table = null;
            List<CovidStatistic> peapolsStatic = new();

            string urlString = "https://www.sortiraparis.com/actualites/coronavirus/articles/240384-vaccination-dans-le-monde-le-vendredi-7-mai-2021-pourcentage-de-population-vacci";
            if (!String.IsNullOrEmpty(urlString))
            {
                WebClient webClient = new();
                string page = webClient.DownloadString(urlString);
                page = WebUtility.HtmlDecode(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(page)));

                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(page);

                table = doc.DocumentNode.SelectSingleNode("//table")
                            .Descendants("tr")
                            .Skip(1)
                            .Where(tr => tr.Elements("td").Count() > 1)
                            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                            .ToList();
            }

            if (table != null)
            {
                foreach (var item in table)
                {
                    peapolsStatic.Add(new CovidStatistic()
                    {
                        Country = item[0].Trim(),
                        VaccinatedPeople = item[1].Trim(),
                        TotalPopulation = item[2].Trim()
                    });
                }
            }

            return peapolsStatic;
        }

        // GET: CovidStatisticController
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Provide the view with the current sort order.
            ViewData["CurrentSort"] = sortOrder;
            // Configure the column heading hyperlinks with the appropriate query string values.
            ViewData["CountrySortParm"] = String.IsNullOrEmpty(sortOrder) ? "country_desc" : "";
            ViewData["VaccinatedSortParm"] = sortOrder == "vaccinated_asc" ? "vaccinated_desc" : "vaccinated_asc";
            ViewData["TotalSortParm"] = sortOrder == "total_asc" ? "total_desc" : "total_asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            // Provide the view with the current filter string.
            ViewData["CurrentFilter"] = searchString;

            List<CovidStatistic> peopols = GetData();
            

            if (!String.IsNullOrEmpty(searchString))
            {
                peopols = peopols.Where(s => s.Country.ToLower().Contains(searchString.ToLower())).ToList();
            }

            peopols = sortOrder switch
            {
                "country_desc" => peopols.OrderByDescending(s => s.Country).ToList(),
                "vaccinated_asc" => peopols.OrderBy(s => s.VaccinatedPeople.Replace(".", "")).ToList(),
                "vaccinated_desc" => peopols.OrderByDescending(s => s.VaccinatedPeople.Replace(".", "")).ToList(),
                "total_asc" => peopols.OrderBy(s => s.TotalPopulation.Replace(".", "")).ToList(),
                "total_desc" => peopols.OrderByDescending(s => s.TotalPopulation.Replace(".", "")).ToList(),
                _ => peopols.OrderBy(s => s.Country).ToList(),
            };

            // Convert the student query to a single page of students in a collection type that supports paging.
            return View(PaginatedList<CovidStatistic>.Create(peopols, pageNumber ?? 1, PAGE_SIZE));
        }
    }
}
