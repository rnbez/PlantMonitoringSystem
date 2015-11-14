using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace PlantMonitoringSystem.Core.Test
{
    [TestClass]
    public class ReadingsUnitTest
    {
        [TestMethod]
        public void GetReadingsTest()
        {
            var lastHour = ViewReadingsBuilder.GetLastHour(20);
            var last24h = ViewReadingsBuilder.GetLast24Hours(20);
            var last7days = ViewReadingsBuilder.GetLast7Days(20);
            Assert.IsTrue(true);
        }
    }
}
