using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    [DataContract]
    public partial class Sensor
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "sensorName")]
        public string SensorName { get; set; }

        [DataMember(Name = "measurementName")]
        public string MeasurementName { get; set; }

        [DataMember(Name = "measurementUnit")]
        public string MeasurementUnit { get; set; }
    }
}
