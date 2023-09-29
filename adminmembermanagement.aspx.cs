using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagaement
{
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string srtcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //go button ID
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getMemberId();
        }

        //active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
             updateMemberStatusById("active");
        }

        //pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
             updateMemberStatusById("pending");
        }

        //deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
             updateMemberStatusById("deactive");
        }

        //delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteMember();
        }


        //defined function
        void getMemberId()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='"
                    + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox2.Text = dr.GetValue(0).ToString();
                        TextBox7.Text = dr.GetValue(10).ToString();
                        TextBox8.Text = dr.GetValue(1).ToString();
                        TextBox3.Text = dr.GetValue(2).ToString();
                        TextBox4.Text = dr.GetValue(3).ToString();
                        TextBox9.Text = dr.GetValue(4).ToString();
                        TextBox10.Text = dr.GetValue(5).ToString();
                        TextBox11.Text = dr.GetValue(6).ToString();
                        TextBox5.Text = dr.GetValue(7).ToString();

                    }
                  
                }
                else
                {
                    Response.Write("<script>alert('Invalide Credentials');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            //Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
        }


        //
        void updateMemberStatusById(string status)
        {
            if (checkIfMemberExiste())
            {
                try
                {
                    SqlConnection con = new SqlConnection(srtcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status='" + status + "' WHERE member_id= '"
                        + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member Status Updated');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
                

            }
            else
            {
                Response.Write("<script>alert('Invalide Member ID');</script>");
            }


        }

        

        //delete member

        void deleteMember()
        {
            if (checkIfMemberExiste())
            {
                try
                {
                    SqlConnection con = new SqlConnection(srtcon);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("DELETE FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Member Deleted Successfuly.');</script>");
                    clearForm();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {

                    Response.Write("<script>alert('" + ex.Message + "');</script>");

                }
                
            }
            else
            {

                Response.Write("<script>alert('Invalide Member ID ');</script>");
            }
        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox5.Text = "";
        }

        bool checkIfMemberExiste()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='"
                    + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }

        }


    }
}