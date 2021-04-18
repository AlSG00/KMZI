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
    public partial class Atbash : Form
    {
        bool stop = false;

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public Atbash()
        {
            InitializeComponent();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();

            for (int i = 0; i < textBox1.TextLength; i++)
            {
                stop = true;
                for (int j = 0; j < rus.Length; j++)
                {
                    if (textBox1.Text[i] == rus[j])
                    {
                        textBox2.Text += rus[rus.Length - 1 - j];//главная формула, по ней вычисляется подставляемая буква
                        stop = false;
                        break;
                    }

                    if (textBox1.Text[i] == RUS[j])
                    {
                        textBox2.Text += RUS[RUS.Length - 1 - j];
                        stop = false;
                        break;
                    }
                }
                for (int j = 0; j < eng.Length; j++)
                {
                    if (textBox1.Text[i] == eng[j])
                    {
                        textBox2.Text += eng[eng.Length - 1 - j];
                        stop = false;
                        break;
                    }
                    if (textBox1.Text[i] == ENG[j])
                    {
                        textBox2.Text += ENG[ENG.Length - 1 - j];
                        stop = false;
                        break;
                    }
                }
                if (stop == true)//если символ не встретился в алфавите, то впишем его в шифротекст без изменений
                {
                    textBox2.Text += textBox1.Text[i];
                    stop = false;
                }
            }           
            listBox1.Items.Add(textBox1.Text);
            listBox1.Items.Add(textBox2.Text);
            button3.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//история кодировок
        {
            textBox1.Clear();
            textBox1.Text += listBox1.SelectedItem;
        }

        private void button4_Click(object sender, EventArgs e)//кнопочка выхода
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)//очистить историю
        {
            listBox1.Items.Clear();
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)//очистить поля
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength > 0)
            {
                button1.Enabled = true;             //Не разрешать нажимать что попало, пока не введен текст 
                button2.Enabled = true;             //Хотя отсутствие этой функции программу не сломает
            }                                       
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }
    }
}
