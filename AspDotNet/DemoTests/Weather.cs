using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoTests
{
    public enum WeatherType
    {
        Rain,
        Sun
    }

    public class Weather
    {
        public double Temperature { get; set; }
        public WeatherType Weathers { get; set; }
    }
}