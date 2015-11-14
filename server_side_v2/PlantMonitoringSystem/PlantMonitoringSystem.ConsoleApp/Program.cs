using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] x = {  1,  2,  3,  4,  5,   6,  7,  8,  9, 10, 11};
            double[] y = { 10, 20, 40, 60, 90, 120, 90, 60, 40, 20, 10};


            var interp = MathNet.Numerics.Interpolation.LinearSpline.InterpolateSorted(x, y);

            Debug.WriteLine(interp.Interpolate(3));
            Debug.WriteLine(interp.Interpolate(5));
            Debug.WriteLine(interp.Interpolate(5.5));
            Debug.WriteLine(interp.Interpolate(6));
            Debug.WriteLine(interp.Interpolate(6.5));
            Debug.WriteLine(interp.Interpolate(9));
        }
    }
}
