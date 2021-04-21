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
    public partial class Gamma : Form
    {
        public Gamma()
        {
            InitializeComponent();
        }

        char[] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
                            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
                            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        // Кнопка "Преобразовать"
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            keyBox.Clear();

            //Если ключ не отвечает условиям, то будет ошибка
            if(!isStartKeyCorrect(startKeyBox.Text))
            {
                MessageBox.Show("Некорректный стартовый ключ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startKeyBox.Clear();
                return;
            }

            char[] symbol = new char[textBox1.TextLength];
            int[] symbol_pos = new int[textBox1.TextLength];
            int[] text = new int[textBox1.TextLength];

            // Преобразуем строку в числа
            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (alphabet.Contains(textBox1.Text[i])) 
                {
                    text[i] = Array.IndexOf(alphabet, textBox1.Text[i]);
                }
                else
                {
                    text[i] = 0;
                    symbol[i] = textBox1.Text[i];
                    symbol_pos[i] = 1;
                }
            }

            // Конвертируем последовательность в байтовую
            ushort[] text_byte = convert_to_byte(text);

            // Конвертирум байтовую последовательность в двоичную
            string[] text_binary = convert_to_binary(text_byte, 8);

            // Генерируем случайную числовую последовательность
            int[] key = generate_key(Convert.ToInt32(startKeyBox.Text.ToString()));

            for(int i = 0; i < key.Length; i++)
            { 
                keyBox.Text += key[i] + " ";
            }

            // Конвертируем последовательность в байтовую
            ushort[] key_byte = convert_to_byte(key);

            // Конвертирум байтовую последовательность в двоичную
            string[] key_binary = convert_to_binary(key_byte, 8);

            // Шифруем исключающим ИЛИ
            string[] result_binary = convert_xor(text_binary, key_binary);

            // Конвертируем двочную последовательность в десятичную
            ushort[] result_byte = convert_to_decimal(result_binary, 8);

            for (int i = 0; i < result_byte.Length; i++)
            {
                if (symbol_pos[i] == 0)
                {
                    textBox2.Text += alphabet[result_byte[i]];
                }
                else
                {
                    textBox2.Text += symbol[i];
                }
            }
        }

        // Кнопка "Открыть файл"
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(); // как открывать файлы разны форматов??????
        }

        // Чтение файла и вывод содержимого в textBox1
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            StreamReader str = new StreamReader(openFileDialog1.FileName); // Как сохранять файлы в разные форматы??????
            textBox1.Clear();
            textBox1.Text += str.ReadToEnd();
            //byte[] byteArray = File.ReadAllBytes(openFileDialog1.FileName);
            //for(int i = 0; i < byteArray.Length; i++)
            //{
            //    textBox1.Text += byteArray[i];
            //}
        }

        // Кнопка "Сохранить файл"
        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        // Сохранение в файл
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
        }

        // Кнопка "Очистить поля"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            keyBox.Clear();
            startKeyBox.Clear();
        }

        // Кнопка "Закрыть"
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Функция генерации ключа линейным конгруэнтным методом
       int[] generate_key(int startIndex)
        {
            int m = 678;
            int a = 1;
            int c = 2;
            int x = startIndex;
            int[] key = new int[textBox1.TextLength];
            key[0] = x;

            for(int i = 1; i < textBox1.TextLength; i++)
            {
                key[i] = (a * key[i - 1] + c) % m;
            }

            return key;
        }

        // Проверка на корректность введенного начального числа для генерации ключа
        bool isStartKeyCorrect(string key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                // Если в строке ключа находится не число
                if (!Char.IsDigit(key[i])) 
                {
                    return false;
                }
            }
            // если числовая строка меньше нуля
            if (Convert.ToInt32(key) < 0) 
            {
                return false;
            }
            return true;
        }

        // Конвертирование десятичного числа в двоичное
        string[] convert_to_binary(ushort[] b_text, int bytes)
        {
            string[] array_of_bytes = new string[b_text.Length];
            for (int i = 0; i < b_text.Length; i++)
            {
                array_of_bytes[i] = Convert.ToString(b_text[i], 2);
                while (array_of_bytes[i].Length < bytes)
                {
                    array_of_bytes[i] = array_of_bytes[i].Insert(0, "0");
                }
                if(array_of_bytes[i].Length > bytes)
                {
                    string temp = array_of_bytes[i];
                    array_of_bytes[i] = null;
                    int count = 0;
                    while (array_of_bytes[i].Length < bytes)
                    {
                        array_of_bytes[i] += temp[count];
                        count++;
                    }
                }
            }
            return array_of_bytes;
        }

        // Конвертирование десятичноых чисел в байтовый формат
        ushort[] convert_to_byte(int[] key)
        {
            ushort[] key_byte = new ushort[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                key_byte[i] = Convert.ToUInt16(key[i]);
            }
            return key_byte;
        }

        // Исключающее ИЛИ
        string[] convert_xor(string[] text, string[] key)
        {
            string[] answer = new string[text.Length];
            string text_temp = null;
            string key_temp = null;
            string answer_temp = null;

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

        // Конвертирование двоичных чисел в десятичные
        ushort[] convert_to_decimal(string[] array, int bytes)
        {
            ushort[] answer = new ushort[array.Length];
            int answer_byte = 0;
            string text_temp = null;

            for (int i = 0; i < array.Length; i++)
            {
                text_temp = array[i];

                for (int j = 0; j < text_temp.Length; j++)
                {
                    if (text_temp[j] == '1')
                    {
                        answer_byte += Convert.ToByte(Math.Pow(2, bytes - 1 - j));
                    }
                }
                answer[i] = Convert.ToByte(answer_byte % alphabet.Length);
                answer_byte = 0;
            }

            return answer;
        }

        // Выполняется, если изменилось содержимое startKeyBox
        private void startKeyBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
