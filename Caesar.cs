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
    public partial class Caesar : Form
    {
        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        bool stop;
        int key;
        //int n;//мощность алфавита

        public Caesar()
        {
            InitializeComponent();

            groupCaezar.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();

            if (textBoxKey.Text == "")
            {
                textBoxKey.Text += "0";
            }

            if (!int.TryParse(textBoxKey.Text, out key))
            {
                //MessageBox.Show("Введите корректный ключ!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxKey.Clear();
            }
            else
            {
                key = Convert.ToInt32(textBoxKey.Text);

                if (radioButton1.Checked == true)
                {
                    for (int i = 0; i < textBox1.TextLength; i++)
                    {
                        stop = true;
                        for (int j = 0; j < rus.Length; j++)//шифрование для русских символов
                        {
                            if (textBox1.Text[i] == rus[j])
                            {
                                textBox2.Text += rus[Math.Abs((j + 33 + key) % 33)];
                                stop = false;
                                break;
                            }

                            if (textBox1.Text[i] == RUS[j])
                            {
                                textBox2.Text += RUS[Math.Abs((j + 33 + key) % 33)];
                                stop = false;
                                break;
                            }
                        }
                        for (int j = 0; j < eng.Length; j++)//щифрование для латинских символов
                        {
                            if (textBox1.Text[i] == eng[j])
                            {
                                textBox2.Text += eng[Math.Abs((j + 26 + key) % 26)];
                                stop = false;
                                break;
                            }
                            if (textBox1.Text[i] == ENG[j])
                            {
                                textBox2.Text +=  ENG[Math.Abs(( j + 26 + key) % 26)];
                                stop = false;
                                break;
                            }
                        }
                        if (stop == true)
                        {
                            textBox2.Text += textBox1.Text[i];
                            stop = false;
                        }
                    }
                }

                if (radioButton2.Checked == true)
                {
                    for (int i = 0; i < textBox1.TextLength; i++)
                    {
                        stop = true;
                        for (int j = 0; j < rus.Length; j++)//дешифрование для русских символов
                        {
                            if (textBox1.Text[i] == rus[j])
                            {
                                textBox2.Text += rus[(j + 33 - key) % 33];
                                stop = false;
                                break;
                            }

                            if (textBox1.Text[i] == RUS[j])
                            {
                                textBox2.Text += RUS[(j + 33 - key) % 33];
                                stop = false;
                                break;
                            }
                        }
                        for (int j = 0; j < eng.Length; j++)//дешифрование для латинских символов
                        {
                            if (textBox1.Text[i] == eng[j])
                            {
                                textBox2.Text += eng[(j + 26 - key) % 26];
                                stop = false;
                                break;
                            }
                            if (textBox1.Text[i] == ENG[j])
                            {
                                textBox2.Text += ENG[(j + 26 - key) % 26];
                                stop = false;
                                break;
                            }
                        }
                        if (stop == true)
                        {
                            textBox2.Text += textBox1.Text[i];
                            stop = false;
                        }
                    }
                }

                listBox1.Items.Add(textBox1.Text);
                listBox1.Items.Add(textBox2.Text);
                button3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBoxKey.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox1.SelectedItem;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupCaezar.Enabled = true;
            button2.Text = "Зашифровать";

            textBox1.Clear();
            textBox2.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupCaezar.Enabled = true;
            button2.Text = "Расшифровать";

            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button3.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            int num;

            if (!int.TryParse(textBoxKey.Text, out num))
            {
                textBoxKey.Clear();
            }
        }
    }
}
