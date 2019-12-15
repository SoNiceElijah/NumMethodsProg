using core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace nm
{
    public partial class MainForm : Form
    {
        DotForm info;

        public MainForm()
        {
            InitializeComponent();

            this.Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mainChart.ChartAreas[0].AxisX.Title = "x";
            mainChart.ChartAreas[0].AxisY.Title = "u";

            mainChart.ChartAreas[0].AxisX.Maximum = 1;
            mainChart.ChartAreas[0].AxisX.Minimum = 0;

            mainChart.Legends.Clear();
            mainChart.Series.Clear();

            // Добавляем новый график
            var ser = mainChart.Series.Add("Diff");
            ser.ChartType = SeriesChartType.Line;
            ser.Color = Color.FromArgb(255, Color.Red);
            ser.BorderWidth = 1;
            ser.Name = "Численное решение";

            var real = mainChart.Series.Add("Real");
            real.ChartType = SeriesChartType.Line;
            real.Color = Color.FromArgb(200, Color.Blue);
            real.BorderWidth = 1;
            real.Name = "Точное решение";

            chart2.Series.Clear();
            chart2.Legends.Clear();

            var sh = chart2.Series.Add("h");
            sh.ChartType = SeriesChartType.Line;
            sh.Color = Color.FromArgb(255, Color.Green);

            ser.Points.AddXY(0, 0);
            ser.Points.AddXY(1, 1);


            sh.Points.AddXY(0, 0);
            sh.Points.AddXY(1, 1);

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 1;

            chart2.ChartAreas[0].AxisX.Title = "x";
            chart2.ChartAreas[0].AxisY.Title = "eps";

        }

        private void rButton1_Click(object sender, EventArgs e)
        {
            double epsMax = -1;
            double kMax = -1;

            int num;
            if (!int.TryParse(textBox1.Text, out num))
                return;

            info = new DotForm();
            Method m = new Method(
                (x) => 1,
                (x) => 1,
                (x) => 1,
                (x) => 1.0/2.0,
                (x) => (Math.PI * Math.PI / 16.0),
                (x) => (Math.Sqrt(2.0)/2.0),
                Math.PI / 4.0,
                1.0, 0.0, num
                );

            double[] v = m.Count();

            double xi = 0;

            double h = 1.0 / num;

            chart2.Series[0].Points.Clear();
            mainChart.Series[0].Points.Clear();

            info.Data = new List<string[]>();
            for (int i = 0; i <= num; ++i)
            {
                double ui = UFunc(xi);
                double eps = (v[i] - ui);

                string[] data = { i + "" ,xi + "", ui + "", v[i] + "", eps + "" };
                info.Data.Add(data);

                if (Math.Abs(eps) > epsMax)
                {
                    epsMax = Math.Abs(eps);
                    kMax = i;
                }

                xi += h;
            }

            int control = 1;
            if (num > 1000)
            {
                control = num / 1000;
                num = 1000;
            }

            xi = 0;
            h = 1.0 / num;
            for (int i = 0; i <= num; ++i)
            {
                double ui = UFunc(xi);
                double eps = (v[control*i] - ui);

                mainChart.Series[0].Points.AddXY(xi, v[control * i]);
                chart2.Series[0].Points.AddXY(xi, Math.Abs(eps));

                xi += h;
            }

            if (num > 1000)
                mainChart.Series[0].Points.AddXY(1, v[v.Length-1]);

            info.param.Text = $"Полученная точность решения: {epsMax}, на {kMax} шаге";

            info.Show();

        }


        bool switcher = true;
        private void dButton1_Click(object sender, EventArgs e)
        {
            mainChart.Series[1].Points.Clear();

            if (switcher)
            {

                dButton1.Text = "Убрать";

                double h = 1.0 / 1000;
                for (double xi = 0; xi <= 1; xi += h)
                {
                    mainChart.Series[1].Points.AddXY(xi, UFunc(xi));
                }

                switcher = false;
            }
            else
            {
                dButton1.Text = "Решение";
                switcher = true;
            }

        }

        private void rButton2_Click(object sender, EventArgs e)
        {
            double epsMax = -1;
            double kMax = -1;

            mainChart.Series[1].Points.Clear();

            int num;
            if (!int.TryParse(textBox2.Text, out num))
                return;

            info = new DotForm();
            Method m = new Method(
                (x) => Math.Sqrt(2)*Math.Sin(x),
                (x) => 1,
                (x) => Math.Sin(2*x),
                (x) => Math.Cos(x)*Math.Cos(x),
                (x) => x*x,
                (x) => Math.Cos(x),
                Math.PI / 4,
                1, 0, num
                );

            Method m2 = new Method(
               (x) => Math.Sqrt(2) * Math.Sin(x),
               (x) => 1,
               (x) => Math.Sin(2 * x),
               (x) => Math.Cos(x) * Math.Cos(x),
               (x) => x * x,
               (x) => Math.Cos(x),
               Math.PI / 4,
               1, 0, num * 2
               );

            double[] v = m.Count();
            double[] u = m2.Count();

            double xi = 0;

            double h = 1.0 / num;

            chart2.Series[0].Points.Clear();
            mainChart.Series[0].Points.Clear();
            info.Data = new List<string[]>();
            for (int i = 0; i <= num; ++i)
            {
                double eps = (v[i] - u[2*i]);

                string[] data = { i + "", xi + "", u[2*i] + "", v[i] + "", eps + "" };
                info.Data.Add(data);

                if (Math.Abs(eps) > epsMax)
                {
                    epsMax = Math.Abs(eps);
                    kMax = i;
                }

                xi += h;
            }

            int control = 1;
            if (num > 1000)
            {
                control = num / 1000;
                num = 1000;
            }

            xi = 0;
            h = 1.0 / num;
            for (int i = 0; i <= num; ++i)
            {
                double eps = (v[control * i] - u[2*control*i]);

                mainChart.Series[0].Points.AddXY(xi, v[control * i]);
                chart2.Series[0].Points.AddXY(xi, Math.Abs(eps));

                xi += h;
            }

            if (num > 1000)
                mainChart.Series[0].Points.AddXY(1, v[v.Length - 1]);

            info.param.Text = $"Полученная точность решения: {epsMax}, на {kMax} шаге";

            info.Show();
        }

        double UFunc(double xi)
        {
            double c1 = -0.3393175; //0.3393175654523851‬
            double c2 = 0.3393175; //0.3393175654523851‬
            double c3 = -0.492041640; //0.4920416408816548
            double c4 = 1.05607925; //1.056079255354578

            if (xi < Math.PI/4)
            {
                return c1* Math.Exp(xi) + c2 * Math.Exp(-xi) + 1;
            }
            else
            {
                return c3 * Math.Exp(Math.PI / Math.Sqrt(8) * xi) + c4 * Math.Exp(-Math.PI / Math.Sqrt(8) * xi) + 1.146318;
            }
        }
    }
  
}
