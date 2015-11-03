using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    [DataContract]
    public partial class SensorReading
    {
        [DataMember(Name="id")]
        public int? Id { get; set; }

        [DataMember(Name = "reading")]
        public int Reading { get; set; }

        [DataMember(Name = "date")]
        public DateTime ReadingDate { get; set; }

        [DataMember(Name = "sensor")]
        public int SensorId { get; set; }

    }
}
