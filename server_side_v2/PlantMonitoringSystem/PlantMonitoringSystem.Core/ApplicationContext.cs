using PlantMonitoringSystem.Database;
using PlantMonitoringSystem.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlantMonitoringSystem.Core
{
    public static class ApplicationContext
    {
        private static ConcurrentDictionary<string, SystemUser> LoggedUsersDic = new ConcurrentDictionary<string, SystemUser>();
        
        public static DatabaseEntities DataBaseContext
        {
            get
            {
                return ModelContext.DataBaseContext;
            }
            set
            {
                ModelContext.DataBaseContext = value;
            }
        }

        public static SystemUser CurrentUser
        {
            get
            {
                return HttpContext.Current.Items["CurrentUser"] as SystemUser;
            }
            set
            {
                HttpContext.Current.Items["CurrentUser"] = value;
            }
        }

        public static bool AddAuthenticatedUser(SystemUser user)
        {
            return LoggedUsersDic.TryAdd(user.AuthToken, user);
        }
        public static SystemUser GetAuthenticatedUser(string token)
        {
            SystemUser user = null;
            return LoggedUsersDic.TryGetValue(token, out user) ? user : null;
        }


        public static bool RemoveAuthenticatedUser(string token)
        {
            SystemUser user = null;
            return LoggedUsersDic.TryRemove(token, out user);
        }
    }
}
