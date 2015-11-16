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

        public static Node Get(int id)
        {
            var result = Context.GetInstance().nodes.FirstOrDefault(x => x.id == id);
            return (Node)result;
        }

        public static async Task<Node> Insert(Node data)
        {
            var ctx = Context.GetInstance();
                        
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

            var ctx = Context.GetInstance();

            var raw = toRaw(data);
            ctx.nodes.Attach(raw);
            System.Data.Entity.Infrastructure.DbEntityEntry<Database.node> entry = ctx.Entry(raw);
            entry.State = System.Data.Entity.EntityState.Modified;
            
            await ctx.SaveChangesAsync();
                        
            return Get((int)data.Id);
        }

        public static async Task<Node> Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public static List<Node> List()
        {
            var result = Context.GetInstance().nodes
                .ToList();
            return result
                    .Select(x => (Node)x)
                    .DefaultIfEmpty(new Node())
                    .ToList();
        }

        public static List<Sensor> ListSensors(int id)
        {
            var result = Context.GetInstance().sensors
                .Where(x => x.node_id == id)
                .Take(100)
                .ToList();
            return result.Select(x => (Sensor)x).ToList();
        }
    }
}
