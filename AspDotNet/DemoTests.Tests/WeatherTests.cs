using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoTests.Tests
{
    /// <summary>
    /// Summary description for WeatherTests
    /// </summary>
    [TestClass]
    public class WeatherTests
    {
        [TestMethod]
        public void GetWeatherDay_WithCork_ReturnSun()
        {
            IDal dal = new Dal();
            Weather weatherDay = dal.GetWeatherDay();
            Assert.AreEqual(25, weatherDay.Temperature);
            Assert.AreEqual(WeatherType.Sun, weatherDay.Weathers);
        }
    }
}
