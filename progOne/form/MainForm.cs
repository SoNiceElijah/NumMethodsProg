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

        int chartCount = 0;

        private double CurFunction(double u0,double v, double r, double l,double x)
        {
            return (v / r) * (1 - Math.Exp((-r / l) * x)) + u0 * Math.Exp((-r / l) * x);
        }

        double minDot;
        double maxDot;

        public MainForm()
        {
            InitializeComponent();

            this.Shown += MainForm_Shown;
        }

        private void correctAxis(double x, double miny, double maxy, double minx = 0)
        {
            try
            {
                var tmp = !string.IsNullOrWhiteSpace(minXTB.Text) ? Convert.ToDouble(minXTB.Text.Replace('.', ',')) : minx;
                mainChart.ChartAreas[0].AxisX.Minimum = tmp;

                tmp = !string.IsNullOrWhiteSpace(maxXTB.Text) ? Convert.ToDouble(maxXTB.Text.Replace('.', ',')) : x;
                mainChart.ChartAreas[0].AxisX.Maximum = tmp;

                if (!string.IsNullOrWhiteSpace(minYTB.Text))
                {
                    mainChart.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(minYTB.Text.Replace('.', ','));
                }

                if (!string.IsNullOrWhiteSpace(minYTB.Text))
                {
                    mainChart.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(maxYTB.Text.Replace('.', ','));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,ex.Message,"Неправильный ввод", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            correctAxis(10,0,5);

            mainChart.ChartAreas[0].AxisX.Title = "x";
            mainChart.ChartAreas[0].AxisY.Title = "u";

            mainChart.Series.Clear();
        }

        Thread super;
        bool sRun = false;

        private void rButton1_Click(object sender, EventArgs e)
        {
            ++chartCount;
            string chartName = "Численное решение №" + chartCount;
            var settings = mainChart.Series.Add(chartName);
            settings.ChartType = SeriesChartType.Line;

            if (sRun)
            {
                sRun = false;
                super.Abort();
                rButton1.Text = "запуск";
                return;
            }

            sRun = true;
            rButton1.Text = "cтоп";

            mainChart.ChartAreas[0].AxisX.Title = "x";
            mainChart.ChartAreas[0].AxisY.Title = "u";

            super = new Thread(() =>
            {
                double u0 = Convert.ToDouble(u0TextBox1.Text.Replace('.', ','));
                double h = Convert.ToDouble(hTextBox1.Text.Replace('.', ','));
                int n = Convert.ToInt32(nTextBox1.Text.Replace('.', ','));
                double eps = Convert.ToDouble(epsTextBox1.Text.Replace('.', ','));
                double rb = Convert.ToDouble(rbTextBox1.Text.Replace('.', ','));

                double r = Convert.ToDouble(rTextBox1.Text.Replace('.', ','));
                double l = Convert.ToDouble(lTextBox1.Text.Replace('.', ','));
                double v = Convert.ToDouble(vTextBox1.Text.Replace('.', ','));

                bool ctrl = !checkBox1.Checked;

                FirstMethod m = new FirstMethod((x, u) => (-r * u / l) + (v/l), 0, u0, h, eps, ctrl);

                mainChart.Invoke(new Action(() => {
                    mainChart.Series[chartName].Points.Clear();
                    //chart1.Series["h"].Points.Clear();
                }));

                ++nums;
                info = new DotForm();
                info.label1.Text = "Запуск номер " + nums + ";";
                


                minDot = double.MaxValue;
                maxDot = double.MinValue;

                double minStep = double.MaxValue;
                double mns = 0;

                double maxStep = double.MinValue;
                double mxs = 0;

                double maxDiff = 0;
                double mxd = 0;

                int count = 0;
                Dot p = null;

                double maxOLP = 0;

                foreach (var i in Enumerable.Range(0, n))
                {
                    double step = m.Step;
                    p = m.nextStep(out double contr, out double olp);
                    if (Math.Abs(p.Y) < 1e-8)
                        p.Y = 0;
                    if (Math.Abs(p.Y) > 10e+20)
                        break;
                    if (Math.Abs(p.X) < 1e-8)
                        p.X = 0;
                    if (Math.Abs(p.X) > 10e+20)
                        break;

                    mainChart.Invoke(new Action(() =>
                    {
                        mainChart.Series[chartName].Points.AddXY(p.X, p.Y);
                        //chart1.Series["h"].Points.AddXY(i, step);
                    }));

                    Console.WriteLine(p.X + " " + p.Y);

                    mainChart.Invoke(new Action(() =>
                    {
                        info.dataGridView1.Rows.Add(i + "", p.Y, contr, olp, step, m.C1, m.C2, CurFunction(u0, v, r, l, p.X), Math.Abs(CurFunction(u0, v, r, l, p.X) - p.Y));
                    }));

                    if (minDot > p.Y)
                        minDot = p.Y;

                    if (maxDot < p.Y)
                        maxDot = p.Y;

                    if (p.X > rb)
                        break;

                    if (maxOLP < Math.Abs(olp))
                        maxOLP = Math.Abs(olp);

                    if (step > maxStep)
                    {
                        maxStep = step;
                        mxs = p.X;
                    }

                    if(step < minStep)
                    {
                        minStep = step;
                        mns = p.X;
                    }

                    if(maxDiff < Math.Abs(CurFunction(u0, v, r, l, p.X) - p.Y))
                    {
                        maxDiff = Math.Abs(CurFunction(u0, v, r, l, p.X) - p.Y);
                        mxd = p.X;
                    }

                    count++;

                }

                mainChart.Invoke(new Action(() =>
                {
                    correctAxis(rb, minDot - 0.01, maxDot + 0.01);
                    info.param.Text = $"Номер шага = : {count}, \nРасстояние до правой границы: {rb - p.X}, \nМаксимальная оценка локальной погрешности (ОЛП): {maxOLP}, \nКоличество делений шага: {m.C1}, \nКоличество удвоений шага: {m.C2}, \nМаксимальный шаг: {maxStep}, достигнут в точке: {mxs}, \nМинимальный шаг: {minStep} , достигнут в точке: {mns}, \nМаксимальное расстояние до функции: {maxDiff}, в точке: {mxd}";

                    info.Show();

                    sRun = false;
                    rButton1.Text = "запуск";
                }));

                
            });

            super.Start();
        }

        bool isUp = false;

        private void dButton1_Click(object sender, EventArgs e)
        {

            double u0 = Convert.ToDouble(u0TextBox1.Text.Replace('.', ','));
            double rb = Convert.ToDouble(rbTextBox1.Text.Replace(',', ','));

            double r = Convert.ToDouble(rTextBox1.Text.Replace('.', ','));
            double l = Convert.ToDouble(lTextBox1.Text.Replace('.', ','));
            double v = Convert.ToDouble(vTextBox1.Text.Replace('.', ','));

            if (!isUp)
            {

                var ser = mainChart.Series.Add("Точное решение");
                ser.ChartType = SeriesChartType.Line;

                foreach (var x in Enumerable.Range(0, Convert.ToInt32(rb) * 100).Select(u => (double)u / 100))
                {
                    var y = (v / r) * (1 - Math.Exp((-r / l) * x)) + u0 * Math.Exp((-r / l) * x);
                    mainChart.Series["Точное решение"].Points.AddXY(x, y);
                }

                isUp = true;
                dButton1.Text = "Убрать";
            }
            else
            {
                var ser = mainChart.Series["Точное решение"];
                mainChart.Series.Remove(ser);

                isUp = false;
                dButton1.Text = "Решение";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double rb = Convert.ToDouble(rbTextBox1.Text.Replace('.', ','));

            correctAxis(rb, minDot - 0.01, maxDot + 0.01);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainChart.Series.Clear();
            chartCount = 0;
        }
    }
  
}
