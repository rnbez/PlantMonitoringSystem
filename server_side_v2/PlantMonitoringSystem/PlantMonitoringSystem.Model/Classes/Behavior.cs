using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    [DataContract]
    public partial class Behavior
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "waterAuto")]
        public bool WaterAuto { get; set; }

        [DataMember(Name = "lightAuto")]
        public bool LightAuto { get; set; }

        [DataMember(Name = "lightStartHour")]
        public decimal LightStartHour { get; set; }

        [DataMember(Name = "lightStopHour")]
        public decimal LightStopHour { get; set; }

        [DataMember(Name = "waterHumLevel")]
        public decimal WaterHumLevel { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }


    }
}
