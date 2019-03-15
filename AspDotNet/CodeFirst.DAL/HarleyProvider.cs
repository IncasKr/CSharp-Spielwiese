using CodeFirst.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirst.DAL
{
    public class HarleyProvider
    {
        private GarageContext garageContext;
        public HarleyProvider(GarageContext context = null)
        {
            // create the database tables, if it does not exist.
            garageContext = context ?? new GarageContext();
        }

        private bool Exist(EntityHarley harleyObject)
        {
            try
            {
                return garageContext.Harleys.Any(h =>
                    h.Capacity.Equals(harleyObject.Capacity) &&
                    h.Color.Equals(harleyObject.Color) &&
                    h.Model.Equals(harleyObject.Model) &&
                    h.Power.Equals(harleyObject.Power)
                );
            }
            catch (Exception ex)
            {
                return false;
            }                          
        }

        public bool Create(EntityHarley newHarley)
        {
            if (newHarley == null || Exist(newHarley))
            {
                return false;
            }
            var createdHarley = garageContext.Harleys.Add(newHarley);
            return garageContext.SaveChanges() > 0;
        }

        public List<EntityHarley> GetHarleys()
        {
            return garageContext.Harleys.ToList();
        }

        public bool Update(EntityHarley harleyObject, bool persist = false)
        {
            if (harleyObject == null)
            {
                return false;
            }
            var foundEntity = garageContext.Harleys.SingleOrDefault(h => h.ID.Equals(harleyObject.ID));
            if (foundEntity == null) return false;
            garageContext.Entry(foundEntity).CurrentValues.SetValues(harleyObject);
            return persist ? garageContext.SaveChanges() > 0 : true;                 
        }
    }
}
