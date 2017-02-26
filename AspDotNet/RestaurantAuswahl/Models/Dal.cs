using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Lists the different registered Restaurants.
    /// </summary>
    public class Dal : IDal
    {
        private RADbContext db;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dal()
        {
            db = new RADbContext();
        }

        public int AddUser(string name, string password)
        {
            return db.Users.Add(new User { FirstName = name, Password = EncodeMD5(password) }).Id;
        }

        public void AddVote(int idSurvey, int idRestaurant, int idUser)
        {
            if (!AlreadyVoted(idSurvey, idUser.ToString()))
            {
                Vote vote = new Vote
                {
                    Restaurant = db.Restaurants.FirstOrDefault(r => r.Id == idRestaurant),
                    User = db.Users.FirstOrDefault(u => u.Id == idUser)
                };
                if (vote != null)
                {
                    db.Votes.Add(vote);
                    db.Surveys.FirstOrDefault(s => s.Date.ToString("dd.MM.yyy") == DateTime.Now.ToString("dd.MM.yyy"))?.Votes.Add(vote);
                }                               
            }            
        }

        public bool AlreadyVoted(int idSurvey, string idUser)
        {
            return db.Surveys.FirstOrDefault(s => s.Id == idSurvey && s.Votes.FirstOrDefault(v => v.User.Id.ToString() == idUser) != null && s.Date == DateTime.Now) != null;
        }

        public User Authenticate(string name, string password)
        {
            return db.Users.FirstOrDefault(u => u.FirstName == name && u.Password == EncodeMD5(password));
        }

        /// <summary>
        /// Create a new restaurant and save it into the database.
        /// </summary>
        /// <param name="name">name of the restaurant</param>
        /// <param name="telephone">telephone of the restaurant</param>
        public void CreateRestaurant(string name, string telephone)
        {
            db.Restaurants.Add(new Restaurant { Name = name, Telephone = telephone });
            db.SaveChanges();
        }

        public int CreateSurvey()
        {
            return db.Surveys.Add(new Survey { Date = DateTime.Now, Votes = new List<Vote>() }).Id;
        }

        /// <summary>
        /// Edite an existing restaurant.
        /// </summary>
        /// <param name="id">ID of the existing restaurant</param>
        /// <param name="name">New name for this restaurant</param>
        /// <param name="telephone">New telephone number for this restaurant</param>
        public void EditRestaurant(int id, string name, string telephone)
        {
            Restaurant rFound = db.Restaurants.FirstOrDefault(resto => resto.Id == id);
            if (rFound != null)
            {
                rFound.Name = name;
                rFound.Telephone = telephone;
                db.SaveChanges();
            }
        }

        private string EncodeMD5(string password)
        {
            string pwdSel = string.Format(GetSelConfig(), password);
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(pwdSel)));
        }

        private string GetSelConfig()
        {
            try
            {
                FileStream fic = null;
                string AppPath = System.IO.Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string path = AppPath + @".config";
                fic = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                XmlTextReader txtReader = new XmlTextReader(fic);
                string strValue = "";

                while (txtReader.Read())
                {
                    XmlNodeType nType = txtReader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        if (txtReader.Name.Equals("Sel"))
                        {
                            strValue = txtReader.GetAttribute("Text");                           
                        }
                    }
                }

                return (strValue != "") ? strValue : "Default {0} sel";
            }
            catch
            {
                return "Default {0} sel";
            }
        }

        /// <summary>
        /// Get the list of all restaurants from the database.
        /// </summary>
        /// <returns>Returns the list of restaurants.</returns>
        public List<Restaurant> GetAllRestaurants()
        {
            return db.Restaurants.ToList();
        }

        public List<Results> GetResults(int idSurvey)
        {
            List<Results> results = new List<Results>();
            List<Vote> votes = db.Surveys.FirstOrDefault(s => s.Id == idSurvey).Votes;

            foreach (var vote in votes)
            {
                if (results.FirstOrDefault(r => r.Name == vote.Restaurant.Name) != null)
                {
                    results.FirstOrDefault(r => r.Name == vote.Restaurant.Name).NumberOfVotes++;
                }
                else
                {
                    results.Add(new Results { Name = vote.Restaurant.Name, Telephone = vote.Restaurant.Telephone, NumberOfVotes = 0 });
                }
            }

            return results;
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string id)
        {
            return db.Users.FirstOrDefault(u => u.Id.ToString() == id);
        }

        /// <summary>
        /// Dispose the data context.
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }

        /// <summary>
        /// Determines if this restaurant already exists into the database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RestaurantExists(string name)
        {
            foreach (var item in db.Restaurants.ToList())
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}