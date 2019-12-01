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
        int nums = 0;
        DotForm info;

        double minDot;
        double maxDot;

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
            real.Color = Color.FromArgb(255, Color.Blue);
            real.BorderWidth = 2;
            real.Name = "Точное решение";

            chart1.Series.Clear();
            chart1.Legends.Clear();

            var sh = chart1.Series.Add("h");
            sh.ChartType = SeriesChartType.Line;

            ser.Points.AddXY(0, 0);
            ser.Points.AddXY(10, 10);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Шаги";

        }

        private void rButton1_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(textBox1.Text, out num))
                return;

            Method m = new Method(
                (x) => 1,
                (x) => 1,
                (x) => 1,
                (x) => 1/2,
                (x) => Math.PI * Math.PI / 16,
                (x) => Math.Sqrt(2)/2,
                Math.PI / 4,
                1, 0, num
                );

            double[] u = m.Count();

            double xi = 0;

            double h = 1.0 / num;

            mainChart.Series[0].Points.Clear();
            for(int i = 0; i <= num; ++i)
            { 
                mainChart.Series[0].Points.AddXY(xi, u[i]);
                xi += h;
            }
        }

        private void dButton1_Click(object sender, EventArgs e)
        {
            mainChart.Series[1].Points.Clear();

            double h = 1.0 / 1000;
            for(double xi = 0; xi < 0.785; xi += h)
            {
                double y = -Math.Exp(xi) + Math.Exp(-xi) + 1;
                mainChart.Series[1].Points.AddXY(xi, y);
            }

            for (double xi = 0.785; xi <= 1; xi += h)
            {
                double y = Math.Exp(Math.PI / Math.Sqrt(8) * xi) + Math.Exp(-Math.PI / Math.Sqrt(8)*xi) + Math.Sqrt(2) * 8 / (Math.PI * Math.PI);
                mainChart.Series[1].Points.AddXY(xi, y);
            }

        }

        private void rButton2_Click(object sender, EventArgs e)
        {
            int num;
            if (!int.TryParse(textBox2.Text, out num))
                return;

            Method m = new Method(
                (x) => Math.Sqrt(2) * Math.Sin(x),
                (x) => 1,
                (x) => Math.Sin(2 * x),
                (x) => Math.Cos(x) * Math.Cos(x),
                (x) => x * x,
                (x) => Math.Cos(x),
                Math.PI / 4,
                1, 0, num
                );

            double[] u = m.Count();

            double xi = 0;

            double h = 1.0 / num;

            mainChart.Series[0].Points.Clear();
            for (int i = 0; i <= num; ++i)
            {
                mainChart.Series[0].Points.AddXY(xi, u[i]);
                xi += h;
            }
        }

       
    }
  
}
