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
        public List<string[]> Data { get; set; }
        int stop = 1000;
        int page = 0;

        public DotForm()
        {
            InitializeComponent();


            this.dataGridView1.Columns.Add("i", "Номер узла");
            this.dataGridView1.Columns.Add("Xi", "Xi");
            this.dataGridView1.Columns.Add("Ui", "Ui");
            this.dataGridView1.Columns.Add("Vi", "Vi");
            this.dataGridView1.Columns.Add("Ui - Vi", "Ui - Vi");

            this.Load += DotForm_Load;
        }

        private void DotForm_Load(object sender, EventArgs e)
        {
            Draw();
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();

            Clipboard.SetText(dataObj.GetText());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((page + 1) * stop < Data.Count)
                page++;

            Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!(page -1  < 0))
                page--;


            Draw();

        }

        void Draw()
        {
            dataGridView1.Rows.Clear();
            for (int i = page * stop; i < (page + 1) * stop && i < Data.Count; ++i)
            {
                dataGridView1.Rows.Add(Data[i][0], Data[i][1], Data[i][2], Data[i][3], Data[i][4]);
            }
        }
    }
}
