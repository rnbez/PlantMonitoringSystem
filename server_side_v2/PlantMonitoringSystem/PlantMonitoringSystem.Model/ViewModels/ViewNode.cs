using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model.ViewModels
{
    [DataContract]
    public class ViewNode
    {
        public static explicit operator ViewNode(Model.Node node)
        {
            if (node != null)
            {
                var view =  new ViewNode()
                {
                    Id = (int)node.Id,
                    FriendlyName = node.FriendlyName,
                    PhysicalAddress = node.PhysicalAddress,
                    Online = false,                  
                    IsLightOn = node.IsLightOn,
                    IsWaterOn = node.IsWaterOn,
                    Sensors = new List<Sensor>(),                                  
                };

                if (node.Sensors != null && node.Sensors.Count > 0)
                {
                    view.Sensors = node.Sensors.Select(x => (Sensor)x).ToList();
                }

                return view;
            }
            else
            {
                return null;
            }
        }

        [DataContract]
        public class Sensor
        {
            public static explicit operator Sensor(Model.Sensor sensor)
            {
                if (sensor != null)
                {
                    return new Sensor()
                    {
                        Id = (int)sensor.Id,
                        FriendlyName = sensor.FriendlyName,
                        Unit = sensor.MeasurementUnit,
                        LastReading = 0m,
                    };
                }
                else
                {
                    return null;
                }
            }

            [DataMember(Name = "id")]
            public int Id { get; set; }

            [DataMember(Name = "friendlyName")]
            public string FriendlyName { get; set; }

            [DataMember(Name = "lastReading")]
            public decimal LastReading { get; set; }

            [DataMember(Name = "measurementUnit")]
            public string Unit { get; set; }
        }
        
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "physicalAddress")]
        public string PhysicalAddress { get; set; }

        [DataMember(Name = "friendlyName")]
        public string FriendlyName { get; set; }

        [DataMember(Name = "online")]
        public bool Online { get; set; }

        [DataMember(Name = "waterOn")]
        public bool IsWaterOn { get; set; }

        [DataMember(Name = "lightOn")]
        public bool IsLightOn { get; set; }

        [DataMember(Name = "sensors")]
        public List<Sensor> Sensors { get; set; }
    }



}
