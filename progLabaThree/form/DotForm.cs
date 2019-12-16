using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nm
{
    public partial class DotForm : Form
    {
        public DotForm()
        {
            InitializeComponent();
        }



        public DotForm(double[] borders, int n, int m, double[] data)
        {
            InitializeComponent();

            int w = 50;
            int s = 30;

            double h = (borders[1] - borders[0]) / n;
            double k = (borders[3] - borders[2]) / m;


            var drop = new Label()
            {
                Location = new Point(0,0),
                Size = new Size(w, s),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "",
                BorderStyle = BorderStyle.FixedSingle,

            };

            panel1.Controls.Add(drop);


            for (int i = 0; i < m; ++i)
            {
                var l = new Label()
                {
                    Location = new Point((i + 1) * w, 0),
                    Size = new Size(w, s),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = borders[0] + i * h + "",
                    BorderStyle = BorderStyle.FixedSingle,
               
                };

                panel1.Controls.Add(l);
            }

            for (int j = 0; j < m; ++j)
            {
                var l = new Label()
                {
                    Location = new Point(0, (j+1) * s),
                    Size = new Size(w, s),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = borders[2] + j * k + "",
                    BorderStyle = BorderStyle.FixedSingle,

                };

                panel1.Controls.Add(l);
            }

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j) {
                    var l = new TextBox()
                    {
                        Location = new Point((i + 1) * w, (j+1)* s),
                        Size = new Size(w, s),
                        AutoSize = false,
                        Text = data[m * j + i] + ""
                    };

                    panel1.Controls.Add(l);

                }

            }

        }
    }
}
