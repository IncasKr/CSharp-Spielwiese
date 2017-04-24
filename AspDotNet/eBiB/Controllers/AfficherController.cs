using eBiB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace eBiB.Controllers
{
    public class AfficherController : Controller
    {
        // GET: Afficher
        public ActionResult Index()
        {
            ViewData["Books"] = new Books().GetBooks();
            ViewData["Authors"] = new Authors().GetAuthors();            
            return View();
        }

        public ActionResult Auteurs()
        {
            ViewData["Authors"] = new Authors().GetAuthors();
            return View();
        }

        public ActionResult Auteur(int id = 0)
        {
            var aut = new Authors().GetAuthors().Find(a => a.ID == id);
            if (aut == null)
            {
                return View("ErrorA01");
            }
            ViewData["AuthorName"] = new Authors().GetAuthors().Find(a => a.ID == id).Name;
            if (! new Books().GetBooks().Exists(b => b.ID == id))
            {
                return View("ErrorL01");
            }
            List<Book> tmp = new List<Book>();
            foreach (var book in new Books().GetBooks())
            {
                if (book.AuthorID == id)
                {
                    tmp.Add(book);
                }
            }
            ViewData["BooksOfAuthor"] = tmp;
            return View();
        }

        public ActionResult Livre(int id = 0)
        {
            var livre = new Books().GetBooks().Find(b => b.ID == id);
            if (livre == null)
            {
                return View("ErrorL02");
            }
            var client = new Clients().GetClients().Find(c => c.Email == livre.ClientID);
            ViewData["ClientName"] = (client != null) ? client.Name : "";
            ViewData["Book"] = livre;
            return View();
        }
    }
}