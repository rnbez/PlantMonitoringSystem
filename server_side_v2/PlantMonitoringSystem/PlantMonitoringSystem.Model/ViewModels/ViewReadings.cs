using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model.ViewModels
{
    [DataContract]
    public class ViewReadings
    {
        [DataMember(Name="name")]
        public string Name { get; set; }
        [DataMember(Name = "values")]
        public Dictionary<string, decimal> Values { get; set; }

        public ViewReadings(string name, Dictionary<string, decimal> values)
        {
            this.Name = name;
            this.Values = values;
        }
    }
}
