using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    public partial class Sensor
    {
        public static explicit operator Sensor(Database.sensor raw)
        {
            if (raw != null)
            {
                return new Sensor()
                {
                    Id = raw.id,
                    SensorType = raw.sensor_type,
                    FriendlyName = raw.friendly_name,
                    MeasurementName = raw.measurement_name,
                    MeasurementUnit = raw.measurement_unit,
                    NodeId = raw.node_id,
                };
            }
            else
            {
                return null;
            }
        }
        public static Database.sensor toRaw(Sensor data)
        {
            var raw = new Database.sensor()
            {
                sensor_type = data.SensorType,
                friendly_name = data.FriendlyName,
                measurement_name = data.MeasurementName,
                measurement_unit = data.MeasurementUnit,
                node_id = data.NodeId,
            };

            if (data.Id != null)
            {
                raw.id = (int)data.Id;
            }

            return raw;
        }
        
        public static Sensor Get(int id)
        {
            var result = Context.GetInstance().sensors.FirstOrDefault(x => x.id == id);
            return (Sensor)result;
        }

        public static async Task<Sensor> Insert(Sensor data)
        {
            var ctx = Context.GetInstance();

            ctx.sensors.Add(toRaw(data));

            await ctx.SaveChangesAsync();
            return (Sensor)ctx.sensors.OrderByDescending(x => x.id).FirstOrDefault();
        }
        
        public static async Task<Sensor> Update(Sensor data)
        {
            var ctx = Context.GetInstance();

            var raw = toRaw(data);
            ctx.sensors.Attach(raw);
            System.Data.Entity.Infrastructure.DbEntityEntry<Database.sensor> entry = ctx.Entry(raw);
            entry.State = System.Data.Entity.EntityState.Modified;

            await ctx.SaveChangesAsync();

            return Get((int)data.Id);
        }

        public static async Task<List<Sensor>> Update(List<Sensor> list)
        {
            var ctx = Context.GetInstance();

            foreach (var data in list)
            {
                var raw = toRaw(data);
                ctx.sensors.Attach(raw);
                System.Data.Entity.Infrastructure.DbEntityEntry<Database.sensor> entry = ctx.Entry(raw);
                entry.State = System.Data.Entity.EntityState.Modified; 
            }

            await ctx.SaveChangesAsync();

            return List();
        }
        
        public static async Task<Sensor> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public static List<Sensor> List()
        {
            var result = Context.GetInstance().sensors
                .Take(100)
                .ToList();
            return result.Select(x => (Sensor)x).ToList();
        }

        public static List<SensorReading> ListReadings(int id)
        {
            var result = Context.GetInstance().sensorreadings
                .Where(x => x.sensor_id == id)
                .OrderByDescending(x => x.reading_date)
                .Take(1000)
                .ToList();
            return result.Select(x => (SensorReading)x).ToList();
        }
    }
}
