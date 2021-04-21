namespace KMZI
{
    partial class Vernam
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
            this.groupVernam = new System.Windows.Forms.GroupBox();
            this.keyBoxProcessed = new System.Windows.Forms.RichTextBox();
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
            this.binaryBox = new System.Windows.Forms.RichTextBox();
            this.groupVernam.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(409, 598);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 26);
            this.button4.TabIndex = 38;
            this.button4.Text = "Закрыть";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupVernam
            // 
            this.groupVernam.Controls.Add(this.binaryBox);
            this.groupVernam.Controls.Add(this.keyBoxProcessed);
            this.groupVernam.Controls.Add(this.listBox1);
            this.groupVernam.Controls.Add(this.keyBox);
            this.groupVernam.Controls.Add(this.label3);
            this.groupVernam.Controls.Add(this.textBox2);
            this.groupVernam.Controls.Add(this.textBox1);
            this.groupVernam.Controls.Add(this.button3);
            this.groupVernam.Controls.Add(this.label2);
            this.groupVernam.Controls.Add(this.button1);
            this.groupVernam.Controls.Add(this.label1);
            this.groupVernam.Controls.Add(this.listBox2);
            this.groupVernam.Controls.Add(this.button2);
            this.groupVernam.Location = new System.Drawing.Point(12, 12);
            this.groupVernam.Name = "groupVernam";
            this.groupVernam.Size = new System.Drawing.Size(539, 555);
            this.groupVernam.TabIndex = 35;
            this.groupVernam.TabStop = false;
            // 
            // keyBoxProcessed
            // 
            this.keyBoxProcessed.Location = new System.Drawing.Point(272, 82);
            this.keyBoxProcessed.Name = "keyBoxProcessed";
            this.keyBoxProcessed.ReadOnly = true;
            this.keyBoxProcessed.Size = new System.Drawing.Size(260, 44);
            this.keyBoxProcessed.TabIndex = 15;
            this.keyBoxProcessed.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(272, 364);
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
            this.textBox2.Location = new System.Drawing.Point(6, 268);
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
            this.button3.Location = new System.Drawing.Point(6, 517);
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
            this.label2.Location = new System.Drawing.Point(3, 348);
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
            this.listBox2.Location = new System.Drawing.Point(6, 364);
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
            this.button2.Text = "Преобразовать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // binaryBox
            // 
            this.binaryBox.Location = new System.Drawing.Point(6, 164);
            this.binaryBox.Name = "binaryBox";
            this.binaryBox.ReadOnly = true;
            this.binaryBox.Size = new System.Drawing.Size(526, 98);
            this.binaryBox.TabIndex = 16;
            this.binaryBox.Text = "";
            // 
            // Vernam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 636);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupVernam);
            this.Name = "Vernam";
            this.Text = "Вернам";
            this.groupVernam.ResumeLayout(false);
            this.groupVernam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupVernam;
        private System.Windows.Forms.RichTextBox keyBoxProcessed;
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
        private System.Windows.Forms.RichTextBox binaryBox;
    }
}