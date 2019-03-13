using CodeFirst.DTO;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirst.DAL
{
    public class HarleyProvider
    {
        private GarageContext context;
        public HarleyProvider()
        {
            // create the database tables, if it does not exist.
            context = new GarageContext();
        }

        private bool Exist(EntityHarley harleyObject)
        {
            return context.Harleys.Any(h => 
                    h.Capacity.Equals(harleyObject.Capacity) &&
                    h.Color.Equals(harleyObject.Color) &&
                    h.Model.Equals(harleyObject.Model) &&
                    h.Power.Equals(harleyObject.Power)
                );               
        }

        public bool Create(EntityHarley newHarley)
        {
            if (newHarley == null || Exist(newHarley))
            {
                return false;
            }
            var createdHarley = context.Harleys.Add(newHarley);
            return context.SaveChanges() > 0;
        }

        public List<EntityHarley> GetHarleys()
        {
            return context.Harleys.ToList();
        }

        public bool Update(EntityHarley harleyObject, bool persist = false)
        {
            if (harleyObject == null)
            {
                return false;
            }
            var foundEntity = context.Harleys.SingleOrDefault(h => h.ID.Equals(harleyObject.ID));
            if (foundEntity == null) return false;
            context.Entry(foundEntity).CurrentValues.SetValues(harleyObject);
            return persist ? context.SaveChanges() > 0 : true;                 
        }
    }
}
