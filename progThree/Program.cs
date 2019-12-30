using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program
{
    class Program
    {
        static double[,] A = { { -500.006, 499.955 }, { 499.995, -500.005 } };
        static void Main(string[] args)
        {
            bool stopper = true;

            var N = 10000;
            var x = 0.0;
            var xmax = 800.0;
            var h = 0.001;
            var eps = 0.00001;

            var p = 2;

            double[] v = U(x);

            Console.WriteLine($"i = {0}\nx = {x}\nh = {h}\nv = [{v[0]},{v[1]}]\n\n");


            double pH = h;
            double[] sV = { v[0], v[1] };
            double[] hV = { v[0], v[1] };
            double[] pV = { v[0], v[1] };

            int c1 = 0;
            int c2 = 0;

            double maxDiff = 0;

            int i = 1;
            while(i <= N)
            {
                pH = h;
                sV = rk(pV, h);
                hV = rk(pV, h * 0.5);
                hV = rk(hV, h * 0.5);
                double s1 = (hV[0] - sV[0]) / (Math.Pow(2, p) - 1);
                double s2 = (hV[1] - sV[1]) / (Math.Pow(2, p) - 1);

                double s = Math.Abs(s1) > Math.Abs(s2) ? s1 : s2;

                double[] e = U(x + h);
                x += h;

                if (Math.Abs(s) > eps)
                {
                    x -= h;
                    h *= 0.5;
                    c1 += 1;
                    continue;
                }
                else if (Math.Abs(s) < (eps / (Math.Pow(2, p + 1))))
                {
                    c2 += 1;
                    h *= 2.0;
                }

                Console.WriteLine($"i = {i}\nx = {x}\nh = {pH}\nВычисленное решение:\nv = [{sV[0]},{sV[1]}]");
                Console.WriteLine($"Точное решение:\nu = [{e[0]},{e[1]}]\n|V(x) - U(x)|:");
                Console.WriteLine($"[{Math.Abs(e[0] - sV[0])},{Math.Abs(e[1] - sV[1])}]");
                Console.WriteLine("||V(x) - U(x)||:");
                Console.WriteLine($"{Math.Sqrt((e[0] - sV[0])* (e[0] - sV[0]) + (e[1] - sV[1]) * (e[1] - sV[1]))}");
                Console.WriteLine($"S = {s}\n\n");

                if (stopper)
                {
                    var command = Console.ReadLine();
                    if (command == "go")
                        stopper = false;
                }

                if (x >= xmax)
                    break;

                pV = sV;
                i++;

                double norm = Math.Sqrt((e[0] - sV[0]) * (e[0] - sV[0]) + (e[1] - sV[1]) * (e[1] - sV[1]));
                if (norm > maxDiff)
                    maxDiff = norm;
            }

            Console.WriteLine("Max{||V(x) - U(x)||}:");
            Console.WriteLine(maxDiff);

            Console.WriteLine($"Делений шага: {c1}");
            Console.WriteLine($"Удвоений шага: {c2}");

            Console.ReadLine();


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
            double[,] mtx = { { 1-A[0,0]*(h/2), -A[0,1] * (h/2) }, { -A[1,0] * (h/2), 1 - A[1,1] * (h/2) } };

            double det = (1.0 / (mtx[0, 0] * mtx[1, 1] - mtx[1, 0] * mtx[0, 1]));

            double[,] inv = { { h*det * mtx[1, 1], -h*det * mtx[0, 1] }, { -h*det * mtx[1, 0], h*det * mtx[0, 0] } };

            double[] tmp = { A[0, 0] * v[0] + A[0, 1] * v[1], A[1, 0] * v[0] + A[1, 1] * v[1] };
            double[] inc = { inv[0, 0] * tmp[0] + inv[0, 1] * tmp[1], inv[1, 0] * tmp[0] + inv[1, 1] * tmp[1] };

            double[] res = { v[0] + inc[0], v[1] + inc[1] };

            return res;
        }

    }
}
