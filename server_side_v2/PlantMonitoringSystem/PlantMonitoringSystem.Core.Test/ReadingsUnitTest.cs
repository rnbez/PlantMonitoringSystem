using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace PlantMonitoringSystem.Core.Test
{
    [TestClass]
    public class ReadingsUnitTest
    {
        [TestMethod]
        public void GetNodesTest()
        {

            var result = Core.NodesView.GetNodeList();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReadingsTest()
        {

            var result = Core.SensorViewReadings.GetLastReadings(20);
            Assert.IsNotNull(result);
        }
    }
}
