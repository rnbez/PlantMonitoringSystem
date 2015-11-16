using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    [DataContract]
    public partial class Node
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "physicalAddress")]
        public string PhysicalAddress { get; set; }

        [DataMember(Name = "friendlyName")]
        public string FriendlyName { get; set; }

        [DataMember(Name = "behaviorId")]
        public int BehaviorId { get; set; }

        [DataMember(Name = "waterOn")]
        public bool IsWaterOn { get; set; }

        [DataMember(Name = "lightOn")]
        public bool IsLightOn { get; set; }

        [DataMember(Name = "sensors", EmitDefaultValue=false)]
        public List<Sensor> Sensors { get; set; }

    }
}
