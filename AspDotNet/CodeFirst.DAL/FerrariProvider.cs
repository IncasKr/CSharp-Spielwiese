using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DAL
{
    public class FerrariProvider
    {
        private readonly GarageContext garageContext;
        public FerrariProvider(GarageContext context = null)
        {
            // create the database tables, if it does not exist.
            garageContext = context ?? new GarageContext();
        }
    }
}
