using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlantMonitoringSystem.Database.Test
{
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void TestGetSensorReading()
        {
            var result = Model.SensorReading.Get(3);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetSensor()
        {
            var result = Model.Sensor.Get(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetSensorReadings()
        {
            var result = Model.Sensor.ListReadings(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetNodeSensors()
        {
            var result = Model.Node.ListSensors(1);
            Assert.IsNotNull(result);
        }
    }
}
