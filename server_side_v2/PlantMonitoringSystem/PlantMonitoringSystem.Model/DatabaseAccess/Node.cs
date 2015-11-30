using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    public partial class Node
    {
        public static explicit operator Node(Database.node raw)
        {
            if (raw != null)
            {
                return new Node()
                {
                    Id = raw.id,
                    PhysicalAddress = raw.physical_address,
                    FriendlyName = raw.friendly_name,
                    IsWaterOn = (bool)raw.water_on,
                    IsLightOn = (bool)raw.light_on,
                    BehaviorId = raw.behavior_id,
                    UserId = raw.user_id,
                };
            }
            else
            {
                return null;
            }
        }
        public static Database.node toRaw(Node data)
        {
            var raw = new Database.node()
            {
                physical_address = data.PhysicalAddress,
                friendly_name = data.FriendlyName,
                water_on = data.IsWaterOn,
                light_on = data.IsLightOn,
                behavior_id = data.BehaviorId,
                user_id = data.UserId,
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
            }

            if (data.Sensors != null)
            {
                foreach (var sensor in data.Sensors)
                {
                    raw.sensors.Add(Sensor.toRaw(sensor));
                }
            }

            return raw;
        }

        public static Node Get(int id, int userId)
        {
            var result = ModelContext.GetInstance().nodes.FirstOrDefault(x => x.id == id && x.user_id == userId);
            return (Node)result;
        }

        public static async Task<Node> Insert(Node data)
        {
            var ctx = ModelContext.GetInstance();
                        
            ctx.nodes.Add(toRaw(data));

            await ctx.SaveChangesAsync();

            return (Node)ctx.nodes.OrderByDescending(x => x.id).FirstOrDefault();
        }

        public static async Task<Node> Update(Node data)
        {
            if (data.Sensors != null)
            {
                await Sensor.Update(data.Sensors);
            }

            var ctx = ModelContext.GetNewInstance();

            var raw = toRaw(data);
            ctx.nodes.Attach(raw);
            System.Data.Entity.Infrastructure.DbEntityEntry<Database.node> entry = ctx.Entry(raw);
            entry.State = System.Data.Entity.EntityState.Modified;
            
            await ctx.SaveChangesAsync();
                        
            return Get((int)data.Id, data.UserId);
        }

        public static Node Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public static List<Node> List(int userId)
        {
            var result = ModelContext.GetInstance().nodes
                .Where(x => x.user_id == userId)
                .ToList();
            return result
                    .Select(x => (Node)x)
                    .DefaultIfEmpty(new Node())
                    .ToList();
        }

        public static List<Sensor> ListSensors(int id)
        {
            var result = ModelContext.GetInstance().sensors
                .Where(x => x.node_id == id)
                .Take(100)
                .ToList();
            return result.Select(x => (Sensor)x).ToList();
        }
    }
}
