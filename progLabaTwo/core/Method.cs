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

        double eps;

        int num;

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
            double h = 1.0 / num;

            Kx = K1x;
            Qx = Q1x;
            Fx = F1x;


            double[] a = new double[num + 1];
            double[] d = new double[num + 1];
            double[] f = new double[num + 1];

            a[0] = d[0] = f[0] = 0;



            double xi = h * 0.5;
            for (int i = 1; i < num + 1; ++i)
            {
                if (xi - 0.5*h < drop && xi + 0.5 * h > drop)
                {
                    a[i] = (drop - xi-0.5*h) / Kx((xi - 0.5 * h + drop)/2);

                    Kx = K2x;

                    a[i] += ( - drop + xi + 0.5 * h) / Kx((xi + 0.5 * h + drop) / 2);
                    a[i] = Math.Pow(1.0 / h * a[i], -1);

                    xi += h;

                    continue;
                }

                a[i] = Kx(xi);
                xi += h;

            }

            Qx = Q1x;
            Fx = F1x;

            xi = h;
            for (int i = 1; i < num + 1; ++i)
            {
                if (xi - 0.5*h < drop && xi + 0.5*h > drop)
                {

                    d[i] = Qx((xi - 0.5 * h + drop) / 2) * (drop - (xi - 0.5 * h));
                    f[i] = Fx((xi - 0.5 * h + drop) / 2) * (drop - (xi - 0.5 * h));

                    Qx = Q2x;
                    Fx = F2x;

                    d[i] += Qx((xi + 0.5 * h + drop) / 2) * (- drop + (xi + 0.5 * h));
                    f[i] += Fx((xi + 0.5 * h + drop) / 2) * (-drop + (xi + 0.5 * h));

                    d[i] *= 1.0 / h;
                    f[i] *= 1.0 / h;

                    xi += h;

                    continue;
                }

                d[i] = Qx(xi);
                f[i] = Fx(xi);

                xi += h;

            }


            double Ai;
            double Bi;
            double Ci;

            double[] alpha = new double[num + 1];
            double[] beta = new double[num + 1];

            alpha[1] = 0;
            beta[1] = mu1;


            //Прямой ход
            for(int i = 1; i < num; ++i)
            {
                Ai = 1.0 / (h * h) * a[i];
                Bi = 1.0 / (h * h) * a[i + 1];
                Ci = (1.0 / (h * h)) * (a[i] + a[i + 1]) + d[i];

                alpha[i + 1] = Bi / (Ci - Ai * alpha[i]);
                beta[i + 1] = (f[i] + Ai * beta[i]) / (Ci - Ai * alpha[i]);
            }

            double[] u = new double[num + 1];
            //Обратный ход

            u[num] = mu2;
            for(int i = num-1; i >= 0; --i)
            {
                u[i] = alpha[i + 1] * u[i + 1] + beta[i + 1];
            }

            return u;
        }

    }
}
