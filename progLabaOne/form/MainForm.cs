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

        private void correctAxis(double x, double miny, double maxy)
        {
            try
            {
                var tmp = !string.IsNullOrWhiteSpace(minXTB.Text) ? Convert.ToDouble(minXTB.Text.Replace('.', ',')) : 0;
                mainChart.ChartAreas[0].AxisX.Minimum = tmp;

                tmp = !string.IsNullOrWhiteSpace(maxXTB.Text) ? Convert.ToDouble(maxXTB.Text.Replace('.', ',')) : x;
                mainChart.ChartAreas[0].AxisX.Maximum = tmp;

                tmp = !string.IsNullOrWhiteSpace(minYTB.Text) ? Convert.ToDouble(minYTB.Text.Replace('.', ',')) : miny;
                mainChart.ChartAreas[0].AxisY.Minimum = tmp;

                tmp = !string.IsNullOrWhiteSpace(maxYTB.Text) ? Convert.ToDouble(maxYTB.Text.Replace('.', ',')) : maxy;
                mainChart.ChartAreas[0].AxisY.Maximum = tmp;
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
            real.Color = Color.FromArgb(255,Color.Blue);
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

        Thread super;
        bool sRun = false;

        private void rButton1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[1].Enabled = false;
            tabControl1.TabPages[2].Enabled = false;

            if (sRun)
            {
                sRun = false;
                super.Abort();
                rButton1.Text = "запуск";

                tabControl1.TabPages[1].Enabled = true;
                tabControl1.TabPages[2].Enabled = true;

                return;
            }

            sRun = true;
            rButton1.Text = "cтоп";

            super = new Thread(() =>
            {
                double u0 = Convert.ToDouble(u0TextBox1.Text.Replace('.', ','));
                double h = Convert.ToDouble(hTextBox1.Text.Replace('.', ','));
                int n = Convert.ToInt32(nTextBox1.Text.Replace('.', ','));
                double eps = Convert.ToDouble(epsTextBox1.Text.Replace('.', ','));
                double rb = Convert.ToDouble(rbTextBox1.Text.Replace('.', ','));

                bool ctrl = !checkBox1.Checked;

                Method m = new Method((x, u) => (-1) * 5 / 2 * u, 0, u0, h, eps, ctrl);

                mainChart.Invoke(new Action(() => {
                    mainChart.Series["Численное решение"].Points.Clear();
                    mainChart.Series["Точное решение"].Points.Clear();


                    chart1.Series["h"].Points.Clear();
                }));

                ++nums;
                info = new DotForm();
                info.label1.Text = "Запуск номер" + nums + "; Метод 2";
                info.param.Text = $"U(0): {u0}, H: {h}, N: {n}, EPS: {eps}, Ctrl: {ctrl}";


                minDot = double.MaxValue;
                maxDot = double.MinValue;

                foreach (var i in Enumerable.Range(0, n))
                {
                    double step = m.Step;
                    Dot p = m.nextStep(out double contr);
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
                        mainChart.Series["Численное решение"].Points.AddXY(p.X, p.Y);
                        chart1.Series["h"].Points.AddXY(i, step);
                    }));

                    Console.WriteLine(p.X + " " + p.Y);

                    mainChart.Invoke(new Action(() =>
                    {
                        info.dataGridView1.Rows.Add(i + "", p.X, step, p.Y, contr, p.Y - contr);
                    }));

                    if (minDot > p.Y)
                        minDot = p.Y;

                    if (maxDot < p.Y)
                        maxDot = p.Y;

                    if (p.X > rb)
                        break;

                }

                mainChart.Invoke(new Action(() =>
                {
                    correctAxis(rb, minDot - 0.01, maxDot + 0.01);

                    info.Show();

                    sRun = false;
                    rButton1.Text = "запуск";

                    tabControl1.TabPages[1].Enabled = true;
                    tabControl1.TabPages[2].Enabled = true;
                }));

                
            });

            super.Start();
        }

        private void dButton1_Click(object sender, EventArgs e)
        {
            double u0 = Convert.ToDouble(u0TextBox1.Text.Replace('.', ','));
            double rb = Convert.ToDouble(rbTextBox1.Text.Replace(',', ','));

            mainChart.Series["Точное решение"].Points.Clear();

            foreach (var x in Enumerable.Range(0, Convert.ToInt32(rb) * 100).Select(u => (double)u / 100))
            {
               var y = u0 * Math.Exp(-5/2*x);
               mainChart.Series["Точное решение"].Points.AddXY(x, y);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void rButton2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Enabled = false;
            tabControl1.TabPages[2].Enabled = false;

            if (sRun)
            {
                sRun = false;
                super.Abort();
                rButton2.Text = "запуск";

                tabControl1.TabPages[0].Enabled = true;
                tabControl1.TabPages[2].Enabled = true;

                return;
            }

            sRun = true;
            rButton2.Text = "cтоп";

            super = new Thread(() =>
            {
                double u0 = Convert.ToDouble(u0TextBox2.Text.Replace('.', ','));
                double h = Convert.ToDouble(hTextBox2.Text.Replace('.', ','));
                int n = Convert.ToInt32(nTextBox2.Text.Replace('.', ','));
                double eps = Convert.ToDouble(epsTextBox2.Text.Replace('.', ','));
                double rb = Convert.ToDouble(rbTextBox2.Text.Replace('.', ','));

                bool ctrl = !checkBox2.Checked;

                Method m = new Method((x, u) => ((Math.Log(x + 1)) / (x * x + 1)) * u * u + u - u * u * u * Math.Sin(10 * x), 0, u0, h, eps, ctrl);


                mainChart.Invoke(new Action(() =>
                {
                    mainChart.Series["Численное решение"].Points.Clear();
                    mainChart.Series["Точное решение"].Points.Clear();


                    chart1.Series["h"].Points.Clear();
                }));

                ++nums;
                info = new DotForm();
                info.label1.Text = "Запуск номер " + nums + "; Метод 2";
                info.param.Text = $"U(0): {u0}, H: {h}, N: {n}, EPS: {eps}, Ctrl: {ctrl}";


                minDot = double.MaxValue;
                maxDot = double.MinValue;

                foreach (var i in Enumerable.Range(0, n))
                {
                    double step = m.Step;
                    Dot p = m.nextStep(out double contr);
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
                        mainChart.Series["Численное решение"].Points.AddXY(p.X, p.Y);
                        chart1.Series["h"].Points.AddXY(i, step);
                    }));

                    Console.WriteLine(p.X + " " + p.Y);

                    mainChart.Invoke(new Action(() =>
                    {
                        info.dataGridView1.Rows.Add(i + "", p.X, step, p.Y, contr, p.Y - contr);
                    }));

                    if (minDot > p.Y)
                        minDot = p.Y;

                    if (maxDot < p.Y)
                        maxDot = p.Y;

                    if (p.X > rb)
                        break;

                }

                mainChart.Invoke(new Action(() =>
                {
                    correctAxis(rb, minDot - 0.01, maxDot + 0.01);

                    info.Show();

                    sRun = false;
                    rButton2.Text = "запуск";

                    tabControl1.TabPages[0].Enabled = true;
                    tabControl1.TabPages[2].Enabled = true;
                }));
            });

            super.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double rb = Convert.ToDouble(rbTextBox2.Text.Replace('.', ','));

            correctAxis(rb, minDot - 0.01, maxDot + 0.01);
        }
    }
  
}
