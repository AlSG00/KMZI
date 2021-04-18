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
    public partial class Gronsfeld : Form
    {
        public Gronsfeld()
        {
            InitializeComponent();

            groupGronsfeld.Enabled = false;
            keyBox.Enabled = false;
            keyBoxProcessed.Enabled = false;
            button2.Enabled = false;
            label4.Text = ": 0";
            label5.Text = ": 0";
            label7.Text = "0";
        }

        int count;
        int count_of_letters;

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            count_of_letters = 0;
            textBox2.Clear();

            for(int i = 0; i < textBox1.TextLength; i++)
            {
                if(char.IsLetter( textBox1.Text[i]))
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
                for(int i = 0; i < textBox1.TextLength; i++)
                {
                    if(char.IsLetter(textBox1.Text[i]))
                    {
                        if(rus.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += rus[Math.Abs((Array.IndexOf(rus, textBox1.Text[i]) + 33 + Convert.ToInt32(keyBoxProcessed.Text[count].ToString())) % 33)];
                        }
                        if (RUS.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += RUS[Math.Abs((Array.IndexOf(RUS, textBox1.Text[i]) + 33 + Convert.ToInt32(keyBoxProcessed.Text[count].ToString())) % 33)];
                        }
                        if (eng.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += eng[Math.Abs((Array.IndexOf(eng, textBox1.Text[i]) + 26 + Convert.ToInt32(keyBoxProcessed.Text[count].ToString())) % 26)];
                        }
                        if (ENG.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += ENG[Math.Abs((Array.IndexOf(ENG, textBox1.Text[i]) + 26 + Convert.ToInt32(keyBoxProcessed.Text[count].ToString())) % 26)];
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
                    if (char.IsLetter(textBox1.Text[i]))
                    {
                        if (rus.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += rus[(Array.IndexOf(rus, textBox1.Text[i]) - Convert.ToInt32(keyBoxProcessed.Text[count].ToString()) + 33) % 33];
                        }
                        if (RUS.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += RUS[(Array.IndexOf(RUS, textBox1.Text[i]) - Convert.ToInt32(keyBoxProcessed.Text[count].ToString()) + 33) % 33];
                        }
                        if (eng.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += eng[(Array.IndexOf(eng, textBox1.Text[i]) - Convert.ToInt32(keyBoxProcessed.Text[count].ToString()) + 26) % 26];
                        }
                        if (ENG.Contains(textBox1.Text[i]))
                        {
                            textBox2.Text += ENG[(Array.IndexOf(ENG, textBox1.Text[i]) - Convert.ToInt32(keyBoxProcessed.Text[count].ToString()) + 26) % 26];
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

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = ": " + textBox1.TextLength;
            if(textBox1.TextLength > 0)
            {
                keyBox.Enabled = true;
            }
            else
            {
                keyBox.Enabled = false;
            }
        }

        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            label5.Text = ": " + keyBox.TextLength;

            if (keyBox.TextLength > 0)
            {
                button2.Enabled = true;
                for (int i = 0; i < keyBox.TextLength; i++)
                {
                    if (!char.IsDigit(keyBox.Text[i]))
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

        private void keyBoxProcessed_TextChanged(object sender, EventArgs e)
        {
            label7.Text = keyBoxProcessed.TextLength.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBox.Clear();
            keyBox.Text += listBox1.SelectedItem;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupGronsfeld.Enabled = true;
            button2.Text = "Зашифровать";
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupGronsfeld.Enabled = true;
            button2.Text = "Расшифровать";
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
