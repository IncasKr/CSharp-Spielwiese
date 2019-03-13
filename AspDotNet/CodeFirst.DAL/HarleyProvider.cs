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

        public List<EntityHarley> GetAllHarleys()
        {
            using (GarageContext context = new GarageContext())
            {
                return context.Harleys.ToList();
            }
        }

    }
}
