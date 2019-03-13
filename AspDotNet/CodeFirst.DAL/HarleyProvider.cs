using CodeFirst.DTO;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirst.DAL
{
    public class HarleyProvider
    {
        public HarleyProvider()
        {

        }

        public List<EntityHarley> GetHarleys()
        {
            // create the database tables, if it does not exist.
            using (GarageContext context = new GarageContext())
            {
                return context.Harleys.ToList();
            }
        }

    }
}
