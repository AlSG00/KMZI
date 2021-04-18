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
    public partial class Vernam : Form
    {
        public Vernam()
        {
            InitializeComponent();

            button2.Enabled = false;
            keyBox.Enabled = false;
        }

        char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
                            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
                            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };


        int count = 0;

        string text_temp = null;
        string key_temp = null;
        string answer_temp = null;

        Random rnd = new Random();

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            keyBoxProcessed.Clear();

            count = 0;
            text_temp = null;
            key_temp = null;
            answer_temp = null;

            char[] symbol = null;
            int[] symbol_pos = null;
            byte[] text_byte = null;
            string[] text_binary = null;
            byte[] key_byte = null;
            string[] key_binary = null;
            string[] answer_binary = null;
            byte[] answer_byte = null;

            key_byte = new byte[textBox1.TextLength];
            symbol = new char[textBox1.TextLength];
            symbol_pos = new int[textBox1.TextLength];
            text_byte = new byte[textBox1.TextLength];

            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (alphabet.Contains(textBox1.Text[i]))
                {
                    text_byte[i] = Convert.ToByte(Array.IndexOf(alphabet, textBox1.Text[i]));
                }
                else
                {
                    key_byte[i] = 127;
                    symbol[i] = textBox1.Text[i];
                    symbol_pos[i] = 1;
                }
            }

            text_binary = convert_to_binary(textBox1.TextLength, text_byte);

            while (keyBoxProcessed.TextLength < textBox1.TextLength)
            {
                keyBoxProcessed.Text += keyBox.Text[count % keyBox.TextLength];
                count++;
            }
            count = 0;

            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (alphabet.Contains(keyBoxProcessed.Text[i]))
                {
                    key_byte[i] = Convert.ToByte(Array.IndexOf(alphabet, keyBoxProcessed.Text[i]));
                }
                else
                {
                    keyBoxProcessed.Text = keyBoxProcessed.Text.Replace(keyBoxProcessed.Text[i], '0');
                }
            }

            key_binary = convert_to_binary(keyBoxProcessed.TextLength, key_byte);

            answer_binary = convert_xor(text_binary, key_binary);

            answer_byte = convert_to_decimal(answer_binary);

            for (int i = 0; i < answer_byte.Length; i++)
            {
                if (symbol_pos[i] == 0)
                {
                    textBox2.Text += alphabet[answer_byte[i]];
                }
                else
                {
                    textBox2.Text += symbol[i];
                }
            }


            listBox2.Items.Add(textBox1.Text);
            listBox2.Items.Add(textBox2.Text);
            listBox1.Items.Add(keyBox.Text);
        }

        string[] convert_to_binary(int length, byte[] b_text)
        {
            string[] array = new string[length];

            for (int i = 0; i < b_text.Length; i++)
            {
                array[i] = Convert.ToString(b_text[i], 2);

                while (array[i].Length < 8)
                {
                    array[i] = array[i].Insert(0, "0");
                }
            }

            return array;
        }

        public byte[] convert_to_decimal(string[] array)
        {
            byte[] answer = new byte[array.Length];
            int answer_byte = 0;

            text_temp = null;

            for(int i = 0; i < array.Length; i++)
            {
                text_temp = array[i];

                for(int j = 0; j < text_temp.Length; j++)
                {
                    if(text_temp[j] == '1')
                    {
                        answer_byte += Convert.ToByte(Math.Pow(2, 7 - j));
                    }
                }

                answer[i] = Convert.ToByte(answer_byte % alphabet.Length);
                answer_byte = 0;
            }

            return answer;
        }

        public string[] convert_xor(string[] text, string[] key)
        {
            string[] answer = new string[text.Length];
            text_temp = null;
            key_temp = null;
            answer_temp = null;

            for (int i = 0; i < text.Length; i++)
            {
                text_temp = text[i];
                key_temp = key[i];

                for (int j = 0; j < text_temp.Length; j++)
                {
                    if (text_temp[j] == key_temp[j])
                    {
                        answer_temp += "0";
                    }
                    else
                    {
                        answer_temp += "1";
                    }
                }
                answer[i] = answer_temp;

                text_temp = null;
                key_temp = null;
                answer_temp = null;
            }

            return answer;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                keyBox.Enabled = true;
            }
            else
            {
                keyBox.Enabled = false;
                button2.Enabled = false;

            }
        }

        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            if (keyBox.TextLength > 0)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
        }
    }
}
