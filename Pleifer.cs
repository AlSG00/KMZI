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
    public partial class Pleifer : Form
    {
        public Pleifer()
        {
            InitializeComponent();
            button2.Enabled = false;
            groupPleifer.Enabled = false;
            keyBox.Enabled = false;
        }
        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        char[] RUS = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char[] ENG = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        char[,] m_rus = { { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з' },
                          { 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п' },
                          { 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч' },
                          { 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' } };                                     // ОПИСАНИЕ
                                                                                                // Идём по введённой строчке и запоминаем координаты найденных символов
        char[,] m_RUS = { { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З' },
                          { 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П' },
                          { 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч' },
                          { 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' } };

        char[,] m_eng = { { 'a', 'b', 'c', 'd', 'e' },
                          { 'f', 'g', 'h', 'i', 'k' },
                          { 'l', 'm', 'n', 'o', 'p' },
                          { 'q', 'r', 's', 't', 'u' },
                          { 'v', 'w', 'x', 'y', 'z' } };

        char[,] m_ENG = { { 'A', 'B', 'C', 'D', 'E' },
                          { 'F', 'G', 'H', 'I', 'K' },
                          { 'L', 'M', 'N', 'O', 'P' },
                          { 'Q', 'R', 'S', 'T', 'U' },
                          { 'V', 'W', 'X', 'Y', 'Z' } };


        bool Eng = false;
        bool Rus = false;
        bool contains = false;
        bool get1 = false;
        bool get2 = false;


        char[,] alphabet_temp = null;
        char[,] alphabet_current = null;
        char[] non_alphabet = null;
        int[] non_alphabet_pos = null;
        char[] non_alphabet_temp = null;
        int[] non_alphabet_pos_temp = null;
        int[] position_of_spaceButton = null;
        int[] position_of_spaceButton_temp = null;
        int[] position_of_upper = null;
        int[] position_of_upper_temp = null;
        int[] position_of_x = null;
        int[] position_of_x_temp = null;
        int[] pos1 = null;
        int[] pos2 = null;
        string temp = null;
        string symbol = null;

        int count = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            processBox.Clear();
            alphabet_current = null;
            position_of_spaceButton_temp = null;
            position_of_upper_temp = null;
            position_of_x_temp = null;
            non_alphabet_pos_temp = null;
            non_alphabet_temp = null;

            temp = null;
            pos1 = new int[2];
            pos2 = new int[2];
            count = 0;

            processBox.Text = textBox1.Text;
            keyBox.Text = replace_missing_letters(keyBox.Text);//в ключе заменяем буквы, который нет в текущем алфивите
            processBox.Text = replace_missing_letters(textBox1.Text);// то же и в самом тексте

            alphabet_temp = null;

            if (Eng == true)
            {
                alphabet_current = m_eng;
                symbol = "x"; //английская х
            }
            else
            {
                alphabet_current = m_rus;
                symbol = "х"; // русская х
            }

            for (int i = 0; i < alphabet_current.GetLength(0); i++)
            {
                for (int j = 0; j < alphabet_current.GetLength(1); j++)
                {
                    temp += alphabet_current[i, j].ToString();
                }
            }

            temp = alphabet_processing(keyBox.Text.ToLower());//обработка алфавита в стиле альберти

            alphabet_temp = new char[alphabet_current.GetLength(0), alphabet_current.GetLength(1)];

            for (int i = 0; i < alphabet_current.GetLength(0); i++)
            {
                for (int j = 0; j < alphabet_current.GetLength(1); j++)
                {
                    alphabet_temp[i, j] = temp[count];
                    count++;
                }
            }


            /*Шифрование--------------------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton1.Checked == true)
            {
                //alphabet_temp = null;
                position_of_spaceButton = null;
                non_alphabet = null;
                non_alphabet_pos = null;
                position_of_upper = null;
                position_of_x = null;
                contains = false;

                //if (Eng == true)
                //{
                //    alphabet_current = m_eng;
                //    symbol = "x"; //английская х
                //}
                //else
                //{
                //    alphabet_current = m_rus;
                //    symbol = "х"; // русская х
                //}

                //for (int i = 0; i < alphabet_current.GetLength(0); i++)
                //{
                //    for (int j = 0; j < alphabet_current.GetLength(1); j++)
                //    {
                //        temp += alphabet_current[i, j].ToString();
                //    }
                //}

                //temp = alphabet_processing(keyBox.Text.ToLower());//обработка алфавита в стиле альберти

                //alphabet_temp = new char[alphabet_current.GetLength(0), alphabet_current.GetLength(1)];

                //for (int i = 0; i < alphabet_current.GetLength(0); i++)
                //{
                //    for (int j = 0; j < alphabet_current.GetLength(1); j++)
                //    {
                //        alphabet_temp[i, j] = temp[count];
                //        count++;
                //    }
                //}


                for (int i = 0; i < processBox.TextLength; i++)
                {
                    contains = false;
                    for (int m = 0; m < alphabet_temp.GetLength(0); m++)
                    {
                        for (int n = 0; n < alphabet_temp.GetLength(1); n++)
                        {
                            if (char.ToLower(processBox.Text[i]) == alphabet_temp[m, n] || processBox.Text[i] == ' ')
                            {
                                contains = true;
                                break;
                            }
                        }
                        if (contains == true)
                        {
                            break;
                        }
                    }
                    if (contains == false)
                    {
                        if (non_alphabet == null)
                        {
                            non_alphabet = new char[1];
                            non_alphabet[0] = processBox.Text[i];
                            non_alphabet_pos = new int[1];
                            non_alphabet_pos[0] = i;
                        }
                        else
                        {
                            non_alphabet_temp = new char[non_alphabet.Length];
                            non_alphabet_temp = non_alphabet;
                            non_alphabet = new char[non_alphabet.Length + 1];
                            non_alphabet_temp.CopyTo(non_alphabet, 0);
                            non_alphabet[non_alphabet.Length - 1] = processBox.Text[i];

                            non_alphabet_pos_temp = new int[non_alphabet_pos.Length];
                            non_alphabet_pos_temp = non_alphabet_pos;
                            non_alphabet_pos = new int[non_alphabet_pos.Length + 1];
                            non_alphabet_pos_temp.CopyTo(non_alphabet_pos, 0);
                            non_alphabet_pos[non_alphabet_pos.Length - 1] = i;
                        }
                    }
                }
                //for (int i = non_alphabet_pos.Length - 1; i >= 0; i--)
                //{
                //    textBox2.Text = textBox2.Text.Remove(non_alphabet_pos[i], 1);
                //}
                if (non_alphabet != null)
                {
                    for (int i = 0; i < non_alphabet.Length; i++)
                    {
                        processBox.Text = processBox.Text.Replace(non_alphabet[i].ToString(), "");
                    }
                }

                for (int i = 0; i < processBox.TextLength; i++)// запоминаем позиции
                {                   
                    if (processBox.Text[i] == ' ')                   //запоминаем  позиции пробелов в тексте с применением расширения массива с использованием временного массива
                    {
                        if (position_of_spaceButton == null)
                        {
                            position_of_spaceButton = new int[1];
                            position_of_spaceButton[0] = i;
                        }
                        else
                        {
                            position_of_spaceButton_temp = new int[position_of_spaceButton.Length];
                            position_of_spaceButton_temp = position_of_spaceButton;
                            position_of_spaceButton = new int[position_of_spaceButton.Length + 1];
                            position_of_spaceButton_temp.CopyTo(position_of_spaceButton, 0);
                            position_of_spaceButton[position_of_spaceButton.Length - 1] = i;
                        }
                    }
                    
                    if(char.IsUpper(processBox.Text[i]))
                    {
                        if (position_of_upper == null)
                        {
                            position_of_upper = new int[1];
                            position_of_upper[0] = i;
                        }
                        else
                        {
                            position_of_upper_temp = new int[position_of_upper.Length];
                            position_of_upper_temp = position_of_upper;
                            position_of_upper = new int[position_of_upper.Length + 1];
                            position_of_upper_temp.CopyTo(position_of_upper, 0);
                            position_of_upper[position_of_upper.Length - 1] = i;
                        }
                    }
                }

                processBox.Text = processBox.Text.Replace(" ", "");
                processBox.Text = processBox.Text.ToLower();

               // if (processBox.TextLength != 2)           /*додумать функцию по вставке биграмм*/
               // {
                    for (int i = 0; i < processBox.TextLength; i += 2)//вставляем х в биграммы
                    {
                        if (i + 1 < processBox.TextLength)
                        {
                            if (processBox.Text[i] == processBox.Text[i + 1])
                            {
                                processBox.Text = processBox.Text.Insert(i + 1, symbol);

                                if (position_of_x == null)
                                {
                                    position_of_x = new int[1];
                                    position_of_x[0] = i + 1;
                                }
                                else
                                {
                                    position_of_x_temp = new int[position_of_x.Length];
                                    position_of_x_temp = position_of_x;
                                    position_of_x = new int[position_of_x.Length + 1];
                                    position_of_x_temp.CopyTo(position_of_x, 0);
                                    position_of_x[position_of_x.Length - 1] = i + 1;
                                }
                            }
                        }
                        else
                        {
                            if (processBox.TextLength % 2 != 0)
                            {
                                processBox.Text = processBox.Text.Insert(processBox.TextLength, symbol);


                                if (position_of_x == null)
                                {
                                    position_of_x = new int[1];
                                    position_of_x[0] = processBox.TextLength - 1;
                                }
                                else
                                {
                                    position_of_x_temp = new int[position_of_x.Length];
                                    position_of_x_temp = position_of_x;
                                    position_of_x = new int[position_of_x.Length + 1];
                                    position_of_x_temp.CopyTo(position_of_x, 0);
                                    position_of_x[position_of_x.Length - 1] = processBox.TextLength - 1;
                                }
                            }
                            break;
                        }
                    }
               // }
                //else
                //{
                //    if(processBox.Text[0] == processBox.Text[1])
                //    {
                //        processBox.Text = processBox.Text.Insert(1, symbol);
                //        processBox.Text = processBox.Text.Insert(processBox.TextLength, symbol);
                //        position_of_x = new int[2];
                //        position_of_x[0] = 1;
                //        position_of_x[1] = 3;
                //    }
                //}


                for (int n = 0; n < processBox.TextLength; n += 2)//процесс шифрования
                {
                    for (int i = 0; i < alphabet_temp.GetLength(0); i++)
                    {
                        for (int j = 0; j < alphabet_temp.GetLength(1); j++)
                        {
                            if (processBox.Text[n] == alphabet_temp[i, j])
                            {
                                pos1[0] = i;
                                pos1[1] = j;
                                get1 = true;
                            }
                            if (processBox.Text[n + 1] == alphabet_temp[i, j])
                            {
                                pos2[0] = i;
                                pos2[1] = j;
                                get2 = true;
                            }
                            if (get1 == true && get2 == true)
                            {
                                if (pos1[0] == pos2[0] && pos1[1] != pos2[1])
                                {
                                    textBox2.Text += alphabet_temp[pos1[0], (pos1[1] + 1) % alphabet_temp.GetLength(1)];
                                    textBox2.Text += alphabet_temp[pos2[0], (pos2[1] + 1) % alphabet_temp.GetLength(1)];
                                }
                                if (pos1[0] != pos2[0] && pos1[1] == pos2[1])
                                {                                 
                                    textBox2.Text += alphabet_temp[(pos1[0] + 1) % alphabet_temp.GetLength(0), pos1[1]];
                                    textBox2.Text += alphabet_temp[(pos2[0] + 1) % alphabet_temp.GetLength(0), pos2[1]];
                                }
                                if (pos1[0] != pos2[0] && pos1[1] != pos2[1])
                                {
                                    textBox2.Text += alphabet_temp[pos1[0], pos2[1]];
                                    textBox2.Text += alphabet_temp[pos2[0], pos1[1]];
                                }
                                break;
                            }
                        }
                        if (get1 == true && get2 == true)
                        {
                            get1 = false;
                            get2 = false;
                            break;
                        }
                    }
                }
            }
/*Расшифрование-----------------------------------------------------------------------------------------------------------------------------------*/
            if (radioButton2.Checked == true)
            {
                processBox.Text = textBox1.Text;

                for (int n = 0; n < processBox.TextLength; n += 2)
                {
                    for (int i = 0; i < alphabet_temp.GetLength(0); i++)
                    {
                        for (int j = 0; j < alphabet_temp.GetLength(1); j++)
                        {
                            if (processBox.Text[n] == alphabet_temp[i, j])
                            {
                                pos1[0] = i;
                                pos1[1] = j;
                                get1 = true;
                            }
                            if (processBox.Text[n + 1] == alphabet_temp[i, j])
                            {
                                pos2[0] = i;
                                pos2[1] = j;
                                get2 = true;
                            }
                            if (get1 == true && get2 == true)
                            {
                                if (pos1[0] == pos2[0] && pos1[1] != pos2[1])
                                {
                                    textBox2.Text += alphabet_temp[pos1[0], (pos1[1] - 1 + alphabet_temp.GetLength(1)) % alphabet_temp.GetLength(1)];
                                    textBox2.Text += alphabet_temp[pos2[0], (pos2[1] - 1 + alphabet_temp.GetLength(1)) % alphabet_temp.GetLength(1)];
                                }
                                if (pos1[0] != pos2[0] && pos1[1] == pos2[1])
                                {
                                    textBox2.Text += alphabet_temp[(pos1[0] - 1 + alphabet_temp.GetLength(0)) % alphabet_temp.GetLength(0), pos1[1]];
                                    textBox2.Text += alphabet_temp[(pos2[0] - 1 + alphabet_temp.GetLength(0)) % alphabet_temp.GetLength(0), pos2[1]];
                                }
                                if (pos1[0] != pos2[0] && pos1[1] != pos2[1])
                                {
                                    textBox2.Text += alphabet_temp[pos1[0], pos2[1]];
                                    textBox2.Text += alphabet_temp[pos2[0], pos1[1]];
                                }
                                break;
                            }
                        }
                        if (get1 == true && get2 == true)
                        {
                            get1 = false;
                            get2 = false;
                            break;
                        }
                    }
                }

                if (position_of_x != null)
                {
                    for (int i = position_of_x.Length - 1; i >= 0; i--)
                    {
                        textBox2.Text = textBox2.Text.Remove(position_of_x[i], 1);
                    }
                }

                if (position_of_spaceButton != null)
                {
                    for (int i = 0; i < position_of_spaceButton.Length; i++)
                    {
                        textBox2.Text = textBox2.Text.Insert(position_of_spaceButton[i], " ");
                    }
                }

               
                if (position_of_upper != null)
                {
                    count = 0;
                    string str = textBox2.Text;
                    textBox2.Clear();
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i == position_of_upper[count])
                        {
                            if (count < position_of_upper.Length - 1)
                            {
                                count++;
                            }
                            textBox2.Text += str[i].ToString().ToUpper();
                        }
                        else
                        {
                            textBox2.Text += str[i];
                        }

                    }
                }

                if (non_alphabet != null)
                {
                    for (int i = 0; i < non_alphabet_pos.Length; i++)
                    {
                        textBox2.Text = textBox2.Text.Insert(non_alphabet_pos[i], non_alphabet[i].ToString());
                    }
                }
                
            }

            listBox1.Items.Add(keyBox.Text);
            listBox2.Items.Add(textBox1.Text);
            listBox2.Items.Add(textBox2.Text);
        }
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        public string replace_missing_letters(string str)
        {
            if(Eng == true)
            {
                str = str.Replace('j', 'i');
                str = str.Replace('J', 'I');
            }
            if(Rus == true)
            {
                str = str.Replace('ё', 'е');
                str = str.Replace('Ё', 'Е');
            }

            return str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            keyBox.Clear();
            processBox.Clear();
            keyBox.Enabled = false;
            button2.Enabled = false;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Text = "Зашифровать";
            groupPleifer.Enabled = true;
            textBox1.Clear();
            keyBox.Clear();
            processBox.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Text = "Расшифровать";
            groupPleifer.Enabled = true;
            textBox1.Clear();
            keyBox.Clear();
            processBox.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //label4.Text = ": " + textBox1.TextLength;

            if (textBox1.TextLength > 0)
            {
                
                keyBox.Enabled = true;


                if (!char.IsLetter(textBox1.Text[0]))
                {
                    textBox1.Clear();
                    keyBox.Enabled = false;
                    Eng = false;
                    Rus = false;
                    //label4.Text = ": " + textBox1.TextLength;
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

                if (radioButton2.Checked == true)
                {
                    for (int i = 0; i < textBox1.TextLength; i++)
                    {
                        if(Rus == true)
                        {
                            if(!rus.Contains(char.ToLower(textBox1.Text[i])))
                            {
                                textBox1.Clear();
                                keyBox.Enabled = false;
                                Eng = false;
                                Rus = false;
                                return;
                            }
                        }
                        if (Eng == true)
                        {
                            if (!eng.Contains(char.ToLower(textBox1.Text[i])))
                            {
                                textBox1.Clear();
                                keyBox.Enabled = false;
                                Eng = false;
                                Rus = false;
                                return;
                            }
                        }
                    }
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
                                keyBox.Clear();
                                keyBox.Enabled = false;
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
                                keyBox.Clear();
                                keyBox.Enabled = false;
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
            //label4.Text = ": " + textBox1.TextLength;
        }

        private void keyBox_TextChanged(object sender, EventArgs e)
        {
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

            if (Eng == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!alph.Contains(m_eng[i, j]))
                        {
                            alph += m_eng[i, j];
                        }
                    }
                }
            }
            if (Rus == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (!alph.Contains(m_rus[i, j]))
                        {
                            alph += m_rus[i, j];
                        }
                    }
                }
            }
            return alph;
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
    }
}
