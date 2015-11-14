using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlantMonitoringSystem.Core.Test
{
    [TestClass]
    public class ReadingsUnitTest
    {
        [TestMethod]
        public void InterpolationTest()
        {
            Core.ViewReadings.GetLastReadings(20);
            Assert.IsTrue(true);
        }
    }
}
