using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantAsuwahl.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace RestaurantAsuwahl.Tests
{    
    [TestClass]
    public class DalTests
    {
        [TestInitialize]
        public void Init_Before_StartTests()
        {
            IDatabaseInitializer<RADbContext> init = new DropCreateDatabaseAlways<RADbContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new RADbContext());
        }

        [TestMethod]
        public void Create_NewRestaurant_And_GetIt()
        {
            using (IDal dal = new Dal())
            {
                if (!dal.RestaurantExists("Vapiano"))
                {
                    dal.CreateRestaurant("Vapiano", "02151 12");
                }
                List<Restaurant> restos = dal.GetAllRestaurants();

                Assert.IsNotNull(restos);
                Assert.AreEqual(1, restos.Count);
                Assert.AreEqual("Vapiano", restos[0].Name);
                Assert.AreEqual("02151 12", restos[0].Telephone);
            }
        }
    }
}
