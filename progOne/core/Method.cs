﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class Dot
    {
        public double X { get; set; }      
        public double Y { get; set; }      
    }

    public class Method
    {
        public delegate double Func(double x, double u);

        public Func Function { get; private set; }
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
        public Method(Func f, double x0, double u0, double s, double e = 1e-7)
        {
            Function = f;
            point = new Dot()
            {
                X = x0,
                Y = u0
            };

            step = s;
            eps = e;
        }

        public Dot nextStep(out double upV)
        {
            Dot next = new Dot();
            double h = step;

            double k1 = Function.Invoke(point.X, point.Y);
            double k2 = Function.Invoke(point.X + h / 2, point.Y + h / 2 * k1);
            double k3 = Function.Invoke(point.X + h / 2, point.Y + h / 2 * k2);
            double k4 = Function.Invoke(point.X + h, point.Y + h * k3);

            next.Y = point.Y + step / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
            next.X = point.X + step;

            Dot half = new Dot();
            h = h / 2;

            k1 = Function.Invoke(point.X, point.Y);
            k2 = Function.Invoke(point.X + h / 2, point.Y + h / 2 * k1);
            k3 = Function.Invoke(point.X + h / 2, point.Y + h / 2 * k2);
            k4 = Function.Invoke(point.X + h, point.Y + h * k3);

            half.Y = point.Y + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
            half.X = point.X + h;

            Dot mes = new Dot();

            k1 = Function.Invoke(half.X, half.Y);
            k2 = Function.Invoke(half.X + h / 2, half.Y + h / 2 * k1);
            k3 = Function.Invoke(half.X + h / 2, half.Y + h / 2 * k2);
            k4 = Function.Invoke(half.X + h, half.Y + h * k3);

            mes.Y = half.Y + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
            mes.X = half.X + h;

            upV = mes.Y;

            double s = (mes.Y - next.Y) / (15);
            if (Math.Abs(s) >= eps)
            {
                step /= 2;
                return nextStep(out upV);
            }
            if (Math.Abs(s) <= eps / 16)
            {
                if (step <= 1e+100)
                    step *= 2;

                //return nextStep(out upV);
            }


            double e = 16 * s;

            next.Y = next.Y - e;
            point = next;
            return point;
        }

    }
}