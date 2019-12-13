using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{

    public class Method
    {
        public delegate double Func(double x);

        public Func K1x { get; private set; }
        public Func Q1x { get; private set; }
        public Func F1x { get; private set; }

        public Func K2x { get; private set; }
        public Func Q2x { get; private set; }
        public Func F2x { get; private set; }

        public Func Kx { get; private set; }
        public Func Qx { get; private set; }
        public Func Fx { get; private set; }

        public double drop;

        public double mu1;
        public double mu2;

        double step;
        double eps;

        int num;

        public double Step { get => step; }


        public Method(Func k1, Func q1, Func f1, Func k2, Func q2, Func f2,double d, double m1, double m2, int n)
        {
            K1x = k1;
            Q1x = q1;
            F1x = f1;

            K2x = k2;
            Q2x = q2;
            F2x = f2;

            drop = d;

            mu1 = m1;
            mu2 = m2;

            num = n;
        }

        public double[] Count()
        {
            double[,] A = new double[num + 1, num + 1];
            double[] B = new double[num + 1];

            for(int i = 0; i < A.Length; ++i)
                A[i / (num + 1), i % (num + 1)] = 0;

            double h = 1.0 / num;

            Kx = K1x;
            Qx = Q1x;
            Fx = F1x;

            B[0] = mu1;
            B[num] = mu2;

            A[0, 0] = 1;
            A[num, num] = 1;


            double[] a = new double[num + 1];
            double[] d = new double[num + 1];
            double[] f = new double[num + 1];

            a[0] = d[0] = f[0] = 0;

            double xi = h;
            for (int i = 1; i < num + 1; ++i)
            {
                if (xi >= drop && xi - h <= drop)
                {
                    a[i] = h / (Kx(drop) - Kx(xi - h));

                    Kx = K2x;
                    Qx = Q2x;
                    Fx = F2x;

                    a[i] += h / (Kx(xi) - Kx(drop));

                    continue;
                }

                a[i] = h / (Kx(xi) - Kx(xi - h));
                xi += h;

            }

            Kx = K1x;
            Qx = Q1x;
            Fx = F1x;

            double xi2 = h * 0.5;
            for (int i = 1; i < num + 1; ++i)
            {
                if (xi + h >= drop && xi <= drop)
                {

                    d[i] = (1.0 / h) * (Qx(drop) - Qx(xi2));
                    f[i] = (1.0 / h) * (Fx(drop) - Fx(xi2));

                    Kx = K2x;
                    Qx = Q2x;
                    Fx = F2x;

                    d[i] += (1.0 / h) * (Qx(drop) - Qx(xi2));
                    f[i] += (1.0 / h) * (Fx(drop) - Fx(xi2));

                    continue;
                }

                d[i] = (1.0 / h) * (Qx(xi2 + h) - Qx(xi2));
                f[i] = (1.0 / h) * (Fx(xi2 + h) - Fx(xi2));

                xi2 += h;

            }

            for (int i = 1; i < num; ++i)
            {
                A[i, i] = (-a[i] - a[i+1]) / (h * h) - d[i];
                A[i, i - 1] = a[i] / (h * h);
                A[i, i + 1] = a[i+1] / (h * h);

                B[i] = -f[i];
            }

            double[] alpha = new double[num + 1];
            double[] beta = new double[num + 1];

            alpha[1] = -A[0, 1];
            beta[1] = mu1;


            //Прямой ход
            for(int i = 1; i < num; ++i)
            {
                alpha[i + 1] = A[i, i + 1] / (-A[i, i] - A[i, i - 1] * alpha[i]);
                beta[i + 1] = (-B[i] + A[i, i - 1] * beta[i]) / (-A[i, i] - A[i, i - 1] * alpha[i]);
            }

            double[] u = new double[num + 1];
            //Обратный ход

            u[num] = (A[num, num - 1] * beta[num] - mu2) / (-A[num, num - 1] - 1);
            for(int i = num-1; i >= 0; --i)
            {
                u[i] = alpha[i + 1] * u[i + 1] + beta[i + 1];
            }

            return u;
        }

    }
}
