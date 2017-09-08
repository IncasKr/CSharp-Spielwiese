using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using LogAnPattern.Tests.Integration;
using LogAnPattern.Tests.Base;

namespace LogAnPattern.Tests.UnitTests
{
    /// <summary>
    /// Summary description for ConfigurationManagerTests
    /// </summary>
    [TestFixture]
    public class ConfigurationManagerTests : BaseTestClass
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Console.WriteLine("in derived");            
        }

        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            ConfigurationManager cm = new ConfigurationManager();
            bool configured = cm.IsConfigured("something");
            //rest of test
        }
    }
}
