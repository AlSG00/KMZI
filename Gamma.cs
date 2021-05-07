using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.CodeDom.Compiler;


namespace KMZI
{
    public partial class Gamma : Form
    {
        public Gamma()
        {
            InitializeComponent();

            button2.Enabled = false;
            startKeyBox.Enabled = false;
        }

        int mod = 6655; // Модуль (mod >= 2)
        byte[] tmpIn;
        byte[] tmpOut;
        byte[] text_byte;
        bool is_text_detailed = false;
        bool is_text_from_file = false;
        // Кнопка "Преобразовать"
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            keyBox.Clear();

            //Если ключ не отвечает условиям, то будет ошибка
            if (!isStartKeyCorrect(startKeyBox.Text))
            {
                MessageBox.Show("Некорректный стартовый ключ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startKeyBox.Clear();
                return;
            }
            startKeyBox.Text = Convert.ToString(Convert.ToInt32(startKeyBox.Text) % mod);

            text_byte = null;

            if (tmpIn == null && is_text_from_file == false)
            {
                //textBox1.Text = BitConverter.ToString(tmpIn);
                tmpIn = Encoding.Default.GetBytes(textBox1.Text);               
            }

            text_byte = tmpIn;
            //else
            //{
            //    text_byte = tmpIn;
            //    //tmpIn = null;
            //}

            // Генерируем случайную числовую последовательность
            int[] key = generate_key(Convert.ToInt32(startKeyBox.Text.ToString()), text_byte.Length);

            // Вывод ключа через файл
            StreamWriter sw = new StreamWriter("out.txt");       
            for (int i = 0; i < key.Length; i++)
            { 
                sw.Write(key[i]);
            }
            sw.Close();
            StreamReader str = new StreamReader("out.txt");
            string key_str = str.ReadToEnd();
            key_str = key_str.Substring(0, text_byte.Length);
            keyBox.Text = key_str;         
            str.Close();

            byte[] key_byte = Encoding.Default.GetBytes(key_str);
            var key_binary = new BitArray(key_byte);
            var text_binary = new BitArray(text_byte);

            text_binary.Xor(key_binary);

            tmpOut = new byte[text_byte.Length];
            text_binary.CopyTo(tmpOut, 0);

            if (is_text_detailed)
            {
                textBox2.Text = BitConverter.ToString(tmpOut);
            }
            else
            {
                textBox2.Text = Encoding.Default.GetString(tmpOut);
            }
            //textBox2.Text = Encoding.Default.GetString(tmpOut);
        }

        // Кнопка "Открыть файл"
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(); 
        }

        // Чтение файла и вывод содержимого в textBox1
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            tmpIn = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
            textBox1.Text = Encoding.Default.GetString(tmpIn);

            is_text_from_file = true;
            tmpIn = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
            //inBox.Text = Encoding.Default.GetString(inFile);
           // progressBar1.Visible = false;
           // progressBar1.Value = 0;
            //inBox.Text = BitConverter.ToString(inFile);
            if (is_text_detailed)
            {
                textBox1.Text = BitConverter.ToString(tmpIn);
            }
            else
            {
                textBox1.Text = Encoding.Default.GetString(tmpIn);
                //tmpIn
                //textBox1.Text = BitConverter.ToString(tmpIn);
            }
        }

        // Кнопка "Сохранить файл"
        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        // Сохранение в файл
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.File.WriteAllBytes(saveFileDialog1.FileName, tmpOut);
        }

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            is_text_from_file = false;
            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            startKeyBox.Clear();
            button2.Enabled = false;
            tmpIn = null;
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Функция генерации ключа линейным конгруэнтным методом
       int[] generate_key(int startIndex, int length)
        {            
            int a = 936; // Множитель (0 <= a < mod)
            int c = 1399; // Приращение (0 <= c < mod)
            int x = startIndex; // Начальное значение (0 <= x < mod)
            int[] key = new int[text_byte.Length];
            key[0] = x;

            for(int i = 1; i < length; i++)
            {
                key[i] = (a * key[i - 1] + c) % mod;
            }
            //for (int i = 0; i < length; i++)
            //{
            //    key[i] %= 256;
            //}
            return key;
        }

        // Проверка на корректность введенного начального числа для генерации ключа
        bool isStartKeyCorrect(string key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                // Если в строке ключа находится не число
                if (!Char.IsDigit(key[i])) 
                {
                    return false;
                }
            }
            // если числовая строка меньше нуля
            if (Convert.ToInt32(key) % mod < 0) 
            {
                return false;
            }
            return true;
        }

        // Выполняется, если изменилось содержимое startKeyBox
        private void startKeyBox_TextChanged(object sender, EventArgs e)
        {
            if(startKeyBox.TextLength > 0 && startKeyBox.TextLength < 6)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        // Выполняется, если изменилось содержимое поля ввода
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength > 0)
            {
                startKeyBox.Enabled = true;
            }
            else
            {
                startKeyBox.Clear();
                startKeyBox.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tmpIn != null /*&& tmpOut != null*/)
            {
                //byte[] temp = new byte[inFile.Length];
                //inFile.CopyTo(temp, 0);
                tmpOut.CopyTo(tmpIn, 0);
                //temp.CopyTo(outFile, 0);
                textBox1.Clear();
                textBox2.Text = textBox1.Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                is_text_detailed = true;
            }
            else
            {
                is_text_detailed = false;
            }
        }
    }
}