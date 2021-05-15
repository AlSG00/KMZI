﻿using System;
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
    public partial class Hill : Form
    {
        public Hill()
        {
            InitializeComponent();

            groupHill.Enabled = false;
            button2.Enabled = false;
            keyBox.Enabled = false;
        }

        Random rnd = new Random();
        char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
                            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
                            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                            '.', ',', '!', '?', '@', '#', '$', '%', ' '};

        int[] symbol_pos; // позиция в которой стоит необрабатываемый символ
        char[] symbol; // для необрабатываемых символов, которые будут возвращены в текст
        decimal[,] key; //ключ в виде матрицы
        double[,] key_algebraic;//матрица алгебраических дополнений
        double[,] key_answer; //ключ для расшифрования
        //int[] _key; //ключ, но не матрица, а строка
        decimal[,] minor; // минор для нахождения обратной матрицы
        int count;

        int mod = 127;
        decimal determinant = 0;

        // Генерация ключа
        public decimal[,] generate_key()
        {
            keyBoxProcessed.Clear();
            // Высчитываем необходимый размер матрицы, чтобы вместить ключ
            while (Math.Pow(Convert.ToDouble(count), 2) < keyBox.TextLength)
            {
                count++;
            }
            key = new decimal[count, count];
            int[] _key = new int[count * count];

            // Дополняем исходный текст до нужной длины
            while (textBox1.TextLength % count != 0) 
            {
                textBox1.Text += ' ';
            }

            symbol = new char[textBox1.TextLength];
            symbol_pos = new int[textBox1.TextLength];

            count = 0;

            // Дополняем слово-ключ до нужной длины
            for (int i = 0; i < key.Length; i++)
            {
                keyBoxProcessed.Text += keyBox.Text[i % keyBox.TextLength]; 
            }

            // Меняем символы в ключе на их позиции в алфавите
            for (int i = 0; i < key.Length; i++)
            {
                if (alphabet.Contains(keyBoxProcessed.Text[i]))
                {
                    _key[count] = Array.IndexOf(alphabet, keyBoxProcessed.Text[i]);
                    count++;
                }
                else
                {
                    _key[count] = mod;
                    count++;
                }
            }
            count = 0;
            // Заполняем матрицу-ключ полученными позициями символов
            for (int i = 0; i < key.GetLength(0); i++)
            {
                for (int j = 0; j < key.GetLength(1); j++)
                {
                    key[i, j] = Convert.ToDecimal(_key[count]);
                    count++;
                }
            }

            // Вычисление определителя ключа. Он должен быть ненулевой
            determinant = Determinant(key); 
            if (determinant == 0)
            {
                MessageBox.Show("Выберите другой ключ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                key = null;
                return key;
            }
            else
            {
                return key;
            }
        }

        // Кнопка "Зашифровать / Расшифровать"
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            decimal[] vector = null;
            decimal[] vector_ans = null;
            count = 0;
            minor = null;

            // Шифрование
            if (radioButton1.Checked == true)
            {
                count = 0;
                keyBoxProcessed.Clear();
                key_algebraic = null;
                key_answer = null;
                //_key = null;

                // Генерируем ключ
                key = generate_key();

                // Если ключ отсутствует, то программа дальше не работает
                if(key == null)
                {
                    return;
                }
                
                // Преобразуем весь введенный текст в численные позиции алфавита
                decimal[] text = new decimal[textBox1.TextLength];
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (alphabet.Contains(textBox1.Text[i]))
                    {
                        text[i] = Array.IndexOf(alphabet, textBox1.Text[i]);
                    }
                    else
                    {
                        text[i] = mod;                  // если встретился необрабатываемый символ
                        symbol_pos[i] = 1;              // запомним, где он стоит
                        symbol[i] = textBox1.Text[i];   // и запомним, что конкретно стоит
                    }
                }

                count = 0;
                // Сам алгоритм
                for (int i = 0; i < textBox1.TextLength; i += key.GetLength(0))
                {
                    vector_ans = new decimal[key.GetLength(0)];
                    if (i + key.GetLength(0) < textBox1.TextLength)
                    {
                        vector = new decimal[key.GetLength(0)]; // Создается вектор
                        for (int j = 0; j < key.GetLength(0); j++)
                        {
                            vector[j] = text[i + j]; // В вектор заносится часть текста
                        }
                        for (int m = 0; m < key.GetLength(0); m++)
                        {
                            for (int n = 0; n < key.GetLength(0); n++)
                            {
                                vector_ans[m] += vector[n] * key[n, m]; // Вектор перемножается на ключ...
                            }
                            vector_ans[m] %= mod; // ...и берется по модулю
                        }
                        for (int j = 0; j < key.GetLength(0); j++)
                        {
                            text[i + j] = vector_ans[j]; // переносим шифротекст в поле вывода
                        }
                    }
                    else // Если исходного текста не хватает на дополнение вектора
                    {
                        vector = new decimal[key.GetLength(0)];
                        for (int j = i; j < textBox1.TextLength; j++)
                        {
                            vector[count] = text[j];
                            count++;
                        }
                        for (int j = count; j < vector.Length; j++)
                        {
                            vector[count] = mod;
                            count++;
                        }
                        for (int m = 0; m < key.GetLength(0); m++)
                        {
                            for (int n = 0; n < key.GetLength(0); n++)
                            {
                                vector_ans[m] += vector[n] * key[n, m];
                            }
                            vector_ans[m] %= mod;
                        }
                        for (int j = 0; j < key.GetLength(0) - ((i + key.GetLength(0)) % text.Length); j++)
                        {
                            text[i + j] = vector_ans[j];
                        }
                    }
                }
                for (int i = 0; i < text.Length; i++)
                {
                    textBox2.Text += alphabet[Convert.ToInt32(text[i])]; // Подставляем буквы вместо чисел
                }
            }

            // Расшифрование
            if (radioButton2.Checked == true)
            {               
                if(key == null)
                {
                    key = generate_key(); 
                }
                if(key == null)
                {
                    return;
                }

                // найдём обратное к определителю
                // Такое число, которое перемноженное на детерминант по модулю дает в остатке 1
                count = 0;
                while (determinant < 0)
                {
                    determinant += mod;
                }
                while ((Convert.ToDecimal(count) * determinant) % mod != 1)
                {
                    count++;
                }
                decimal determinant_inversive = count; // обратное к определителю
                count = 0;

                // Стряпаем миноры
                key_algebraic = new double[key.GetLength(0), key.GetLength(1)];
                for (int i = 0; i < key.GetLength(0); i++)
                {
                    for (int j = 0; j < key.GetLength(1); j++)
                    {
                        minor = new decimal[key.GetLength(0) - 1, key.GetLength(1) - 1]; // создаем минор
                        count = 0;
                        int a = 0;
                        int b = 0;
                        for (int m = 0; m < key.GetLength(0); m++)
                        {
                            for (int n = 0; n < key.GetLength(1); n++)
                            {
                                if (m != i && n != j)
                                {
                                    if (b < minor.GetLength(1))
                                    {
                                        minor[a, b] = key[m, n]; //набиваем минор числами
                                        b++;
                                    }
                                    else
                                    {
                                        a++;
                                        b = 0;
                                        minor[a, b] = key[m, n]; //набиваем минор числами
                                        b++;
                                    }
                                }
                            }
                        }
                        decimal test = Determinant(minor); // Дополнительный минор (определитель минора)
                        while (test < 0)
                        {
                            test += mod;
                        }

                        // Теперь сделаем алгебраическое дополнение...
                        key_algebraic[i, j] = Convert.ToDouble(((Convert.ToDecimal(Math.Pow(-1, i + j)) * test)) % mod);
                        while (key_algebraic[i, j] < 0) //...и снова возьмем по модулю
                        {
                            key_algebraic[i, j] += mod;
                        }
                    }
                }
                /*закончили стряпать миноры-------------------------*/

                key_algebraic = trans(key_algebraic); // Затранспонировали ключ относительно главной диагонали
                key_answer = new double[key.GetLength(0), key.GetLength(1)];

                // Находим обратную матрицу. Она будет ключом расшифровки
                for (int i = 0; i < key.GetLength(0); i++)
                {
                    for (int j = 0; j < key.GetLength(1); j++)
                    {
                        key_answer[i, j] = Convert.ToDouble(Convert.ToDecimal(key_algebraic[i, j]) * determinant_inversive % mod);
                    }
                } // Получили матрицу для расшифровки

                decimal[] text = new decimal[textBox1.TextLength];
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (alphabet.Contains(textBox1.Text[i]))
                    {
                        text[i] = Array.IndexOf(alphabet, textBox1.Text[i]);
                    }
                    else
                    {
                        text[i] = mod; // необрабатываемый символ
                    }
                }

                count = 0;

                /*Далее, как в шифровании, разбиваем текст на векторы 
                 *и перемножаем на получившуюся обратную матрицу*/
                for (int i = 0; i < textBox1.TextLength; i += key.GetLength(0))
                {
                    vector_ans = new decimal[key.GetLength(0)];
                    if (i + key.GetLength(0) < textBox1.TextLength)
                    {
                        vector = new decimal[key.GetLength(0)];
                        for (int j = 0; j < key.GetLength(0); j++)
                        {
                            vector[j] = text[i + j];
                        }
                        for (int m = 0; m < key.GetLength(0); m++)
                        {
                            for (int n = 0; n < key.GetLength(0); n++)
                            {
                                vector_ans[m] += vector[n] * Convert.ToInt32(key_answer[n, m]);
                            }
                            vector_ans[m] %= mod;
                        }
                        for (int j = 0; j < key.GetLength(0); j++)
                        {
                            text[i + j] = vector_ans[j];
                        }
                    }
                    else
                    {
                        vector = new decimal[key.GetLength(0)];
                        for (int j = i; j < textBox1.TextLength; j++)
                        {
                            vector[count] = text[j];
                            count++;
                        }
                        for (int j = count; j < vector.Length; j++)
                        {
                            vector[count] = mod;
                            count++;
                        }
                        for (int m = 0; m < key.GetLength(0); m++)
                        {
                            for (int n = 0; n < key.GetLength(0); n++)
                            {
                                vector_ans[m] += vector[n] * Convert.ToInt32(key_answer[n, m]);
                            }
                            vector_ans[m] %= mod;
                        }
                        for (int j = 0; j < key.GetLength(0) - ((i + key.GetLength(0)) % text.Length); j++)
                        {
                            text[i + j] = vector_ans[j];
                        }
                    }
                }

                // Численнные результаты расшифрования меняем на символьные
                for (int i = 0; i < text.Length; i++)
                {
                    if (symbol_pos[i] == 1)
                    {
                        textBox2.Text += symbol[i];
                    }
                    else
                    {
                        textBox2.Text += alphabet[Convert.ToInt32(text[i])];
                    }
                }

                for (int i = 0; i < symbol.Length; i++)
                {
                    if (symbol_pos[i] == 1)
                    {
                        textBox2.Text[i].ToString().Replace(textBox2.Text[i], symbol[i]);
                    }
                }
            }
            listBox2.Items.Add(textBox1.Text);
            listBox2.Items.Add(textBox2.Text);
            listBox1.Items.Add(keyBox.Text);
        }

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            keyBox.Clear();
            keyBox.Enabled = false;
            button2.Enabled = false;
        }

        // Кнопка "Очистить историю"
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Если изменилось состояние радиальной кнопки "Зашифровать"
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
            button2.Text = "Зашифровать";
            groupHill.Enabled = true;
        }

        // Если изменилось состояние радиальной кнопки "Расшифровать"
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            keyBox.Clear();
            keyBoxProcessed.Clear();
            button2.Text = "Расшифровать";
            groupHill.Enabled = true;
        }

        // Если изменилось содержимое поля ввода шифроемого сообщения
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

        // Если изменилось содержимое поля ввода ключа
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

        // Транспонирование относительно главной диагонали
        double[,] trans(double[,] array)
        {
            double[,] temp = new double[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    temp[i, j] = array[i, j];
                }
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = temp[j, i];
                }
            }

            return array;
        }

        /*Сторонняя функция вычисления определителя матрицы*/
        private Decimal Determinant(Decimal[,] array)
        {
            int n = (int)Math.Sqrt(array.Length);

            if (n == 1)
            {
                return array[0, 0];
            }

            Decimal det = 0;

            for (int k = 0; k < n; k++)
            {
                det += array[0, k] * Cofactor(array, 0, k);
            }

            return det;
        }

        private Decimal Cofactor(Decimal[,] array, int row, int column)
        {
            return Convert.ToDecimal(Math.Pow(-1, column + row)) * Determinant(Minor(array, row, column));
        }

        /*Сторонняя функция для вычисления минора матрицы.
          Используется только для функционирования тредуемой функции*/
        private Decimal[,] Minor(Decimal[,] array, int row, int column)
        {
            int n = (int)Math.Sqrt(array.Length);
            Decimal[,] minor = new Decimal[n - 1, n - 1];

            int _i = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == row)
                {
                    continue;
                }
                int _j = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }
                    minor[_i, _j] = array[i, j];
                    _j++;
                }
                _i++;
            }
            return minor;
        }

        // Выбран элемент из истории сообщений
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text += listBox2.SelectedItem;
        }

        // Выбран элемент из Истории ключей
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            keyBox.Clear();
            keyBox.Text += listBox1.SelectedItem;
        }
    }
}
