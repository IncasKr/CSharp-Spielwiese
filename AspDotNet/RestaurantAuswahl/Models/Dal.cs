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
            User user = db.Users.Add(new User { FirstName = name, Password = EncodeMD5(password) });
            if (user != null)
            {
                db.SaveChanges();
                return user.Id;
            }
            else
            {
                return 0;
            }
        }

        public void AddVote(int idSurvey, int idRestaurant, int idUser)
        {
            Vote vote = new Vote
            {
                Restaurant = db.Restaurants.FirstOrDefault(r => r.Id == idRestaurant),
                User = db.Users.FirstOrDefault(u => u.Id == idUser)
            };
            if (vote != null)
            {
                db.Surveys.FirstOrDefault(s => s.Id == idSurvey)?.Votes.Add(vote);
                db.SaveChanges();
            }                                           
        }

        public bool AlreadyVoted(int idSurvey, string idUser)
        {
            int id;
            if (int.TryParse(idUser, out id))
            {
                return db.Surveys.FirstOrDefault(s => s.Id == idSurvey && s.Votes.FirstOrDefault(v => v.User.Id == id) != null) != null;
            }
            return false;
        }

        public User Authenticate(string name, string password)
        {
            var pwd = EncodeMD5(password);
            return db.Users.FirstOrDefault(u => u.FirstName == name && u.Password == pwd);
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
            Survey survey = db.Surveys.Add(new Survey { Date = DateTime.Now, Votes = new List<Vote>() });
            if (survey != null)
            {
                db.SaveChanges();
                return survey.Id;
            }
            else
            {
                return 0;
            }
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

        private static string GetSelConfig()
        {
            try
            {
                string filePath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\machine.config";
                XmlTextReader txtReader = new XmlTextReader(filePath);
                txtReader.WhitespaceHandling = WhitespaceHandling.None;
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
            List<Vote> votes = db.Surveys.FirstOrDefault(s => s.Id == idSurvey).Votes;
            List<Results> results = new List<Results>();

            foreach (var vote in votes)
            {
                if (results.FirstOrDefault(r => r.Name == vote.Restaurant.Name) != null)
                {
                    results.FirstOrDefault(r => r.Name == vote.Restaurant.Name).NumberOfVotes++;
                }
                else
                {
                    results.Add(new Results { Name = vote.Restaurant.Name, Telephone = vote.Restaurant.Telephone, NumberOfVotes = 1 });
                }
            }

            return results;
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
            return null;
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
            return db.Restaurants.Any(r => string.Compare(r.Name, name, StringComparison.CurrentCultureIgnoreCase) == 0);
        }
    }
}