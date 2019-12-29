using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class MDot
    {
        public double X { get; set; }
        public double U1 { get; set; }
        public double U2 { get; set; }
    }

    public class Method
    {
        public delegate double Func(double x, double u1, double u2);

        public Func Function1 { get; private set; }
        public Func Function2 { get; private set; }

        MDot point;
        double step;

        public int C1 { private set; get; } = 0;
        public int C2 { private set; get; } = 0;

        bool control;
        double eps;
        public double Step { get => step; }

        public double Lenght { get; set; }

        /// <summary>
        /// Создает объект для работы с методом РК(4)
        /// </summary>
        /// <param name="f">Функция двух пременных</param>
        /// <param name="x0">Точка x0</param>
        /// <param name="u0">Точка u0</param>
        /// <param name="s">Первоначальный шаг</param>
        public Method(Func f1, Func f2, double x0, double u10, double u20, double s, double e, bool ctrl)
        {
            Function1 = f1;
            Function2 = f2;

            point = new MDot()
            {
                X = x0,
                U1 = u10,
                U2 = u20
            };

            step = s;
            eps = e;

            control = ctrl;
        }

        public MDot nextStep(out double upV, out double param, out double len)
        {
            MDot next = new MDot();
            double h = step;

            next.X = point.X + h;

            double k1 = Function1.Invoke(point.X, point.U1, point.U2);
            double l1 = Function2.Invoke(point.X, point.U1, point.U2);
            double k2 = Function1.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k1, point.U2 + (h / 2) * l1);
            double l2 = Function2.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k1, point.U2 + (h / 2) * l1);
            double k3 = Function1.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k2, point.U2 + (h / 2) * l2);
            double l3 = Function2.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k2, point.U2 + (h / 2) * l2);
            double k4 = Function1.Invoke(point.X + h, point.U1 + h * k3, point.U2 + h * l3);
            double l4 = Function2.Invoke(point.X + h, point.U1 + h * k3, point.U2 + h * l3);


            next.U1 = point.U1 + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            next.U2 = point.U2 + (h / 6) * (l1 + 2 * l2 + 2 * l3 + l4);

            MDot half = new MDot();
            h = h / 2;

            half.X = point.X + h;

            k1 = Function1.Invoke(point.X, point.U1, point.U2);
            l1 = Function2.Invoke(point.X, point.U1, point.U2);
            k2 = Function1.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k1, point.U2 + (h / 2) * l1);
            l2 = Function2.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k1, point.U2 + (h / 2) * l1);
            k3 = Function1.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k2, point.U2 + (h / 2) * l2);
            l3 = Function2.Invoke(point.X + (h / 2), point.U1 + (h / 2) * k2, point.U2 + (h / 2) * l2);
            k4 = Function1.Invoke(point.X + h, point.U1 + h * k3, point.U2 + h * l3);
            l4 = Function2.Invoke(point.X + h, point.U1 + h * k3, point.U2 + h * l3);

            half.U1 = point.U1 + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            half.U2 = point.U2 + (h / 6) * (l1 + 2 * l2 + 2 * l3 + l4);

            MDot mes = new MDot();

            mes.X = half.X + h;

            k1 = Function1.Invoke(half.X, half.U1, half.U2);
            l1 = Function2.Invoke(half.X, half.U1, half.U2);
            k2 = Function1.Invoke(half.X + (h / 2), half.U1 + (h / 2) * k1, half.U2 + (h / 2) * l1);
            l2 = Function2.Invoke(half.X + (h / 2), half.U1 + (h / 2) * k1, half.U2 + (h / 2) * l1);
            k3 = Function1.Invoke(half.X + (h / 2), half.U1 + (h / 2) * k2, half.U2 + (h / 2) * l2);
            l3 = Function2.Invoke(half.X + (h / 2), half.U1 + (h / 2) * k2, half.U2 + (h / 2) * l2);
            k4 = Function1.Invoke(half.X + h, half.U1 + h * k3, half.U2 + h * l3);
            l4 = Function2.Invoke(half.X + h, half.U1 + h * k3, half.U2 + h * l3);

            mes.U1 = half.U1 + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            mes.U2 = half.U2 + (h / 6) * (l1 + 2 * l2 + 2 * l3 + l4);

            upV = mes.U2;

            len = Math.Sqrt(step * step + (point.U2 - next.U2) * (point.U2 - next.U2));

            param = 0;
            if (control)
            {
                double s1 = (mes.U2 - next.U2) / (15);
                double s2 = (mes.U1 - next.U1) / (15);
                

                if (Math.Abs(s2) >= eps || Math.Abs(s1) >= eps)
                {
                    C1++;
                    step /= 2;
                    return nextStep(out upV, out param, out len);
                }
                if (Math.Abs(s2) <= eps / 15 || Math.Abs(s1) <= eps / 15)
                {
                    if (step <= 1e+100)
                    {
                        C2++;
                        step *= 2;
                    }
                }

                double e1 = 16 * s1;
                double e2 = 16 * s2;

                // next.U1 = next.U1 + e2;
                // next.U2 = next.U2 + e1;


                param = e1;
            }
            point = next;

            return point;
        }
    }
}
