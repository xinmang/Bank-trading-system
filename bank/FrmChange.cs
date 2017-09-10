using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace bank
{
    public partial class FrmChange : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ASUS\LIJDB;Initial Catalog=bank;Integrated Security=True");
        SqlCommand comm;
        string _name = "";

        public FrmChange(string name)
        {
            InitializeComponent();
            this._name = name;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password1 = this.maskedTextBox2.Text;
            string password2 = this.maskedTextBox3.Text;
            string Standardm1 = "0";
            string Standardm2 = "0";
            string Standardm3 = "0";
            Standardm1 = this.maskedTextBox1.Text.Trim();
            Standardm2 = this.maskedTextBox1.Text.Trim();
            Standardm3 = this.maskedTextBox1.Text.Trim();
            Regex digitregex1 = new Regex(@"^[0-9]\d*[.]?\d*$");
            Regex digitregex2 = new Regex(@"^[0-9]\d*[.]?\d*$");
            Regex digitregex3 = new Regex(@"^[0-9]\d*[.]?\d*$");
            if (digitregex1.IsMatch(Standardm1) == false || digitregex2.IsMatch(Standardm2) == false || digitregex3.IsMatch(Standardm3) == false)
            {
                MessageBox.Show("输入非法!请输入纯数字！");
                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
                maskedTextBox3.Clear();
            }
            else
            {
                con.Open();
                Dao dao = new Dao();
                bool boo = dao.GetEnter(_name, maskedTextBox1.Text);                     //修改密码之前先查询原始密码是否正确 
                if (boo)
                {
                    if (maskedTextBox2.Text == maskedTextBox3.Text)                     //两次密码是否相等
                    {
                        string str = "UPDATE banktable set pass='" + maskedTextBox3.Text + "'Where cardid='" + _name + "'";
                        comm = new SqlCommand(str, con);
                        int result = comm.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("密码修改成功！");
                            con.Close();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码修改失败！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("两次密码输入不一致！");
                        maskedTextBox1.Clear();
                        maskedTextBox2.Clear();
                        maskedTextBox3.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("原密码输入错误！请重新输入");
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();
                    maskedTextBox3.Clear();
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
