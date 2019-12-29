namespace nm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.maxYTB = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.minYTB = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.maxXTB = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.minXTB = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.elTextBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pTextBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.epsTextBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lTextBox1 = new System.Windows.Forms.TextBox();
            this.rButton1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.hTextBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nTextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainChart
            // 
            chartArea3.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.mainChart.Legends.Add(legend3);
            this.mainChart.Location = new System.Drawing.Point(12, 287);
            this.mainChart.Name = "mainChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.mainChart.Series.Add(series3);
            this.mainChart.Size = new System.Drawing.Size(700, 365);
            this.mainChart.TabIndex = 12;
            this.mainChart.Text = "chart1";
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(782, 353);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(0, 195);
            this.chart1.TabIndex = 15;
            this.chart1.Text = "chart1";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button1);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.maxYTB);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.minYTB);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.maxXTB);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.minXTB);
            this.groupBox7.Location = new System.Drawing.Point(12, 231);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(700, 50);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Настройки";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(313, 20);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(34, 13);
            this.label26.TabIndex = 7;
            this.label26.Text = "MaxY";
            // 
            // maxYTB
            // 
            this.maxYTB.Location = new System.Drawing.Point(353, 17);
            this.maxYTB.Name = "maxYTB";
            this.maxYTB.Size = new System.Drawing.Size(55, 20);
            this.maxYTB.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(215, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(31, 13);
            this.label25.TabIndex = 5;
            this.label25.Text = "MinY";
            // 
            // minYTB
            // 
            this.minYTB.Location = new System.Drawing.Point(252, 17);
            this.minYTB.Name = "minYTB";
            this.minYTB.Size = new System.Drawing.Size(55, 20);
            this.minYTB.TabIndex = 4;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(109, 20);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(34, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "MaxX";
            // 
            // maxXTB
            // 
            this.maxXTB.Location = new System.Drawing.Point(149, 17);
            this.maxXTB.Name = "maxXTB";
            this.maxXTB.Size = new System.Drawing.Size(55, 20);
            this.maxXTB.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(10, 20);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "MinX";
            // 
            // minXTB
            // 
            this.minXTB.Location = new System.Drawing.Point(47, 17);
            this.minXTB.Name = "minXTB";
            this.minXTB.Size = new System.Drawing.Size(55, 20);
            this.minXTB.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.elTextBox1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.pTextBox1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.epsTextBox1);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lTextBox1);
            this.groupBox4.Controls.Add(this.rButton1);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.hTextBox1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.nTextBox1);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(307, 213);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Параметры";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(126, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 33);
            this.button2.TabIndex = 43;
            this.button2.Text = "Стереть";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "Постоянный шаг";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "El - жесткость";
            // 
            // elTextBox1
            // 
            this.elTextBox1.Location = new System.Drawing.Point(11, 71);
            this.elTextBox1.Name = "elTextBox1";
            this.elTextBox1.Size = new System.Drawing.Size(136, 20);
            this.elTextBox1.TabIndex = 40;
            this.elTextBox1.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "P - поперечная сила";
            // 
            // pTextBox1
            // 
            this.pTextBox1.Location = new System.Drawing.Point(152, 32);
            this.pTextBox1.Name = "pTextBox1";
            this.pTextBox1.Size = new System.Drawing.Size(136, 20);
            this.pTextBox1.TabIndex = 38;
            this.pTextBox1.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Эпсилон";
            // 
            // epsTextBox1
            // 
            this.epsTextBox1.Location = new System.Drawing.Point(11, 110);
            this.epsTextBox1.Name = "epsTextBox1";
            this.epsTextBox1.Size = new System.Drawing.Size(136, 20);
            this.epsTextBox1.TabIndex = 36;
            this.epsTextBox1.Text = "0.001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "L - длина бруса";
            // 
            // lTextBox1
            // 
            this.lTextBox1.Location = new System.Drawing.Point(9, 32);
            this.lTextBox1.Name = "lTextBox1";
            this.lTextBox1.Size = new System.Drawing.Size(136, 20);
            this.lTextBox1.TabIndex = 25;
            this.lTextBox1.Text = "1.1";
            // 
            // rButton1
            // 
            this.rButton1.Location = new System.Drawing.Point(9, 176);
            this.rButton1.Name = "rButton1";
            this.rButton1.Size = new System.Drawing.Size(111, 31);
            this.rButton1.TabIndex = 11;
            this.rButton1.Text = "запуск";
            this.rButton1.UseVisualStyleBackColor = true;
            this.rButton1.Click += new System.EventHandler(this.rButton1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(152, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "h - начальный шаг";
            // 
            // hTextBox1
            // 
            this.hTextBox1.Location = new System.Drawing.Point(153, 71);
            this.hTextBox1.Name = "hTextBox1";
            this.hTextBox1.Size = new System.Drawing.Size(136, 20);
            this.hTextBox1.TabIndex = 27;
            this.hTextBox1.Text = "0.001";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(152, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Количество шагов";
            // 
            // nTextBox1
            // 
            this.nTextBox1.Location = new System.Drawing.Point(153, 110);
            this.nTextBox1.Name = "nTextBox1";
            this.nTextBox1.Size = new System.Drawing.Size(136, 20);
            this.nTextBox1.TabIndex = 29;
            this.nTextBox1.Text = "1000";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(328, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 213);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Справка";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(253, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "P - поперечная сила";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(253, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "El - жесткость";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "L - длина бруса";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(245, 36);
            this.label10.TabIndex = 20;
            this.label10.Text = "Все значения вводятся в международной системе единиц (СИ)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "Задача имеет уравнение вида:\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(205, 60);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(9, 136);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 17);
            this.checkBox2.TabIndex = 44;
            this.checkBox2.Text = "Контроль длины";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 664);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.mainChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Процесс установления тока в цепи с самоиндукцией";
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox maxYTB;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox minYTB;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox maxXTB;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox minXTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox epsTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lTextBox1;
        private System.Windows.Forms.Button rButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox hTextBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nTextBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox elTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

