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

            double xi = h;
            bool changed = false;

            for(int i = 1; i < num; ++i)
            {
                if(!changed && xi >= drop)
                {
                    Kx = K2x;
                    Qx = Q2x;
                    Fx = F2x;

                    changed = true;
                }

                double ai = h * Kx(xi);
                double ai1 = h * Kx(xi+h);
                double di = 1 / h * Qx(xi);
                double fi = 1 / h * Fx(xi);

                A[i, i] = (-ai - ai1) / (h * h) - di;
                A[i, i - 1] = ai / (h * h);
                A[i, i + 1] = ai1 / (h * h);

                B[i] = -fi;

                xi += h;
            }

            double[] alpha = new double[num + 1];
            double[] beta = new double[num + 1];

            alpha[1] = A[0, 1];
            beta[1] = mu1;


            //Прямой ход
            for(int i = 1; i < num; ++i)
            {
                alpha[i + 1] = A[i, i + 1] / (-A[i, i] - A[i, i - 1] * alpha[i]);
                beta[i + 1] = (B[i] + A[i, i - 1] * beta[i]) / (-A[i, i] - A[i, i - 1] * alpha[i]);
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
