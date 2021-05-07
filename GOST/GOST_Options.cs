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
    public partial class GOST_Options : Form
    {
        public GOST_Options()
        {
            InitializeComponent();
        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            //toolTip1.Show("Отображение считываемых файлов и результата шифрования в виде множества байтов. " +
            //       "\nВнимание! Включение данной функции может негативно сказаться на быстродействии системы!", "My tooltip");
            ToolTip t = new ToolTip();
            t.SetToolTip(checkBox1, "Отображение считываемых файлов и результата шифрования в виде множества байтов. " +
                   "\nВнимание! Включение данной функции может негативно сказаться на быстродействии системы!");

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        public bool Send_Details()
        {
            if (checkBox1.Checked)
            {
                return true;
            }
            else if (!checkBox1.Checked)
            {
                return false;
            }
            return false;
        }
    }
}
