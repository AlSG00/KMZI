using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KMZI
{
    public partial class PoliCypher : Form
    {
        public PoliCypher()
        {
            InitializeComponent();

            button6.Enabled = false;
            button5.Enabled = false;
        }

        // Один экземпляр класса соответствует одному столбцу
        class Column
        {
            public int[] occurrence_of_letters { get; set; } // Число встреченных букв
            public int count_of_letters { get; set; }        // Сумма всех встреченных букв
            public float match_index { get; set; }           // Индекс совпадений
            public float match_index_max = 0f;               
            public int max_index_shift { get; set; }         // Смещение, при котором индекс достигает наибольшего значения
        }

        class Kaziski
        {
            public List<int> match_position;
            public string template;
            public int[] match_distance;
            public int gcd;
        }

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
                       'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
                       'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                       'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                       'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                       's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        bool isRussian = false; // Является ли текст русскоязычным
        bool textError = false; // Поддается ли текст обработке
        char[] alphabet = null; // Текущий алфавит

        Column[] column = null;
        Column[] column_shifted = null;
        Kaziski[] kaziski = null;

        // Кнопка "Преобразовать"
        private void button2_Click(object sender, EventArgs e)
        {
            indexBox.Clear();
            resultBox.Clear();
            listBox1.Items.Clear();
            column_shifted = null;
            isRussian = check_language(textBox1.Text); // Смотрим, какой язык будет испоьлзоваться в дальнейшей работе

            if (textError)
            {
                MessageBox.Show("Загруженный текст не поддается обработке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Language_setup(); // Исходя из функции выше, проводим первоначальную настройку

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    numericUpDown1.Minimum = 1;
                    Match_index_method();
                    break;
                case 1:
                    numericUpDown1.Minimum = 1;
                    Autocorrelation_method();
                    break;
                case 2:
                    numericUpDown1.Minimum = 3;
                    Kaziski_test();
                    break;
                default:
                    MessageBox.Show("Выберите метод", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    break;
            }
        }

        // Функция вычисления индекса совпадений для столбца
        float Calculate_match_index(int[] letters, int count)
        {
            float index = 0f;

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] != 0)
                {
                    index += letters[i] * (letters[i] - 1);
                }
            }
            index = index / (count * (count - 1));

            return index;
        }

        float Calculate_reciprocal_match_index(int[] letters, int count, int[] letters_shifted, int count_shifted)
        {
            float reciprocal_index = 0f;

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] != 0)
                {
                    reciprocal_index += letters[i] * letters_shifted[i];
                }
            }
            reciprocal_index = reciprocal_index / (count * count_shifted);

            return reciprocal_index;
        }

        // Проверка, какой язык в тексте основной
        public bool check_language(string text)
        {
            textError = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (rus.Contains(char.ToLower(text[i])))
                    {
                        return true;
                    }
                    else if (eng.Contains(char.ToLower(text[i])))
                    {
                        return false;
                    }
                }
            }

            textError = true;
            return false;
        }

        // Установка алфавита для текущего языка
        private void Language_setup()
        {
            if (!textError)
            {
                if (isRussian)
                {
                    alphabet = rus;
                }
                else
                {
                    alphabet = eng;
                }
            }
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Кнопка "Открыть файл"
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader str = new StreamReader(openFileDialog1.FileName); // Как сохранять файлы в разные форматы??????
            textBox1.Clear();
            textBox1.Text += str.ReadToEnd();
            str.Close();
        }

        // Кнопка "Использовать ключ" (Виженер во всей красе)
        private void button5_Click(object sender, EventArgs e)
        {
            resultBox.Clear();

            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите ключ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            int count = 0;

            string key = listBox1.SelectedItem.ToString();
            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (alphabet.Contains(Char.ToLower(textBox1.Text[i])))
                {
                    resultBox.Text += alphabet[(Array.IndexOf(alphabet, textBox1.Text[i]) + alphabet.Length - Array.IndexOf(alphabet, (key[count % key.Length]))) % alphabet.Length];
                    count++;
                }
                else
                {
                    resultBox.Text += textBox1.Text[i];
                }
            }
        }

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            indexBox.Clear();
            resultBox.Clear();
            listBox1.Items.Clear();
        }

        // Кнопка "Подобрать ключи"
        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            // Вносим в строку только те символы текста, которые содержатся в алфавите
            string text = "";
            for (int x = 0; x < textBox1.TextLength; x++)
            {
                if (alphabet.Contains(Char.ToLower(textBox1.Text[x])))
                {
                    text += textBox1.Text[x];
                }
            }


            column = new Column[Convert.ToInt32(numericUpDown1.Value)];
            for (int i = 0; i < column.Length; i++)
            {
                column[i] = new Column();
                column[i].occurrence_of_letters = new int[alphabet.Length];
            }

            // Идём по каждому столбцу...
            for (int i = 0; i < column.Length; i++)
            {
                // ...считаем встречаемые буквы в каждом столбце
                for (int j = i; j < text.Length; j += column.Length)
                {
                    if (j < text.Length)
                    {
                        if (alphabet.Contains(Char.ToLower(text[j])))
                        {
                            column[i].occurrence_of_letters[Array.IndexOf(alphabet, Char.ToLower(text[j]))] += 1;
                        }
                        column[i].count_of_letters++;
                    }
                }
                // Вычисляем индекс совпадений
                column[i].match_index = Calculate_match_index(column[i].occurrence_of_letters, column[i].count_of_letters);
            }


            column_shifted = new Column[column.Length];
            for (int i = 0; i < column.Length; i++)
            {
                column_shifted[i] = new Column();
                column_shifted[i].occurrence_of_letters = new int[alphabet.Length];
            }

            // Начинаем перебор
            for (int x = 1; x < alphabet.Length; x++)
            {
                // На каждом этапе делаем сдвиг строки по Цезарю
                string temp_text = Caesar_shift(text, x);

                for (int i = 1; i < column_shifted.Length; i++)
                {
                    column_shifted[i].count_of_letters = 0;
                    for (int k = 0; k < alphabet.Length; k++)
                    {
                        column_shifted[i].occurrence_of_letters[k] = 0;
                    }

                    // Считаем встречаемые буквы в каждом столбце сдвинутого текста
                    for (int j = i; j < temp_text.Length; j += column_shifted.Length)
                    {
                        if (j < temp_text.Length)
                        {
                            if (alphabet.Contains(Char.ToLower(temp_text[j])))
                            {
                                column_shifted[i].occurrence_of_letters[Array.IndexOf(alphabet, Char.ToLower(temp_text[j]))] += 1;
                            }
                            column_shifted[i].count_of_letters++;
                        }
                    }
                    // Расчитываем взаимный индекс совпадений
                    column_shifted[i].match_index = Calculate_reciprocal_match_index(column[0].occurrence_of_letters, column[0].count_of_letters,
                                                                                     column_shifted[i].occurrence_of_letters, column_shifted[i].count_of_letters);

                    // Для каждого столбца запоминаем максимальный полученный индекс
                    if (column_shifted[i].match_index > column_shifted[i].match_index_max)
                    {
                        column_shifted[i].match_index_max = column_shifted[i].match_index;
                        column_shifted[i].max_index_shift = x;
                    }
                }
            }

            // Генерируем и выводим ключи
            for (int i = 0; i < alphabet.Length; i++)
            {
                listBox1.Items.Add(Generate_keys(i));
            }

            button5.Enabled = true;
        }

        // Фактически, шифр Цезаря для сдвига текста
        string Caesar_shift(string text, int shift)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(Char.ToLower(text[i])))
                {
                    result += alphabet[Math.Abs((Array.IndexOf(alphabet, Char.ToLower(text[i])) + alphabet.Length + shift) % alphabet.Length)];
                }
                else
                {
                    result += text[i];
                }
            }

            return result;
        }

        string Shift_cycle(string text, int shift)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                result += text[(i + shift) % text.Length];
            }

            return result;
        }

        // Генерация ключей
        string Generate_keys(int shift)
        {
            string result = "";

            result += alphabet[shift];
            for (int i = 1; i < column_shifted.Length; i++)
            {
                // По формуле генерируем ключи
                result += alphabet[(shift + alphabet.Length - column_shifted[i].max_index_shift) % alphabet.Length];
            }

            return result;
        }

        // Метод индекса совпадений
        private void Match_index_method()
        {
            string text = "";
            // Вгосим в строку только символы, содержащиеся в алфавите
            for (int x = 0; x < textBox1.TextLength; x++)
            {
                if (alphabet.Contains(Char.ToLower(textBox1.Text[x])))
                {
                    text += textBox1.Text[x];
                }
            }

            // Создаем столбцы и инициализируем переменные
            column = new Column[Convert.ToInt32(numericUpDown1.Value)];
            for (int i = 0; i < column.Length; i++)
            {
                column[i] = new Column();
                column[i].occurrence_of_letters = new int[alphabet.Length];
            }

            // Идём по каждому столбцу
            for (int i = 0; i < column.Length; i++)
            {
                //  Считаем встречаемые буквы в каждом столбце
                for (int j = i; j < text.Length; j += column.Length)
                {
                    if (j < text.Length) // возможно, стоит убрать
                    {
                        if (alphabet.Contains(Char.ToLower(text[j])))
                        {
                            column[i].occurrence_of_letters[Array.IndexOf(alphabet, Char.ToLower(text[j]))] += 1;
                        }
                        column[i].count_of_letters++;
                    }
                }
                // Вычисляем индекс совпадений для данного столбца
                column[i].match_index = Calculate_match_index(column[i].occurrence_of_letters, column[i].count_of_letters);
            }

            // Выводим полученные индексы
            indexBox.Clear();
            for (int i = 0; i < column.Length; i++)
            {
                indexBox.Text += (i + 1) + ") " + column[i].match_index.ToString() + Environment.NewLine;
            }

            button6.Enabled = true;
        }

        // Автокореляционный метод
        private void Autocorrelation_method()
        {
            // Вносим в строку только символы, которые есть в алфавите
            string text = "";
            for (int x = 0; x < textBox1.TextLength; x++)
            {
                if (alphabet.Contains(Char.ToLower(textBox1.Text[x])))
                {
                    text += textBox1.Text[x];
                }
            }

            // Запускаем цикл
            for (int x = 1; x < text.Length; x++)
            {
                float coin = 0f;
                float corr = 0f;
                string temp_text = Shift_cycle(textBox1.Text, x); // Делаем циклический сдвиг

                // Считаем совпадения сдвинутой строки с исходной
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == temp_text[i])
                    {
                        coin++;
                    }
                }

                // Вычисляем автокорелляционный коэффициент для данного сдвига
                corr = coin / (text.Length - x);
                indexBox.Text += x + ") " + corr + Environment.NewLine;
            }

            // Всё, ожно побирать ключи, открываем кнопку
            button6.Enabled = true;
        }

        // Тест Казиски
        private void Kaziski_test()
        {
            // Вносим только символы, которые содержатся в алфавите
            string text = "";
            for (int x = 0; x < textBox1.TextLength; x++)
            {
                if (alphabet.Contains(Char.ToLower(textBox1.Text[x])))
                {
                    text += textBox1.Text[x];
                }
            }

            string template = ""; // Здесь будет шаблон, совпадения с которыми будут искаться по тексту
            kaziski = new Kaziski[text.Length - Convert.ToInt32(numericUpDown1.Value) + 1];
            for (int i = 0; i < kaziski.Length; i++)
            {
                kaziski[i] = new Kaziski();
                kaziski[i].match_position = new List<int>();
            }

            // Идём по всему тексту в поисках лбых совпадений
            for (int x = 0; x < text.Length - numericUpDown1.Value + 1; x++)
            {
                // Берем в качестве шаблона блок текста
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    template += text[x + i];
                }
                kaziski[x].template = template;


                int index = text.IndexOf(template, 0);
                // Ищем и запоминаем любые вхождения шаблона в строку
                while (index > -1)
                {
                    kaziski[x].match_position.Add(index);
                    index = text.IndexOf(template, index + template.Length);
                }
                template = "";
            }

            // Перебираем все обработанные шаблоны...
            for (int i = 0; i < kaziski.Length; i++)
            {
                // ...и если шаблон встречался в строке больше двух раз...
                if (kaziski[i].match_position.Count > 1)
                {
                    // ...то вычисляем дистанции меежду позициями
                    kaziski[i].match_distance = new int[kaziski[i].match_position.Count - 1];
                    for (int j = 0; j < kaziski[i].match_position.Count - 1; j++)
                    {
                        kaziski[i].match_distance[j] = Math.Abs(kaziski[i].match_position[j] - kaziski[i].match_position[j + 1]);
                    }
                }
                else
                {
                    continue;
                }
            }


            for (int i = 0; i < kaziski.Length; i++)
            {
                if (kaziski[i].match_position.Count > 1)
                {
                    int result = kaziski[i].match_distance[0];

                    for (int j = 1; j < kaziski[i].match_distance.Length; j++)
                    {
                        // Вычисляем НОД для всех найденных расстояний, где было больше двух совпадений
                        result = Calculate_GCD(kaziski[i].match_distance[j], result);
                    }

                    kaziski[i].gcd = result;
                }
            }

            // Считаем, сколько раз было больше двух совпадений...
            int count = 0;
            for (int i = 0; i < kaziski.Length; i++)
            {
                if (kaziski[i].match_position.Count > 1)
                {
                    count++;
                }
            }

            // ...чтобы создать массив НОДов
            int[] gcd_array = new int[count];
            count = 0;
            for (int i = 0; i < kaziski.Length; i++)
            {
                if (kaziski[i].match_position.Count > 1)
                {
                    gcd_array[count] = kaziski[i].gcd;
                    count++;
                }

            }

            if (gcd_array.Length == 0)
            {
                indexBox.Text += "Нет совпадений";
                return;
            }
            // Находим самый частовстречающийся НОД...
            var most = gcd_array.GroupBy(x => x).OrderByDescending(x => x.Count()).First();

            // ...и предполагаем, что он кратен вероятной длине ключа
            indexBox.Text += "Вероятная длина: " + most.Key + Environment.NewLine;
            for (int i = 0; i < kaziski.Length; i++)
            {
                if (kaziski[i].match_position.Count > 1)
                {
                    indexBox.Text += (i + 1) + ") " + kaziski[i].template + " - " + kaziski[i].gcd + Environment.NewLine;
                }
            }
            button6.Enabled = true;

        }

        // Функция вычисления НОД
        int Calculate_GCD(int a, int b)
        {
            while (b != 0)
                b = a % (a = b);

            return a;
        }
    }
}
