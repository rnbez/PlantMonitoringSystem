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
                friendly_name = data.PhysicalAddress,
                behavior_id = data.BehaviorId
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
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
            throw new NotImplementedException();
        }

        public static async Task<Node> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public static List<Sensor> ListSensors(int id)
        {
            var result = Context.GetInstance().sensors
                .Where(x => x.node_id == id)
                .OrderByDescending(x => x.friendly_name)
                .Take(1000)
                .ToList();
            return result.Select(x => (Sensor)x).ToList();
        }
    }
}
