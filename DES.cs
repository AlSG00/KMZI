using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace KMZI
{
    public partial class DES : Form
    {
        public DES()
        {
            InitializeComponent();
        }

        // Стартовая перестановка блока текста
        int[] start_IP = { 58, 50, 42, 34, 26, 18, 10, 2,
                           60, 52, 44, 36, 28, 20, 12, 4,
                           62, 54, 46, 38, 30, 22, 14, 6,
                           64, 56, 48, 40, 32, 24, 16, 8,
                           57, 49, 41, 33, 25, 17, 9, 1,
                           59, 51, 43, 35, 27, 19, 11, 3,
                           61, 53, 45, 37, 29, 21, 13, 5,
                           63, 55, 47, 39, 31, 23, 15, 7 };

        // Конечная перестановка блока текста
        int[] end_IP = { 40, 8, 48, 16, 56, 24, 64, 32,
                         39, 7, 47, 15, 55, 23, 63, 31,
                         38, 6, 46, 14, 54, 22, 62, 30,
                         37, 5, 45, 13, 53, 21, 61, 29,
                         36, 4, 44, 12, 52, 20, 60, 28,
                         35, 3, 43, 11, 51, 19, 59, 27,
                         34, 2, 42, 10, 50, 18, 58, 26,
                         33, 1, 41, 9, 49, 17, 57, 25 };

        // Таблица расширения блока текста
        int[] p_box_expand = { 32, 1, 2, 3, 4, 5, 
                               4, 5, 6, 7, 8, 9, 
                               8, 9, 10, 11, 12, 13, 
                               12, 13, 14, 15, 16, 17, 
                               16, 17, 18, 19, 20, 21, 
                               20, 21, 22, 23, 24, 25, 
                               24, 25, 26, 27, 28, 29, 
                               28, 29, 30, 31, 32, 1 };

        // Прямой p-бокс
        int[] p_box_straight = { 16, 7, 20, 21, 29, 12, 28, 17,
                                 1, 15, 23, 26, 5, 18, 31, 10,
                                 2, 8, 24, 14, 32, 27, 3, 9,
                                 19, 13, 30, 6, 22, 11, 4, 25 };

        // Перестановка ключа для избавления от контрольных битов
        int[] key_replace_start = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 
                                    10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 
                                    63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 
                                    14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };

        // Конечная перестановка сжатия ключа
        int[] key_replace_compress = { 14, 17, 11, 24, 1, 5, 3, 28,
                                       15, 6, 21, 10, 23, 19, 12, 4,
                                       26, 8, 16, 7, 27, 20, 13, 2,
                                       41, 52, 31, 37, 47, 55, 30, 40,
                                       51, 45, 33, 48, 44, 49, 39, 56,
                                       34, 53, 46, 42, 50, 36, 29, 32 };

        // Тест трёхмерного массива (до чего я докатился...)
        byte[,,] s_table = { // S1
                      { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                        { 0, 15, 7, 4, 14, 2, 13 ,1, 10, 6, 12, 11, 9, 5, 3, 8 },
                        { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                        { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } },
                      // S2
                      { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13 ,12, 0, 5 ,10 },
                        { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                        { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                        { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } },
                      // S3
                      { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                        { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                        { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                        { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } },
                      // S4
                      { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                        { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                        { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                        { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } },
                      // S5
                      { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                        { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                        { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                        { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } },
                      // S6
                      { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                        { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                        { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                        { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } },
                      // S7
                      { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                        { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                        { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                        { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } },
                      // S8
                      { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                        { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                        { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                        { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } } };
       
        /*  S-блоки  */
        int[,] s1 = { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                      { 0, 15, 7, 4, 14, 2, 13 ,1, 10, 6, 12, 11, 9, 5, 3, 8 },
                      { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                      { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };

        int[,] s2 = { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13 ,12, 0, 5 ,10 },
                      { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                      { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                      { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };

        int[,] s3 = { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                      { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                      { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                      { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };

        int[,] s4 = { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                      { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                      { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                      { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } };

        int[,] s5 = { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                      { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                      { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                      { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };

        int[,] s6 = { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                      { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                      { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                      { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };

        int[,] s7 = { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                      { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                      { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                      { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };

        int[,] s8 = { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                      { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                      { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                      { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };

        byte[] inFile;
        byte[] outFile;
        bool is_text_from_file = false;
        bool is_text_detailed;

        // Кнопка "Преобразовать"
        private void button6_Click(object sender, EventArgs e)
        {
            outBox.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0: // Режим простой замены
                    ECB_Mode();
                    break;
                case 1: // Режим сцепления блоков
                    CBC_Mode();
                    break;
                case 2: // Режим обратной связи по шифротексту
                    CFB_Mode();
                    break;
                case 3: // Режим обратной связи по выходу
                    OFB_Mode();
                    break;
            }
        }

        // Шифрование в режиме "электронной кодовой книги" (простая замена)
        private void ECB_Mode()
        {   
            // Проверка длины ключа
            if (!Check_KeyLength())
                return;
       
            string block;

            //
            //
            // ГДЕ-ТО ТЕРЯЕТСЯ ПОЛОВИНА БЛОКОВ
            //
            //

            // Формируем раундовый ключ для дальнейшего шифрования
            char[,] round_key = Perform_Key_Replace_Start(keyBox.Text);
            BitArray[] key = new BitArray[16];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = new BitArray(48, false);
                for (int j = 0; j < key[i].Length; j++)
                {
                    // Неизвестно, насколько правильно это работает!!!
                    if (round_key[i, j] == '1')
                        key[i][j] = true;
                }
            }
            if (is_text_from_file == false && inFile == null)
            {
                inFile = Encoding.Default.GetBytes(inBox.Text);
            }

            outFile = new byte[inFile.Length];

            // Идём по каждому блоку текста (64 бита каждый)
            for (int i = 0; i < inFile.Length; i += 8)
            {
                // Выполним стартовую перестановку
                // ОНА ТОЖЕ ВСЯ НЕПРАВИЛЬНАЯ. СТАРТОВАЯ ПЕРЕСТАНОВКА ДОЛЖНА БЫТЬ ДЛЯ ВСЕГО БЛОКА ТЕКСТА
                // Может уже правильная, не знаю :)
                byte[] temp = new byte[8];
                Array.Copy(inFile, i, temp, 0, temp.Length);
                var temp_bits = new BitArray(temp);
                var temp_bits2 = new BitArray(temp);
                for (int m = 0; m < temp.Length; m++)
                {
                    temp_bits2[m] = temp_bits[start_IP[m] - 1];
                }
                temp_bits2.CopyTo(inFile, i);
                byte[] text_senior = new byte[4];
                byte[] text_junior = new byte[4];

                Array.Copy(inFile, i, text_senior, 0, text_senior.Length);
                Array.Copy(inFile, i + 4, text_junior, 0, text_junior.Length);

                var bits_senior = new BitArray(text_senior);
                var temp_senior = new BitArray(text_senior);
                var bits_junior = new BitArray(text_junior);
                var temp_junior = new BitArray(text_junior);
                //var bits_junior_old = new BitArray(text_junior);

                
                // НЕПРАВИЛЬНО! Я НЕ УЧЕЛ РАЗБИЕНИЕ БЛОКА НА ДВЕ ПОЛОВИНКИ!
                // Вроде бы теперь учел :)
                // Начинаем 16 раундов преобразования
                for (int j = 0; j < 16; j++)
                {
                    // Запомним правый блок до преобразования
                    var bits_junior_old = new BitArray(bits_junior.Length);
                    for (int m = 0; m < bits_junior.Length; m++)
                    {
                        bits_junior_old[m] = bits_junior[m];
                    }

                    // Шаг 1 - Расширим правый блок до 48 бит с помощью P-бокса
                    for (int m = 0; m < temp_junior.Length; m++)
                    {
                        temp_junior[m] = bits_junior[m];
                    }
                    bits_junior = new BitArray(48, false); // Инициализируем массив большего размера
                    for (int m = 0; m < bits_junior.Length; m++)
                    {
                        bits_junior[m] = temp_junior[p_box_expand[m] - 1];
                    }
                    /* Закончили расширять блок*/

                    // Шаг 2 - XOR-им блок с элементом раундового ключа
                    bits_junior.Xor(key[j]);

                    // Шаг 3 - делаем замену с использованием s-блоков
                    bits_junior = S_Block_Replace(bits_junior);

                    // ЧТО ТАМ КУДА ДОЛЖНО КСОРИТЬСЯ И КАК БЛОКИ МЕНЯЮТСЯ МЕСТАМИ???
                    // Шаг 4 - делаем перетасовку битов с помощью прямого P-бокса
                    for (int m = 0; m < temp_junior.Length; m++)
                    {
                        temp_junior[m] = bits_junior[m];
                    }
                    for (int m = 0; m < bits_junior.Length; m++)
                    {
                        bits_junior[m] = temp_junior[p_box_straight[m] - 1];
                    }

                    //Шаг 5 - БЕЗ ПОНЯТИЯ. Вроде XOR-им получившийся правый блок с левым
                    bits_senior.Xor(bits_junior);

                    //Шаг 6 - меняем блоки местами
                    if (j != 15) // Ели не последний раунд, то меняем местами
                    {
                        for (int m = 0; m < bits_senior.Length; m++)
                        {
                            bits_junior[m] = bits_senior[m];
                            //bits_senior[m] = bits_junior_old[m];
                        }
                        for (int m = 0; m < bits_senior.Length; m++)
                        {
                            //bits_junior[m] = bits_senior[m];
                            bits_senior[m] = bits_junior_old[m];
                        }
                    }
                }

                // Конечная перестановка
                bits_senior.CopyTo(temp, 0);
                bits_junior.CopyTo(temp, 4);
                temp_bits = new BitArray(temp);
                temp_bits2 = new BitArray(temp);
                for (int m = 0; m < temp.Length; m++)
                {
                    temp_bits2[m] = temp_bits[end_IP[m] - 1];
                }
                temp_bits2.CopyTo(outFile, i);

                
                //for (int j = 0; j < temp.Length; j++)
                //{
                //    outFile[i + j] = temp[end_IP[j]];
                //}
            }

            if (is_text_detailed)
            {
                outBox.Text = BitConverter.ToString(outFile);
            }
            else
            {
                outBox.Text = Encoding.Default.GetString(outFile);
            }
            // Дальше запустится цикл обработки текста*
            // берем блок и делаем первичню перестановку*
            // затем делаем расширение с помощью еще одной перестановки*
            // расширенный блок XOR-им с элементом раундового ключа, полученного ранее*
            // через s-блоки делаем замену*
            // Далее, ещё перестановка*
            // ксор со старшей половиной*
            // старшая -> младшая, а старая младшая -> старшая  *    
            // Это были 16 раундов веселья. А на последок конечная перестановка
        }

        // Шифрование в режиме сцепления блоков
        private void CBC_Mode()
        {
            inBox.Text = "CBC"; // заглушка
        }

        // Шифрование в режиме обратной связи по шифротексту
        private void CFB_Mode()
        {
            inBox.Text = "CFB"; // заглушка
        }

        // Шифрование в режиме обратной связи по выходу
        private void OFB_Mode()
        {
            inBox.Text = "OFB"; // заглушка
        }

        // Подготовка раундового ключа
        /*1. перестановка битов по таблице
          2. деление на два блока по 26 бит
          3. генерация ключей циклическим сдвигом
          4. перестановка сжатия */
        private char[,] Perform_Key_Replace_Start(string key)
        {
            int count = 0;
            string binary_key = "";
            string temp = "";
            byte[] k = Encoding.Default.GetBytes(key);

            BitArray key_bits = new BitArray(k);
            // Конвертирование в двоичную форму
            for (int i = 0; i < key.Length; i++)
            {
                temp = Convert.ToString(k[i], 2);
                char[] sReverse = temp.ToCharArray();
                Array.Reverse(sReverse);
                temp = new string(sReverse);

                while (temp.Length < 8)
                {
                    temp = temp.Insert(0, "0");
                }

                binary_key += temp;
            }

            // Добавление контрольных битов
            for (int i = 0; i < 64; i++)
            {
                if(i == 7 || i == 15 || i == 23 || i == 31 || i == 39 || i == 47 || i == 55 || i == 63 )
                {
                    if(count % 2 != 0)
                    {
                        binary_key = binary_key.Insert(i, "0");
                        //key_bits[i] = false;
                    }
                    else
                    {
                        binary_key = binary_key.Insert(i, "1");
                        //key_bits[i] = true;
                    }
                    count = 0;
                    continue;
                }
                if (binary_key[i] == '1')
                    count++;             
            }

            // Начальная перестановка ключа
            char[] temp2 = new char[key_replace_start.Length];
            for(int i = 0; i < key_replace_start.Length; i++)
            {
                temp2[i] = binary_key[key_replace_start[i] - 1];                            
            }

            char[] left_block = new char[28];
            char[] right_block = new char[28];

            // Разбили ключ на 2 блока
            Array.Copy(temp2, 0, left_block, 0, 28);
            Array.Copy(temp2, 28, right_block, 0, 28);

            char[,] round_key = new char[16, 48]; // Массив под раундовый ключ

            // Делаем раундовый ключ
            for (int i = 0; i < 16; i++)
            {
                // Циклический сдвиг влево на 1-2 бита
                if (i == 0 || i == 1 || i == 8 || i == 15)
                {
                    char[] temp3 = { left_block[0], left_block[1] };
                    char[] temp4 = { right_block[0], right_block[1] };
                    for (int j = 0; j < right_block.Length - 2; j++)
                    {
                        left_block[j] = left_block[j + 2];
                        right_block[j] = right_block[j + 2];
                    } 
                    int index = 0;
                    for (int j = left_block.Length - 2; j < left_block.Length; j++)
                    {
                        left_block[j] = temp3[index]; // ТУТ НИЧЕГО НЕ РАБОТАЕТ
                        right_block[j] = temp4[index];
                        index++;
                    }
                }
                else
                {
                    char temp3 = left_block[0];
                    char temp4 = right_block[0];
                    for (int j = 0; j < left_block.Length - 1; j++)
                    {
                        left_block[j] = left_block[j + 1];
                        right_block[j] = right_block[j + 1];
                    }
                    left_block[left_block.Length - 1] = temp3;
                    right_block[right_block.Length - 1] = temp4;
                }

                // объединяем два блока обратно в один
                char[] temp5 = new char[56];
                left_block.CopyTo(temp5, 0);
                right_block.CopyTo(temp5, 28);

                // Производим перестановку сжатия
                char[] temp6 = new char[key_replace_compress.Length];
                for (int j = 0; j < key_replace_compress.Length; j++)
                {
                    temp6[j] = temp5[key_replace_compress[j] - 1];
                }

                //Результат заносим в массив как элемент раундового ключа
                for(int j = 0; j < temp6.Length; j++)
                {
                    round_key[i, j] = temp6[j];
                }
            }

            // Если расшифрование, то раундовый ключ выстраивается задом наперед
            if(radioButton2.Checked)
            {
                for (int m = 0; m < round_key.GetLength(0) / 2; m++)
                {
                    for (int n = 0; n < round_key.GetLength(1); n++)
                    {
                        char k_temp = round_key[m, n];
                        round_key[m, n] = round_key[7 - m, n];
                        round_key[7 - m, n] = k_temp;
                    }
                }
            }

            return round_key;
        }

        // Функция замены через s-блоки
        private BitArray S_Block_Replace(BitArray block)
        {
            int index = 0;
            int str; // Номер строки для s-блока
            int clmn; // Номер столбца для s-блока
            string bin = ""; // Сюда будут заноситься биты дял обработки
            BitArray result_block = new BitArray(32, false);
            // Блок текста разделится на 8 векторов по 6 бит
            int[,] int_block = new int[8, 6];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (block[index] == true)
                        int_block[i, j] = 1;

                    index++;
                }
            }

            // Идём по каждому блоку
            for (int i = 0; i < 8; i++)
            {
                str = 0;
                clmn = 0;
                bin = "";

                bin += int_block[i, 0].ToString() + int_block[i, 5].ToString();
                for (int m = 0; m < bin.Length; m++)
                {
                    if (bin[m] == '1')
                        str += Convert.ToInt32(Math.Pow(2, bin.Length - m - 1));
                }

                bin = "";
                for (int j = 1; j < 5; j++)
                {
                    bin += int_block[i, j];
                }
                for (int m = 0; m < bin.Length; m++)
                {
                    if (bin[m] == '1')
                        clmn += Convert.ToInt32(Math.Pow(2, bin.Length - m - 1));
                }

                //bin = Convert.ToString(s_table[i, str, clmn], 2);
                // Получаем число из таблицы
                var binary = new BitArray(s_table[i, str, clmn]);

                // Переносим полученное число в результирующий массив
                for(int m = 0; m < binary.Length; m++)
                {
                    result_block[i + m] = binary[m];
                }
                // В соответствии со значениями из обработанной строки делаем соответствующие замены в int-массиве
                //for (int x = 0; x < binary_block.Length; x++)
                //{
                //    if (binary_block[x].ToString() == "0")
                //    {
                //        block[m * 4 + x] = 0;
                //    }
                //    else if (binary_block[x].ToString() == "1")
                //    {
                //        block[m * 4 + x] = 1;
                //    }
                //}
                
            }


            return result_block;
        }

        // Проверка длины ключа: должно быть 8 символов (64 бит)
        private bool Check_KeyLength()
        {
            // Если ключ недостаточной длины - дополняем его зацикливанием
            if (keyBox.TextLength < 7)
            {
                var result = MessageBox.Show("Недостаточная длина ключа!\n Дополнить ключ при помощи закикливания?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int count = 0;
                    int length = keyBox.TextLength;

                    while (keyBox.TextLength < 7)
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
            else if (keyBox.TextLength > 7)
            {
                var result = MessageBox.Show("Избыточная длина ключа!\n Сократить ключ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string temp = "";
                    for (int i = 0; i < 7; i++)
                    {
                        temp += keyBox.Text[i];
                    }
                    keyBox.Clear();
                    for (int i = 0; i < 7; i++)
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

        // Функция открытия файла
        private void Open_File()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    is_text_from_file = true;
                    inFile = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    //ProgressBar_Reset();

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

        // Функция сохранения файла
        private void Save_File()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(saveFileDialog1.FileName, outFile);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                is_text_detailed = true;
            }
            else
            {
                is_text_detailed = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_File();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save_File();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_File();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Главное - перенести сформировавшийся масив байтов
            if (inFile != null && outFile != null)
            {
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
    }
}
