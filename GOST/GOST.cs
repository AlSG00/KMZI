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
            progressBar1.Visible = false;
            label1.Text = "0";
            label1.BackColor = Color.OrangeRed;
            checkBox1.Enabled = false;
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

        GOST_Options options;

        // Кнопка Шифрования/Расшифрования
        private void button6_Click(object sender, EventArgs e)
        {
            outBox.Clear();

            //Thread[] t = new Thread(Perform_Simple_Replacement());
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
            int index = 0;
            int count = 0;    

            // Если входные данные НЕ были загружены из файла, значит массив байтов сейчас пуст 
            // и его необходимо заполнить данными из поля ввода
            if(is_text_from_file == false || inFile == null)
            {
                inFile = Encoding.Default.GetBytes(inBox.Text);
            }

            // Проверка длины ключа
            if (!Check_KeyLength())
                return;

            // Если выран режим шифрования
            if (radioButton1.Checked)
            {
                // Считаем, сколько байтов не хватает шифруемому тексту до кратности 8-ми
                count = inFile.Length;
                while (count % 8 != 0)
                {
                    count++;
                }

                byte[] junkBytes = Encoding.Default.GetBytes("0");
                byte[] inTemp = new byte[inFile.Length];
                inFile.CopyTo(inTemp, 0);
                inFile = new byte[count];

                // Расширяем исходный массив байтов на нужное количество и в конце приписываем несколько байтов
                for (int i = 0; i < inTemp.Length; i++)
                {
                   inFile[i] = inTemp[i];
                }
                for (int i = inTemp.Length; i < inFile.Length; i++)
                {
                    inFile[i] = junkBytes[0];
                }

                outFile = new byte[inFile.Length]; // Сразу сделаем выходной массив

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
            
            byte[] key_byte = Encoding.Default.GetBytes(keyBox.Text); // Переводим текст и ключ в байты
            byte[] text_byte = inFile;
            //byte[] outFile2 = outFile;

            var key_bit = new BitArray(key_byte); // Переводим байтовые значения текста и ключа в биты
            var text_bit = new BitArray(text_byte);

            int[,] binary_key = new int[8, 32]; // массив для двоичного представления ключа
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 32; j++)
                {
                    if(key_bit.Get(index))
                    {
                        binary_key[i, j] = 1;
                    }
                    index++;
                }
            }
            progressBar1.Minimum = 0;
            progressBar1.Maximum = inFile.Length / 8;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Visible = true;

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
                int[] result = new int[32];

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

                    int remainder = 0; // остаток от суммирования (который переносится при сложении в столбик)

                    ////Шаг 1////////////////////////////////////////////////////// 
                    ////сложение двоичных чисел по модулю 32
                    for (int m = 31; m >= 0; m--)
                    {
                        if (junior_int[m] == 0 && binary_key[index, m] == 0 && remainder == 0)
                        {
                            junior_int[m] = 0;
                            remainder = 0;
                        }
                        else if ((junior_int[m] == 0 && binary_key[index, m] == 0 && remainder == 1) ||
                                 (junior_int[m] == 0 && binary_key[index, m] == 1 && remainder == 0) ||
                                 (junior_int[m] == 1 && binary_key[index, m] == 0 && remainder == 0))
                        {
                            junior_int[m] = 1;
                            remainder = 0;
                        }
                        else if ((junior_int[m] == 0 && binary_key[index, m] == 1 && remainder == 1) ||
                                 (junior_int[m] == 1 && binary_key[index, m] == 0 && remainder == 1) ||
                                 (junior_int[m] == 1 && binary_key[index, m] == 1 && remainder == 0))
                        {
                            junior_int[m] = 0;
                            remainder = 1;
                        }
                        else if (junior_int[m] == 1 && binary_key[index, m] == 1 && remainder == 1)
                        {
                            junior_int[m] = 1;
                            remainder = 1;
                        }
                    }
                    index = (index + 1) % 8; // меняем блок накладываемого ключа

                    // При шифровании ключ инвертируется на последние 8 раундов
                    if (radioButton1.Checked)
                    {
                        if (j == 23)
                        {
                            for (int m = 0; m < binary_key.GetLength(0) / 2; m++)
                            {
                                for (int n = 0; n < binary_key.GetLength(1); n++)
                                {
                                    int k_temp = binary_key[m, n];
                                    binary_key[m, n] = binary_key[7 - m, n];
                                    binary_key[7 - m, n] = k_temp;
                                }
                            }
                        }
                    }
                    // При расшифровании ключ инвертируется после первых 8-ми раундов
                    else if (radioButton2.Checked)
                    {
                        if (j == 7)
                        {
                            for (int m = 0; m < binary_key.GetLength(0) / 2; m++)
                            {
                                for (int n = 0; n < binary_key.GetLength(1); n++)
                                {
                                    int k_temp = binary_key[m, n];
                                    binary_key[m, n] = binary_key[7 - m, n];
                                    binary_key[7 - m, n] = k_temp;
                                }
                            }
                        }
                    }

                    ////Шаг 2////////////////////////////////////////////////////// 
                    ////Разбиение младшей части на блоки по 4 бита с целью замены по таблице s-блоков
                    int[] blocks_4_bits = new int[8];
                    for(int m = 0; m < blocks_4_bits.Length; m++)
                    {
                        for(int n = 0; n < 4; n++)
                        {
                            if(junior_int[m * 4 + n] == 1)
                            {
                                // Тут 4-х битное число преобразуется в десятичное
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
                            // Делаем все значени 4-х битными
                            binary_block = binary_block.Insert(0, "0");
                        }

                        // В соответствии со значениями из обработанной строки делаем соответствующие замены в int-массиве
                        for (int x = 0; x < binary_block.Length; x++)
                        {
                            if (binary_block[x].ToString() == "0")
                            {
                                junior_int[m * 4 + x] = 0;
                            }
                            else if (binary_block[x].ToString() == "1")
                            {
                                junior_int[m * 4 + x] = 1;
                            }
                        }
                    }

                    ////Шаг 3////////////////////////////////////////////////////// 
                    ////Циклический сдвиг на 11 бит влево
                    string shifted_junior = "";
                    for (int m = 0; m <junior_int.Length; m++)
                    {
                        shifted_junior += junior_int[(m + 11) % junior_int.Length];
                    }
                    for (int m = 0; m < junior_int.Length; m++)
                    {
                        if(shifted_junior[m].ToString() == "0")
                        {
                            junior_int[m] = 0;
                        }
                        else
                        {
                            junior_int[m] = 1;
                        }
                    }
                    ////Шаг 4////////////////////////////////////////////////////// 
                    ////Сложение младшей половины со старшей по модулю 2 (исключающее ИЛИ)
                    for (int m = 0; m < junior_int.Length; m++)
                    {
                        if (junior_int[m] == senior_int[m])
                        {
                            junior_int[m] = 0;
                        }
                        else
                        {
                            junior_int[m] = 1;
                        }
                    }
                    ////Шаг 5////////////////////////////////////////////////////// 
                    ////Смена блоков местами
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
                // Конец раунда. Меняем местами старший и младший разряды

                senior_bits.CopyTo(outFile, i);
                junior_bits.CopyTo(outFile, i + 4);
                progressBar1.PerformStep();
            }

            // Отрезаем лишние блоки байтов при расшифровке
            if (radioButton2.Checked)
            {
                byte[] outTemp = new byte[outFile.Length];
                outFile.CopyTo(outTemp, 0);
                outFile = new byte[outTemp.Length - Convert.ToInt32(keyBox.Text[0].ToString())];
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
            // Заглушка. Потом убрать!
            MessageBox.Show("Данный режим ещё не реализован, так что можете переключиться на другой режим, " +
                            "ну или посидеть тут с пустыми окошками... Дело ваше.", "Внимание!", 
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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

        // Кнопка "Выход" из меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Кнопка "Очистить" для поля ввода
        private void inBox_Clear_Click(object sender, EventArgs e)
        {
            inFile = null;
            inBox.Clear();
            is_text_from_file = false;
            progressBar1.Visible = false;
            progressBar1.Value = 0;
        }

        // Кнопка "Очистить" для поля вывода
        private void button4_Click(object sender, EventArgs e)
        {
            outBox.Clear();
            progressBar1.Visible = false;
            progressBar1.Value = 0;
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
                    inFile = null;
                    inBox.Clear();
                    outBox.Clear();
                    keyBox.Clear();
                    is_text_from_file = false;
                    progressBar1.Visible = false;
                    progressBar1.Value = 0;
                }
            }
            else
            {
                inFile = null;
                inBox.Clear();
                outBox.Clear();
                keyBox.Clear();
                is_text_from_file = false;
                progressBar1.Visible = false;
                progressBar1.Value = 0;
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

        // Функция для открытия файла
        private void Open_File()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                
                    is_text_from_file = true;
                    inFile = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    progressBar1.Visible = false;
                    progressBar1.Value = 0;

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

        // Проверка ключа: длина должна быть 32 символа (256 бит)
        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            label1.Text = keyBox.TextLength.ToString(); // Над полем ввода ключа отображается длина введенного ключа     
            if (comboBox1.SelectedIndex == 0)           // В зависимости от длины ключа меняется цветовая индикация и состояние кнопки
            {
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
            }
        }

        // Кнопка переноса текста из окна результата в окно ввода
        private void button1_Click(object sender, EventArgs e)
        {   
            // Главное - перенести сформировавшийся масив байтов
            if (inFile != null)
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

        // Переключатель режима шифрования
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case (0):
                    groupBox2.Enabled = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;
                    break;
                case (1):
                    groupBox2.Enabled = true;
                    break;
                default:
                    groupBox2.Enabled = false;
                    break;
            }
        }

        // Кнопка "Шифрование" - активирует оставшиеся groupBox
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;
            checkBox1.Enabled = true;
        }

        // Кнопка "Расшифрование" - активирует оставшиеся groupBox
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;
            checkBox1.Enabled = true;
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

        // Функция проверки и исправления ключа
        private bool Check_KeyLength()
        {
            // Если ключ недостаточной длины - дополняем его зацикливанием
            if (keyBox.TextLength < 32)
            {
                var result = MessageBox.Show("Недостаточная длина ключа!\n Дополнить ключ при помощи закикливания?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

            // Ключ нормальной длины, так что true
            return true;
        }

        // Кнопка "Очистить поле" для keyBox
        private void button4_Click_1(object sender, EventArgs e)
        {
            keyBox.Clear();
            progressBar1.Visible = false;
            progressBar1.Value = 0;
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
    }
}
