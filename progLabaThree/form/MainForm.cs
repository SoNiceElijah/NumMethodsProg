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
using System.IO;

namespace nm
{
    public partial class MainForm : Form
    {
        DotForm info;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox3.Text);
            int m = Convert.ToInt32(textBox4.Text);

            double eps = Convert.ToDouble(textBox2.Text.Replace('.', ','));
            double max = Convert.ToInt32(textBox1.Text);


            List<Method.Func> param1 = new List<Method.Func>();

            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);


            double[] param2 = { -1, 1, -1, 1 };

            Method meth = new Method(
                (x,y) => 4,
                param1.ToArray(),
                param2,
                n,m
            );


            var result = meth.Calculate(eps, max, out double num, out double diff);

            Console.WriteLine(num + " " + diff);

            info = new DotForm(param2,n, m, meth.Result);
            info.Info.Text = $"Эпсилон = {diff}, Количество итераций = {num} ";

            info.Show();

            if(checkBox1.Checked)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "txt";
                sfd.Filter = "Text files (*.txt)|*.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        double h = (param2[1] - param2[0]) / n;
                        double k = (param2[3] - param2[2]) / m;
                        for (int i = 0; i < n + 1; ++i)
                            for(int j = 0; j < m + 1; ++j)
                            {
                                sw.Write($"{(param2[0] + i*h).ToString().Replace(',', '.')},{(param2[2] + j*k).ToString().Replace(',', '.')},{(meth.Result[i,j]).ToString().Replace(',', '.')}\n");
                            }
                    }

                    Method.Func example = (x, y) => 1 - x * x - y * y;
                    using (StreamWriter sw = new StreamWriter(sfd.FileName + ".empl"))
                    {
                        double h = (param2[1] - param2[0]) / n;
                        double k = (param2[3] - param2[2]) / m;
                        for (int i = 0; i < n + 1; ++i)
                            for (int j = 0; j < m + 1; ++j)
                            {
                                sw.Write($"{(param2[0] + i * h).ToString().Replace(',', '.')},{(param2[2] + j * k).ToString().Replace(',', '.')},{(example(param2[0] + i * h, param2[2] + j * k)).ToString().Replace(',', '.')}\n");
                            }
                    }
                }
            }

        }
    }
  
}
