using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        kubilay kb = new kubilay();
        private void Form1_Load(object sender, EventArgs e)
        {
            kb.sorulari_getir(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string soruno = comboBox1.SelectedValue.ToString();
            kb.cevaplari_getir(soruno, listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex>=0)
            { 
            string soruno = comboBox1.SelectedValue.ToString();
            string cevapno = listBox1.SelectedValue.ToString();
            kb.oyver(cevapno);
            kb.grafik_ciz(soruno, chart1);
            }
            else
            {
                MessageBox.Show("Bir cevap seçmelisiniz!");
            }
        }
    }
}
