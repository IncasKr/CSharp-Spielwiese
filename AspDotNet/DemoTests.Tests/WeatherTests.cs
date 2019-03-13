using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            Weather fakeWeather = new Weather
            {
                Temperature = 18,
                Weathers = WeatherType.Sun
            };
            Mock<IDal> mock = new Mock<IDal>();
            mock.Setup(d => d.GetWeatherDay()).Returns(fakeWeather);

            IDal fakeDal = mock.Object;
            Weather weatherDay = fakeDal.GetWeatherDay();
            Assert.AreEqual(18, weatherDay.Temperature);
            Assert.AreEqual(WeatherType.Sun, weatherDay.Weathers);
        }
    }
}
