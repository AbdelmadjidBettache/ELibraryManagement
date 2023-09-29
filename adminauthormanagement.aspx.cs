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
    public partial class adminauthormanagment : System.Web.UI.Page
    {
        string srtcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //Add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExiste())
            {
                Response.Write("<script>alert('Author ID Existe');</script>");
            }
            else
            {
                addNewAuthor();
            }
        }
    
        //Update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExiste())
            {
                updateAuthor();
                
            }
            else
            {
                Response.Write("<script>alert('Author Does Not Existe');</script>");
            }

        }

        //Delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExiste())
            {
                deleteAuthor();

            }
            else
            {
                Response.Write("<script>alert('Author Does Not Existe');</script>");
            }

        }

        //Go button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorById();
        }


        //get author
        void getAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='"
                    + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalide Author ID');</script>");
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                
            }
        }


        //delete author
        void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Deleted Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }

        }


        //update author
        void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name WHERE author_id='"+TextBox1.Text.Trim()+"'", con);
               
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Updated Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }

        }



        // add new author
        void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl" +
                    "(author_id,author_name) " + "VALUES (@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Added Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }



        //user defined function
        bool checkIfAuthorExiste()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='" 
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

                con.Close();
                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }

        }

        //clear functio
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}