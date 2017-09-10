using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;
using System.Collections;


namespace bank
{
    class Dao
    {
        public System.Windows.Forms.Form ff;
        public System.Windows.Forms.Timer timer;
        public Dao()
        {}

        public SqlConnection getCon()
        {
            SqlConnection con = new SqlConnection(@"Data Source=ASUS\LIJDB;Initial Catalog=bank;Integrated Security=True");
            return con;
        }


        public bool GetEnter(string card, string pass)
        {

            bool boo = false;
            string select = "select * from banktable where cardid='" +card+ "' and pass='" +pass+ "'";
            SqlConnection con = this.getCon();

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(select, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                    boo = true;
            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            finally
            {
              con.Close();
            }          
            return boo;
        }


        public float GetMoney(string card)
        {
            float num = 0.0f;
            string select = "select money from banktable where cardid='" + card + "'";
            SqlConnection con = this.getCon();

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(select, con);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                    num = (float)reader.GetSqlDouble(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return num;
        }


        public void change()
        {
            this.ff.Opacity += 0.1;
            if (this.ff.Opacity > 100)
                this.timer.Dispose();
        }


        // 存款更新
        public bool update(string card, float money)
        {
            bool boo = false;
            float mm = this.GetMoney(card) + money;
            string update = "update banktable set [money]='" +mm+ "' where cardid='" +card+ "'";
            SqlConnection con = this.getCon();

            try
            {
                SqlCommand com = new SqlCommand(update, con);
                con.Open();
                com.ExecuteNonQuery();
                boo = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return boo;
        }


    
        // 支取更新 
        public bool updatee(string card, float money)
        {
            bool boo = false;
            float mm = this.GetMoney(card) - money;
            string up = "update banktable set [money]='" +mm+ "' where cardid='" + card + "'";
            SqlConnection con = this.getCon();

            try
            {
                SqlCommand com = new SqlCommand(up,con);
                con.Open();
                com.ExecuteNonQuery();
                boo = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return boo;

        }

  
        // 增加存款记录
        public void AddSelect(string card, float money)
        {
            SqlConnection con = this.getCon();
            System.DateTime current = new DateTime();
            current = DateTime.Now;
            int _Year = current.Year;
            int _Month = current.Month;
            int _Day = current.Day;
            string st = "insert into SelectMoney (cardid,timeYear,timeDiana,timeDay,depositMoney) values ('" + card + "'," + _Year + "," + _Month + "," + _Day + "," + money + ")";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st,con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }            
        }


        // 增加取款记录  
        public void AddSelect1(string card, float money)
        {
            SqlConnection con = this.getCon();
            System.DateTime current = new DateTime();
            current = DateTime.Now;
            int _Year = current.Year;
            int _Month = current.Month;
            int _Day = current.Day;
            string st = "insert into SelectMoney(cardid,timeYear,timeDiana,timeDay,fetchMoney) values('" + card + "'," + _Year + "," + _Month + "," + _Day + "," + money + ")";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void AddTransfer(string na, string card, float money)
        {
            SqlConnection con = this.getCon();
            System.DateTime current = new DateTime();
            current = DateTime.Now;
            int _Year = current.Year;
            int _Month = current.Month;
            int _Day = current.Day;
            string st = "insert into SelectMoney (cardid,timeYear,timeDiana,timeDay,Transferid,TransferMoney) values('" + na + "'," + _Year + "," + _Month + "," + _Day + ",'" + card + "'," + money + ")";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

       
        // 获取年份记录列表   
        public ArrayList SelectYear(string card)
        {
            ArrayList lis = new ArrayList();
            SqlConnection con = this.getCon();
            string st = "select distinct timeYear from SelectMoney where cardid='" + card + "'";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lis.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return lis;
        }

     
        // 获取月份记录列表
        public ArrayList SelectMonth(string card,int year)
        {
            ArrayList lis = new ArrayList();
            SqlConnection con = this.getCon();
            string st = "select distinct timeDiana from SelectMoney where cardid='" + card + "' and timeYear="+year;

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lis.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return lis;
        }


   
        // 获取天记录列表  
        public ArrayList SelectDay(string card,int year,int month)
        {
            ArrayList lis = new ArrayList();
            SqlConnection con = this.getCon();
            string st = "select distinct timeDay from SelectMoney where cardid='" + card + "' and timeYear=" + year + " and timeDiana=" + month;

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(st, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lis.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return lis;
        }


        public bool BoolNmae(string card)
        {
            SqlConnection con = this.getCon();
            string str = "select * from banktable where cardid='" + card + "'";

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                    return true;                              //如查到结果
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return false;
        }

   
        //转账     
        //接入账户
        //转出金额     
        public bool TransferMoney(string card, string name1, float money)
        {
            SqlConnection con = this.getCon();
            float fol = this.GetMoney(card) - money;
            float foo = this.GetMoney(name1) + money;
            string srt = "update banktable set [money]=" + fol + " where cardid='" + card + "' update banktable set [money]=" + foo + " where cardid='" + name1 + "'";
          
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(srt, con);
                int i = com.ExecuteNonQuery();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return false;
        }

        

    }
}
