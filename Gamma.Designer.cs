﻿namespace KMZI
{
    partial class Gamma
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
            this.startKeyBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.keyBox = new System.Windows.Forms.RichTextBox();
            this.groupVernam.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(409, 576);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 26);
            this.button4.TabIndex = 40;
            this.button4.Text = "Закрыть";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupVernam
            // 
            this.groupVernam.Controls.Add(this.keyBox);
            this.groupVernam.Controls.Add(this.progressBar1);
            this.groupVernam.Controls.Add(this.startKeyBox);
            this.groupVernam.Controls.Add(this.label2);
            this.groupVernam.Controls.Add(this.button5);
            this.groupVernam.Controls.Add(this.button3);
            this.groupVernam.Controls.Add(this.textBox2);
            this.groupVernam.Controls.Add(this.textBox1);
            this.groupVernam.Controls.Add(this.button1);
            this.groupVernam.Controls.Add(this.label1);
            this.groupVernam.Controls.Add(this.button2);
            this.groupVernam.Location = new System.Drawing.Point(12, 12);
            this.groupVernam.Name = "groupVernam";
            this.groupVernam.Size = new System.Drawing.Size(539, 539);
            this.groupVernam.TabIndex = 39;
            this.groupVernam.TabStop = false;
            // 
            // startKeyBox
            // 
            this.startKeyBox.Location = new System.Drawing.Point(7, 235);
            this.startKeyBox.Name = "startKeyBox";
            this.startKeyBox.Size = new System.Drawing.Size(45, 20);
            this.startKeyBox.TabIndex = 14;
            this.startKeyBox.TextChanged += new System.EventHandler(this.startKeyBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ключ";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(272, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(260, 26);
            this.button5.TabIndex = 11;
            this.button5.Text = "Сохранить файл";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(58, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(209, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "Открыть файл";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 338);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(526, 172);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(525, 164);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 306);
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
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Текст";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 26);
            this.button2.TabIndex = 6;
            this.button2.Text = "Преобразовать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 516);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(526, 17);
            this.progressBar1.TabIndex = 41;
            // 
            // keyBox
            // 
            this.keyBox.Location = new System.Drawing.Point(58, 213);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(474, 87);
            this.keyBox.TabIndex = 42;
            this.keyBox.Text = "";
            // 
            // Gamma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 614);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupVernam);
            this.Name = "Gamma";
            this.Text = "Гаммирование";
            this.groupVernam.ResumeLayout(false);
            this.groupVernam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupVernam;
        private System.Windows.Forms.RichTextBox textBox2;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox startKeyBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox keyBox;
    }
}