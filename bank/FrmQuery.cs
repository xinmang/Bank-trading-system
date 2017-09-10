using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;


namespace bank
{
    public partial class FrmQuery : Form
    {
        Dao dao = new Dao();
        private string _card;
        SqlConnection con;
        SqlDataAdapter Ada;
        DataSet dst;
        SqlParameter par = new SqlParameter();
        
        public FrmQuery(string card)
        {
            InitializeComponent();
            this._card = card;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //打开窗体载入
        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“bankDataSet1.SelectMoney”中。您可以根据需要移动或删除它。
            this.selectMoneyTableAdapter.Fill(this.bankDataSet1.SelectMoney);
            ArrayList lis = dao.SelectYear(this._card);
            foreach (int ss in lis)
            {
                this.comboBox1.Items.Add(ss);
            }
            this.label2.Text = this._card;
            this.con = dao.getCon();
            dst = new DataSet();
            string sql = @"select*from selectMoney where cardid='" + _card + "'";
            this.Ada = new SqlDataAdapter(sql, con);
            this.Ada.Fill(this.dst, "SelectMoney");
            DataTable contacts = dst.Tables["selectMoney"];
            dataGridView1.DataSource = contacts;
        }

        // 选择年 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.label2.Text = this._card;
            ArrayList liss = new ArrayList();
            liss = dao.SelectMonth(this._card, int.Parse(this.comboBox1.Text));
            this.comboBox2.Items.Clear();
            foreach(int oo in liss)
            {
              this.comboBox2.Items.Add(oo);
              this.comboBox2.Enabled=true;
            }
        }

   
        // 选择月 
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            ArrayList list = new ArrayList();
            list = dao.SelectDay(this._card, int.Parse(this.comboBox1.Text),int.Parse(this.comboBox2.Text));
            this.comboBox3.Items.Clear();
            foreach (int oo in list)
            {
                this.comboBox3.Items.Add(oo);
                this.comboBox3.Enabled = true;
            }
        }

       
        // 选择日 
        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.comboBox1.Text.Equals("") || this.comboBox2.Text.Equals("") || this.comboBox3.Text.Equals(""))
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            this.con = dao.getCon();
            dst = new DataSet();
            string sql = @"select*from selectMoney where cardid='" + _card + "'and timeyear='"+comboBox1.Text+"' and timediana='" + comboBox2.Text +"' and timeday='" + comboBox3.Text +"'";          
            this.Ada = new SqlDataAdapter(sql, con);
            this.Ada.Fill(this.dst, "SelectMoney");
            DataTable contacts=dst.Tables["selectMoney"];
            dataGridView1.DataSource=contacts;
        }

     

        
     


    }
}
