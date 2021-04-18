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
    public partial class Vizhiner : Form
    {
        public Vizhiner()
        {
            InitializeComponent();

            groupVizhiner.Enabled = false;
            keyBox.Enabled = false;
            keyBoxProcessed.Enabled = false;
            label4.Text = ": 0";
            label5.Text = ": 0";
            label7.Text = "0";
            button2.Enabled = false;
        }

        bool Rus = false;
        bool Eng = false;
        int count;
        int count_of_letters;


        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            count = 0;
            count_of_letters = 0;


            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (char.IsLetter(textBox1.Text[i]))
                {
                    count_of_letters++;
                }
            }
            if (keyBox.TextLength < count_of_letters)
            {
                int length_of_key = keyBox.TextLength;
                int index = 0;
                while (keyBoxProcessed.TextLength < count_of_letters)
                {
                    keyBoxProcessed.Text += keyBox.Text[index % length_of_key];
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < count_of_letters; i++)
                {
                    keyBoxProcessed.Text += keyBox.Text[i];
                }
            }

            /* Шифрование ----------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (rus.Contains(textBox1.Text[i]) || RUS.Contains(textBox1.Text[i]) || eng.Contains(textBox1.Text[i]) || ENG.Contains(textBox1.Text[i]))
                    {
                        if (Eng == true && char.IsUpper(textBox1.Text[i]))
                        {
                            textBox2.Text += ENG[Math.Abs((Array.IndexOf(ENG, textBox1.Text[i]) + 26 + 1 + Array.IndexOf(ENG, char.ToUpper(keyBoxProcessed.Text[count]))) % 26)];
                        }
                        if (Eng == true && char.IsLower(textBox1.Text[i]))
                        {
                            textBox2.Text += eng[Math.Abs((Array.IndexOf(eng, textBox1.Text[i]) + 26 + 1 + Array.IndexOf(eng, char.ToLower(keyBoxProcessed.Text[count]))) % 26)];
                        }
                        if (Rus == true && char.IsUpper(textBox1.Text[i]))
                        {
                            textBox2.Text += RUS[Math.Abs((Array.IndexOf(RUS, textBox1.Text[i]) + 33 + 1 + Array.IndexOf(RUS, char.ToUpper(keyBoxProcessed.Text[count]))) % 33)];
                        }
                        if (Rus == true && char.IsLower(textBox1.Text[i]))
                        {
                            textBox2.Text += rus[Math.Abs((Array.IndexOf(rus, textBox1.Text[i]) + 33 + 1 + Array.IndexOf(rus, char.ToLower(keyBoxProcessed.Text[count]))) % 33)];
                        }
                        count++;
                    }
                    else
                    {
                        textBox2.Text += textBox1.Text[i];
                    }
                }

                listBox2.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);
                listBox1.Items.Add(keyBox.Text);
            }

            /* Расшифрование ----------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (rus.Contains(textBox1.Text[i]) || RUS.Contains(textBox1.Text[i]) || eng.Contains(textBox1.Text[i]) || ENG.Contains(textBox1.Text[i]))
                    {
                        if (Eng == true && char.IsUpper(textBox1.Text[i]))
                        {
                            textBox2.Text += ENG[(Array.IndexOf(ENG, textBox1.Text[i]) + 26 - 1 - Array.IndexOf(ENG, char.ToUpper(keyBoxProcessed.Text[count]))) % 26];
                        }
                        if (Eng == true && char.IsLower(textBox1.Text[i]))
                        {
                            textBox2.Text += eng[(Array.IndexOf(eng, textBox1.Text[i]) + 26 - 1 - Array.IndexOf(eng, char.ToLower(keyBoxProcessed.Text[count]))) % 26];
                        }
                        if (Rus == true && char.IsUpper(textBox1.Text[i]))
                        {
                            textBox2.Text += RUS[(Array.IndexOf(RUS, textBox1.Text[i]) + 33 - 1 - Array.IndexOf(RUS, char.ToUpper(keyBoxProcessed.Text[count]))) % 33];
                        }
                        if (Rus == true && char.IsLower(textBox1.Text[i]))
                        {
                            textBox2.Text += rus[(Array.IndexOf(rus, textBox1.Text[i]) + 33 - 1 - Array.IndexOf(rus, char.ToLower(keyBoxProcessed.Text[count]))) % 33];
                        }
                        count++;
                    }
                    else
                    {
                        textBox2.Text += textBox1.Text[i];
                    }
                }

                listBox2.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);
                listBox1.Items.Add(keyBox.Text);
            }
        }
        /*----------------------------------------------------------------------------------------------------------------------------------*/

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupVizhiner.Enabled = true;
            button2.Text = "Зашифровать";
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupVizhiner.Enabled = true;
            button2.Text = "Расшифровать";
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
            Rus = false;
            Eng = false;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = ": " + textBox1.TextLength;

            if (textBox1.TextLength > 0)
            {
                keyBox.Enabled = true;


                if (!char.IsLetter(textBox1.Text[0]))
                {
                    textBox1.Clear();
                    keyBox.Enabled = false;
                    Eng = false;
                    Rus = false;
                    label4.Text = ": " + textBox1.TextLength;
                    return;
                }

                for (int i = 0; i < 26; i++)
                {
                    if (textBox1.Text[0] == eng[i] || textBox1.Text[0] == ENG[i])
                    {
                        Eng = true;
                        break;
                    }
                }
                if (Eng == false)
                {
                    Rus = true;
                }

                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (Eng == true)
                    {
                        for (int j = 0; j < 33; j++)
                        {
                            if (textBox1.Text[i] == rus[j] || textBox1.Text[i] == RUS[j])
                            {
                                textBox1.Clear();
                                Eng = false;
                                break;
                            }
                        }
                    }
                    if (Rus == true)
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if (textBox1.Text[i] == eng[j] || textBox1.Text[i] == ENG[j])
                            {
                                textBox1.Clear();
                                Rus = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                keyBox.Enabled = false;
                Eng = false;
                Rus = false;
            }
            label4.Text = ": " + textBox1.TextLength;
        }



        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            keyBoxProcessed.Clear();

            label5.Text = ": " + keyBox.TextLength;

            if (keyBox.TextLength > 0)
            {
                button2.Enabled = true;

                for (int i = 0; i < keyBox.TextLength; i++)
                {
                    if (char.IsLetter(keyBox.Text[i]))
                    {
                        if (rus.Contains(keyBox.Text[i]) || RUS.Contains(keyBox.Text[i]))
                        {
                            if (Eng == true)
                            {
                                button2.Enabled = false;
                                keyBox.Clear();
                                break;
                            }
                        }

                        if (eng.Contains(keyBox.Text[i]) || ENG.Contains(keyBox.Text[i]))
                        {
                            if (Rus == true)
                            {
                                button2.Enabled = false;
                                keyBox.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        button2.Enabled = false;
                        keyBox.Clear();
                        break;
                    }
                }
            }
            else
            {
                button2.Enabled = false;
            }
            label5.Text = ": " + keyBox.TextLength;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBox.Clear();
            keyBox.Text += listBox1.SelectedItem;
        }

        private void keyBoxProcessed_TextChanged(object sender, EventArgs e)
        {
            label7.Text = keyBoxProcessed.TextLength.ToString();
        }
    }
}
