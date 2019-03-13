using System.Collections.Generic;
using WAA.DAL;
using WAA.DTO;

namespace WAA.BLL
{
    public static class PersonManager
    {
        public static List<PersonEntity> LoadData()
        {
            return new PersonProvider().LoadData();
        }
    }
}
