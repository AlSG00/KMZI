namespace KMZI
{
    partial class PoliCypher
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
            this.groupHitIndex = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.indexBox = new System.Windows.Forms.RichTextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.resultBox = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupHitIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(782, 627);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 26);
            this.button4.TabIndex = 40;
            this.button4.Text = "Закрыть";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupHitIndex
            // 
            this.groupHitIndex.Controls.Add(this.listBox1);
            this.groupHitIndex.Controls.Add(this.button6);
            this.groupHitIndex.Controls.Add(this.button5);
            this.groupHitIndex.Controls.Add(this.indexBox);
            this.groupHitIndex.Controls.Add(this.numericUpDown1);
            this.groupHitIndex.Controls.Add(this.resultBox);
            this.groupHitIndex.Controls.Add(this.textBox1);
            this.groupHitIndex.Controls.Add(this.button1);
            this.groupHitIndex.Controls.Add(this.label1);
            this.groupHitIndex.Controls.Add(this.button2);
            this.groupHitIndex.Location = new System.Drawing.Point(12, 44);
            this.groupHitIndex.Name = "groupHitIndex";
            this.groupHitIndex.Size = new System.Drawing.Size(911, 555);
            this.groupHitIndex.TabIndex = 39;
            this.groupHitIndex.TabStop = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(538, 298);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(152, 26);
            this.button6.TabIndex = 20;
            this.button6.Text = "Подобрать ключи";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(696, 298);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(209, 26);
            this.button5.TabIndex = 19;
            this.button5.Text = "Использовать ключ";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // indexBox
            // 
            this.indexBox.Location = new System.Drawing.Point(538, 61);
            this.indexBox.Name = "indexBox";
            this.indexBox.Size = new System.Drawing.Size(152, 231);
            this.indexBox.TabIndex = 18;
            this.indexBox.Text = "";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(538, 35);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(152, 20);
            this.numericUpDown1.TabIndex = 17;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(6, 330);
            this.resultBox.Name = "resultBox";
            this.resultBox.ReadOnly = true;
            this.resultBox.Size = new System.Drawing.Size(899, 219);
            this.resultBox.TabIndex = 16;
            this.resultBox.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(526, 258);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 298);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 298);
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 26);
            this.button3.TabIndex = 19;
            this.button3.Text = "Открыть файл";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(696, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(209, 225);
            this.listBox1.TabIndex = 21;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Метод индекса совпадений",
            "Автокорреляционный метод",
            "Тест Казиски"});
            this.comboBox1.Location = new System.Drawing.Point(170, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 41;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // PoliCypher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 665);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupHitIndex);
            this.Name = "PoliCypher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Криптоанализ полиалфавитных шифров";
            this.groupHitIndex.ResumeLayout(false);
            this.groupHitIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupHitIndex;
        private System.Windows.Forms.RichTextBox resultBox;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RichTextBox indexBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}