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
    public partial class Start : Form
    {    
        public Start()
        {
            InitializeComponent();
        
            this.Width = 274;
            this.Height = 567;

            button3.Enabled = true;     // Атбаш
            button5.Enabled = true;     // Сцитала   
            button6.Enabled = true;     // Квадрат Полибия
            button7.Enabled = true;     // Цезарь
            button8.Enabled = true;     // Кардано
            button9.Enabled = true;     // Ришелье
            button10.Enabled = true;    // Диск Альберти
            button11.Enabled = true;    // Гронсфельд
            button12.Enabled = true;    // Виженер
            button13.Enabled = true;    // Плейфер
            button14.Enabled = true;    // Криптосистема Хилла
            button15.Enabled = true;    // Вернам
            button16.Enabled = true;    // Частотный криптоанализ
            button17.Enabled = true;    // Криптоанализ полиалфавитных шифров
            button18.Enabled = true;    // Гаммирование
            button19.Enabled = true;    // DES
            button1.Enabled = true;     // ГОСТ

            groupStartButtons.Visible = true;
            groupStartButtons.Enabled = true;
        }

        Options formSetting;
        Atbash formAtbash;
        Skitala formSkitala;
        Polibium formPolibia;
        Caesar formCaezar;
        FormHelp help;
        Kardano formKardano;
        Rishelie formRishelie;
        Alberti formAlberti;
        Gronsfeld formGronsfeld;
        Vizhiner formVizhiner;
        Pleifer formPleifer;
        Hill formHill;
        Vernam formVernam;
        Freq formFreq;
        PoliCypher formPoliCypher;
        Gamma formGamma;
        DES formDES;
        GOST formGOST;

        // Кнопка "Настройки"
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formSetting == null || formSetting.IsDisposed)
            {
                formSetting = new Options();
                formSetting.Show();
            }
            else
            {
                formSetting.Activate();
            }
        }

        // Кнопка "Атбаш"
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (formAtbash == null || formAtbash.IsDisposed)
            {
                formAtbash = new Atbash();
                formAtbash.Show();
            }
            else
            {
                formAtbash.Activate();
            }
        }

        // Кнопка "Сцитала"
        private void button5_Click(object sender, EventArgs e)
        {           
            if (formSkitala == null || formSkitala.IsDisposed)
            {
                formSkitala = new Skitala();
                formSkitala.Show();
            }
            else
            {
                formSkitala.Activate();
            }
        }

        // Кнопка "Квадрат Полибия"
        private void button6_Click(object sender, EventArgs e)
        {
            if (formPolibia == null || formPolibia.IsDisposed)
            {
                formPolibia = new Polibium();
                formPolibia.Show();
            }
            else
            {
                formPolibia.Activate();
            }
        }

        // Кнопка "Цезарь"
        private void button7_Click(object sender, EventArgs e)
        {            
            if (formCaezar == null || formCaezar.IsDisposed)
            {
                formCaezar = new Caesar();
                formCaezar.Show();
            }
            else
            {
                formCaezar.Activate();
            }
        }

        // Кнопка "Помощь" во вкладке "Справка"
        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (help == null || help.IsDisposed)
            {
                help = new FormHelp();
                help.Show();
            }
            else
            {
                help.Activate();
            }
        }

        // Кнопка "Кардано"
        private void button8_Click(object sender, EventArgs e)
        {
            if (formKardano == null || formKardano.IsDisposed)
            {
                formKardano = new Kardano();
                formKardano.Show();
            }
            else
            {
                formKardano.Activate();
            }
        }

        // Кнопка "Ришелье"
        private void button9_Click(object sender, EventArgs e)
        {
            if (formRishelie == null || formRishelie.IsDisposed)
            {
                formRishelie = new Rishelie();
                formRishelie.Show();
            }
            else
            {
                formRishelie.Activate();
            }
        }

        // Кнопка "Диск Альберти"
        private void button10_Click(object sender, EventArgs e)
        {
            if (formAlberti == null || formAlberti.IsDisposed)
            {
                formAlberti = new Alberti();
                formAlberti.Show();
            }
            else
            {
                formAlberti.Activate();
            }
        }

        // Кнопка "Гронсфельд"
        private void button11_Click(object sender, EventArgs e)
        {
            if (formGronsfeld == null || formGronsfeld.IsDisposed)
            {
                formGronsfeld = new Gronsfeld();
                formGronsfeld.Show();
            }
            else
            {
                formGronsfeld.Activate();
            }
        }

        // Кнопка "Виженер"
        private void button12_Click(object sender, EventArgs e)
        {
            if (formVizhiner == null || formVizhiner.IsDisposed)
            {
                formVizhiner = new Vizhiner();
                formVizhiner.Show();
            }
            else
            {
                formVizhiner.Activate();
            }
        }

        // Кнопка "Плейфер"
        private void button13_Click(object sender, EventArgs e)
        {
            if (formPleifer == null || formPleifer.IsDisposed)
            {
                formPleifer = new Pleifer();
                formPleifer.Show();
            }
            else
            {
                formPleifer.Activate();
            }
        }

        // Кнопка "Криптосистем Хилла"
        private void button14_Click(object sender, EventArgs e)
        {
            if (formHill == null || formHill.IsDisposed)
            {
                formHill = new Hill();
                formHill.Show();
            }
            else
            {
                formHill.Activate();
            }
        }

        // Кнопка "Вернам"
        private void button15_Click(object sender, EventArgs e)
        {
            if (formVernam == null || formVernam.IsDisposed)
            {
                formVernam = new Vernam();
                formVernam.Show();
            }
            else
            {
                formVernam.Activate();
            }
        }

        // Кнопка "Частотный криптоанализ"
        private void button16_Click(object sender, EventArgs e)
        {
            if (formFreq == null || formFreq.IsDisposed)
            {
                formFreq = new Freq();
                formFreq.Show();
            }
            else
            {
                formFreq.Activate();
            }
        }

        // Кнопка "Криптоанализ Полиалфавитных шифров"
        private void button17_Click(object sender, EventArgs e)
        {
            if (formPoliCypher == null || formPoliCypher.IsDisposed)
            {
                formPoliCypher = new PoliCypher();
                formPoliCypher.Show();
            }
            else
            {
                formPoliCypher.Activate();
            }
        }

        // Кнопка "Гаммирование"
        private void button18_Click(object sender, EventArgs e)
        {
            if (formGamma == null || formGamma.IsDisposed)
            {
                formGamma = new Gamma();
                formGamma.Show();
            }
            else
            {
                formGamma.Activate();
            }
        }

        // Кнопка "DES"
        private void button19_Click(object sender, EventArgs e)
        {
            if (formDES == null || formDES.IsDisposed)
            {
                formDES = new DES();
                formDES.Show();
            }
            else
            {
                formDES.Activate();
            }
        }

        // Кнопка "ГОСТ"
        private void button1_Click(object sender, EventArgs e)
        {

            if (formGOST == null || formGOST.IsDisposed)
            {
                formGOST = new GOST();
                formGOST.Show();
            }
            else
            {
                formGOST.Activate();
            }
        }

        /* Следующие кнопки дублируют кнопки из основной формы и расположены во вкладке "Шифры" */
        
        // Кнопка "Атбаш"
        private void атбашToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click_1(sender, e);
        }

        // Кнопка "Сцитала"
        private void сциталаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        // Кнопка "Квадрат Полибия"
        private void квадратПолибияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        // Кнопка "Цезарь"
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        // Кнопка "Кардано"
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        // Кнопка "Ришелье"
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            button9_Click(sender, e);
        }

        // Кнопка "Диск Альберти"
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            button10_Click(sender, e);
        }

        // Кнопка "Гронсфельд"
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        // Кнопка "Виженер"
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            button12_Click(sender, e);
        }

        // Кнопка "Плейфер"
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            button13_Click(sender, e);
        }

        // Кнопка "Криптосистема Хилла"
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            button14_Click(sender, e);
        }

        // Кнопка "Вернам"
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            button15_Click(sender, e);
        }

        // Кнопка "Частотный критоанализ"
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            button16_Click(sender, e);
        }

        // Криптоанализ полиалфавитных шифров
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            button17_Click(sender, e);
        }

        // Кнопка "Гаммирование"
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            button18_Click(sender, e);
        }

        // Кнопка "DES"
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            button19_Click(sender, e);
        }

        // Кнопка "ГОСТ"
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        // Кнопка "Выход"
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
