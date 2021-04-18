using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMZI
{
    public partial class Skitala : Form
    {
        public Skitala()
        {
            InitializeComponent();

            textBox5.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            groupSkitala.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = 0;          //число столбцов
            int m = 0;          //число строк
            int k = 0;          //для заполнения пустот
            int count = 0;      //счётчик, чтоб красиво заполнить матрицу, идя по всей строке текста
            int length = 0;     //длина сообщения

            char[,] encryptor;//тут бедт шифротекст                //ДА, МОЖНО БЫЛО ОБЪЕДИНИТЬ ЭТИ ДВЕ МАТРИЦЫ В ОДНУ
            char[,] decryptor;//тут будет расширфрованный текст    //НО ТАК БУДЕТ НАГЛЯДНЕЕ, ИМХО

            textBox4.Clear();
            richTextBox1.Clear();

            m = Convert.ToInt32(textBox5.Text); //строки
            n = Convert.ToInt32(textBox6.Text); //столбцы
            length = textBox3.TextLength;       //длина текста
            k = textBox3.TextLength % m;        //сколько символов не хватает

            if (k > 0)//вычисляем, сколько символов вписать в конце, чтоб шифр работал корректно
            {
                textBox3.Text += new string('_', m - k);
            }
 /*-----------------------------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton1.Checked == true)//ЕСЛИ ВЫБРАНО ШИФРОВАНИЕ
            {
                encryptor = new char[n, m];//делаем матрицу для записи

                for (int i = 0; i < m; i++)//вносим весь текст в матрицу
                {
                    for (int j = 0; j < n; j++)
                    {
                        encryptor[j, i] = textBox3.Text[count];
                        richTextBox1.Text += encryptor[j, i];
                        count++;
                    }
                    richTextBox1.Text += Environment.NewLine;
                }
                richTextBox1.Text += Environment.NewLine;

                for (int i = 0; i < n; i++)//выводим шифротекст
                {
                    for (int j = 0; j < m; j++)
                    {
                        textBox4.Text += encryptor[i, j];
                    }
                    //richTextBox1.Text += Environment.NewLine;
                }
                button3.Enabled = true;
                listBox2.Items.Add(textBox3.Text);
                listBox2.Items.Add(textBox4.Text);
            }
 /*-----------------------------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)//ЕСЛИ ВЫБРАНО ДЕШИФРОВАНИЕ
            {
                decryptor = new char[n, m];//делаем матрицу для записи

                for (int i = 0; i < n; i++)//вносим в неё шифротекст
                {
                    for (int j = 0; j < m; j++)
                    {
                        decryptor[i, j] = textBox3.Text[count];
                        richTextBox1.Text += decryptor[i, j];
                        count++;
                    }
                    richTextBox1.Text += Environment.NewLine;
                }
                richTextBox1.Text += Environment.NewLine;


                for (int i = 0; i < m; i++)//крутим-вертим и выводим расшифрованный текст
                {
                    for (int j = 0; j < n; j++)
                    {
                        textBox4.Text += decryptor[j, i]; 
                        //richTextBox1.Text += decryptor[j, i];
                    }
                    //richTextBox1.Text += Environment.NewLine;
                }
                button3.Enabled = true;
                listBox2.Items.Add(textBox3.Text);
                listBox2.Items.Add(textBox4.Text);
            }        
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void textBox5_TextChanged(object sender, EventArgs e) //если меняется число строк, то автоматически пересчитывается необходимое число столбцов
        {
            int m = 0;
            decimal n = 0;
            int k = textBox3.TextLength;

            if (k != 0)
            {
                // for (int i = 0; i < textBox5.TextLength; i++)
                // {


                if (textBox5.Text == "")
                {
                    textBox5.Clear();
                    textBox5.Text += '1';
                }
                for (int i = 0; i < textBox5.TextLength; i++)
                {
                    if (textBox5.Text[i] == '0' || textBox5.Text[i] == '-')
                    {
                        textBox5.Clear();
                        textBox5.Text += '1';
                    }
                }
                if (!int.TryParse(textBox5.Text, out m))
                {
                    textBox5.Clear();
                }
                else
                {
                    m = Convert.ToInt32(textBox5.Text);

                    n = (k - 1) / m;//пересчёт числа столбцов при изменении числа строк
                    n++;

                    textBox6.Clear();
                    textBox6.Text += n;
                }
                //}
            }
            /*for(int i = 0; i < textBox5.TextLength; i++)
            {

            }*/
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                textBox5.Enabled = true;
                textBox5_TextChanged(sender, e);
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                textBox5.Enabled = false;

            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox3.Text += listBox2.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            richTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            richTextBox1.Clear();
            button3.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupSkitala.Enabled = true;
            textBox3.Clear();
            textBox4.Clear();
            button2.Text = "Зашифровать";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupSkitala.Enabled = true;
            button2.Text = "Расшифровать";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            string temp ="";
            for(int i = 0; i < textBox5.Text.Length; i++)
            {
                if(Char.IsDigit(Convert.ToChar(textBox5.Text[i])))
                {
                    temp += textBox5.Text[i];
                }
                else
                {
                    temp += "";
                }
            }
        }
    }
}
