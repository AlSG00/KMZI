using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.CodeDom.Compiler;

namespace KMZI
{
    public partial class GOST : Form
    {
        public GOST()
        {
            InitializeComponent();

            button6.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            progressBar1.Visible = false;
            label1.Text = "0";
            label4.Text = "0";
            label1.BackColor = Color.OrangeRed;
            label4.BackColor = Color.OrangeRed;
            checkBox1.Enabled = false;
            synchroBox.Enabled = false;
            keyBox.Enabled = false;
        }

        // Набор s-блоков для выполнения замены
        int[,] s_blocks = { { 4, 10, 9, 2, 13, 8, 0, 14, 6, 11, 1, 12, 7, 15, 5, 3 },
                            { 14, 11, 4, 12, 6, 13, 15, 10, 2, 3, 8, 1, 0, 7, 5, 9 },
                            { 5, 8, 1, 13, 10, 3, 4, 2, 14, 15, 12, 7, 6, 0, 9, 11 },
                            { 7, 13, 10, 1, 0, 8, 9, 15, 14, 4, 6, 12, 11, 2, 5, 3 },
                            { 6, 12, 7, 1, 5, 15, 13, 8, 4, 10, 9, 14, 0, 3, 11, 2 },
                            { 4, 11, 10, 0, 7, 2, 1, 13, 3, 6, 8, 5, 9, 12, 15, 14 },
                            { 13, 11, 4, 1, 3, 15, 5, 9, 0, 10, 14, 7, 6, 8, 2, 12 },
                            { 1, 15, 13, 0, 5, 7, 10, 4, 9, 2, 3, 14, 6, 11, 8, 12 }, }; 

        byte[] inFile;                  // Набор входных байтов (берётся из файла или введенного текста
        byte[] outFile;                 // Набор выходных байтов (появляется в результате  работы алгоритма)
        bool is_text_from_file = false; // Если текст был прочитан из файла
        bool is_text_detailed;          // Нужен ли подробный вывод (всех байтов)
        int index = 0;
        bool is_gamming = false;
        byte[] junkBytes = Encoding.Default.GetBytes("0"); // Мусорные байты длля дополнения до нужного размера
        GOST_Options options;

