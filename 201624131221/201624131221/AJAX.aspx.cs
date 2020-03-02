using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _201624131221
{
    public partial class third : System.Web.UI.Page
    {
        String sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; ";
        protected void Page_Load(object sender, EventArgs e)
        {   //操作前提示
            Button2.Attributes["onClick"] = "javascript:return confirm('你确认要插入吗？');";
            Button3.Attributes["onClick"] = "javascript:return confirm('你确认要修改吗？');";
            Button4.Attributes["onClick"] = "javascript:return confirm('你确认要删除吗？');";
            Button7.Attributes["onClick"] = "javascript:return confirm('你确认要插入吗？');";
            Button8.Attributes["onClick"] = "javascript:return confirm('你确认要修改吗？');";
            Button9.Attributes["onClick"] = "javascript:return confirm('你确认要删除吗？');";
            if (!Page.IsPostBack)
            {
                ShowData();
            }
            if (!Page.IsPostBack)
            {
                ShowData1();
            }
        }
        //显示员工资料至 GridView
        void ShowData()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM equipment", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                GridView5.DataSource = dr;
                GridView5.DataBind();
            }
        }

        //显示员工资料至 GridView
        void ShowData1()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM circulate", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                GridView6.DataSource = dr;
                GridView6.DataBind();
            }
        }

        //equipment表操作

        //查询功能
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
                    ShowData();
                    /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                    int rs = Convert.ToInt32(cmd.ExecuteScalar());
                    if (rs > 0)
                    {
                        Label1.Text = "查询成功,查询结果如下：";
                        SqlDataReader dr = cmd.ExecuteReader();
                        GridView1.DataSource = dr;
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

        //插入功能
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
                            string sqlstr = string.Format("INSERT INTO equipment(设备编号,设备名称,生产厂家,登记日期,维修次数,设备负责人)" + "VALUES('{0}',N'{1}',N'{2}','{3}','{4}',N'{5}')", txtId.Text, txtName.Text, txtcompany.Text, txtdatetime.Text, txtdegree.Text, txtpeople.Text);
                            SqlCommand cmd1 = new SqlCommand(sqlstr, cn);
                            cmd1.ExecuteNonQuery();
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

        //修改功能
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
                    if ((int)cmd.ExecuteScalar() > 0)//该记录存在，可以进行修改
                    {
                        string sqlstr = string.Format("UPDATE equipment SET 设备名称=N'{0}',生产厂家=N'{1}',登记日期='{2}',维修次数='{3}',设备负责人=N'{4}'" +
                        " WHERE 设备编号='{5}'", txtName.Text, txtcompany.Text, txtdatetime.Text, txtdegree.Text, txtpeople.Text, txtId.Text);
                        SqlCommand cmd1 = new SqlCommand(sqlstr, cn);
                        cmd1.ExecuteNonQuery();
                        ShowData();
                        /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                        int rs = cmd1.ExecuteNonQuery();
                        if (rs > 0)
                        {
                            Response.Write("<script>alert('修改成功！')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('该记录不存在或输入格式有误，请重新修改！')</script>");
                        }
                        cn.Close();
                    }
                    else
                    {
                        Response.Write("<script>alert('请先查询该记录是否已存在')</script>");
                    }
                }
            }
        }

        //删除功能
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
                        string sqlstr1 = string.Format("DELETE FROM equipment where 设备编号='{0}' ", txtId.Text);
                        SqlCommand cmd2 = new SqlCommand(sqlstr1, cn);
                        cmd2.ExecuteNonQuery();
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

        //刷新 Response.Redirect(Request.Url.ToString());      

        //circulate操作

        //查询功能
        protected void Button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                if (txtId1.Text == "" && txttime.Text == "" && txtbName.Text == "")
                {
                    Label2.Text = "请输入要查询的设备编号或借出时间或借用人姓名进行查询！";                  
                }
                else
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    String sql = "";
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
                    ShowData1();
                    /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                    int rs = Convert.ToInt32(cmd.ExecuteScalar());
                    if (rs > 0)
                    {
                        Label2.Text = "查询成功,查询结果如下：";
                        SqlDataReader dr = cmd.ExecuteReader();
                        GridView3.DataSource = dr;
                        GridView3.DataBind();
                    }
                    else
                    {                       
                        Label2.Text = "查询失败！该记录不存在！";
                    }
                    cn.Close();
                }
            }

        }

        //插入功能
        protected void Button7_Click(object sender, EventArgs e)
        {

            if (txtId1.Text == "" || txttime.Text == "" || txtbName.Text == "")
            {
                Label2.Text = "设备编号和借出时间和借用人姓名不能为空！";
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
                                    string sqlstr1 = string.Format("INSERT INTO circulate(设备编号,借出时间,借用人姓名,设备负责人,是否已还)" + "VALUES('{0}','{1}',N'{2}',N'{3}',N'{4}')", txtId1.Text, txttime.Text, txtbName.Text, txtpl.Text, DropDownList1.SelectedItem.Text);
                                    SqlCommand cmd3 = new SqlCommand(sqlstr1, cn);
                                    cmd3.ExecuteNonQuery();
                                    Label2.Text = "插入成功！";
                                    ShowData1();
                                    cn.Close();
                                }
                                catch (Exception ex)
                                {
                                    Label2.Text = "插入失败！";
                                }
                            }
                            else
                            {
                                Label2.Text = "该设备已被借出！";
                            }
                        }
                        else//该记录不在借出信息表中，可插入
                        {

                            try
                            {
                                string sqlstr1 = string.Format("INSERT INTO circulate(设备编号,借出时间,借用人姓名,设备负责人,是否已还)" + "VALUES('{0}','{1}',N'{2}',N'{3}',N'{4}')", txtId1.Text, txttime.Text, txtbName.Text, txtpl.Text, DropDownList1.SelectedItem.Text);
                                SqlCommand cmd2 = new SqlCommand(sqlstr1, cn);
                                cmd2.ExecuteNonQuery();
                                Label2.Text = "插入成功！";
                                ShowData1();
                                cn.Close();
                            }
                            catch (Exception ex)
                            {
                                Label2.Text = "插入失败！";
                            }
                        }
                    }
                    else
                    {
                        Label2.Text = "请先查询是否有该设备,若不存在无法借出!";
                    }
                }
            }
        }


        //修改功能
        protected void Button8_Click(object sender, EventArgs e)
        {

            if (txtId1.Text == "" || txttime.Text == "" || txtbName.Text == "")
            {
                Label2.Text = "设备编号和借出时间和借用人姓名不能为空！";
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
                        string sqlstr = string.Format("UPDATE circulate SET 设备负责人=N'{0}',是否已还=N'{1}',借出时间= '{2}',借用人姓名=N'{3}'" +
                     " WHERE 设备编号='{4}'", txtpl.Text, DropDownList1.SelectedItem.Text, txttime.Text, txtbName.Text, txtId1.Text);
                        SqlCommand cmd1 = new SqlCommand(sqlstr, cn);
                        cmd1.ExecuteNonQuery();
                        ShowData1();
                        /*执行 SQL 语句操作，无返回记录集信息，只返回受影响的记录数*/
                        int rs = cmd1.ExecuteNonQuery();
                        if (rs > 0)
                        {
                            Label2.Text = "修改成功！";
                        }
                        else
                        {
                            Label2.Text = "该记录不存在或输入格式有误，请重新修改！";
                        }
                        cn.Close();
                    }
                    else
                    {
                        Label2.Text = "请先查询该记录是否已存在";
                    }
                }
            }
        }

        //删除功能
        protected void Button9_Click(object sender, EventArgs e)
        {
            if (txtId1.Text == "" && txttime.Text == "" && txtbName.Text == "")
            {
                Label2.Text = "请输入要删除的设备编号或借出时间或借用人姓名！";
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from [circulate] where 是否已还=N'是' and 设备编号= " + txtId1.Text.Trim() + ""), cn);
                    if ((int)cmd.ExecuteScalar() > 0) //已还可删除
                    {
                        SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from [circulate] where 设备编号='{0}' and  借出时间= '{1}' and 借用人姓名=N'{2}'", txtId1.Text, txttime.Text, txtbName.Text), cn);
                        if ((int)cmd1.ExecuteScalar() > 0)//该记录符合删除条件，可以删除
                        {
                            string sqlstr1 = string.Format("DELETE FROM circulate where 设备编号='{0}' and  借出时间= '{1}' and 借用人姓名=N'{2}'", txtId1.Text, txttime.Text, txtbName.Text);
                            SqlCommand cmd2 = new SqlCommand(sqlstr1, cn);
                            cmd2.ExecuteNonQuery();                         
                            Label2.Text = "删除成功！";
                            ShowData1();
                            cn.Close();
                        }
                        else
                        {
                            Label2.Text = "删除失败！必须保证设备编号，借出时间，借用人一致才可以删除";
                        }
                    }
                    else
                    {
                        Label2.Text = "设备尚未归还，无法删除！";
                    }
                }
            }
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}