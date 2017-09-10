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
    public partial class FrmTake : Form
    {
        string card;
        float money;
        public FrmTake(string _ff,string _vv)
        {
            InitializeComponent();
            this.card = _ff;
            this.money =float.Parse( _vv);
        }

     
       private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("取出金额不能为空！");
            }
            else
            {
                Dao dao = new Dao();
                float flo = float.Parse(this.textBox1.Text);
                if (flo <= money)
                {
                    bool boo = dao.updatee(this.card, flo);
                    if (boo)
                    {
                        dao.AddSelect1(card, flo);
                        MessageBox.Show("您支取了" + this.textBox1.Text + "元");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("余额不足，请重新输入取款金额。");
                    this.textBox1.Text = "";
                }
            }
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
    }
}
