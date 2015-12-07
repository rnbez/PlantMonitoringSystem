using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    public partial class Behavior
    {
        public static explicit operator Behavior(Database.behavior raw)
        {
            if (raw != null)
            {
                return new Behavior()
                {
                    Id = raw.id,
                    Name = raw.name,
                    LightAuto = raw.lightAuto,
                    WaterAuto = raw.waterAuto,
                    LightStartHour = raw.lightStartHour,
                    LightStopHour = raw.lightStopHour,
                    WaterHumLevel = raw.waterHumLevel,
                    UserId = raw.user_id
                };
            }
            else
            {
                return null;
            }
        }
       
        public static Database.behavior toRaw(Behavior data)
        {
            var raw = new Database.behavior()
            {
                name = data.Name,
                lightAuto = data.LightAuto,
                waterAuto = data.WaterAuto,
                lightStartHour = data.LightStartHour,
                lightStopHour = data.LightStopHour,
                waterHumLevel = data.WaterHumLevel,
                user_id = data.UserId, 
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
            }

            return raw;
        }

        public static Behavior Get(int id, int userId)
        {
            var result = ModelContext.GetInstance().behaviors.FirstOrDefault(x => x.id == id && x.user_id == userId);
            return (Behavior)result;
        }

        public static async Task<Behavior> Insert(Behavior data)
        {
            var ctx = ModelContext.GetInstance();

            ctx.behaviors.Add(toRaw(data));

            await ctx.SaveChangesAsync();

            return (Behavior)ctx.behaviors.OrderByDescending(x => x.id).FirstOrDefault();
        }

        public static async Task<Behavior> Update(Behavior data)
        {
            var ctx = ModelContext.GetNewInstance();

            var raw = toRaw(data);
            ctx.behaviors.Attach(raw);
            System.Data.Entity.Infrastructure.DbEntityEntry<Database.behavior> entry = ctx.Entry(raw);
            entry.State = System.Data.Entity.EntityState.Modified;

            await ctx.SaveChangesAsync();

            return Get((int)data.Id, data.UserId);
        }

        public static Behavior Delete(int id)
        {
            throw new NotImplementedException();
        }

        public static List<Behavior> List(int userId)
        {
            var result = ModelContext.GetInstance().behaviors
                .Where(x => x.user_id == userId)
                .ToList();
            if (result != null && result.Count > 0)
            {
                return result
                    .Select(x => (Behavior)x)
                    .ToList();
            }
            else
            {
                return new List<Behavior>();
            }

        }
    }
}
