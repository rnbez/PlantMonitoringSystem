using PlantMonitoringSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    internal static class Context
    {
        public static DatabaseEntities GetInstance()
        {
            return new DatabaseEntities();
        }
    }
}
