using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PlantMonitoringSystem.Database.Test
{
    [TestClass]
    public class DatabaseUnitTest
    {
        [TestMethod]
        public void TestContext()
        {
            var ctx = new  PlantMonitoringSystem.Database.DatabaseEntities();

            Assert.IsTrue(true);
        }
    }
}
