using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    public partial class SensorReading
    {
        public static SensorReading Build(Database.sensorreading raw)
        {
            if (raw != null)
            {
                return new SensorReading()
                {
                    Id = raw.id,
                    Reading = raw.reading,
                    ReadingDate = raw.reading_date,
                    SensorId = raw.sensor_id,
                };
            }
            else
            {
                return null;
            }
        }
        public static Database.sensorreading toRaw(SensorReading data)
        {
            var raw = new Database.sensorreading()
            {
                reading = data.Reading,
                reading_date = data.ReadingDate,
                sensor_id = data.SensorId,
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
            }

            return raw;
        }

        public static List<SensorReading> List()
        {
            var result = Context.GetInstance().sensorreadings
                .OrderByDescending(x => x.reading_date)
                .Take(1000)
                .ToList();
            return result.Select(x => Build(x)).ToList();
        }

        public static SensorReading Get(int id)
        {
            var result = Context.GetInstance().sensorreadings.FirstOrDefault(x => x.id == id);
            return Build(result);
        }

        public static async Task<SensorReading> Insert(SensorReading data)
        {
            var ctx = Context.GetInstance();

            ctx.sensorreadings.Add(toRaw(data));

            await ctx.SaveChangesAsync();
            return Build(ctx.sensorreadings.OrderByDescending(x => x.id).FirstOrDefault());
        }

    }
}
