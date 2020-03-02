using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace _201624131221
{
    public partial class second : System.Web.UI.Page
    {
        String sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; ";
        protected void Page_Load(object sender, EventArgs e)
        {   //操作前提示
            Button2.Attributes["onClick"] = "javascript:return confirm('你确认要插入吗？');";
            Button3.Attributes["onClick"] = "javascript:return confirm('你确认要修改吗？');";
            Button4.Attributes["onClick"] = "javascript:return confirm('你确认要删除吗？');";
            Button6.Attributes["onClick"] = "javascript:return confirm('你确认要插入吗？');";
            Button7.Attributes["onClick"] = "javascript:return confirm('你确认要修改吗？');";
            Button8.Attributes["onClick"] = "javascript:return confirm('你确认要删除吗？');";
            if (!Page.IsPostBack)
            {
                ShowData();
                ShowData1();
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

        //显示员工资料至 GridView4
        void ShowData1()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [circulate] ORDER BY [借出时间]", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                GridView4.DataSource = dr;
                GridView4.DataBind();
            }
        }

        //equipment表操作

        //查询
        protected void Button1_Click1(object sender, EventArgs e)
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
        }

        //插入     
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                Response.Write("<script>alert('设备编号不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from equipment where 设备编号= '{0}'", txtId.Text), cn);
                    if ((int)cmd.ExecuteScalar() > 0)  //该记录已存在，无法插入
                    {
                        Response.Write("<script>alert('请先查询该记录是否已存在')</script>");

                    }
                    else
                    {
                        try
                        {
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
                            Response.Write("<script>alert('插入成功！')</script>");
                            ShowData();
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('插入失败！')</script>");
                            //Response.Write(ex.Message);
                        }
                    }
                    cn.Close();
                }
            }
        }
         
        //修改
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                Response.Write("<script>alert('设备编号不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from equipment where 设备编号= '{0}'", txtId.Text), cn);
                    if ((int)cmd.ExecuteScalar()> 0)//该记录存在，可以进行修改
                    {
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
                        Response.Write("<script>alert('修改成功！')</script>");                                           
                    }
                    else
                    {
                        Response.Write("<script>alert('修改失败！请先查询该记录是否已存在')</script>");
                    }
                    cn.Close();
                    ShowData();
                }
            }
        }
        
        //删除
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                Response.Write("<script>alert('请输入要删除的设备编号！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from equipment where 设备编号= '{0}'", txtId.Text), cn);
                    if ((int)cmd.ExecuteScalar() > 0)
                    {
                        string sno = txtId.Text;
                        string sql = "SELECT * from equipment where 设备编号='" + sno + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        ds.Tables[0].Rows[0].Delete();
                        adapter.Update(ds);
                 
                        Response.Write("<script>alert('删除成功！')</script>");
                        ShowData();
                    }
                    else
                    {
                        Response.Write("<script>alert('删除失败，请确认该记录存在！')</script>");
                    }
                    cn.Close();
                }
            }
        }


        //circulate操作

        //查询
        protected void Button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                if (txtId1.Text == "" && txttime.Text == "" && txtbName.Text == "")
                {
                    Response.Write("<script>alert('请输入要查询的设备编号或借出时间或借用人姓名进行查询！')</script>");
                }
                else
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    String sql = "";   //列举所有情况
                    if (txtId1.Text != "" && txttime.Text == "" && txtbName.Text == "")
                        sql = "SELECT * FROM [circulate] where 设备编号 LIKE'%" + txtId1.Text.Trim() + "%'";
                    if (txtId1.Text != "" && txttime.Text != "" && txtbName.Text == "")
                        sql = "SELECT * FROM [circulate] where 设备编号 LIKE'%" + txtId1.Text.Trim() + "%' and 借出时间 LIKE N'%" + txttime.Text.Trim() + "%'";
                    if (txtId1.Text != "" && txttime.Text == "" && txtbName.Text != "")
                        sql = "SELECT * FROM [circulate] where 设备编号 LIKE'%" + txtId1.Text.Trim() + "%' and 借用人姓名 LIKE N'%" + txtbName.Text.Trim() + "%'";
                    if (txtId1.Text == "" && txttime.Text != "" && txtbName.Text == "")
                        sql = "SELECT * FROM [circulate] where 借出时间 LIKE N'%" + txttime.Text.Trim() + "%'";
                    if (txtId1.Text == "" && txttime.Text == "" && txtbName.Text != "")
                        sql = "SELECT * FROM [circulate] where 借用人姓名 LIKE N'%" + txtbName.Text.Trim() + "%'";
                    if (txtId1.Text == "" && txttime.Text != "" && txtbName.Text != "")
                        sql = "SELECT * FROM [circulate] where 借出时间 LIKE'%" + txttime.Text.Trim() + "%' and 借用人姓名 LIKE N'%" + txtbName.Text.Trim() + "%'";
                    if (txtId1.Text != "" && txttime.Text != "" && txtbName.Text != "")
                        sql = "SELECT * FROM [circulate] where 设备编号 LIKE'%" + txtId1.Text.Trim() + "%' and 借出时间 LIKE N'%" + txttime.Text.Trim() + "%' and 借用人姓名 LIKE N'%" + txtbName.Text.Trim() + "%'";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();               
                    ShowData1();
                    /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                    int rs = Convert.ToInt32(cmd.ExecuteScalar());
                    if (rs > 0) //查询到数据，放到GridView3中
                    {
                        Label2.Text = "查询成功,查询结果如下：";
                        SqlDataAdapter Adapter = new SqlDataAdapter();
                        Adapter.SelectCommand = cmd;
                        DataSet myDs = new DataSet();
                        Adapter.Fill(myDs);
                        GridView3.DataSource = myDs.Tables[0].DefaultView;
                        GridView3.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('查询失败！该记录不存在！')</script>");
                    }
                    cn.Close();
                }
            }
        }

        //插入
        protected void Button6_Click(object sender, EventArgs e)
        {

            if (txtId1.Text == "" || txttime.Text == "" || txtbName.Text == "")
            {
                Response.Write("<script>alert('设备编号和借出时间和借用人姓名不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from [equipment] where 设备编号= '{0}'", txtId1.Text), cn);
                    if ((int)cmd.ExecuteScalar() > 0)  //该记录在equipment中存在，再看是否已借出
                    {
                        SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from [circulate] where 设备编号= '{0}'", txtId1.Text), cn);
                        if ((int)cmd1.ExecuteScalar() > 0)//该记录在借出信息表中
                        {
                            SqlCommand cmd2 = new SqlCommand(string.Format("select Count(*) from [circulate] where 是否已还=N'是' and 设备编号= " + txtId1.Text.Trim() + ""), cn);
                            if ((int)cmd2.ExecuteScalar() > 0)//已还，可借出
                            {
                                try
                                {
                                    string Id1 = txtId1.Text;
                                    string time = txttime.Text;
                                    string Name = txtbName.Text;
                                    string pl = txtpl.Text;
                                    string DL = DropDownList1.SelectedItem.Text;                                   
                                    string sql = "SELECT * from circulate";
                                    SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                                    DataSet ds = new DataSet();
                                    adapter.Fill(ds);

                                    DataRow row = ds.Tables[0].NewRow();
                                    row[0] = Id1;
                                    row[1] = time;
                                    row[2] = Name;
                                    row[3] = pl;
                                    row[4] = DL;                            

                                    ds.Tables[0].Rows.Add(row);
                                    adapter.Update(ds);
                                    
                                    Response.Write("<script>alert('插入成功！')</script>");
                                    ShowData1();
                                    cn.Close();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("<script>alert('插入失败！')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('该设备已被借出！')</script>");
                            }
                        }
                        else//该记录不在借出信息表中，可插入
                        {
                            try
                            {
                                string Id1 = txtId1.Text;
                                string time = txttime.Text;
                                string Name = txtbName.Text;
                                string pl = txtpl.Text;
                                string DL = DropDownList1.SelectedItem.Text;
                                string sql = "SELECT * from circulate";
                                SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);

                                DataRow row = ds.Tables[0].NewRow();
                                row[0] = Id1;
                                row[1] = time;
                                row[2] = Name;
                                row[3] = pl;
                                row[4] = DL;

                                ds.Tables[0].Rows.Add(row);
                                adapter.Update(ds);

                                Response.Write("<script>alert('插入成功！')</script>");
                                ShowData1();
                                cn.Close();
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<script>alert('插入失败！')</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('请先查询是否有该设备,若不存在无法借出!')</script>");
                    }
                }
            }
        }

        //修改
        protected void Button7_Click(object sender, EventArgs e)
        {

            if (txtId1.Text == "" || txttime.Text == "" || txtbName.Text == "")
            {
                Response.Write("<script>alert('设备编号和借出时间和借用人姓名不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from [circulate] where 设备编号='{0}' and  借出时间= '{1}' and 借用人姓名=N'{2}'", txtId1.Text, txttime.Text, txtbName.Text), cn);
                    if ((int)cmd.ExecuteScalar() > 0)//该记录存在，可以进行修改
                    {
                        string Id1 = txtId1.Text;
                        string time = txttime.Text;
                        string Name = txtbName.Text;
                        string pl = txtpl.Text;
                        string DL = DropDownList1.SelectedItem.Text;
                        string sql = "SELECT * from circulate where 设备编号='" + Id1 + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        DataRow row = ds.Tables[0].Rows[0];                                       
                        row[3] = pl;
                        row[4] = DL;
                      
                        adapter.Update(ds);
                        ShowData1();
                        Response.Write("<script>alert('修改成功！')</script>");                                                                                                              
                    }
                    else
                    {
                        Response.Write("<script>alert('修改失败！请先查询该记录是否已存在')</script>");
                    }
                    cn.Close();
                }
            }
        }

        //删除
        protected void Button8_Click(object sender, EventArgs e)
        {
            if (txtId1.Text == "" && txttime.Text == "" && txtbName.Text == "")
            {
                Response.Write("<script>alert('请输入要删除的设备编号或借出时间或借用人姓名！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from [circulate] where 是否已还=N'是' and 设备编号= '" + txtId1.Text.Trim() + "'"), cn);
                    if ((int)cmd.ExecuteScalar() > 0) //已还可删除
                    {
                        SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from [circulate] where 设备编号='{0}' and  借出时间= '{1}' and 借用人姓名=N'{2}'", txtId1.Text, txttime.Text, txtbName.Text), cn);
                        if ((int)cmd1.ExecuteScalar() > 0)//该记录符合删除条件，可以删除
                        {
                            string Id1 = txtId1.Text;
                            string time = txttime.Text;
                            string Name = txtbName.Text;
                            string sql = "SELECT * from circulate where 设备编号='" + Id1 + "' and  借出时间= '"+time+"' and 借用人姓名=N'"+Name+"' ";
                            SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
                            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            ds.Tables[0].Rows[0].Delete();  //删除
                            adapter.Update(ds);
                         
                            Response.Write("<script>alert('删除成功！')</script>");
                            ShowData1();
                            cn.Close();
                        }
                        else
                        {
                            Response.Write("<script>alert('删除失败！必须保证设备编号，借出时间，借用人一致才可以删除')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('设备尚未归还，无法删除！')</script>");
                    }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}