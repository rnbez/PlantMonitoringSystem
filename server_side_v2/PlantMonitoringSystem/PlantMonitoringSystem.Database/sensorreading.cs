//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlantMonitoringSystem.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class sensorreading
    {
        public int id { get; set; }
        public decimal reading { get; set; }
        public System.DateTime reading_date { get; set; }
        public int sensor_id { get; set; }
    
        public virtual sensor sensor { get; set; }
    }
}
