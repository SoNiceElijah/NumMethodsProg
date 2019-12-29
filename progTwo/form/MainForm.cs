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
                double l = Convert.ToDouble(lTextBox1.Text.Replace('.', ','));
                double h = Convert.ToDouble(hTextBox1.Text.Replace('.', ','));
                int n = Convert.ToInt32(nTextBox1.Text.Replace('.', ','));
                double eps = Convert.ToDouble(epsTextBox1.Text.Replace('.', ','));

                double el = Convert.ToDouble(elTextBox1.Text.Replace('.', ','));
                double pp = Convert.ToDouble(pTextBox1.Text.Replace('.', ','));

                bool ctrl = !checkBox1.Checked;

                Method m = new Method(
                (x, u1, u2) => ((1 / l) + -x * (1 / (l * l))) * pp * l * l / (el),
                (x, u1, u2) => u1,
                0, 0, 0, h, eps, ctrl);

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

                int count = 0;
                MDot p = null;

                double maxOLP = 0;
                double sum = 0;

                MDot prev = null;
                foreach (var i in Enumerable.Range(0, n))
                {
                    double step = m.Step;
                    p = m.nextStep(out double contr, out double olp, out double len);
                    if (Math.Abs(p.U2) < 1e-20)
                        p.U2 = 0;
                    if (Math.Abs(p.U2) > 10e+20)
                        break;
                    if (Math.Abs(p.X) < 1e-20)
                        p.X = 0;
                    if (Math.Abs(p.X) > 10e+20)
                        break;

                    sum += len;

                    bool needToBreak = false;
                    if (checkBox2.Checked)
                    {
                        if (sum > l)
                        {
                            var diff = sum - l;
                            var proc = diff / len;

                            var cStep = step * (proc);
                            var up = (p.U2 - prev.U2) * (proc);

                            p.X -= cStep;
                            p.U2 -= up;

                            needToBreak = true; ;
                        }
                    }

                    mainChart.Invoke(new Action(() =>
                    {
                        mainChart.Series[chartName].Points.AddXY(p.X, p.U2);
                        //chart1.Series["h"].Points.AddXY(i, step);
                    }));

                    if (!checkBox2.Checked)
                    {
                        if (p.X > 5)
                            break;
                    }
                    else if (needToBreak)
                        break;

                    Console.WriteLine(p.X + " " + p.U2);

                    mainChart.Invoke(new Action(() =>
                    {
                        info.dataGridView1.Rows.Add(i + "", p.U2, contr, olp, step, m.C1, m.C2);
                    }));

                    if (minDot > p.U2)
                        minDot = p.U2;

                    if (maxDot < p.U2)
                        maxDot = p.U2;

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

                    count++;
                    prev = p;

                }

                mainChart.Invoke(new Action(() =>
                {
                    correctAxis(10, minDot - 0.01, maxDot + 0.01);
                    info.param.Text = $"Номер шага = : {count},\nМаксимальная оценка локальной погрешности (ОЛП): {maxOLP}, \nКоличество делений шага: {m.C1}, \nКоличество удвоений шага: {m.C2}, \nМаксимальный шаг: {maxStep}, достигнут в точке: {mxs}, \nМинимальный шаг: {minStep} , достигнут в точке: {mns}";

                    info.Show();

                    sRun = false;
                    rButton1.Text = "запуск";
                }));

                
            });

            super.Start();
        }

        bool isUp = false;

        private void button1_Click(object sender, EventArgs e)
        {
            correctAxis(10, minDot - 0.01, maxDot + 0.01);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainChart.Series.Clear();
            chartCount = 0;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
  
}
