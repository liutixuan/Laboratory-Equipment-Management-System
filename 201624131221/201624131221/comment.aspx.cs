using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _201624131221
{
    public partial class 意见回复 : System.Web.UI.Page
    {
        String sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; ";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text == "")
            {
                Response.Write("<script>alert('回复内容不能为空！')</script>");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = sqlconn;
                    cn.Open();                  
                        try
                        {
                            string a= "1";
                            string sqlstr = string.Format("INSERT INTO Comments(Postid,Commentdate,Comment)" + "VALUES('{0}','{1}',N'{2}',)" ,a , DateTime.Now.ToString(),TextBox1.Text );
                            SqlCommand cmd1 = new SqlCommand(sqlstr, cn);
                            cmd1.ExecuteNonQuery();
                            Response.Write("<script>alert('插入成功！')</script>");
                           
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('插入失败！')</script>");
                            //Response.Write(ex.Message);
                        }
                    
                    cn.Close();
                }
            }
        }
    }
}