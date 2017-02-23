using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;

namespace HelloWorld.Tests
{
    [TestClass]
    public class RoutingTest
    {
        public static RouteData DefineUrl(string url)
        {
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c =>
                c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            return routes.GetRouteData(mockContext.Object);
        }

        [TestMethod]
        public void Routes_PageHome2_ReturnHomeControllerAndMethodIndexAndPram2()
        {
            RouteData routeData = DefineUrl("~/Home/Index/2");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("2", routeData.Values["id"])
            );
        }
    }
}
