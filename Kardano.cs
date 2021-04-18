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
    public partial class Kardano : Form
    {
        public Kardano()
        {
            InitializeComponent();

            button2.Enabled = false;
            button5.Enabled = false;

            groupBox2.Enabled = false;
            groupKardano.Enabled = false;
        }

        char[] garbage = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                       'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                       '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '_', '+', '|', '/', '*', '`', '~', '!', '@', '#', '$', '%', '^', '&',
                       '(', ')', '.', ','};

        int key_maxLength = 5;

        string[,] temp;
        string[,] grid = null;
        string[,] matrix = null;
        char[,] answer = null;
        int[,] coords = null;
        int matrix_temp_width;
        int matrix_temp_length;
        int matrix_side;
        int count;
        int count_transitions;
        bool end_of_line;
        int count_garbage;
        Random rnd = new Random();



        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();

            count_garbage = 0;
            matrix_temp_width = 0;
            matrix_temp_length = 0;
            count = 0;
            count_transitions = 0;

            answer = new char[matrix_side, matrix_side];

            for (int i = 0; i < matrix_side; i++)
            {
                for (int j = 0; j < matrix_side; j++)
                {
                    answer[i, j] += '0';
                }
            }
            end_of_line = false;

            if (textBox1.TextLength > 0)
            {
                if (radioButton1.Checked == true)/*ШИФРОВАНИЕ-------------------------------------------------------------------------------------------*/
                {
                    while (end_of_line == false)
                    {
                        for (int i = 0; i < matrix_side; i++)
                        {
                            for (int j = 0; j < matrix_side; j++)
                            {
                                if (grid[i, j] == "@")
                                {
                                    if (count < textBox1.TextLength)
                                    {
                                        answer[i, j] = textBox1.Text[count];
                                        count++;
                                    }
                                    else
                                    {
                                        if (radioButton3.Checked == true)
                                        {
                                            answer[i, j] = ' ';
                                        }
                                        else
                                        {
                                            answer[i, j] = garbage[rnd.Next(0, 82)];
                                            count_garbage++;
                                        }
                                    }
                                }
                            }
                        }
                        grid = Transposition(grid);
                        count_transitions++;

                        if (count_transitions == 4)
                        {
                            for (int i = 0; i < matrix_side; i++)
                            {
                                for (int j = 0; j < matrix_side; j++)
                                {
                                    textBox2.Text += answer[i, j];
                                }
                            }
                            answer = new char[matrix_side, matrix_side];
                            for (int i = 0; i < matrix_side; i++)
                            {
                                for (int j = 0; j < matrix_side; j++)
                                {
                                    answer[i, j] += '0';
                                }
                            }
                            count_transitions = 0;

                            if (count >= textBox1.TextLength)
                            {
                                end_of_line = true;
                            }
                        }
                    }

                    listBox2.Items.Add(textBox1.Text);
                    listBox2.Items.Add(textBox2.Text);

                }
                if (radioButton2.Checked == true) /*РАСШИФРОВКА----------------------------------------------------*/
                {
                    answer = new char[matrix_side, matrix_side];
                    for (int i = 0; i < matrix_side; i++)
                    {
                        for (int j = 0; j < matrix_side; j++)
                        {
                            answer[i, j] += '0';
                        }
                    }

                    while (end_of_line == false)
                    {
                        if (count_transitions == 0)
                        {
                            answer = new char[matrix_side, matrix_side];
                            for (int i = 0; i < matrix_side; i++)
                            {
                                for (int j = 0; j < matrix_side; j++)
                                {
                                    answer[i, j] += '0';
                                }
                            }

                            for (int i = 0; i < matrix_side; i++)
                            {
                                for (int j = 0; j < matrix_side; j++)
                                {
                                    if (count < textBox1.TextLength)
                                    {
                                        answer[i, j] = textBox1.Text[count];
                                        count++;
                                    }
                                    else
                                    {
                                        if (radioButton3.Checked == true)
                                        {
                                            answer[i, j] = ' ';
                                        }
                                        else
                                        {
                                            answer[i, j] = garbage[rnd.Next(0, 82)];
                                        }
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < matrix_side; i++)
                        {
                            for (int j = 0; j < matrix_side; j++)
                            {
                                if (grid[i, j] == "@")
                                {
                                    textBox2.Text += answer[i, j];
                                }
                            }
                        }
                        grid = Transposition(grid);
                        count_transitions++;

                        if (count_transitions == 4)
                        {
                            count_transitions = 0;

                            if (count >= textBox1.TextLength)
                            {
                                end_of_line = true;
                            }
                        }
                    }

                    listBox2.Items.Add(textBox1.Text);
                    listBox2.Items.Add(textBox2.Text);
                }      
            }
            else
            {
                MessageBox.Show("Пустое окно ввода.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                button5.Enabled = true;

                for (int i = 0; i < textBox3.TextLength; i++)
                {
                    if (!char.IsDigit(textBox3.Text[i]))
                    {
                        textBox3.Clear();
                        break;
                    }

                }
                if (textBox3.TextLength > key_maxLength)
                {
                    textBox3.Clear();
                }
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            gridBox.Clear();

            button2.Enabled = false;
            button5.Enabled = false;

            grid = null;
            matrix_side = 0;

            label5.BackColor = Color.Red;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            button2.Text = "Зашифровать";

            groupBox2.Enabled = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            button2.Text = "Расшифровать";

            groupBox2.Enabled = true;
        }

        public string[,] Transposition(string[,] array)//функция транспонирования
        {
            string[,] result = new string[array.GetLength(1), array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {

                    result[j, array.GetLength(0) - 1 - i] = array[i, j];
                }
            }

            return result;
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string[,] Generate_grid(int m_size)
        {
            int num = 0;
            int max = 0;
            int a = 0;
            int b = 0;

            string[,] array = new string[m_size, m_size];
            string[,] result = null;

            temp = null;
            count = 0;
            matrix = null;
            matrix = new string[m_size, m_size];

            for (int i = 0; i < m_size; i++)
            {
                for (int j = 0; j < m_size; j++)
                {
                    matrix[i, j] = "0";
                }
            }

            matrix_temp_width = m_size / 2;

            if (m_size % 2 == 1)
            {
                matrix_temp_length = matrix_temp_width + 1;
            }
            else
            {
                matrix_temp_length = matrix_temp_width;
            }

            temp = new string[matrix_temp_width, matrix_temp_length];

            for (int i = 0; i < matrix_temp_width; i++)      //temp
            {
                for (int j = 0; j < matrix_temp_length; j++)
                {
                    num++;
                    temp[i, j] = num.ToString();
                }
            }

            for (int i = 0; i < matrix_temp_width; i++)//левый верх
            {
                for (int j = 0; j < matrix_temp_length; j++)
                {
                    matrix[i, j] = temp[i, j];
                }
            }

            temp = Transposition(temp);

            a = 0;
            b = 0;

            for (int i = 0; i < temp.GetLength(0); i++) // правый верх
            {
                for (int j = temp.GetLength(0); j < m_size; j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            temp = Transposition(temp);

            a = 0;
            b = 0;

            for (int i = temp.GetLength(1); i < m_size; i++) // правый низ
            {
                for (int j = temp.GetLength(0); j < m_size; j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            temp = Transposition(temp);

            a = 0;
            b = 0;

            for (int i = temp.GetLength(1); i < m_size; i++) // левый низ
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            max = array.Length / 4;


            coords = new int[4 * max, 2];
            for (int c = 1; c <= max; c++)
            {
                for (int i = 0; i < matrix_side; i++)
                {
                    for (int j = 0; j < matrix_side; j++)
                    {
                        if (c == Convert.ToInt32(matrix[i, j].ToString()))
                        {
                            coords[count, 0] = i;
                            coords[count, 1] = j;
                            count++;
                        }

                    }
                }
            }

            for (int i = 0; i < m_size; i++)
            {
                for (int j = 0; j < m_size; j++)
                {
                    array[i, j] = "0";
                }
            }

            for (int i = 0; i < max; i++)//формируем решётку
            {
                int k = i * 4 + rnd.Next(0, 4);
                array[coords[k, 0], coords[k, 1]] = "@";
            }

            result = array;

            return result;
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------*/
        private void button5_Click(object sender, EventArgs e)
        {
            gridBox.Clear();
            grid = null;
            matrix_side = Convert.ToInt32(textBox3.Text.ToString());

            grid = Generate_grid(matrix_side);

            for (int i = 0; i < matrix_side; i++)//тестовый вывод
            {
                for (int j = 0; j < matrix_side; j++)
                {
                    if (grid[i, j] == "0")
                    {
                        gridBox.Text += grid[i, j] + " ";
                    }
                    else
                    {
                        gridBox.Text += "1 ";
                    }
                }
                gridBox.Text += Environment.NewLine;
            }

            button2.Enabled = true;
            label5.BackColor = Color.LimeGreen;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupKardano.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupKardano.Enabled = true;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }
    }
}
