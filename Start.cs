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

            button3.Enabled = true;     //Атбаш
            button5.Enabled = true;     //Сцитала   
            button6.Enabled = true;     //Квадрат Полибия
            button7.Enabled = true;     //Цезарь
            button8.Enabled = true;     //Кардано
            button9.Enabled = true;     //Ришелье
            button10.Enabled = true;    //Диск Альберти
            button11.Enabled = true;    //Гронсфельд
            button12.Enabled = true;    //Виженер
            button13.Enabled = true;    //Плейфер
            button14.Enabled = true;    //Криптосистема Хилла
            button15.Enabled = true;    //Вернам
            button16.Enabled = true;    //Частотный криптоанализ
            button17.Enabled = false;   //Криптоанализ полиалфавитных шифров
            button18.Enabled = false;   //Гаммирование
            button19.Enabled = false;   //***
            button1.Enabled = false;    //***

            groupStartButtons.Visible = true;
            groupStartButtons.Enabled = true;
        }

        private void атбашToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click_1(sender, e);
        }

        private void сциталаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options formSetting = new Options();
            formSetting.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Atbash formAtbash = new Atbash();
            formAtbash.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Skitala formSkitala = new Skitala();
            formSkitala.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Polibium formPolibia = new Polibium();
            formPolibia.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Caesar formCaezar = new Caesar();
            formCaezar.Show();
        }

        private void цезарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void квадратПолибияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp help = new FormHelp();
            help.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Kardano formKardano = new Kardano();
            formKardano.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Rishelie formRishelie = new Rishelie();
            formRishelie.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Alberti formAlberti = new Alberti();
            formAlberti.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Gronsfeld formGronsfeld = new Gronsfeld();
            formGronsfeld.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Vizhiner formVizhiner = new Vizhiner();
            formVizhiner.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Pleifer formPleifer = new Pleifer();
            formPleifer.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Hill formHill = new Hill();
            formHill.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Vernam formVernam = new Vernam();
            formVernam.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Freq formFreq = new Freq();
            formFreq.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            PoliCypher formPoliCypher = new PoliCypher();
            formPoliCypher.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Gamma formGamma = new Gamma();
            formGamma.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }
    }
}
