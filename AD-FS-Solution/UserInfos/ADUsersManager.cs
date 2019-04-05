using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace UserInfos
{
    public class ADUsersManager
    {
        public List<Users> GetADUsers(string domain)
        {
            try
            {
                List<Users> lstADUsers = new List<Users>();
                string DomainPath = $"LDAP://{domain}/DC={domain.Split('.')[0]},DC=com";
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot)
                {
                    Filter = "(&(objectClass=user)(objectCategory=person))"
                };
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("usergroup");
                search.PropertiesToLoad.Add("displayname");//first name
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") &&
                                 result.Properties.Contains("mail") &&
                            result.Properties.Contains("displayname"))
                        {
                            Users objSurveyUsers = new Users
                            {
                                Email = (String)result.Properties["mail"][0] + "^" + (String)result.Properties["displayname"][0],
                                UserName = (String)result.Properties["samaccountname"][0],
                                DisplayName = (String)result.Properties["displayname"][0]
                            };
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetCurrentUser(string domain)
        {
            try
            {
                string userName = Environment.UserName;//HttpContent.Current.User.Identity.Name.Split('\\')[1].ToString();
                string displayName = GetADUsers(domain).Where(x =>
                  x.UserName == userName).Select(x => x.DisplayName).First();
                return displayName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class Users
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool isMapped { get; set; }
    }
}
