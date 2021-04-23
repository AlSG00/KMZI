namespace KMZI
{
    partial class Pleifer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupPleifer = new System.Windows.Forms.GroupBox();
            this.processBox = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.keyBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupPleifer.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(409, 537);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 26);
            this.button4.TabIndex = 34;
            this.button4.Text = "Закрыть";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(117, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(108, 17);
            this.radioButton2.TabIndex = 33;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Расшифрование";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupPleifer
            // 
            this.groupPleifer.Controls.Add(this.processBox);
            this.groupPleifer.Controls.Add(this.listBox1);
            this.groupPleifer.Controls.Add(this.keyBox);
            this.groupPleifer.Controls.Add(this.label3);
            this.groupPleifer.Controls.Add(this.textBox2);
            this.groupPleifer.Controls.Add(this.textBox1);
            this.groupPleifer.Controls.Add(this.button3);
            this.groupPleifer.Controls.Add(this.label2);
            this.groupPleifer.Controls.Add(this.button1);
            this.groupPleifer.Controls.Add(this.label1);
            this.groupPleifer.Controls.Add(this.listBox2);
            this.groupPleifer.Controls.Add(this.button2);
            this.groupPleifer.Location = new System.Drawing.Point(12, 35);
            this.groupPleifer.Name = "groupPleifer";
            this.groupPleifer.Size = new System.Drawing.Size(539, 475);
            this.groupPleifer.TabIndex = 31;
            this.groupPleifer.TabStop = false;
            // 
            // processBox
            // 
            this.processBox.Location = new System.Drawing.Point(272, 82);
            this.processBox.Name = "processBox";
            this.processBox.ReadOnly = true;
            this.processBox.Size = new System.Drawing.Size(260, 44);
            this.processBox.TabIndex = 20;
            this.processBox.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(272, 270);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(260, 147);
            this.listBox1.TabIndex = 13;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(272, 34);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(260, 44);
            this.keyBox.TabIndex = 12;
            this.keyBox.Text = "";
            this.keyBox.TextChanged += new System.EventHandler(this.keyBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ключ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 174);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(526, 65);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 92);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 423);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(526, 26);
            this.button3.TabIndex = 7;
            this.button3.Text = "Очистить историю";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "История";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "Очистить поля";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Текст";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(6, 270);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(260, 147);
            this.listBox2.TabIndex = 6;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 26);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(90, 17);
            this.radioButton1.TabIndex = 32;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Шифрование";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Pleifer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 575);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.groupPleifer);
            this.Controls.Add(this.radioButton1);
            this.Name = "Pleifer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Плейфер";
            this.groupPleifer.ResumeLayout(false);
            this.groupPleifer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupPleifer;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox keyBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox textBox2;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RichTextBox processBox;
    }
}