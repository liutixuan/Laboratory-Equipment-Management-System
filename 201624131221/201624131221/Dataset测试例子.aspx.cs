using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

namespace _201624131221
{
    public partial class Dataset : System.Web.UI.Page
    {
        String sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowData();
            }

        }
        //显示员工资料至 GridView2
        void ShowData()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [equipment] ORDER BY [设备编号]", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                GridView2.DataSource = dr;
                GridView2.DataBind();
            }
        }
        /* DataSet使用范例
         1.利用连接字符串创建连接对象cn
         2.根据具体需求写sql，总是写select语句，通过where限定
         3.利用cn,sql创建adapter
         4.利用adapter来构造一个SqlCommandBuilder对象scb
         5.实例化DataSet ds
         6.利用adapter的Fill办法对ds进行填充
         7.根据不同操作要求进行增删查改操作。其中删改查要取得相应的数据，增不需要数据
          1）增：利用drow=set.Tables[0].NewRow()获取行结构，然后逐字段赋值
                     drow[index 字段名]==xx;
                     dorwt添加到指定表格的Rows集合中
          2) 删：tables[0].Rows[0].Delete()
          3) 改  drow=tables[0[.Rows[0]; drow[index 字段名]=xx;
          4) 查  根据数据的多少，循环或单条处理
         8.利用adapter的Updata办法ds更新数据库（查询不需要）         
          */

        //查询
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                if (txtId.Text == "" && txtName.Text == "")
                {
                    Response.Write("<script>alert('请输入要查询的设备编号或设备名称进行查询！')</script>");
                }
                else
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    String sql = "";
                    if (txtId.Text != "" && txtName.Text == "")
                        sql = "SELECT * FROM [equipment] where 设备编号 LIKE'%" + txtId.Text.Trim() + "%'";
                    if (txtId.Text == "" && txtName.Text != "")
                        sql = "SELECT * FROM [equipment] where 设备名称 LIKE N'%" + txtName.Text.Trim() + "%'";
                    if (txtId.Text != "" && txtName.Text != "")
                        sql = "SELECT * FROM [equipment] where 设备编号 LIKE'%" + txtId.Text.Trim() + "%' and 设备名称 LIKE N'%" + txtName.Text.Trim() + "%'";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                    ShowData();
                    /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                    int rs = Convert.ToInt32(cmd.ExecuteScalar());
                    if (rs > 0)
                    {
                        Label1.Text = "查询成功,查询结果如下：";
                        SqlDataAdapter Adapter = new SqlDataAdapter();
                        Adapter.SelectCommand = cmd;
                        DataSet myDs = new DataSet();
                        Adapter.Fill(myDs);
                        GridView1.DataSource = myDs.Tables[0].DefaultView;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('查询失败！该记录不存在！')</script>");
                    }
                    cn.Close();
                }
            }

            //另查办法
            //using (SqlConnection cn = new SqlConnection())
            //{                                                                            //另路

            //    cn.ConnectionString = sqlconn;                                         //cn.ConnectionString = sqlconn; 
            //    cn.Open();                                                             //cn.Open();                                                                                                                                 
            //    string sno = txtId.Text;                                               //SqlCommand cmd = cn.CreateCommand();
            //    string sql = "SELECT * from equipment where 设备编号='" + sno + "'";   //cmd.Command="";产生命令文本
            //    SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);                  //SqlDataReader reader=cmd.ExecuteReader();  执行命令，因为是查找命令，将查找的结果放在只读对象reader中
            //    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);     //if(reader.Read())  读取reader对象中的下一行数据
            //    DataSet ds = new DataSet();                                            //{txtId.Text=reader.GetString(1)  取当前行第一列.......}
            //    adapter.Fill(ds);//可自动打开连接和用完后自动关闭连接                 //reader.close();cn.Close();
            //    DataRow row = ds.Tables[0].Rows[0];
            //    txtName.Text = row[1].ToString();
            //    txtcompany.Text = row[2].ToString();
            //    txtdatetime.Text = row[3].ToString();
            //    txtdegree.Text = row[4].ToString();
            //    txtpeople.Text = row[5].ToString();       

            //    ShowData();
            //    cn.Close();
            //}


        }

        //删除
        protected void Button2_Click(object sender, EventArgs e)
        {   //办法1
            //using (SqlConnection cn = new SqlConnection())
            //{
            //    cn.ConnectionString = sqlconn;
            //    cn.Open();
            //    string sno = txtId.Text;
            //    string sqlstr = string.Format("DELETE FROM a WHERE 编号=@sno");
            //    SqlCommand cmd = new SqlCommand(sqlstr, cn);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.Add(new SqlParameter("@sno", sno));
            //    cmd.ExecuteNonQuery();
            //    ShowData();
            //}

            //办法2
            //using (SqlConnection cn = new SqlConnection())
            //{
            //    cn.ConnectionString = sqlconn;
            //    cn.Open();
            //    string sqlstr = string.Format("DELETE FROM equipment WHERE 设备编号='{0}' ", txtId.Text);
            //    SqlDataAdapter Adapter = new SqlDataAdapter(sqlstr, cn);
            //    DataSet myDs = new DataSet();
            //    Adapter.Fill(myDs);
            //    cn.Close();
            //    Label1.Text = "删除成功！";
            //    ShowData();
            //}

            //办法三-首选
            using (SqlConnection cn = new SqlConnection())
            {

                cn.ConnectionString = sqlconn;
                cn.Open();
                string sno = txtId.Text;               
                string sql = "SELECT * from equipment where 设备编号='"+sno+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                ds.Tables[0].Rows[0].Delete();
                adapter.Update(ds);

                ShowData();
                cn.Close();
            }

        }

        //插入
        protected void Button3_Click(object sender, EventArgs e)
        {   //办法1
            //using (SqlConnection cn = new SqlConnection())
            //   {
            //    cn.ConnectionString = sqlconn;
            //    cn.Open();
            //    try
            //    {
            //        if (txtId.Text.Trim() == "")
            //            Label1.Text = "插入失败！";
            //        else
            //        {
            //            string sqlstr = string.Format("INSERT INTO equipment(设备编号,设备名称,生产厂家,登记日期,维修次数,设备负责人)" + "VALUES('{0}',N'{1}',N'{2}','{3}','{4}',N'{5}')", txtId.Text, txtName.Text, txtcompany.Text, txtdatetime.Text, txtdegree.Text, txtpeople.Text);
            //            SqlDataAdapter Adapter = new SqlDataAdapter(sqlstr, cn);
            //            DataSet myDs = new DataSet();
            //            Adapter.Fill(myDs);
            //            cn.Close();
            //            Label1.Text = "插入成功！";
            //        }
            //    }
            //    catch
            //    {
            //        Label1.Text = "该设备已存在，请重新输入！";
            //    }
            //    ShowData();
            //}


            // 办法2
            //using (SqlConnection cn = new SqlConnection())
            //{

            //    cn.ConnectionString = sqlconn;
            //    cn.Open();
            //    SqlCommand cmd = cn.CreateCommand();                              
            //    string sno = txtId.Text;//设置命令参数值
            //    string name = txtName.Text;
            //    string company = txtcompany.Text;
            //    string time = txtdatetime.Text;
            //    string degree = txtdegree.Text;
            //    string people = txtpeople.Text;
            //    //产生命令文本
            //    cmd.CommandText = "INSERT INTO equipment(设备编号,设备名称,生产厂家,登记日期,维修次数,设备负责人)  VALUES(@sno,@name,@company,@time,@degree,@people)";
            //    cmd.CommandType = CommandType.Text;//设置命令类型为文本类型
            //    cmd.Parameters.Add(new SqlParameter("@sno", sno));//设置命令参数
            //    cmd.Parameters.Add(new SqlParameter("@name", name));
            //    cmd.Parameters.Add(new SqlParameter("@company", company));
            //    cmd.Parameters.Add(new SqlParameter("@time", time));
            //    cmd.Parameters.Add(new SqlParameter("@degree", degree));
            //    cmd.Parameters.Add(new SqlParameter("@people", people));
            //    cmd.ExecuteNonQuery();//执行命令
            //    ShowData();
            //    cn.Close();
            //}

            //办法三-首选
            using (SqlConnection cn = new SqlConnection())
            {

                cn.ConnectionString = sqlconn;
                cn.Open();          
                string sno = txtId.Text;
                string name = txtName.Text;
                string company = txtcompany.Text;
                string time = txtdatetime.Text;
                string degree = txtdegree.Text;
                string people = txtpeople.Text;
                string sql = "SELECT * from equipment";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataRow row = ds.Tables[0].NewRow();
                row[0] = sno;
                row[1] = name;
                row[2] = company;
                row[3] = time;
                row[4] = degree;
                row[5] = people;

                ds.Tables[0].Rows.Add(row);
                adapter.Update(ds);

                ShowData();
                cn.Close();
            }

        }

        //修改
        protected void Button4_Click(object sender, EventArgs e)
        {   //办法1
            //    using (SqlConnection cn = new SqlConnection())
            //    {
            //        cn.ConnectionString = sqlconn;
            //        cn.Open();                                  
            //        string sqlstr = string.Format("UPDATE equipment SET 设备名称=N'{0}',生产厂家=N'{1}',登记日期='{2}',维修次数='{3}',设备负责人=N'{4}'" +
            //        " WHERE 设备编号='{5}'", txtName.Text, txtcompany.Text, txtdatetime.Text, txtdegree.Text, txtpeople.Text, txtId.Text);
            //        SqlDataAdapter Adapter = new SqlDataAdapter(sqlstr, cn);
            //        DataSet myDs = new DataSet();
            //        Adapter.Fill(myDs);
            //        cn.Close();              
            //        ShowData();               
            //    }

            //办法2
            //using (SqlConnection cn = new SqlConnection())
            //{
            //    cn.ConnectionString = sqlconn;
            //    cn.Open();
            //    SqlCommand cmd = cn.CreateCommand();
            //    string sno = txtId.Text;
            //    string name = txtName.Text;
            //    string company = txtcompany.Text;
            //    string time = txtdatetime.Text;
            //    string degree = txtdegree.Text;
            //    string people = txtpeople.Text;
            //    cmd.CommandText = "UPDATE equipment Set 设备名称 = @name,生产厂家 = @company,登记日期 = @time,维修次数 = @degree,设备负责人 = @people where 设备编号 = @sno";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.Add(new SqlParameter("@sno", sno));//不可无
            //    cmd.Parameters.Add(new SqlParameter("@name", name));
            //    cmd.Parameters.Add(new SqlParameter("@company", company));
            //    cmd.Parameters.Add(new SqlParameter("@time", time));
            //    cmd.Parameters.Add(new SqlParameter("@degree", degree));
            //    cmd.Parameters.Add(new SqlParameter("@people", people));
            //    cmd.ExecuteNonQuery();
            //    ShowData();
            //    cn.Close();
            //}

            //办法三-首选
            using (SqlConnection cn = new SqlConnection())
            {

                cn.ConnectionString = sqlconn;
                cn.Open();
                string sno = txtId.Text;
                string name = txtName.Text;
                string company = txtcompany.Text;
                string time = txtdatetime.Text;
                string degree = txtdegree.Text;
                string people = txtpeople.Text;
                string sql = "SELECT * from equipment where 设备编号='" + sno + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataRow row = ds.Tables[0].Rows[0];
                row[1] = name;
                row[2] = company;
                row[3] = time;
                row[4] = degree;
                row[5] = people;

                adapter.Update(ds);

                ShowData();
                cn.Close();
            }


        }



        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}

    