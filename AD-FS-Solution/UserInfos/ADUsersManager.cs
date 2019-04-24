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
            List<Users> lstADUsers = new List<Users>();
            try
            {
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
                search.PropertiesToLoad.Add("userAccountControl");
                search.PropertiesToLoad.Add("distinguishedName");
                search.PropertiesToLoad.Add("pwdLastSet");
                search.PropertiesToLoad.Add("accountExpires");
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        string[] departmentsToLook = {"intracall", "Intracall-Test", "Systemhaus"};
                        string tmp = ((String)result.Properties["distinguishedName"][0]);
                        string[] departments = tmp.Split(',')
                            .Where(it => it.ToLower().Contains("ou=")).ToArray().Select(val=>new string(val.Split('=')[1].ToCharArray())).ToArray();
                        if (result.Properties.Contains("samaccountname") &&
                            result.Properties.Contains("mail") &&
                            result.Properties.Contains("displayname") &&
                            departments.Any(dpt => departmentsToLook.Any(dp => dp.ToLower().Equals(dpt.ToLower()))))
                        {
                            var date = result.Properties["pwdLastSet"];
                            Users objSurveyUsers = new Users
                            {
                                Email = (String)result.Properties["mail"][0] + "^" + (String)result.Properties["displayname"][0],
                                UserName = (String)result.Properties["samaccountname"][0],
                                DisplayName = (String)result.Properties["displayname"][0],
                                AccountType = (String)result.Properties["distinguishedName"][0],
                                AccountExpires = LdapHelper.ConvertLargeIntegerToDate(LdapHelper.ConvertLargeIntegerToLong(result.Properties["accountExpires"][0])),
                                PwdLastSet = LdapHelper.ConvertLargeIntegerToDate(LdapHelper.ConvertLargeIntegerToLong(result.Properties["pwdLastSet"][0]))
                            };
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return lstADUsers;
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
        public string AccountType { get; set; }
        public DateTime PwdLastSet { get; set; }
        public DateTime AccountExpires { get; set; }
    }
}
