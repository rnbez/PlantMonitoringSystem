using PlantMonitoringSystem.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Core
{
    public static class ViewReadingsBuilder
    {
        public static ViewReadings GetLastHour(int sensorId)
        {
            ViewReadings view = new ViewReadings("Last Hour", new Dictionary<string, decimal>());
            var now = DateTime.Now;
            var nowRounded = new DateTime(now.Year, now.Month, now.Day, now.Hour, (Convert.ToInt32(now.Minute / 10) * 10), 0, 0);

            for (int i = 6; i >= 0; i--)
            {
                var startDate= nowRounded.AddMinutes(-(i * 10));
                var endDate = startDate.AddMinutes(10);
                var key = string.Format("{0:00}:{1:00}", endDate.Hour, endDate.Minute);
                
                if (endDate > now)
                {
                    endDate = now;
                    key = string.Format("{0:00}:{1:00}", now.Hour, now.Minute+1);
                }

                var avg = Model.Sensor.AverageReadings(sensorId, startDate, endDate);
                view.Values.Add(key, avg);
            }            


            if (view.Values.Count > 0)
            {
                return view;
            }
            else
            {
                return null;
            }

        }

        public static ViewReadings GetLast24Hours(int sensorId)
        {
            ViewReadings view = new ViewReadings("Last 24 Hours", new Dictionary<string, decimal>());
            var now = DateTime.Now;
            var nowRounded = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, 0);

            for (int i = 6; i >= 0; i--)
            {
                var startDate = nowRounded.AddHours(-(i * 4));
                var endDate = startDate.AddHours(4);
                var key = string.Format("{0} {1:00}h", endDate.DayOfWeek.ToString().Substring(0, 3), endDate.Hour);

                if (endDate > now)
                {
                    endDate = now;
                    key = string.Format("{0} {1:00}h", endDate.DayOfWeek.ToString().Substring(0, 3), endDate.Hour + 1);
                }

                var avg = Model.Sensor.AverageReadings(sensorId, startDate, endDate);
                view.Values.Add(key, avg);
            }

            if (view.Values.Count > 0)
            {
                return view;
            }
            else
            {
                return null;
            }

        }

        public static ViewReadings GetLast7Days(int sensorId)
        {
            ViewReadings view = new ViewReadings("Last 7 Days", new Dictionary<string, decimal>());
            var now = DateTime.Now;
            var nowRounded = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);

            for (int i = 6; i >= 0; i--)
            {
                var startDate = nowRounded.AddDays(-i);
                var endDate = startDate.AddDays(1);
                var key = string.Format("{0:00}/{1:00}", startDate.Month, startDate.Day);

                if (endDate > now)
                {
                    endDate = now;
                    //key = string.Format("{0:00}/{1:00}", endDate.DayOfWeek.ToString().Substring(0, 3), endDate.Hour + 1);
                }

                var avg = Model.Sensor.AverageReadings(sensorId, startDate, endDate);
                view.Values.Add(key, avg);
            }

            if (view.Values.Count > 0)
            {
                return view;
            }
            else
            {
                return null;
            }

        }

    }
}
