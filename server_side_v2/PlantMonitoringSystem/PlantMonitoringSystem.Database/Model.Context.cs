﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<behavior> behaviors { get; set; }
        public virtual DbSet<node> nodes { get; set; }
        public virtual DbSet<sensor> sensors { get; set; }
        public virtual DbSet<sensorreading> sensorreadings { get; set; }
        public virtual DbSet<systemUser> systemUsers { get; set; }
    }
}
