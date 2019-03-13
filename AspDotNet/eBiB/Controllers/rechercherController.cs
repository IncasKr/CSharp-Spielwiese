using eBiB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiB.Controllers
{
    /// <summary>
    /// Search Control Class.
    /// </summary>
    public class RechercherController : Controller
    {
        /// <summary>
        /// Search a book by keyword in the title or in the author's name
        /// </summary>
        /// <param name="id">The keyword</param>
        /// <returns>The view of the books found.</returns>
        public ActionResult Livre(string id = "")
        {
            if (id == "")
            {
                return View("ErrorL03");
            }
            var auteurs = new Authors().GetAuthors();
            var livres = new Books().GetBooks().FindAll(b => b.Title.ToLower().Contains(id.ToLower()) || auteurs.Find(aut => aut.ID.Equals(b.AuthorID)).Name.ToLower().Contains(id.ToLower()));
            var clients = new Clients().GetClients().FindAll(c => livres.Exists(l => l.ClientID == c.Email));
            if (livres == null)
            {
                return View("ErrorL02");
            }
            ViewData["Books"] = livres;
            ViewData["Authors"] = auteurs;
            ViewData["Clients"] = clients;
            ViewData["key-word"] = id;
            return View();
        }
    }
}