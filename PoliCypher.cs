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
    public partial class PoliCypher : Form
    {
        public PoliCypher()
        {
            InitializeComponent();
        }

        char[] rus = { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з',
                       'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п',
                       'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч',
                       'ш', 'щ', 'ъ', 'ы', 'э', 'ю', 'я', ' ' };

        char[] eng = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                       'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                       's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ' };

        bool isRussian = false; // Является ли текст русскоязычным
        bool textError = false; // Поддается ли текст обработке
        char[] alphabet = null;

        private void button2_Click(object sender, EventArgs e)
        {
            isRussian = check_language(textBox1.Text); // Смотрим, какой язык будет испоьлзоваться в дальнейшей работе

            if (!textError)
            {
                language_setup(); // Исходя из функции выше, проводим первоначальную настройку
            }
            else
            {
                MessageBox.Show("Загруженный текст не поддается обработке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

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

        private void language_setup()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
