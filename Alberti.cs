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
    public partial class Alberti : Form
    {
        public Alberti()
        {
            InitializeComponent();

            groupAlberti.Enabled = false;
            alphabetKey.Enabled = false;
            keyBoxProcessed.Enabled = false;
            keyBox.Enabled = false;
            label4.Text = ": 0";
            label5.Text = ": 0";
            label7.Text = "0";
            label9.Text = "0";
            button2.Enabled = false;
        }

        int count;
        int count_of_letters;
        bool Eng = false;
        bool Rus = false;
        string alphabet_temp;

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
       
        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            alphabet_temp = null;
            textBox2.Clear();
            keyBoxProcessed.Clear();
            count_of_letters = 0;

            //обработка ключа
            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (char.IsLetter(textBox1.Text[i]))    //считаем число букв в сообщении. Отсеиваем прочие символы
                {
                    count_of_letters++;
                }
            }
            if (keyBox.TextLength < count_of_letters)                           //Если длина ключа меньше длины сообщения
            {                                                                   //то делаем самокопирование ключа до нужной длины
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

            alphabet_temp = alphabet_processing(alphabetKey.Text.ToLower());    //обрабатываем алфавит под ключ

            char[] temp = new char[alphabet_temp.Length];
            char[] TEMP = new char[alphabet_temp.Length];

            for(int i = 0; i < alphabet_temp.Length; i++)
            {
                temp[i] = alphabet_temp[i];
                TEMP[i] = char.ToUpper(alphabet_temp[i]);
            }
            /* Шифрование ----------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton1.Checked == true)
            {
                for(int i = 0; i < textBox1.TextLength; i++)
                {
                    if(char.IsLetter(textBox1.Text[i]))
                    {
                        if (Eng == true)
                        {
                            if (char.IsLower(textBox1.Text[i]))     //вычисляем смещение и шифруем
                            {
                                textBox2.Text += temp[(Array.IndexOf(eng, textBox1.Text[i]) + temp.Length + 26 - 1 - Array.IndexOf(eng, char.ToLower(keyBoxProcessed.Text[count]))) % temp.Length];
                            }
                            else
                            {
                                textBox2.Text += TEMP[(Array.IndexOf(ENG, textBox1.Text[i]) + TEMP.Length + 26 - 1 - Array.IndexOf(ENG, char.ToUpper(keyBoxProcessed.Text[count]))) % TEMP.Length];
                            }
                        }
                        if (Rus == true)
                        {
                            if (char.IsLower(textBox1.Text[i]))
                            {
                                textBox2.Text += temp[(Array.IndexOf(rus, textBox1.Text[i]) + temp.Length + 33 - 1 - Array.IndexOf(rus, char.ToLower(keyBoxProcessed.Text[count]))) % temp.Length];
                            }
                            else
                            {
                                textBox2.Text += TEMP[(Array.IndexOf(RUS, textBox1.Text[i]) + TEMP.Length + 33 - 1 - Array.IndexOf(RUS, char.ToUpper(keyBoxProcessed.Text[count]))) % TEMP.Length];
                            }
                        }
                        count++;
                    }
                    else
                    {
                        textBox2.Text += textBox1.Text[i];
                    }
                }
            }
            /* Расшифрование ----------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (char.IsLetter(textBox1.Text[i]))
                    {
                        if (Eng == true)
                        {
                            if (char.IsLower(textBox1.Text[i]))
                            {
                                textBox2.Text += eng[Math.Abs((Array.IndexOf(temp, textBox1.Text[i]) + 26 + 1 + Array.IndexOf(eng, char.ToLower(keyBoxProcessed.Text[count]))) % temp.Length)];
                            }
                            else
                            {
                                textBox2.Text += ENG[Math.Abs((Array.IndexOf(TEMP, textBox1.Text[i]) + 26 + 1 + Array.IndexOf(ENG, char.ToUpper(keyBoxProcessed.Text[count]))) % TEMP.Length)];
                            }
                        }
                        if (Rus == true)
                        {
                            if (char.IsLower(textBox1.Text[i]))
                            {
                                textBox2.Text += rus[Math.Abs((Array.IndexOf(temp, textBox1.Text[i]) + 33 + 1 + Array.IndexOf(rus, char.ToLower(keyBoxProcessed.Text[count]))) % temp.Length)];
                            }
                            else
                            {
                                textBox2.Text += RUS[Math.Abs((Array.IndexOf(TEMP, textBox1.Text[i]) + 33 + 1 + Array.IndexOf(RUS, char.ToUpper(keyBoxProcessed.Text[count]))) % TEMP.Length)];
                            }
                        }
                        count++;
                    }
                    else
                    {
                        textBox2.Text += textBox1.Text[i];
                    }
                }
            }

            listBox2.Items.Add(textBox1.Text);
            listBox2.Items.Add(textBox2.Text);
            listBox1.Items.Add(alphabetKey.Text);
            listBox3.Items.Add(keyBox.Text);
        }
        /*----------------------------------------------------------------------------------------------------------------------------------*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = ": " + textBox1.TextLength;

            if (textBox1.TextLength > 0)
            {
                alphabetKey.Enabled = true;

                if(!char.IsLetter(textBox1.Text[0]))
                {
                    textBox1.Clear();
                    alphabetKey.Enabled = false;
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
                alphabetKey.Enabled = false;
                Eng = false;
                Rus = false;
            }
            label4.Text = ": " + textBox1.TextLength;
        }

        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            label7.Text = keyBox.TextLength.ToString();
            keyBoxProcessed.Clear();

            if (keyBox.TextLength > 0)
            {
                label11.Visible = false;
                button2.Enabled = true;

                for (int i = 0; i < keyBox.TextLength; i++)
                {
                    if (char.IsLetter(keyBox.Text[i]))
                    {
                        if (rus.Contains(keyBox.Text[i]) || RUS.Contains(keyBox.Text[i]))
                        {
                            if (Eng == true)
                            {
                                keyBox.Clear();
                                button2.Enabled = false;
                                label11.Visible = true;
                                break;
                            }
                        }

                        if (eng.Contains(keyBox.Text[i]) || ENG.Contains(keyBox.Text[i]))
                        {
                            if (Rus == true)
                            {
                                keyBox.Clear();
                                button2.Enabled = false;
                                label11.Visible = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        keyBox.Clear();
                        button2.Enabled = false;
                        label11.Visible = true;
                        break;
                    }
                }
            }
            else
            {
                label11.Visible = true;
                button2.Enabled = false;
            }

            label7.Text = keyBox.TextLength.ToString();
        }

        private void keyBoxProcessed_TextChanged(object sender, EventArgs e)
        {
            label9.Text = keyBoxProcessed.TextLength.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            alphabetKey.Clear();
            alphabetKey.Text += listBox1.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            alphabetKey.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
            Eng = false;
            Rus = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupAlberti.Enabled = true;
            button2.Text = "Зашифровать";
            textBox1.Clear();
            keyBox.Clear();
            alphabetKey.Clear();
            keyBoxProcessed.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupAlberti.Enabled = true;
            button2.Text = "Расшифровать";
            textBox1.Clear();
            keyBox.Clear();
            alphabetKey.Clear();
            keyBoxProcessed.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //keyBox.Clear();
            //keyBox.Text += listBox3.SelectedItem;
        }

        private void keyBox_TextChanged_1(object sender, EventArgs e)
        {
           

        }

        public string alphabet_processing(string alph) // функция-обработчик алфавита
        {
            string tempo = null;

            alph.ToLower(); 
            tempo = alph[0].ToString();

            if (alph.Length > 1)
            {
                for (int i = 0; i < alph.Length; i++)
                {
                    if (!tempo.Contains(alph[i]))
                    {
                        tempo += alph[i];       //вносим ключ в начало нового алфавита (без повторов!)
                    }                           //Позже по порядку вписываем оставшиеся буквы из алфавита, если они еще не присутствуют в новом алфавите
                }
                alph = tempo;
            }

            if(Eng == true)
            {
                for(int i = 0; i < 26; i++)
                {
                    if(!alph.Contains(eng[i]))
                    {
                        alph += eng[i];
                    }
                }
            }
            if (Rus == true)
            {
                for (int i = 0; i < 33; i++)
                {
                    if (!alph.Contains(rus[i]))
                    {
                        alph += rus[i];
                    }
                }
            }
            return alph;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label5.Text = ": " + alphabetKey.TextLength;

            keyBox.Clear();
            if (alphabetKey.TextLength > 0)
            {
                label10.Visible = false;
                keyBox.Enabled = true;

                for (int i = 0; i < alphabetKey.TextLength; i++)
                {
                    if (char.IsLetter(alphabetKey.Text[i]))
                    {
                        if (rus.Contains(alphabetKey.Text[i]) || RUS.Contains(alphabetKey.Text[i]))
                        {
                            if (Eng == true)
                            {
                                alphabetKey.Clear();
                                keyBox.Enabled = false;
                                label10.Visible = true;
                                break;
                            }
                        }

                        if (eng.Contains(alphabetKey.Text[i]) || ENG.Contains(alphabetKey.Text[i]))
                        {
                            if (Rus == true)
                            {
                                alphabetKey.Clear();
                                keyBox.Enabled = false;
                                label10.Visible = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        alphabetKey.Clear();
                        keyBox.Enabled = false;
                        label10.Visible = true;
                        break;
                    }
                }
            }
            else
            {
                label10.Visible = true;
                keyBox.Enabled = false;
                button2.Enabled = false;
            }
            label5.Text = ": " + alphabetKey.TextLength;

        }
    }
}
