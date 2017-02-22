using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return View("Error");
            }
            else
            {
                ViewData["Name"] = id;
                return View();
            }           
        }

        public ActionResult ListClients()
        {
            Clients clients = new Clients();
            ViewData["Clients"] = clients.GetClients();
            return View();
        }

        public ActionResult FindClient(string name)
        {
            ViewData["Name"] = name;
            Clients clients = new Clients();
            Client client = clients.GetClients().FirstOrDefault(c => c.Name == name);
            if (client != null)
            {
                ViewData["Age"] = client.Age;
                return View("Found");
            }
            return View("NotFound");
        }
    }
}