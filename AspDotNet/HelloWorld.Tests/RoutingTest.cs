using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace HelloWorld.Tests
{
    [TestClass]
    public class RoutingTest
    {
        public static RouteData DefineUrl(string url)
        {
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            return routes.GetRouteData(mockContext.Object);
        }

        [TestMethod]
        public void Routes_PageHome_ReturnHomeControllerAndMethodIndex()
        {
            RouteData routeData = DefineUrl("~/");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Start", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Routes_PageHome2_ReturnHomeControllerAndMethodIndexAndPram2()
        {
            RouteData routeData = DefineUrl("~/Home/Index/2");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("2", routeData.Values["id"]);
        }

        [TestMethod]
        public void Routes_WeatherToday_ReturnWeatherControllerAndDisplayAndPramToday()
        {
            DateTime today = DateTime.Now;
            RouteData routeData = DefineUrl($"~/{today.Day}/{today.Month}/{today.Year}");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Weather", routeData.Values["controller"]);
            Assert.AreEqual("Display", routeData.Values["action"]);
            Assert.AreEqual(today.Day.ToString(), routeData.Values["day"]);
            Assert.AreEqual(today.Month.ToString(), routeData.Values["month"]);
            Assert.AreEqual(today.Year.ToString(), routeData.Values["year"]);
        }
    }
}
