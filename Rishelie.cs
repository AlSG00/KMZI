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
    public partial class Rishelie : Form
    {
        int messageLength; //длина сообщения без пробелов. Пригодиться для проверки корректности ввода ключа
        int keyLength = 0;

        public Rishelie()
        {
            InitializeComponent();

            groupRishelie.Enabled = false;
            keyBox.Enabled = false;
            button5.Enabled = false;
            button3.Enabled = false;
        }

        public bool keyCheck(string key) // проверка корректности введенного ключа
        {
            int count = 0;
            bool check_length = false;
            bool check_number_order = false;
            bool isOrdered = false;

            if (keyBox.Text[keyBox.TextLength - 1] == ' ')      //Если последний элемент строки - пробел,
            {                                                   //возвращаем ошибку
                return false;
            }

            string[] intKey = key.Split(' ');                   //Пользуясь пробелом, разбиваем строку с ключом на отдельные элементы. Их вносим в новый строковый массив

            if (keyLength + intKey.Length <= textBox1.TextLength)   
            {
                keyLength += intKey.Length;     //Если сумма длин ключей + длина нового вводимого ключа не превышают длину сообщения
                check_length = true;            //То поднимаем флаг: проверка длины пройдена
            }

            while (count < intKey.Length)       //Проверка на последовательность введенных значенй для ключа
            {
                isOrdered = false;

                for (int i = 0; i < intKey.Length; i++)
                {
                    if (count == Convert.ToInt32(intKey[i].ToString()))     //идём по строке и проверяем, чтобы ключ содержал полную последовательность чисел, без пропусков 
                    {                                                       //(т.е. 0 1 2 3 и т.д. Варианты 0 2 3 или 0 1 1 не допускаются
                        isOrdered = true;
                        break;
                    }
                }
                if (isOrdered)
                {
                    count++;
                }
                else
                {
                    break;
                }

                if (count == intKey.Length)     //Если цикл сумел добраться до этого пункта
                {                               //значит проверка прошла успешно - ставим флаг
                    check_number_order = true;
                }
            }

            if (check_length == true && check_number_order == true) //Если успешно пройдены обе проверки,
            {                                                       //то подтверждаем корректность ключа
                return true;                                    
            }
            else
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = 0;
            messageLength = 0;
            int key_position = 0;
            string currentKey = null;
            string[] currentKey_massive = null;

            /* шифрование --------------------------------------------------------------------------------------------------------------------*/
            if (radioButton1.Checked == true)
            {
                textBox2.Clear();

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    currentKey = listBox1.Items[i].ToString();
                    currentKey_massive = currentKey.Split(' ');

                    for (int j = 0; j < currentKey_massive.Length; j++)
                    {
                        textBox2.Text += textBox1.Text[Convert.ToInt32(currentKey_massive[j].ToString()) + key_position];
                    }
                    key_position += currentKey_massive.Length;
                }

                ////если ключей меньше чем само сообщение
                for (int i = key_position; i < textBox1.TextLength; i++)
                {
                    textBox2.Text += textBox1.Text[i];
                }

                button3.Enabled = true;
                listBox2.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);
            }

            /* расшифрование -----------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)
            {
                textBox2.Clear();

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    currentKey = listBox1.Items[i].ToString();
                    currentKey_massive = currentKey.Split(' ');

                    while (index < currentKey_massive.Length)
                    {
                        for (int j = 0; j < currentKey_massive.Length; j++)
                        {
                            if (index == Convert.ToInt32(currentKey_massive[j].ToString()))
                            {
                                textBox2.Text += textBox1.Text[j + key_position];
                                index++;
                                break;
                            }
                        }
                    }
                    key_position += currentKey_massive.Length;
                    index = 0;
                }

                ////если ключей меньше чем само сообщение
                for (int i = key_position; i < textBox1.TextLength; i++)
                {
                    textBox2.Text += textBox1.Text[i];
                }

                button3.Enabled = true;
                listBox2.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);

            }
        }
        /*-----------------------------------------------------------------------------------------------------------------*/
        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
        }

        private void button6_Click(object sender, EventArgs e) //очистить список ключей
        {
            listBox1.Items.Clear();
            keyLength = 0;
        }

        private void button5_Click(object sender, EventArgs e) // добавить ключ в список
        {
            if (keyCheck(keyBox.Text))          //если введенный ключ корректен
            {                                   //то добавляем его в listBox
                listBox1.Items.Add(keyBox.Text);
            }
            else
            {
                MessageBox.Show("Ошибка", "Неверный формат ключа", MessageBoxButtons.OK);
                keyBox.Clear();
            }        
        }

        private void button4_Click_1(object sender, EventArgs e) // кнопка "закрыть"
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e) // кнопка "Очистить историю"
        {
            listBox2.Items.Clear();
            button3.Enabled = true;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (groupRishelie.Enabled == false)
                groupRishelie.Enabled = true;

            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            button2.Text = "Зашифровать";
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (groupRishelie.Enabled == false)
                groupRishelie.Enabled = true;

            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            button2.Text = "Расшифровать";
        }

        private void keyBox_TextChanged_1(object sender, EventArgs e)
        {
            if (keyBox.TextLength > 0)
            {
                if (keyBox.Text[0] == ' ')
                {
                    keyBox.Clear();
                }
                for (int i = 0; i < keyBox.TextLength; i++)
                {
                    if (!char.IsDigit(keyBox.Text[i]) && keyBox.Text[i] != ' ')
                    {
                        keyBox.Clear();
                        break;
                    }
                }
                for (int i = 0; i < keyBox.TextLength - 1; i++)
                {
                    if (keyBox.Text[i] == ' ' && keyBox.Text[i + 1] == ' ')
                    {
                        keyBox.Clear();
                        break;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength > 0)
            {
                keyBox.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                keyBox.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBox.Clear();
            keyBox.Text += listBox1.SelectedItem;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }
    }
}
