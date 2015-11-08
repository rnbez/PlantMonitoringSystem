//using PlantMonitoringSystem.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlantMonitoringSystem.Core
//{
//    public static class NodeGreeting
//    {
//        public static async Task<Node> doHandshake(Node node)
//        {
//            if (node == null) throw new ArgumentException("node");

//            if (node.Id == null)
//            {
//                node = await Model.Node.Insert(node);
//            }
//            else
//            {
//                node = await Model.Node.Update(node);
//            }

//            return node;
//        }
//    }
//}
