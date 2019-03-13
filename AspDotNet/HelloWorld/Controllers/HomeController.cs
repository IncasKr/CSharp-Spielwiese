using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Start(string id)
        {
            return HtmlHelper.GenerateLink(Request.RequestContext, RouteTable.Routes, "Zur link", null, "Index", "Home", new RouteValueDictionary { { "id", id } }, null);
        }
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return View("Error");
            }
            else
            {
                var tab = id.Split('/');
                if (tab.Length == 1)
                {
                    ViewData["Name"] = id;
                }
                else
                {
                    id = tab[0];
                    for (int i = 1; i < tab.Length; i++)
                    {
                        if (i == tab.Length - 1)
                        {
                            id += $" and {tab[i]}";
                        }
                        else
                        {
                            id += $", {tab[i]}";
                        }
                    }
                    ViewData["Name"] = id;
                }
                return View();
            }           
        }

        public ActionResult ListClients()
        {
            Clients clients = new Clients();
            ViewData["Clients"] = clients.GetClients();
            return View();
        }

        public ActionResult FindClient(string id)
        {
            ViewData["Name"] = id;
            Clients clients = new Clients();
            Client client = clients.GetClients().FirstOrDefault(c => c.Name == id);
            if (client != null)
            {
                ViewData["Age"] = client.Age;
                return View("Found");
            }
            return View("NotFound");
        }
    }
}