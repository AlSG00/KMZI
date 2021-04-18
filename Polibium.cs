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
    public partial class Polibium : Form
    {
        bool coin;          // флаг совпадения символа с одним из алфавитов
        int[] type;         // определяет, к какому из алфавитов принадлежит встреченный символ
        string vert;        // вертикальная координата
        string hor;         // горизонтальная координата
        string cypherCoord; // запись обеих координат в одну строчку
        int[] coord;        // преобразование текстовой записи координат в числовую
        int index;

        char[,] rus = { { 'а', 'б', 'в', 'г', 'д', 'е' },
                        { 'ё', 'ж', 'з', 'и', 'й', 'к' },
                        { 'л', 'м', 'н', 'о', 'п', 'р' },
                        { 'с', 'т', 'у', 'ф', 'х', 'ц' },
                        { 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь' },
                        { 'э', 'ю', 'я', '+', '-', '/' } };                                     // ОПИСАНИЕ
                                                                                                // Идём по введённой строчке и запоминаем координаты найденных символов
        char[,] RUS = { { 'А', 'Б', 'В', 'Г', 'Д', 'Е' },                                       // Параллельно заполняем строчку, в которой будет отражаться, к какому
                        { 'Ё', 'Ж', 'З', 'И', 'Й', 'К' },                                       // алфавиту принадлежит конкретный символ
                        { 'Л', 'М', 'Н', 'О', 'П', 'Р' },                                       
                        { 'С', 'Т', 'У', 'Ф', 'Х', 'Ц' }, 
                        { 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь' }, 
                        { 'Э', 'Ю', 'Я', '+', '-', '/' } };

        char[,] eng = { { 'a', 'b', 'c', 'd', 'e', '*' }, 
                        { 'f', 'g', 'h', 'i', 'k', '0' }, 
                        { 'l', 'm', 'n', 'o', 'p', '9' }, 
                        { 'q', 'r', 's', 't', 'u', '8' }, 
                        { 'v', 'w', 'x', 'y', 'z', '7' },
                        { '1', '2', '3', '4', '5', '6' } };

        char[,] ENG = { { 'A', 'B', 'C', 'D', 'E', '*' }, 
                        { 'F', 'G', 'H', 'I', 'K', '0' }, 
                        { 'L', 'M', 'N', 'O', 'P', '9' }, 
                        { 'Q', 'R', 'S', 'T', 'U', '8' }, 
                        { 'V', 'W', 'X', 'Y', 'Z', '7' },
                        { '1', '2', '3', '4', '5', '6' } };

        public Polibium()
        {
            InitializeComponent();

            groupSkitala.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            type = new int[textBox1.TextLength];
            cypherCoord = null;
            vert = null;
            hor = null;
            index = 0;
/*Шифрование--------------------------------------------------------------------------------------------------------------------------------------------*/ 
            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    coin = false; 
                    for (int m = 0; m < 6; m++)
                    {
                        for (int n = 0; n < 6; n++)
                        {
                            if (textBox1.Text[i] == rus[m, n])      // ОПИСАНИЕ
                            {                                       // Если мы находим совпадение с алфавитом
                                vert += m.ToString();               // Запоминаем вертикальную координату
                                hor += n.ToString();                // Запоминаем горизонтальную координату
                                type[i] = 1;                        // Запоминаем алфавит, в котором найден символ
                                coin = true;                        // Ставим флаг, что символ был найден в одном из алфавитов
                                break;                              // Вываливаемся из цикла во избежание дальнейшего бесполезного шествия по алфавитам
                            }
                            if (textBox1.Text[i] == RUS[m, n])
                            {
                                vert += m.ToString();
                                hor += n.ToString();
                                type[i] = 2;
                                coin = true;
                                break;
                            }
                            if (textBox1.Text[i] == eng[m, n])
                            {
                                vert += m.ToString();
                                hor += n.ToString();
                                type[i] = 3;
                                coin = true;
                                break;
                            }
                            if (textBox1.Text[i] == ENG[m, n])
                            {
                                vert += m.ToString();
                                hor += n.ToString();
                                type[i] = 4;
                                coin = true;
                                break;
                            }
                        }
                    }
                    if (coin == false)      // Если флаг не был поднят
                    {                       // значит символ не был найден ни в одном из алфавитов,
                        type[i] = 0;        // присвоим ему уникальный тип алфавита - нулевой
                    }
                }

                cypherCoord += hor;   // Записываем все вертикальные и горизонтальные координаты
                cypherCoord += vert;  // в одну сплошную строчку
                coord = new int[cypherCoord.Length]; 

                for (int i = 0; i < cypherCoord.Length; i++)
                {
                    coord[i] = Convert.ToInt32(cypherCoord[i].ToString());  //Конвертируем строковое представление координат в числовое для дальнейшей работы
                }

                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (type[i] == 0)                                           // Если символ не был встречен в алфавите, то перенесём его без изменения.
                    {                                                           // В остальных случаях пойдём по coord и в соответствии с полученными
                        textBox2.Text += textBox1.Text[i];                      // координатами впишем новые символы, опираясь на ранее сохраненную строку
                    }                                                           // с типом алфавита, в котором был встречен шифруемый символ.
                    if (type[i] == 1)                                           
                    {
                        textBox2.Text += rus[coord[index + 1], coord[index]];
                        index += 2;
                    }
                    if (type[i] == 2) 
                    {
                        textBox2.Text += RUS[coord[index + 1], coord[index]];
                        index += 2;
                    }
                    if (type[i] == 3)
                    {
                        textBox2.Text += eng[coord[index + 1], coord[index]];
                        index += 2;
                    }
                    if (type[i] == 4)
                    {
                        textBox2.Text += ENG[coord[index + 1], coord[index]];
                        index += 2;
                    }

                }
            }
/*Расшифрование--------------------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    coin = false;
                    for (int m = 0; m < 6; m++)
                    {
                        for (int n = 0; n < 6; n++)
                        {
                            if (textBox1.Text[i] == rus[m, n])  // По принципу, схожему с шифрованием,
                            {                                   // запоминаем координаты каждого встреченного символа
                                cypherCoord += n.ToString();    // и тип алфавита, если символ у нему принадлежит
                                cypherCoord += m.ToString();    
                                type[i] = 1;
                                coin = true;
                                break;
                            }
                            if (textBox1.Text[i] == RUS[m, n])
                            {
                                cypherCoord += n.ToString();
                                cypherCoord += m.ToString();
                                type[i] = 2;
                                coin = true;
                                break;
                            }
                            if (textBox1.Text[i] == eng[m, n])
                            {
                                cypherCoord += n.ToString();
                                cypherCoord += m.ToString();
                                type[i] = 3;
                                coin = true;
                                break;
                            }
                            if (textBox1.Text[i] == ENG[m, n])
                            {
                                cypherCoord += n.ToString();
                                cypherCoord += m.ToString();
                                type[i] = 4;
                                coin = true;
                                break;
                            }
                        }
                    }
                    if (coin == false)
                    {
                        type[i] = 0;
                    }
                }

                for (int i = 0; i < cypherCoord.Length / 2; i++)        // Теперь у нас имеется сплошная строка с координатами.
                {                                                       // Мы разбиваем её пополам:
                    hor += cypherCoord[i];                              // первая половина - горизонтальные координаты,
                    vert += cypherCoord[cypherCoord.Length / 2 + i];    // вторая - вертикальные
                }

                int[] intVert = new int[vert.Length];                   // Далее, сконвертируем строковые координаты в числа,
                int[] intHor = new int[hor.Length];                     // предварительно подготовив соответствующие массивы под оба вида координат.

                for(int i = 0; i < vert.Length; i++)
                {
                    intVert[i] = Convert.ToInt32(vert[i].ToString());
                    intHor[i] = Convert.ToInt32(hor[i].ToString());
                }

                for (int i = 0; i < textBox1.TextLength; i++)           // Принцип подстановки символов практически идентичен таковому при шифровании
                {                                                               
                    if (type[i] == 0) 
                    {
                        textBox2.Text += textBox1.Text[i];
                    }
                    if (type[i] == 1) 
                    {
                        textBox2.Text += rus[intVert[index], intHor[index]];
                        index++;
                    }
                    if (type[i] == 2)
                    {
                        textBox2.Text += RUS[intVert[index], intHor[index]];
                        index++;
                    }
                    if (type[i] == 3)
                    {
                        textBox2.Text += eng[intVert[index], intHor[index]];
                        index++;
                    }
                    if (type[i] == 4) 
                    {
                        textBox2.Text += ENG[intVert[index], intHor[index]];
                        index++;
                    }

                }

            }
            listBox2.Items.Add(textBox1.Text);
            listBox2.Items.Add(textBox2.Text);
            button3.Enabled = true;
        }
/*Прочее--------------------------------------------------------------------------------------------------------------------------------------------*/

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupSkitala.Enabled = true;
            button2.Text = "Зашифровать";
            textBox1.Clear();
            textBox2.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupSkitala.Enabled = true;
            button2.Text = "Расшифровать";
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            button3.Enabled = false;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
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
    }
}
