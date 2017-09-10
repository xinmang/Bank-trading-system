using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bank
{
    public partial class FrmSave : Form
    {
        string card;
        public FrmSave(string _ff)
        {
            InitializeComponent();
            this.card = _ff;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("存入金额不能为空！");
            }
            else
            {
                Dao dao = new Dao();
                float flo = float.Parse(this.textBox1.Text);
                bool boo = dao.update(this.card, flo);
                if (boo)
                {
                    dao.AddSelect(card, flo);
                    MessageBox.Show("您存入了" + this.textBox1.Text + "元");
                    this.Close();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
    }
}