        // Кнопка Шифрования/Расшифрования
        private void button6_Click(object sender, EventArgs e)
        {
            outBox.Clear();
            if (inBox.TextLength == 0)
            {
                MessageBox.Show("Введите текст.", "Ошибка шифрования", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            // Выбор режима работы шифра
            switch (comboBox1.SelectedIndex)
            {
                case 0: // Выполнить простую замену
                    Perform_Simple_Replacement(); 
                    break;
                case 1: // Выполнить гаммирование с обратной связью
                    Perform_Gamma_With_Back_Connection();
                    break;
            }
        }

        // Алгоритм шифрования в режиме простой замены
        private void Perform_Simple_Replacement()
        {
            int count = 0;
            index = 0;

            // Проверка длины ключа
            if (!Check_KeyLength())
                return;

            if (radioButton2.Checked)
            {
                if (!Char.IsDigit(keyBox.Text[0]))
                {
                    MessageBox.Show("Неверный формат ключа!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
                else if (Convert.ToInt32(keyBox.Text[0].ToString()) > 7)
                {
                    MessageBox.Show("Неверный формат ключа!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
            }

            // Если входные данные НЕ были загружены из файла, значит массив байтов сейчас пуст 
            // и его необходимо заполнить данными из поля ввода
            if (is_text_from_file == false && inFile == null)
            {
                inFile = Encoding.Default.GetBytes(inBox.Text);
            }

            // Если выбран режим шифрования
            if (radioButton1.Checked)
            {
                // Считаем, сколько байтов не хватает шифруемому тексту до кратности 8-ми
                count = inFile.Length;
                while (count % 8 != 0)
                {
                    count++;
                }
              
                byte[] inTemp = new byte[inFile.Length]; // переносим входные данные во временный массив
                inFile.CopyTo(inTemp, 0);
                inFile = new byte[count]; // расширяем массив входных данных

                // Расширяем исходный массив байтов на нужное количество и в конце приписываем несколько байтов
                for (int i = 0; i < inTemp.Length; i++)
                {
                    inFile[i] = inTemp[i];
                }
                for (int i = inTemp.Length; i < inFile.Length; i++)
                {
                    inFile[i] = junkBytes[0];
                }
               
                // Вместо первого символа ключа подставим число, равное количеству символов, приписанных в конце
                // чтобы потом убрать их из расшифрованного текста
                string key = "";
                key += Convert.ToString(count - inTemp.Length);
                inTemp = null;
                for (int i = 1; i < keyBox.TextLength; i++)
                {
                    key += keyBox.Text[i];
                }

                keyBox.Text = key;
            }

            outFile = new byte[inFile.Length]; // Сразу сделаем выходной массив

            // Переводим текст и ключ в байты
            byte[] key_byte = Encoding.Default.GetBytes(keyBox.Text); 
            byte[] text_byte = inFile;

            // Переводим байтовые значения текста и ключа в биты
            var key_bit = new BitArray(key_byte); 
            var text_bit = new BitArray(text_byte);

            // массив для двоичного представления ключа
            int[,] binary_key = new int[8, 32]; 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (key_bit.Get(index))
                    {
                        binary_key[i, j] = 1;
                    }
                    index++;
                }
            }
            ProgressBar_Default();

            if(radioButton2.Checked)
            {
                if(inFile.Length % 8 != 0)
                {
                    inBox.Clear();
                    inFile = null;
                    MessageBox.Show("Некорректный ввод", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }              
            }

            // Идём по блокам текста
            for (int i = 0; i < inFile.Length; i += 8)
            {
                // Выделяет место под старшую и младшую половину блока
                byte[] temp_senior_byte = new byte[4];
                byte[] temp_junior_byte = new byte[4];

                // Копируем блок текста в созданные половины, разбив его на старшую и младшую половины
                Array.Copy(text_byte, i, temp_senior_byte, 0, 4);
                Array.Copy(text_byte, i + 4, temp_junior_byte, 0, 4);

                // Преобразуем половины блока в массивы битов
                var senior_bits = new BitArray(temp_senior_byte);
                var junior_bits = new BitArray(temp_junior_byte);

                // Создаем численные массивы, поскольку массивы битов представлены в формате bool - а я хочу числа
                /* Стоит присмотреться. Возможно проще не создавать численные форматы, а работать с тем, что есть*/
                int[] senior_int = new int[32];
                int[] junior_int = new int[32];
                int[] junior_int_old = new int[32];

                // Копируем значения массива битов в численные массивы
                for (int m = 0; m < 32; m++)
                {
                    if (senior_bits.Get(m))
                    {
                        senior_int[m] = 1;
                    }
                    if (junior_bits.Get(m))
                    {
                        junior_int[m] = 1;
                        junior_int_old[m] = 1;
                    }
                }

                // Начинаем раунды преобразования
                index = 0;
                for (int j = 0; j < 32; j++)
                {

                    //Шаг 1 - сложение двоичных чисел по модулю 32
                    junior_int = Perform_Sum_By_32(junior_int, binary_key);
                    index = (index + 1) % 8; // меняем блок накладываемого ключа

                    // При шифровании ключ инвертируется на последние 8 раундов
                    if (radioButton1.Checked)
                    {
                        if (j == 23)
                        {
                            binary_key = Invert_key_GOST(binary_key);
                        }
                    }
                    // При расшифровании ключ инвертируется после первых 8-ми раундов
                    else if (radioButton2.Checked)
                    {
                        if (j == 7)
                        {
                            binary_key = Invert_key_GOST(binary_key);
                        }
                    }

                    // Шаг 2 - разбиение младшей части на блоки по 4 бита с целью замены по таблице s-блоков
                    junior_int = Four_Bit_Replace(junior_int);

                    // Шаг 3 - циклический сдвиг влево на 11 бит
                    junior_int = Shift_Binary_Array(junior_int);

                    // Шаг 4 - Сложение младшей половины со старшей по модулю 2 (исключающее ИЛИ)
                    junior_int = Xor_Int_Arrays(junior_int, senior_int);

                    // Шаг 5 - Смена блоков местами
                    if (j == 31) // 32-й райнд. Новый младший блок становится старшим, старый младший остается и становится вместо нового младшего
                    {
                        junior_int.CopyTo(senior_int, 0);
                        junior_int_old.CopyTo(junior_int, 0);

                    }
                    else // 0-31-й раунд. Старый младший становится старшим, новый младший становится старым младшим
                    {
                        junior_int_old.CopyTo(senior_int, 0);
                        junior_int.CopyTo(junior_int_old, 0);
                    }
                }

                // переносим значения из int в BitArray
                for (int m = 0; m < junior_int.Length; m++)
                {
                    if (junior_int[m] == 1)
                    {
                        junior_bits[m] = true;
                    }
                    else
                    {
                        junior_bits[m] = false;
                    }

                    if (senior_int[m] == 1)
                    {
                        senior_bits[m] = true;
                    }
                    else
                    {
                        senior_bits[m] = false;
                    }
                }

                // Заносим результаты в выходной массив
                senior_bits.CopyTo(outFile, i);
                junior_bits.CopyTo(outFile, i + 4);
                progressBar1.PerformStep();
            }

            // Отрезаем лишние блоки байтов при расшифровке
            if (radioButton2.Checked)
            {
                byte[] outTemp = new byte[outFile.Length]; // Копируем выходные данные во временный массив
                outFile.CopyTo(outTemp, 0);              
                // Пересоздаем выходной массив по принципу: длина минус число ранее приписанных байт
                outFile = new byte[outTemp.Length - Convert.ToInt32(keyBox.Text[0].ToString())];
                // Заносим данные обратно в массив
                for (int i = 0; i < outFile.Length; i++)
                {
                    outFile[i] = outTemp[i];
                }
                outTemp = null;
            }

            // Метод отображения текста в зависимости от режима вывода (обычный или подробный)
            if (is_text_detailed)
            {
                outBox.Text = BitConverter.ToString(outFile);
            }
            else
            {
                outBox.Text = Encoding.Default.GetString(outFile);
            }
        }

        // Алгоритм шифрования в режиме гаммирования с обратной связью
        private void Perform_Gamma_With_Back_Connection()
        {
            if (!Check_KeyLength())
                return;

            // проверка корректности синхропосылки
            if (!Check_SynchroBox_Length())
                return;

            if (radioButton2.Checked)
            {
                if (!Char.IsDigit(keyBox.Text[0]))
                {
                    MessageBox.Show("Неверный формат ключа!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
                else if (Convert.ToInt32(keyBox.Text[0].ToString()) > 7)
                {
                    MessageBox.Show("Неверный формат ключа!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
            }

            // Если входные данные НЕ были загружены из файла, значит массив байтов сейчас пуст 
            // и его необходимо заполнить данными из поля ввода
            if (is_text_from_file == false && inFile == null)
            {
                inFile = Encoding.Default.GetBytes(inBox.Text);
            }

            outFile = new byte[inFile.Length]; // Сразу сделаем выходной массив

            byte[] synchro_package = Encoding.Default.GetBytes(synchroBox.Text);
            byte[] text_byte = inFile;
            byte[] key_byte = Encoding.Default.GetBytes(keyBox.Text);

            var text_bit = new BitArray(text_byte);
            var synchro_bits = new BitArray(synchro_package);
            var key_bit = new BitArray(key_byte);

            index = 0;
            int[,] binary_key = new int[8, 32]; // массив для двоичного представления ключа
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (key_bit.Get(index))
                    {
                        binary_key[i, j] = 1;
                    }
                    index++;
                }
            }

            index = 0;

            // перегоняем синхропосылку в массив
            int[] synchro_binary = new int[64];
            for (int m = 0; m < 64; m++)
            {
                if (synchro_bits.Get(m))
                {
                    synchro_binary[m] = 1;
                }
            }

            ProgressBar_Default();

            // Идём по блокам текста
            for (int i = 0; i < inFile.Length; i += 8)
            {
                // Выделяет место под старшую и младшую половину блока
                byte[] temp_senior_byte = new byte[4];
                byte[] temp_junior_byte = new byte[4];

                // Копируем блок текста в созданные половины, разбив его на старшую и младшую половины
                if (i == 0)
                {
                    // В качестве первого блока выступает синхропосылка
                    Array.Copy(synchro_package, 0, temp_senior_byte, 0, 4);
                    Array.Copy(synchro_package, 4, temp_junior_byte, 0, 4);
                }
                else
                {
                    // В качестве последующих блоков выступают блоки текста
                    if (radioButton1.Checked)
                    {
                        // При шифровании используются ранее зашифрованные блоки текста
                        Array.Copy(outFile, i - 8, temp_senior_byte, 0, 4);
                        Array.Copy(outFile, i - 4, temp_junior_byte, 0, 4);
                    }
                    else
                    {
                        // При расшифровании используются зашифрованные блоки текста
                        Array.Copy(inFile, i - 8, temp_senior_byte, 0, 4);
                        Array.Copy(inFile, i - 4, temp_junior_byte, 0, 4);
                    }
                }

                // Преобразуем половины блока в массивы битов
                var senior_bits = new BitArray(temp_senior_byte);
                var junior_bits = new BitArray(temp_junior_byte);

                // Создаем численные массивы, поскольку массивы битов представлены в формате bool - а я хочу числа
                /* Стоит присмотреться. Возможно проще не создавать численные форматы, а работать с тем, что есть*/
                int[] senior_int = new int[32];
                int[] junior_int = new int[32];
                int[] junior_int_old = new int[32];

                // Копируем значения массива битов в численные массивы
                for (int m = 0; m < 32; m++)
                {
                    if (senior_bits.Get(m))
                    {
                        senior_int[m] = 1;
                    }
                    if (junior_bits.Get(m))
                    {
                        junior_int[m] = 1;
                        junior_int_old[m] = 1;
                    }
                }


                // Начинаем раунды преобразования
                index = 0;
                for (int j = 0; j < 32; j++)
                {

                    junior_int = Perform_Sum_By_32(junior_int, binary_key);
                    index = (index + 1) % 8; // меняем блок накладываемого ключа

                    // При шифровании ключ инвертируется на последние 8 раундов
                    if (j == 23)
                    {
                        binary_key = Invert_key_GOST(binary_key);
                    }

                    // Шаг 2 - разбиение младшей части на блоки по 4 бита с целью замены по таблице s-блоков
                    junior_int = Four_Bit_Replace(junior_int);

                    // Шаг 3 - циклический сдвиг влево на 11 бит
                    junior_int = Shift_Binary_Array(junior_int);

                    // Шаг 4 - Сложение младшей половины со старшей по модулю 2(исключающее ИЛИ)
                    junior_int = Xor_Int_Arrays(junior_int, senior_int);
                    // Смена блоков местами
                    if (j == 31) // 32-й райнд. Новый младший блок становится старшим, старый младший остается и становится вместо нового младшего
                    {
                        junior_int.CopyTo(senior_int, 0);
                        junior_int_old.CopyTo(junior_int, 0);

                    }
                    else // 0-31-й раунд. Старый младший становится старшим, новый младший становится старым младшим
                    {
                        junior_int_old.CopyTo(senior_int, 0);
                        junior_int.CopyTo(junior_int_old, 0);
                    }
                }

                // переносим значения из int в BitArray
                for (int m = 0; m < junior_int.Length; m++)
                {
                    if (junior_int[m] == 1)
                    {
                        junior_bits[m] = true;
                    }
                    else
                    {
                        junior_bits[m] = false;
                    }

                    if (senior_int[m] == 1)
                    {
                        senior_bits[m] = true;
                    }
                    else
                    {
                        senior_bits[m] = false;
                    }
                }

                /* начинаем стряпать гамму */

                // обработанные половины блока заносим в массив и будем использовать как блок гаммы
                byte[] gamma = new byte[8];
                senior_bits.CopyTo(gamma, 0);
                junior_bits.CopyTo(gamma, 4);

                // Во времнный массив вносим часть текста дял обработки
                byte[] temp_text;
                if (i + 8 > inFile.Length)
                {
                    temp_text = new byte[inFile.Length - i];
                }
                else
                {
                    temp_text = new byte[8];
                }

                Array.Copy(inFile, i, temp_text, 0, temp_text.Length);

                // ТУТ МОЖНО УПРОСТИТЬ
                var temp_bits = new BitArray(temp_text);
                var gamma_bits = new BitArray(gamma);

                int[] temp_int = new int[temp_bits.Length];
                int[] gamma_int = new int[gamma_bits.Length];

                for (int m = 0; m < temp_int.Length; m++)
                {
                    if (temp_bits.Get(m))
                    {
                        temp_int[m] = 1;
                    }
                    if (gamma_bits.Get(m))
                    {
                        gamma_int[m] = 1;
                    }
                }

                temp_int = Xor_Int_Arrays(temp_int, gamma_int);
                for (int m = 0; m < temp_bits.Length; m++)
                {
                    if (temp_int[m] == 1)
                    {
                        temp_bits[m] = true;
                    }
                    else
                    {
                        temp_bits[m] = false;
                    }

                }
                temp_bits.CopyTo(outFile, i);

                progressBar1.PerformStep();
            }

            if (is_text_detailed)
            {
                outBox.Text = BitConverter.ToString(outFile);
            }
            else
            {
                outBox.Text = Encoding.Default.GetString(outFile);
            }
        }

        // Побайтовое суммирование 32-битных чисел по модулю 2^32
        private int[] Perform_Sum_By_32(int[] a, int[,] b)
        {
            int remainder = 0;

            for (int m = 31; m >= 0; m--)
            {
                if (a[m] == 0 && b[index, m] == 0 && remainder == 0)
                {
                    a[m] = 0;
                    remainder = 0;
                }
                else if ((a[m] == 0 && b[index, m] == 0 && remainder == 1) ||
                         (a[m] == 0 && b[index, m] == 1 && remainder == 0) ||
                         (a[m] == 1 && b[index, m] == 0 && remainder == 0))
                {
                    a[m] = 1;
                    remainder = 0;
                }
                else if ((a[m] == 0 && b[index, m] == 1 && remainder == 1) ||
                         (a[m] == 1 && b[index, m] == 0 && remainder == 1) ||
                         (a[m] == 1 && b[index, m] == 1 && remainder == 0))
                {
                    a[m] = 0;
                    remainder = 1;
                }
                else if (a[m] == 1 && b[index, m] == 1 && remainder == 1)
                {
                    a[m] = 1;
                    remainder = 1;
                }
            }

            return a;
        }

        // Построчная инверсия ключа специально для ГОСТа
        private int[,] Invert_key_GOST(int[,] key)
        {
            for (int m = 0; m < key.GetLength(0) / 2; m++)
            {
                for (int n = 0; n < key.GetLength(1); n++)
                {
                    int k_temp = key[m, n];
                    key[m, n] = key[7 - m, n];
                    key[7 - m, n] = k_temp;
                }
            }
            return key;
        }

        // Замена байтов 4-х битных блоков с использзованием s-блоков
        private int[] Four_Bit_Replace(int[] block)
        {

            int[] blocks_4_bits = new int[8]; // Создаем массив для 4-х битных блоков
            for (int m = 0; m < blocks_4_bits.Length; m++)
            {
                for (int n = 0; n < 4; n++)
                {
                    if (block[m * 4 + n] == 1)
                    {
                        // Тут 4-х битное двоичное число преобразуется в десятичное
                        blocks_4_bits[m] += Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble(3 - n)));
                    }
                }

                // Поиск по таблице и замена
                for (int x = 0; x < 16; x++)
                {
                    if (blocks_4_bits[m] == x)
                    {
                        blocks_4_bits[m] = s_blocks[7 - m, x];
                        break;
                    }
                }

                //преобразуем числа обратно в биты
                string binary_block = Convert.ToString(blocks_4_bits[m], 2);
                while (binary_block.Length < 4)
                {
                    // Делаем все значения 4-х битными
                    binary_block = binary_block.Insert(0, "0");
                }

                // В соответствии со значениями из обработанной строки делаем соответствующие замены в int-массиве
                for (int x = 0; x < binary_block.Length; x++)
                {
                    if (binary_block[x].ToString() == "0")
                    {
                        block[m * 4 + x] = 0;
                    }
                    else if (binary_block[x].ToString() == "1")
                    {
                        block[m * 4 + x] = 1;
                    }
                }
            }

            return block;
        }

        // Циклический сдвиг бинарного массива
        private int[] Shift_Binary_Array(int[] block)
        {
            string shifted_block = "";
            for (int m = 0; m < block.Length; m++)
            {
                shifted_block += block[(m + 11) % block.Length];
            }
            for (int m = 0; m < block.Length; m++)
            {
                if (shifted_block[m].ToString() == "0")
                {
                    block[m] = 0;
                }
                else
                {
                    block[m] = 1;
                }
            }

            return block;
        }

        // XOR для бинарных массивов, представленных в формате int
        private int[] Xor_Int_Arrays(int[] a, int[] b)
        {
            for (int m = 0; m < a.Length; m++)
            {
                if (a[m] == b[m])
                {
                    a[m] = 0;
                }
                else
                {
                    a[m] = 1;
                }
            }

            return a;
        }

        // Кнопка переноса текста из окна результата в окно ввода
        private void button1_Click(object sender, EventArgs e)
        {
            if (outFile != null)
            {
                inFile = new byte[outFile.Length];

                outFile.CopyTo(inFile, 0);
                inBox.Clear();
                inBox.Text = outBox.Text;

                // Для удобства шифроваание автоматически переклчается на расшифрование и обратно
                if (radioButton1.Checked)
                {
                    radioButton2.Checked = true;
                }
                else if (radioButton2.Checked)
                {
                    radioButton1.Checked = true;
                }
            }
        }
        // Функция проверки и исправления ключа
        private bool Check_KeyLength()
        {
            // Если ключ недостаточной длины - дополняем его зацикливанием
            if (keyBox.TextLength < 32)
            {
                var result = MessageBox.Show("Недостаточная длина ключа!\n Дополнить ключ при помощи зацикливания?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int count = 0;
                    int length = keyBox.TextLength;

                    while (keyBox.TextLength < 32)
                    {
                        keyBox.Text += keyBox.Text[count % length];
                        count++;
                    }

                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return false;
                }
            }
            // Если ключ избыточной длины - обрезаем его
            else if (keyBox.TextLength > 32)
            {
                var result = MessageBox.Show("Избыточная длина ключа!\n Сократить ключ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string temp = "";
                    for (int i = 0; i < 32; i++)
                    {
                        temp += keyBox.Text[i];
                    }
                    keyBox.Clear();
                    for (int i = 0; i < 32; i++)
                    {
                        keyBox.Text += temp[i];
                    }

                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        // Функция проверки и исправления синхропосылки
        private bool Check_SynchroBox_Length()
        {
            // Если ключ недостаточной длины - дополняем его зацикливанием
            if (synchroBox.TextLength < 8)
            {
                var result = MessageBox.Show("Недостаточная длина синхропосылки!\n Дополнить синхропосылку при помощи зацикливания?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (synchroBox.TextLength == 0)
                    {
                        synchroBox.Text += "1";
                    }
                    int count = 0;
                    int length = synchroBox.TextLength;
                 
                    while (synchroBox.TextLength < 8)
                    {
                        synchroBox.Text += synchroBox.Text[count % length];
                        count++;
                    }

                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return false;
                }
            }
            else if (synchroBox.TextLength > 8) // Если ключ избыточной длины - обрезаем его
            {
                var result = MessageBox.Show("Избыточная длина синхропосылки!\n Сократить синхропосылку?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string temp = "";
                    for (int i = 0; i < 8; i++)
                    {
                        temp += synchroBox.Text[i];
                    }
                    synchroBox.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        synchroBox.Text += temp[i];
                    }

                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        // Кнопка "Открыть" из меню
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        // Кнопка "Открыть..." из groupBox
        private void button5_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        // Кнопка "Сохранить" из меню
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            Save_File();
        }

        // Кнопка "Сохранить..." из groupBox
        private void button2_Click(object sender, EventArgs e)
        {
            Save_File();
        }

        // Функция для открытия файла
        private void Open_File()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    is_text_from_file = true;
                    inFile = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    ProgressBar_Reset();

                    // Если объем файла превышает 400 Мб, то будет ошибка (число в байтах)
                    if (inFile.Length > 419430400)
                    {
                        MessageBox.Show("Превышен допустимый размер файла.\n  Размер открываемого файла не должен превышать 400 Мб", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        inFile = null;
                        return;
                    }
                    else
                    {
                        // Если нужен подробный вывод, то текст будет представлен в виде набора байтов
                        if (is_text_detailed)
                        {
                            inBox.Text = BitConverter.ToString(inFile);
                        }
                        else
                        {
                            inBox.Text = Encoding.Default.GetString(inFile);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Превышен допустимый размер файла.\n  Размер открываемого файла не должен превышать 400 Мб", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        // Функция для сохранения файла
        private void Save_File()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(saveFileDialog1.FileName, outFile);
            }
        }

        // Кнопка "Очистить" для поля ввода
        private void inBox_Clear_Click(object sender, EventArgs e)
        {
            inFile = null;
            inBox.Clear();
            is_text_from_file = false;
            ProgressBar_Reset();
        }

        // Кнопка "Очистить" для поля вывода
        private void button4_Click(object sender, EventArgs e)
        {
            outBox.Clear();
            ProgressBar_Reset();
        }

        // Кнопка "Очистить все поля"
        private void clear_all_Click(object sender, EventArgs e)
        {
            // Если хоть в одном поле остался текст - то нужно предупредить, что данные будут утеряны 
            if (keyBox.TextLength > 0 || outBox.TextLength > 0 || inBox.TextLength > 0)
            {
                var result = MessageBox.Show("Вы уверены, что хотите очистить все поля?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Clear_All_Fields();
                    is_text_from_file = false;
                    ProgressBar_Reset();
                    RadioButton_Reset();
                    Set_Fields_State(false);
                    inFile = null;
                    outFile = null;
                }
            }
            else
            {
                Clear_All_Fields();              
                is_text_from_file = false;
                ProgressBar_Reset();
                inFile = null;
                outFile = null;
                RadioButton_Reset();
                Set_Fields_State(false);
            }

        }

        // Кнопка "Закрыть"
        private void close_Click(object sender, EventArgs e)
        {
            // Если в полях остался текст - нужно предупредить о потере данных при закрытии окна
            if (keyBox.TextLength > 0 || outBox.TextLength > 0 || inBox.TextLength > 0)
            {
                var result = MessageBox.Show("Вы уверены, что хотите закрыть форму? Несохраненные данные будут потеряны.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        // Кнопка "Выход" из меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Цветовая индикация и активация кнопки в зависимости от длины ключа
        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            label1.Text = keyBox.TextLength.ToString(); // Над полем ввода ключа отображается длина введенного ключа     
                                                        // В зависимости от длины ключа меняется цветовая индикация и состояние кнопки
            //if (comboBox1.SelectedIndex == 0)
            //{
                if (keyBox.TextLength == 32)
                {
                    label1.BackColor = Color.LimeGreen;
                    label1.ForeColor = Color.Black;
                    button6.Enabled = true;
                }
                else if (keyBox.TextLength > 32)
                {
                    label1.BackColor = Color.Gold;
                    label1.ForeColor = Color.Black;
                    button6.Enabled = true;
                }
                else if (keyBox.TextLength > 0 && keyBox.TextLength < 32)
                {
                    label1.BackColor = Color.OrangeRed;
                    label1.ForeColor = Color.White;
                    button6.Enabled = true;
                }
                else if (keyBox.TextLength == 0)
                {
                    label1.BackColor = Color.OrangeRed;
                    label1.ForeColor = Color.White;
                    button6.Enabled = false;
                }
            //}
        }

        // Цветовая индикация и активация кнопки в зависимости от длины синхропосылки
        private void synchroBox_TextChanged(object sender, EventArgs e)
        {
            label4.Text = synchroBox.TextLength.ToString(); // Над полем ввода ключа отображается длина введенного ключа     

            if (synchroBox.TextLength == 8)
            {
                label4.BackColor = Color.LimeGreen;
                label4.ForeColor = Color.Black;
                button6.Enabled = true;
            }
            else if (synchroBox.TextLength > 8)
            {
                label4.BackColor = Color.Gold;
                label4.ForeColor = Color.Black;
                button6.Enabled = true;
            }
            else if (synchroBox.TextLength > 0 && synchroBox.TextLength < 8)
            {
                label4.BackColor = Color.OrangeRed;
                label4.ForeColor = Color.White;
                button6.Enabled = true;
            }
            else if (synchroBox.TextLength == 0)
            {
                label4.BackColor = Color.OrangeRed;
                label4.ForeColor = Color.White;
                button6.Enabled = false;
            }

        }

        // Переключатель режима шифрования
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case (0):
                    Clear_All_Fields();
                    groupBox2.Enabled = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    ProgressBar_Reset();
                    is_gamming = false;
                    RadioButton_Reset();
                    Set_Fields_State(false);
                    break;
                case (1):
                    Clear_All_Fields();
                    RadioButton_Reset();
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    groupBox2.Enabled = true;
                    is_gamming = true;
                    ProgressBar_Reset();
                    Set_Fields_State(false);
                    break;
                default:
                    RadioButton_Reset();
                    groupBox2.Enabled = false;
                    is_gamming = false;
                    Set_Fields_State(false);
                    label1.Visible = false;
                    ProgressBar_Reset();
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
            }
        }

        // Кнопка "Шифрование" - активирует оставшиеся groupBox
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Set_Fields_State(true);
            if (is_gamming)
            {
                synchroBox.Enabled = true;
            }
            else
            {
                synchroBox.Enabled = false;
            }
            keyBox.Enabled = true;
        }

        // Кнопка "Расшифрование" - активирует оставшиеся groupBox
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Set_Fields_State(true);
            if (is_gamming)
            {
                synchroBox.Enabled = true;
            }
            else
            {
                synchroBox.Enabled = false;
            }
            keyBox.Enabled = true;
        }

        // Кнопка "Настройки" - вызывает форму с настройками. Сейчас  не работает
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (options == null || options.IsDisposed)
            {
                options = new GOST_Options();
                options.Show();
            }
            else
            {
                options.Activate();
            }
        }

        // Галочка "Подробный текст"
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                is_text_detailed = true;
            }
            else
            {
                is_text_detailed = false;
            }
        }

        // Вывод подсказки при наведении курсора на "Подробный текст"
        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();    
            
            t.SetToolTip(checkBox1, "Отображение считываемых файлов и результата шифрования в виде множества байтов. " +
                                    "\nВключение данной функции может негативно сказаться на быстродействии системы.");
        }

        // Очистка всех полей
        private void Clear_All_Fields()
        {
            keyBox.Clear();
            synchroBox.Clear();
            inFile = null;
            inBox.Clear();
            outBox.Clear();
        }

        // Сбросить выделение с радиальных кнопок
        private void RadioButton_Reset()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // Групповая установка состояний полей ввода
        private void Set_Fields_State(bool state)
        {
            groupBox3.Enabled = state;
            groupBox4.Enabled = state;
            groupBox5.Enabled = state;
            checkBox1.Enabled = state;
        }

        // Первоначальная настройка ProgressBar'а
        private void ProgressBar_Default()
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = inFile.Length / 8;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
        }

        // Сброс ProgressBar'а
        private void ProgressBar_Reset()
        {
            progressBar1.Value = 0;
            progressBar1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                keyBox.Clear();
                int count = 0;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    byte[] key = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    string k = Encoding.UTF8.GetString(key);
                    while (keyBox.TextLength < 32)
                    {
                        keyBox.Text += k[count % k.Length];
                        count++;
                    }
                    k = "";
                }
            }
            catch
            {
                MessageBox.Show("Не удалось открыть файл!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Сохранить ключ в файл"
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, keyBox.Text);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                synchroBox.Clear();
                int count = 0;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    byte[] package = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    string k = Encoding.UTF8.GetString(package);
                    while (synchroBox.TextLength < 8)
                    {
                        synchroBox.Text += k[count % k.Length];
                        count++;
                    }
                    k = "";
                }
            }
            catch
            {
                MessageBox.Show("Не удалось открыть файл!", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Сохранить синхропосылку в файл"
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, synchroBox.Text);
            }
        }
    }
}
