using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace bank
{
    public partial class FrmTransfer : Form
    {
        string _card;
        public FrmTransfer(string card)
        {
            InitializeComponent();
            this._card = card;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = this._card;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            Dao dao = new Dao();
            this.textBox2.Text = dao.GetMoney(this._card).ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "" || this.textBox4.Text == "")
            {
                MessageBox.Show("卡号和金额不能为空！");
            }
            else
            {
                string Standardm = "0";
                Standardm = this.textBox3.Text.Trim();
                Regex digitregex = new Regex(@"^[0-9]\d*[.]?\d*$");
                if (digitregex.IsMatch(Standardm) == false)
                {
                    MessageBox.Show("输入非法!请输入纯数字！");
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    if (this.textBox3.Text == _card)
                    {
                        MessageBox.Show("不能给自己转账！");
                        this.textBox3.Text = "";
                    }
                    else
                    {
                        Dao dao = new Dao();
                        if (dao.BoolNmae(this.textBox1.Text))
                        {
                            float foo = dao.GetMoney(this.textBox1.Text);
                            if (foo < float.Parse(this.textBox4.Text))
                            {
                                MessageBox.Show("您的余额不足！");
                                return;
                            }
                            else
                            {
                                bool boo = dao.TransferMoney(this.textBox1.Text, this.textBox3.Text, float.Parse(this.textBox4.Text));
                                bool bol = dao.BoolNmae(this.textBox3.Text);
                                if (bol)
                                {
                                    if (boo)
                                    {
                                        dao.AddTransfer(this.textBox1.Text, this.textBox3.Text, float.Parse(this.textBox4.Text));
                                        MessageBox.Show("转账成功!");
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("转入账户名有错误！");
                                    this.textBox3.Text = "";
                                    this.textBox4.Text = "";
                                }
                            }
                        }
                    }
                }

            }
        }





    }
}
