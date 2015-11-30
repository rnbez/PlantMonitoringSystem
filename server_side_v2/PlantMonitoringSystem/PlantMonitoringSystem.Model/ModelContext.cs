using PlantMonitoringSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlantMonitoringSystem.Model
{
    public static class ModelContext
    {
        public static DatabaseEntities DataBaseContext
        {
            get
            {
                return HttpContext.Current.Items["DataBaseContext"] as DatabaseEntities;
            }
            set
            {
                HttpContext.Current.Items["DataBaseContext"] = value;
            }
        }
        public static DatabaseEntities GetInstance()
        {
            return DataBaseContext;
        }

        public static DatabaseEntities GetNewInstance()
        {
            return new DatabaseEntities();
        }

    }
}
