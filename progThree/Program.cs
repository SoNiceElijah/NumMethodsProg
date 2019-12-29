using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program
{
    class Program
    {
        static double[] A = { -500.006, 499.955, 499.995, -500.005 };
        static void Main(string[] args)
        {
            var auto = true;
            
            var N = 10000;
            var x = 0;
            var xmax = 800;
            var h = 0.001;
            var eps = 0.00001;
        }

        static double[] U(double x)
        {
            double[] a1 = { 10 * Math.Exp(-0.01 * x), 10 * Math.Exp(-0.01 * x) };
            double[] a2 = { 3 * Math.Exp(-1000 * x), -3 * Math.Exp(-1000 * x) };

            double[] res = { a1[0] - a2[0], a1[1] - a2[1] };

            return res;
        }

        static double[] rk(double[] v, double h)
        {
            double[,] E = { { 1, 0 }, { 0, 1 } };

            


        }

    }
}
