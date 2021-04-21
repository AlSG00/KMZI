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

        int key_maxLength = 5; // макссимальная длина числа, используемого в качестве ключа
        string[,] grid = null; // Массив для решётки
        int matrix_side; // Сторона матрицы
        int count; // Просто счётчик
        Random rnd = new Random();


        // Кнопка "Зашифровать / Расшифровать"
        private void button2_Click(object sender, EventArgs e)
        {
            
              textBox2.Clear();
            count = 0;
            int count_transitions = 0;
            char[,] answer = new char[matrix_side, matrix_side];

            for (int i = 0; i < matrix_side; i++)
            {
                for (int j = 0; j < matrix_side; j++)
                {
                    answer[i, j] += '0';
                }
            }
            bool end_of_line = false;

            if (textBox1.TextLength > 0)
            {
                // Шифрование
                if (radioButton1.Checked == true)
                {
                    while (end_of_line == false)
                    {
                        for (int i = 0; i < matrix_side; i++)
                        {
                            for (int j = 0; j < matrix_side; j++)
                            {
                                if (grid[i, j] == "@") // Если нашли "прорезь"
                                {
                                    if (count < textBox1.TextLength) // Если строка ещё не закончиась
                                    {
                                        answer[i, j] = textBox1.Text[count]; // То вносим элемент сттроки в матрицу
                                        count++;
                                    }
                                    else
                                    {
                                        // Иначе наполняем недостающую часть матрицы мусором или пробелами в зависимости от выбранной опции
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
                        grid = Transposition(grid); // Транспонируем решетку для дальнейшего заполнения
                        count_transitions++;

                        if (count_transitions == 4) // Если решетка совершила полный оборот и матрица полностью заполнилась...
                        {
                            for (int i = 0; i < matrix_side; i++)
                            {
                                for (int j = 0; j < matrix_side; j++)
                                {
                                    textBox2.Text += answer[i, j]; // То выгружаем матрицу в строку-ответ
                                }
                            }
                            answer = new char[matrix_side, matrix_side]; // Саму матрицу опустошаем
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
                }

                //Расшифрование
                if (radioButton2.Checked == true)
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

                        // Вытаскиваем буквы из прорезей и записываем в textBox2 поочередно
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
                }
                listBox2.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Пустое окно ввода.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Если изменилась размерность решётки
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

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            gridBox.Clear();
            button2.Enabled = false;
            button5.Enabled = false;
            grid = null; // Удаляется решетка
            matrix_side = 0;
            label5.BackColor = Color.Red; // Индикатор становится красным
        }

        // Кнопка "Очистить историю"
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        // Изменено состояние радиальной кнопки "Шифрование"
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            button2.Text = "Зашифровать";
            groupBox2.Enabled = true;
        }

        // Изменено состояние радиальной кнопки "Расшифрование"
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            button2.Text = "Расшифровать";
            groupBox2.Enabled = true;
        }

        // Функция транспониррования матрицы
        public string[,] Transposition(string[,] array)
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
        
        // Функция генерации решётки Кардано
        public string[,] Generate_grid(int m_size)
        {
            int num = 0; // Счётчик для правильного заполнения матрицы числами от 1 до 

            int max = 0; // Максимаьное чиссло, которое может встретиться в каждой четверти большшого квадрата (вычисляется позже)
            int a = 0; // Просто счетчик
            int b = 0; // Просто счетчик

            string[,] array = new string[m_size, m_size]; // С этиим массивом будет работать
            string[,] result = null; // Результирующий массив для вывода

            string[,] temp = null; // Временный массив для маленького квадрата
            string[,] matrix = new string[m_size, m_size];
            count = 0; // Просто счетчик

            // Создали первичную матрицу и заполнили её нулями
            for (int i = 0; i < m_size; i++)
            {
                for (int j = 0; j < m_size; j++)
                {
                    matrix[i, j] = "0";
                }
            }

            // Вычисляем размеры маленького квадрата
            int matrix_temp_width = m_size / 2;
            int matrix_temp_length = 0;
            if (m_size % 2 == 1)
            {
                matrix_temp_length = matrix_temp_width + 1;
            }
            else
            {
                matrix_temp_length = matrix_temp_width;
            }

            // Создаем маленький квадрат и заполняем его числами
            temp = new string[matrix_temp_width, matrix_temp_length];
            for (int i = 0; i < matrix_temp_width; i++)
            {
                for (int j = 0; j < matrix_temp_length; j++)
                {
                    num++;
                    temp[i, j] = num.ToString();
                }
            }

            // Заполняем левый верх большого квадрата
            for (int i = 0; i < matrix_temp_width; i++)
            {
                for (int j = 0; j < matrix_temp_length; j++)
                {
                    matrix[i, j] = temp[i, j];
                }
            }

            temp = Transposition(temp); // Поворачиваем маленький квадрат
            a = 0;
            b = 0;

            // Заполняем правый верх большого квадрата
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = temp.GetLength(0); j < m_size; j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            temp = Transposition(temp); // Поворачиваем маленький квадрат
            a = 0;
            b = 0;

            //Заполняем правый низ большого квадрата
            for (int i = temp.GetLength(1); i < m_size; i++)
            {
                for (int j = temp.GetLength(0); j < m_size; j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            temp = Transposition(temp); // Поворачиваем маленький квадрат
            a = 0;
            b = 0;

            //Заполняем левый низ большого квадрата
            for (int i = temp.GetLength(1); i < m_size; i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    matrix[i, j] = temp[a, b];
                    b++;
                }
                a++;
                b = 0;
            }

            max = array.Length / 4; // Максимальное число, встречаемое в каждой четверти большого квадрата
            int[,] coords = new int[4 * max, 2]; // Массив координат для каждой четвверки чисел в большом квадрате
            for (int c = 1; c <= max; c++)
            {
                for (int i = 0; i < matrix_side; i++)
                {
                    for (int j = 0; j < matrix_side; j++)
                    {
                        // Запоминаем четыре пары координат для каждого числа в большом квадрате
                        if (c == Convert.ToInt32(matrix[i, j].ToString()))
                        {
                            coords[count, 0] = i;
                            coords[count, 1] = j;
                            count++;
                        }

                    }
                }
            }

            // Заполняем финалььный массив нулями
            for (int i = 0; i < m_size; i++)
            {
                for (int j = 0; j < m_size; j++)
                {
                    array[i, j] = "0";
                }
            }

            /*С использованием координат формируем решетку, делая случайные "прорези" в тех местах, 
             * которые отвечают требованиям формирования решётки - 
             * прорези не должны накладываться друг на друга при повороте решётки*/
            for (int i = 0; i < max; i++)
            {
                int k = i * 4 + rnd.Next(0, 4);
                array[coords[k, 0], coords[k, 1]] = "@";
            }

            result = array; // Финальный массив - решётка передается в результирующий и возвращается в main
            return result;
        }

        // Кнопка "Сгенерировать"
        private void button5_Click(object sender, EventArgs e)
        {
            gridBox.Clear();
            grid = null;
            matrix_side = Convert.ToInt32(textBox3.Text.ToString());

            grid = Generate_grid(matrix_side); // Генерируем решётку под указанный размер

            // Вывод решёттки в gridBox для наглядности
            for (int i = 0; i < matrix_side; i++)
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
            label5.BackColor = Color.LimeGreen; // Индикатор наличия решётки
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Изменено состояние радиально й кнопки "Без мусора"
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupKardano.Enabled = true;
        }

        // Изменено состояние радиально й кнопки "С мусором"
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupKardano.Enabled = true;
        }

        // Ессли выбран элемент из ListBox1
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }
    }
}
