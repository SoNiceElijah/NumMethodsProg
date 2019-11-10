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

        public MainForm()
        {
            InitializeComponent();

            this.Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mainChart.ChartAreas[0].AxisX.Minimum = 0;
            mainChart.ChartAreas[0].AxisX.Maximum = 5;

            mainChart.ChartAreas[0].AxisX.Title = "x (длина)";

            mainChart.ChartAreas[0].AxisY.Minimum = -5;
            mainChart.ChartAreas[0].AxisY.Maximum = 5;

            mainChart.ChartAreas[0].AxisY.Title = "u (изгиб)";

            mainChart.Legends.Clear();
            mainChart.Series.Clear();
            // Добавляем новый график
            var ser = mainChart.Series.Add("Diff");
            ser.ChartType = SeriesChartType.Line;
            ser.Color = Color.FromArgb(255, Color.Red);
            ser.BorderWidth = 1;

            var real = mainChart.Series.Add("Real");
            real.ChartType = SeriesChartType.Line;
            real.Color = Color.FromArgb(100,Color.Blue);
            real.BorderWidth = 2;

            chart1.Series.Clear();
            chart1.Legends.Clear();

            var sh = chart1.Series.Add("h");
            sh.ChartType = SeriesChartType.Line;

            ser.Points.AddXY(0, 0);
            ser.Points.AddXY(10, 10);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Шаги";

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            double l = Convert.ToDouble(lTextBox.Text.Replace('.',','));
            double p = Convert.ToDouble(pTextBox.Text.Replace('.', ','));
            double ei = Convert.ToDouble(eiTextBox.Text.Replace('.', ','));
            double x0 = 0;
            double h = Convert.ToDouble(hTextBox.Text.Replace('.', ','));

            Method m = new Method(                
                (x, u1, u2) => ((1 / l) + -x * (1 / (l * l))) * p * l * l / (ei),
                (x, u1, u2) => u1,
                0 ,0 , 0, h);


            mainChart.Series["Diff"].Points.Clear();
            mainChart.Series["Real"].Points.Clear();
            chart1.Series["h"].Points.Clear();

            ++nums;
            info = new DotForm();
            info.label1.Text = "Запуск номер " + nums;
            info.param.Text = $"L: {l}, P: {p}, EI: {ei}, H: {h}";

            int i = 0;
            double sum = 0;
            Dot d = null;
            while (sum < l)
            {
                double step = m.Step;
                d = m.nextStep(out double contr, out double len);
                if (Math.Abs(d.U2) < 1e-8)
                    d.U2 = 0;
                if (Math.Abs(d.U2) > 10e+20)
                    break;
                if (Math.Abs(d.X) < 1e-8)
                    d.X = 0;
                if (Math.Abs(d.X) > 10e+20)
                    break;
                mainChart.Series["Diff"].Points.AddXY(d.X,d.U2);
                chart1.Series["h"].Points.AddXY(i,step);

                Console.WriteLine(d.X + " " +d.U2);

                info.dataGridView1.Rows.Add(i + "",d.X, step, d.U2, contr, d.U2 - contr);
                ++i;
                sum += len;
            }

            mainChart.Series["Real"].Points.AddXY(d.X, d.U2);
            mainChart.Series["Real"].Points.AddXY(d.X, d.U2 + p / 2);
            mainChart.Series["Real"].Points.AddXY(d.X + 0.05, d.U2 + p / 2 - (p / 2) / Math.Abs(p / 2) * 0.1);
            mainChart.Series["Real"].Points.AddXY(d.X - 0.05, d.U2 + p / 2 - (p / 2) / Math.Abs(p / 2) * 0.1);
            mainChart.Series["Real"].Points.AddXY(d.X, d.U2 + p / 2);


            info.Show();

            // foreach (var x in Enumerable.Range(0, 1000).Select(u => (double)u / 100))
            // {
            //     var y = (v / r) * (1 - Math.Exp((-r / l) * x)) + i0 * Math.Exp((-r / l) * x);
            //     mainChart.Series["Real"].Points.AddXY(x, y);
            // }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    double l = Convert.ToDouble(lTextBox.Text.Replace('.', ','));
        //    double r = Convert.ToDouble(pTextBox.Text.Replace('.', ','));
        //    double v = Convert.ToDouble(eiTextBox.Text.Replace('.', ','));
        //    double i0 = 0;
        //    double h = Convert.ToDouble(hTextBox.Text.Replace('.', ','));
        //    int n = Convert.ToInt32(nTextBox.Text.Replace('.', ','));

        //    mainChart.Series["Real"].Points.Clear();

        //    foreach (var x in Enumerable.Range(0, 1000).Select(u => (double)u / 100))
        //    {
        //       var y = (v / r) * (1 - Math.Exp((-r / l) * x)) + i0 * Math.Exp((-r / l) * x);
        //       mainChart.Series["Real"].Points.AddXY(x, y);
        //    }
        //}
    }
  
}
