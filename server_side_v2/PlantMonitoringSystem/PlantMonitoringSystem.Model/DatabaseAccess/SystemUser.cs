using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    public partial class SystemUser
    {
        public static explicit operator SystemUser(Database.systemUser raw)
        {
            if (raw != null)
            {
                return new SystemUser()
                {
                    Id = raw.id,
                    Username = raw.username,
                    Email = raw.email,
                    //Password = raw.password,
                };
            }
            else
            {
                return null;
            }
        }
        public static Database.systemUser toRaw(SystemUser data)
        {
            var raw = new Database.systemUser()
            {
                username = data.Username,
                password = data.Password,
                email = data.Email
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
            }

            return raw;
        }

        public static SystemUser Get(int id)
        {
            var result = ModelContext.GetInstance().systemUsers.FirstOrDefault(x => x.id == id);
            return (SystemUser)result;
        }

        public static async Task<SystemUser> Insert(SystemUser data)
        {
            var ctx = ModelContext.GetInstance();
                        
            ctx.systemUsers.Add(toRaw(data));

            await ctx.SaveChangesAsync();

            return (SystemUser)ctx.systemUsers.OrderByDescending(x => x.id).FirstOrDefault();
        }

        public static async Task<SystemUser> Update(SystemUser data)
        {
            var ctx = ModelContext.GetNewInstance();

            var raw = toRaw(data);
            ctx.systemUsers.Attach(raw);
            System.Data.Entity.Infrastructure.DbEntityEntry<Database.systemUser> entry = ctx.Entry(raw);
            entry.State = System.Data.Entity.EntityState.Modified;
            
            await ctx.SaveChangesAsync();
                        
            return Get((int)data.Id);
        }

        public static async Task<Node> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public static SystemUser Authenticate(string username, string password)
        {
            password = string.IsNullOrWhiteSpace(password) ? string.Empty : SystemUser.Base64Encode(password);
            var result = ModelContext
                .GetInstance()
                .systemUsers
                .FirstOrDefault(x => x.username == username && x.password == password);
            return (SystemUser)result;
        }

        public static bool UserExists(string username)
        {
            return ModelContext.GetInstance().systemUsers.Any(x => x.username == username);
        }

        public static List<Sensor> ListNodes(int id)
        {
            var result = ModelContext.GetInstance().sensors
                .Where(x => x.node_id == id)
                .Take(100)
                .ToList();
            return result.Select(x => (Sensor)x).ToList();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
