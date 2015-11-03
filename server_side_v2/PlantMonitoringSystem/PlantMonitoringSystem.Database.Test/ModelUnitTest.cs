using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlantMonitoringSystem.Database.Test
{
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void TestGet()
        {
            var result = Model.SensorReading.Get(3);
            Assert.IsNotNull(result);
        }
    }
}
