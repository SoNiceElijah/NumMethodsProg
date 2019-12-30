using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{

    public class Method
    {
        public bool Silence { get; set; } = false;

        public delegate double Func(double x, double y);

        public Func F { get; private set; }
        public Func M1 { get; private set; }
        public Func M2 { get; private set; }
        public Func M3 { get; private set; }
        public Func M4 { get; private set; }

        public double[,] Result { get; private set; }

        public double a, b, c, d;

        int m;
        int n;

        /// <summary>
        /// Создает объект для работы с методом РК(4)
        /// </summary>
        /// <param name="f">Функция двух пременных</param>
        /// <param name="x0">Точка x0</param>
        /// <param name="u0">Точка u0</param>
        /// <param name="s">Первоначальный шаг</param>
        public Method(Func f, Func[] Mi, double[] border, int _m, int _n)
        {
            F = f;

            M1 = Mi[0];
            M2 = Mi[1];
            M3 = Mi[2];
            M4 = Mi[3];

            a = border[0];
            b = border[1];
            c = border[2];
            d = border[3];

            n = _n;
            m = _m;
        }

        //Метод, который реализует метод
        public double[] Calculate(double eps, double max, out double num, out double diff, out double Z, out double R)
        {

            num = 0;

            //Искомый вектор
            double[,] v = new double[n + 1, m + 1];


            //Заполняем нулями
            for (int j = 1; j < m; ++j)
                for (int i = 1; i < n; ++i)
                    v[i, j] = 0;

            //Краевые условия

            double k = (d - c) / m;
            for(int j = 0; j <= m; ++j)
            {
                v[0, j] = M1(a, j * k + c);
                v[n, j] = M2(b, j * k + c);
            }

            double h = (b - a) / n;
            for (int i = 0; i <= n; ++i)
            {
                v[i, 0] = M3(i * h + a, c);
                v[i, m] = M4(i * h + a, d);
            }

            double k2 = -1.0 / (k * k);
            double h2 = -1.0 / (h * h);
            double a2 = -2.0 * (h2 + k2);

            double mEps = 100;
            while (num < max && mEps > eps) {

                mEps = 0;
                for (int j = 1; j < m; ++j)
                {
                    for (int i = 1; i < n; ++i)
                    {
                        double tmp = v[i, j];

                        v[i, j] = - (h2 * (v[i + 1, j] + v[i - 1, j]) + k2 * (v[i, j + 1] + v[i, j - 1]));
                        v[i, j] = v[i, j] + F(a + i * h, c + j * k);
                        v[i, j] = v[i, j] / a2;

                        double cEps = Math.Abs(v[i, j] - tmp);
                        if (mEps < cEps)
                            mEps = cEps;
                    }
                }

                num++;

                if(!Silence)
                {
                    Console.WriteLine("\nNum: " + num);
                    for (int j = 1; j < m; ++j)
                    {
                        for (int i = 1; i < n; ++i)
                            Console.Write(v[i, j] + "\t");

                        Console.Write('\n');
                    }
                }
            }

            diff = mEps;

            double mZ = 0;
            for (int i = 0; i < n + 1; ++i)
            {
                for (int j = 0; j < m + 1; ++j)
                {
                    double z = Math.Abs(M1(a + i * h, c + j * k) - v[i, j]);
                    if (mZ < z)
                        mZ = z;
                }
            }

            Z = mZ;

            double mR = 0;
            for (int i = 0; i < n + 1; ++i)
            {
                for (int j = 0; j < m + 1; ++j)
                {
                    double r = -(h2 * (M1(a + (i + 1) * (h), c + j * k) + (M1(a + (i - 1) * (h), c + j * k))) + k2 * (M1(a + i * h, c + (j + 1) * k) + M1(a + i * h, c + (j - 1) * k)));
                    r = r + F(a + i * h, c + j * k);
                    r = r - a2 * M1(a + i * h, c + (j) * k);
                    if (mR < Math.Abs(r))
                        mR = Math.Abs(r);
                }
            }

            R = mR;



            Result = v;
            List<double> res = new List<double>();

            for (int j = 1; j < m; ++j)
                for (int i = 1; i < n; ++i)
                    res.Add(v[i, j]);


            return res.ToArray();
        }
    }
}
