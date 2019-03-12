using BLL;
using DTO;
using System.Linq;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ThreeTierServiceLibrary : IThreeTierServiceLibrary
    {
        public PersonEntity GetPersonByName(string name)
        {
            return PersonManager.LoadData().Where(x => x.LastName.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
        }       
    }
}
