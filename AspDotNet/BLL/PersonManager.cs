using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL
{
    public static class PersonManager
    {
        public static List<PersonEntity> LoadData()
        {
            return new PersonProvider().LoadData();
        }
    }
}
