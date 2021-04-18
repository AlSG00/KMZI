using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KMZI
{
    public partial class Gamma : Form
    {
        public Gamma()
        {
            InitializeComponent();
        }

        // Кнопка "Преобразовать"
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text += textBox1.Text;
        }

        // Кнопка "Открыть файл"
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        // Чтение файла и вывод содержимого в textBox1
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            //StreamReader str = new StreamReader(openFileDialog1.FileName);
            //textBox1.Clear();
            //textBox1.Text += str.ReadToEnd();
        }

        // Кнопка "Сохранить файл"
        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
        }

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
