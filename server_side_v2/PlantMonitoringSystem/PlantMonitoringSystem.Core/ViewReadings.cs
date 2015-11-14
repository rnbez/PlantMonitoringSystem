using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Core
{

    public static class ViewReadings
    {
        //last hour
        //last day
        //last week
        public static void GetLastReadings(int sensorId)
        {
            var now = DateTime.Now;


            var readings = Model.Sensor.ListReadings(sensorId, now.AddHours(-2), now);
            //MathNet.Numerics.Interpolate.Common().Interpolate(point)
            //MathNet.Numerics.Interpolation.LinearSpline.InterpolateSorted(x, y);
            var dict = readings
                .Select(r => new { r.ReadingDate.Ticks, r.Reading })
                .ToDictionary(r => Convert.ToDouble(r.Ticks), r => Convert.ToDouble(r.Reading));

            double[] x = dict.Keys.ToArray();
            double[] y = dict.Values.ToArray();
            var comInterp = MathNet.Numerics.Interpolate.Common(x, y);
            var linInterp = MathNet.Numerics.Interpolation.LinearSpline.InterpolateSorted(x, y);

            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, (Convert.ToInt32(now.Minute/10)*10), 0, 0);
            for (int i = 6; i >= 0; i--)
            {
                var t = now.AddMinutes(-(i*10));
                var ticks = Convert.ToDouble(t.Ticks);
                var a = comInterp.Interpolate(ticks);
                var b = linInterp.Interpolate(ticks);
                var str = string.Format("{0:00}/{1:00} => a: {2:0.00}; b: {3:0.00}", t.Hour, t.Minute, a, b);
                System.Diagnostics.Debug.WriteLine(str);
            }

        }
    }
}
