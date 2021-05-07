using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KMZI
{
    public partial class Freq : Form
    {
        public Freq()
        {
            InitializeComponent();

            button3.Enabled = false;
        }

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 
                       'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 
                       'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 
                       'ш', 'щ', 'ъ', 'ы', 'э', 'ю', 'я', ' ' };

        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 
                       'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 
                       's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ' };

        char[] sample_rus = { ' ', 'о', 'е', 'а', 'и', 'н', 'т', 'с', 
                              'р', 'в', 'л', 'к', 'м', 'д', 'п', 'у', 
                              'я', 'ы', 'з', 'ъ', 'б', 'г', 'ч', 'й', 
                              'х', 'ж', 'ю', 'ш', 'ц', 'щ', 'э', 'ф' };

        char[] sample_eng = { ' ', 'e', 't', 'a', 'o', 'n', 'i', 's', 'r', 
                              'h', 'l', 'd', 'c', 'u', 'p', 'f', 'm', 'w', 
                              'y', 'b', 'g', 'v', 'k', 'q', 'x', 'j', 'z' };

        float[] sample_rus_freq = { 0.175f, 0.09f, 0.072f, 0.062f, 0.062f, 0.053f, 0.053f, 0.045f,
                                    0.04f, 0.038f, 0.035f, 0.028f, 0.026f, 0.025f, 0.023f, 0.021f, 
                                    0.018f, 0.016f, 0.016f, 0.014f, 0.014f, 0.013f, 0.012f, 0.01f, 
                                    0.009f, 0.007f, 0.006f, 0.006f, 0.004f, 0.003f, 0.003f, 0.002f };

        float[] sample_eng_freq = { 0.175f, 0.123f, 0.096f, 0.081f, 0.079f, 0.072f, 0.071f, 0.066f, 0.06f, 
                                    0.051f, 0.04f, 0.036f, 0.032f, 0.031f, 0.023f, 0.023f, 0.022f, 0.02f, 
                                    0.019f, 0.016f, 0.016f, 0.009f, 0.005f, 0.002f, 0.002f, 0.001f, 0.001f};

        char[] alphabet = null;             // сюда установится текущий алафвит
        char[] alphabet_sorted = null;      // текущий алфавит, но отсортированный по невозрастанию частот встречаемости символов
        char[] alphabet_sample = null;      // образец частот для текущего алфавита
        char[] alphabet_sample_temp = null; // временное решение для перезаписи
        float[] alphabet_freq = null;       // массив частот для символов
        float[] alphabet_sample_freq = null;// массив частот для образца 

        float temp_f;
        char temp_ch;

        bool isRussian = false; // Является ли текст русскоязычным
        bool textError = false; // Поддается ли текст обработке

        // Первичная обработка текста
        private void button2_Click(object sender, EventArgs e)
        {
            temp_f = 0f;

            sortedBox.Clear();
            sampleBox.Clear();
            textBox2.Clear();

            isRussian = check_language(textBox1.Text); // Смотрим, какой язык будет испоьлзоваться в дальнейшей работе

            if (!textError)
            {
                language_setup(); // Исходя из функции выше, проводим первоначальную настройку
            }
            else
            {
                MessageBox.Show("Загруженный текст не поддается обработке", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            this.chart1.Series["Sample"].Points.Clear();
            this.chart1.Series["Current"].Points.Clear();

            hystogram_generate_sample(alphabet, alphabet_sample, alphabet_sample_freq, "Sample");

            hystogram_generate_current(alphabet, alphabet_freq);

            for(int i = 0; i < alphabet_freq.Length - 1; i++)
            {
                for(int j = 0; j < alphabet_freq.Length - 1; j++)
                {
                    if(alphabet_freq[j] < alphabet_freq[j + 1])
                    {
                        temp_f = alphabet_freq[j];
                        alphabet_freq[j] = alphabet_freq[j + 1];
                        alphabet_freq[j + 1] = temp_f;

                        temp_ch = alphabet_sorted[j];
                        alphabet_sorted[j] = alphabet_sorted[j + 1];
                        alphabet_sorted[j + 1] = temp_ch;
                    }
                }
            }

            textBox1.Text = textBox1.Text.ToLower();

            for(int i = 0; i < alphabet.Length; i++)
            {
                sortedBox.Text += alphabet_sorted[i];
                sampleBox.Text += alphabet_sample[i];
            }
            update_listBox();

            this.chart1.Series["Current"].Points.Clear();
            hystogram_generate_current(alphabet, alphabet_freq);

            button3.Enabled = true;
        }

        //Конечное преобразовние текста
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
           
            if (sortedBox.TextLength < alphabet_sorted.Length)
            {
                for (int j = 0; j < alphabet_sorted.Length; j++)
                {
                    if (!sortedBox.Text.Contains(alphabet_sorted[j]))
                    {
                        sortedBox.Text += alphabet_sorted[j];
                        if (sortedBox.TextLength == alphabet_sorted.Length)
                        {
                            break;
                        }
                    }
                }

            }
            if (sampleBox.TextLength < alphabet_sample.Length)
            {
                for (int j = 0; j < alphabet_sample.Length; j++)
                {
                    if (!sampleBox.Text.Contains(alphabet_sample[j]))
                    {
                        sampleBox.Text += alphabet_sample[j];
                        if (sampleBox.TextLength == alphabet_sample.Length)
                        {
                            break;
                        }
                    }
                }
            }

            alphabet_sample_temp = new char[alphabet.Length];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet_sample_temp[i] = sampleBox.Text[i];
                alphabet_sorted[i] = sortedBox.Text[i];
            }

            string result = "";
            for (int i = 0; i < textBox1.TextLength; i++)
            {
                if (alphabet_sorted.Contains(textBox1.Text[i]))
                {
                    result += alphabet_sample_temp[Array.IndexOf(alphabet_sorted, textBox1.Text[i])];
                }
                else
                {
                    result += textBox1.Text[i];
                }
            }
            textBox2.Clear();
            textBox2.Text += result;
            alphabet_freq = calculate_frequency(textBox2.Text, alphabet);
            this.chart1.Series["Current"].Points.Clear();
            hystogram_generate_current(alphabet, alphabet_freq);
            update_listBox();
        }

        // Генерация гистограммы для алфавита-образца
        public void hystogram_generate_sample(char[] alphabet, char[] sample, float[] sample_freq, string series)
        {
            this.chart1.ChartAreas[0].AxisX.Interval = 1;

            for (int i = 0; i < alphabet.Length; i++)
            {
                this.chart1.Series[series].Points.AddXY(alphabet[i].ToString(), sample_freq[Array.IndexOf(sample, alphabet[i])]);
            }
        }

        //Генерация гистограммы для текущего текста
        public void hystogram_generate_current(char[] alphabet, float[] sample_freq)
        {
            this.chart1.ChartAreas[0].AxisX.Interval = 1;

            for (int i = 0; i < alphabet.Length; i++)
            {
                this.chart1.Series["Current"].Points.AddXY(alphabet[i].ToString(), sample_freq[i]);
            }
        }

        // Вычисление частот встречемости в тексте
        public float[] calculate_frequency(string text, char[] alphabet)
        {
            float[] frequency = new float[alphabet.Length];

            for(int i = 0; i < text.Length; i++)
            {
                if(alphabet.Contains(char.ToLower(text[i])))
                {
                    frequency[Array.IndexOf(alphabet, char.ToLower(text[i]))] += 1f;
                }
            }

            for(int i = 0; i < frequency.Length; i++)
            {
                frequency[i] /= text.Length;
            }

            return frequency;
        }

        // Определение основного языка входного текста
        public bool check_language(string text)
        {
            textError = false;

            for(int i = 0; i < text.Length; i++)
            {
                if(char.IsLetter(text[i]))
                {
                    if(rus.Contains(char.ToLower(text[i])))
                    {
                        return true;
                    }
                    else if(eng.Contains(char.ToLower(text[i])))
                    {
                        return false;
                    }
                }
            }

            textError = true;
            return false;          
        }

        // Кнопка "Открыть файл"
        public void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();         
        }

        // Открытие и чтение содержимого файла
        public void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader str = new StreamReader(openFileDialog1.FileName);

            textBox1.Clear();

            textBox1.Text += str.ReadToEnd();

            isRussian = check_language(textBox1.Text);

            language_setup();    
        }

        // Установить алфавит по-умолчанию, настроить гистограммы, рассчитать частоты
        private void language_setup()
        {
            if (!textError)
            {
                set_alphabet();
                alphabet_freq = calculate_frequency(textBox1.Text, alphabet);
                this.chart1.Series["Sample"].Points.Clear();
                this.chart1.Series["Current"].Points.Clear();
                hystogram_generate_sample(alphabet, alphabet_sample, alphabet_sample_freq, "Sample");
            }
            else
            {
                MessageBox.Show("Загруженный текст не поддается обработке", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            }
        }

        // Получить нужный алфавит
        private void set_alphabet()
        {
            if (isRussian)                                          // Если язык текста - русский
            {
                alphabet = rus;                                     // Обрабатываемый алфавит - русский
                alphabet_sample = sample_rus;                       // Образец - русский
                alphabet_sample_freq = sample_rus_freq;             // Частоты - для русского образца
                alphabet_sorted = new char[alphabet.Length];        // Выделяем место под сортируемый алфавит
                alphabet.CopyTo(alphabet_sorted, 0);                // Копируем алфавит в сортированный массив для... сортировки в дальнейшем
                textBox1.Text = textBox1.Text.Replace("ё", "е");    // Производим замены
                textBox1.Text = textBox1.Text.Replace("Ё", "Е");
                textBox1.Text = textBox1.Text.Replace("ь", "ъ");
                textBox1.Text = textBox1.Text.Replace("Ь", "Ъ");
            }
            else
            {
                alphabet = eng;
                alphabet_sample = sample_eng;
                alphabet_sample_freq = sample_eng_freq;
                alphabet_sorted = new char[alphabet.Length];
                alphabet.CopyTo(alphabet_sorted, 0);
            }
        }

        // Обновить данные в ListBox1
        void update_listBox()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < alphabet.Length; i++)
            {
                string listBoxText = sortedBox.Text[i].ToString() + " -> " + sampleBox.Text[i].ToString();
                listBox1.Items.Add(listBoxText);
            }
        }

        // Закрыть форму
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Очистка полей
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            sortedBox.Clear();
            sampleBox.Clear();
            textBox2.Clear();
            listBox1.Items.Clear();
            this.chart1.Series["Sample"].Points.Clear();
            this.chart1.Series["Current"].Points.Clear();
            button3.Enabled = false;
        }
    }
}
