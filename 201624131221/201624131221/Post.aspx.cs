using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _201624131221
{
    public partial class 即席创作 : System.Web.UI.Page
    {
        String sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; ";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ShowData();
                ShowData1();
            }
        }

        //显示员工资料至 GridView1
        void ShowData()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                string sql= "select Posts.Postid,Username,Postdate,Subject,Post,Comment from Posts, Comments where Posts.Postid = Comments.Postid";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

        //显示员工资料至 GridView2
        void ShowData1()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                string sql = "select * from Posts";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                GridView2.DataSource = dr;
                GridView2.DataBind();
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {                             
            if (TextBox1.Text == ""||TextBox2.Text == ""||TextBox3.Text == "")
            {
                Response.Write("<script>alert('用户姓名和意见主题和意见内容都不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();

                    SqlCommand cmd = new SqlCommand(string.Format("select Count(*) from Posts where Username= N'{0}' ", TextBox1.Text), cn);
                    if ((int)cmd.ExecuteScalar() > 0)  //该记录已存在，无法插入
                    {
                        Response.Write("<script>alert('该意见发表已经发表了')</script>");

                    }
                    else
                    {
                        try
                        {
                            string sqlstr = string.Format("INSERT INTO Posts(Username,Postdate,Subject,Post) " + "VALUES(N'{0}','{1}',N'{2}',N'{3}')", TextBox1.Text, DateTime.Now.ToString(), TextBox2.Text, TextBox3.Text);
                            SqlCommand cmd1 = new SqlCommand(sqlstr, cn);
                            cmd1.ExecuteNonQuery();
                            Response.Write("<script>alert('发表成功！')</script>");
                            ShowData();
                            ShowData1();
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

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}