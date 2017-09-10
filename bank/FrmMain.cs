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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.2;                     //每次改变Form的不透明属性       
            if (this.Opacity >= 1.0)                 //当Form完全显示时，停止计时    
            {
                this.timer1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }


        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            string card = this.textBox1.Text;
            string pass = this.textBox2.Text;
            if (card == "" || pass == "")
            {
                MessageBox.Show("卡号和密码不能为空！");
            }
            else
            {
                string Standardm = "0";                                        //初始化一个变量
                Standardm = this.textBox2.Text.Trim();                         //文本框内容赋值给变量
                Regex digitregex = new Regex(@"^[0-9]\d*[.]?\d*$");            // 初始化正则表达式
                if (digitregex.IsMatch(Standardm) == false)                    //判断文本框内容是否符合正则表达式
                {
                    MessageBox.Show("输入非法!请输入纯数字！");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    Dao dao = new Dao();
                    bool boo = dao.GetEnter(card, pass);
                    if (boo)
                    {
                        groupBox2.Visible = true;
                        label5.Text = card;
                        label6.Text = dao.GetMoney(card).ToString();
                        groupBox2.Text = "尊敬的客户，您好";
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                        button8.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("密码或用户名不正确!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
        }


        //清空
        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }


        //查询按钮
        private void button3_Click(object sender, EventArgs e)
        {
            FrmQuery form5 = new FrmQuery(this.label5.Text);
            form5.ShowDialog();
        }

        //存款
        private void button6_Click(object sender, EventArgs e)
        {
            FrmSave form3 = new FrmSave(this.label5.Text);
            form3.ShowDialog();
            Dao dao = new Dao();
            label6.Text = dao.GetMoney(this.label5.Text).ToString();
        }

        //取款
        private void button7_Click(object sender, EventArgs e)
        {
            FrmTake form4 = new FrmTake(this.label5.Text, this.label6.Text);
            form4.ShowDialog();
            Dao dao = new Dao();
            label6.Text = dao.GetMoney(this.label5.Text).ToString();
        }

        //转账
        private void button8_Click(object sender, EventArgs e)
        {
            FrmTransfer form6 = new FrmTransfer(this.label5.Text);
            form6.ShowDialog();
            Dao dao = new Dao();
            label6.Text = dao.GetMoney(this.label5.Text).ToString();
        }

        //退出
        private void button9_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
        }

        //修改密码
        private void button4_Click(object sender, EventArgs e)
        {
            FrmChange form1 = new FrmChange(this.label5.Text);
            form1.ShowDialog();
        }
    }
}
