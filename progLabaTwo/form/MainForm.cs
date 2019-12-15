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
            for(int i = 0; i <= num; ++i)
            {
                double ui = UFunc(xi);
                double eps = (v[i] - ui);

                mainChart.Series[0].Points.AddXY(xi, v[i]);
                chart2.Series[0].Points.AddXY(xi, Math.Abs(eps));

                info.dataGridView1.Rows.Add(xi,ui,v[i],eps);

                if (eps > epsMax)
                    epsMax = eps;

                xi += h;
            }

            info.param.Text = $"Полученная точность решения: {epsMax}";

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
            mainChart.Series[1].Points.Clear();

            int num;
            if (!int.TryParse(textBox2.Text, out num))
                return;

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
            for (int i = 0; i <= num; ++i)
            {
                mainChart.Series[0].Points.AddXY(xi, v[i]);
                chart2.Series[0].Points.AddXY(xi, Math.Abs(v[i] - u[2*i]));

                xi += h;
            }
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
