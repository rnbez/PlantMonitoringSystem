using PlantMonitoringSystem.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Core
{
    public static class NodesView
    {
        public static List<ViewNode> GetNodeList()
        {
            var viewList = new List<ViewNode>();
            var modelNodes = Model.Node.List((int)Core.ApplicationContext.CurrentUser.Id);

            foreach (var modelNode in modelNodes)
            {
                modelNode.Sensors = Model.Node.ListSensors((int)modelNode.Id);
                var viewNode = (ViewNode)modelNode;
                viewNode.Online = false;

                foreach (var sensor in viewNode.Sensors)
                {
                    var reading = Model.Sensor.LastReading(sensor.Id);
                    sensor.LastReading = reading != null ? reading.Reading : 0;
                    if (!viewNode.Online)
                    {
                        viewNode.Online = Model.Sensor.IsOnline(sensor.Id);
                    }
                }

                viewList.Add(viewNode);
            }
            return viewList;
        }

        public static ViewNode GetNode(int id)
        {
            var node = Model.Node.Get(id, (int)Core.ApplicationContext.CurrentUser.Id);
            node.Sensors = Model.Node.ListSensors(id);
            var viewNode = (ViewNode)node;
            viewNode.Online = false;

            foreach (var sensor in viewNode.Sensors)
            {
                var reading = Model.Sensor.LastReading(sensor.Id);
                sensor.LastReading = reading != null ? reading.Reading : 0;
                if (!viewNode.Online)
                {
                    viewNode.Online = Model.Sensor.IsOnline(sensor.Id);
                }
            }

            return viewNode;
        }
    }
}
