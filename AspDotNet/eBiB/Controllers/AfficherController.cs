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

        public ActionResult Auteur(int id)
        {
            var aut = new Authors().GetAuthors().Find(a => a.ID == id);
            if (aut == null)
            {
                return View("ErrorA01");
            }
            ViewData["Author"] = new Authors().GetAuthors().Find(a => a.ID == id).Name;
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
    }
}