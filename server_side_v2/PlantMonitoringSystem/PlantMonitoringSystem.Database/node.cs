//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantMonitoringSystem.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class node
    {
        public node()
        {
            this.sensors = new HashSet<sensor>();
        }
    
        public int id { get; set; }
        public string physical_address { get; set; }
        public int behavior_id { get; set; }
        public string friendly_name { get; set; }
    
        public virtual behavior behavior { get; set; }
        public virtual ICollection<sensor> sensors { get; set; }
    }
}
