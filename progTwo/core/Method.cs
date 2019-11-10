using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class Dot
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

        Dot point;
        double step;
        double eps;
        public double Step { get => step; }
        
        /// <summary>
        /// Создает объект для работы с методом РК(4)
        /// </summary>
        /// <param name="f">Функция двух пременных</param>
        /// <param name="x0">Точка x0</param>
        /// <param name="u0">Точка u0</param>
        /// <param name="s">Первоначальный шаг</param>
        public Method(Func f1, Func f2, double x0, double u10, double u20, double s, double e = 1e-7)
        {
            Function1 = f1;
            Function2 = f2;

            point = new Dot()
            {
                X = x0,
                U1 = u10,
                U2 = u20
            };

            step = s;
            eps = e;
        }

        public Dot nextStep(out double upV, out double len)
        {
            Dot next = new Dot();
            double h = step;

            next.X = point.X + h;

            double k1 = Function1.Invoke(point.X, point.U1, point.U2);
            double k2 = Function2.Invoke(point.X, point.U1, point.U2);

            next.U1 = point.U1 + h / 2 * (k1 + Function1.Invoke(point.X + h, point.U1 + h * k1, point.U2 + h * k2));
            next.U2 = point.U2 + h / 2 * (k2 + Function2.Invoke(point.X + h, point.U1 + h * k1, point.U2 + h * k2));

            Dot half = new Dot();
            h = h / 2;

            half.X = point.X + h;

            k1 = Function1.Invoke(point.X, point.U1, point.U2);
            k2 = Function2.Invoke(point.X, point.U1, point.U2);

            half.U1 = point.U1 + h / 2 * (k1 + Function1.Invoke(point.X + h, point.U1 + h * k1, point.U2 + h * k2));
            half.U2 = point.U2 + h / 2 * (k2 + Function2.Invoke(point.X + h, point.U1 + h * k1, point.U2 + h * k2));

            Dot mes = new Dot();

            mes.X = half.X + h;

            k1 = Function1.Invoke(half.X, half.U1, half.U2);
            k2 = Function2.Invoke(half.X, half.U1, half.U2);

            mes.U1 = half.U1 + h / 2 * (k1 + Function1.Invoke(half.X + h, half.U1 + h * k1, half.U2 + h * k2));
            mes.U2 = half.U2 + h / 2 * (k2 + Function2.Invoke(half.X + h, half.U1 + h * k1, half.U2 + h * k2));

            upV = mes.U2;
            len = Math.Sqrt(step * step + (point.U2 - next.U2) * (point.U2 - next.U2));

            double s = (mes.U2 - next.U2) / (3);
            if (Math.Abs(s) >= eps)
            {
                step /= 2;
                return nextStep(out upV, out len);
            }
            if(Math.Abs(s) <= eps/3)
            {
                if (step <= 1e+100)
                    step *= 2;
            }

            double e = 3 * s;

            next.U2 = next.U2 + e;
            point = next;

            return point;
        }

    }
}
