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
    public partial class adminpublishermanagement : System.Web.UI.Page
    {
        string srtcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExiste())
            {
                Response.Write("<script>alert('Publisher ID Existe');</script>");
            }
            else
            {
                addNewPublisher();
            }

        }
        //Update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExiste())
            {
                updatePublisher();

            }
            else
            {
                Response.Write("<script>alert('Publisher Does Not Existe');</script>");
            }

        }
        //delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExiste())
            {
                deletePublisher();

            }
            else
            {
                Response.Write("<script>alert('Publisher Does Not Existe');</script>");
            }

        }


        //functions ************************

        //get publisher
        void getPublisherById()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl WHERE publisher_id='"
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
                    Response.Write("<script>alert('Invalide Publisher ID');</script>");
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }


        //delete Publisher
        void deletePublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM publisher_master_tbl WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Publisher Deleted Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }

        }


        //update publisher
        void updatePublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name=@publisher_name WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Publisher Updated Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }

        }



        // add new publisher
        void addNewPublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl" +
                    "(publisher_id,publisher_name) " + "VALUES (@publisher_id,@publisher_name)", con);
                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Publisher Added Successful.');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }



        //user defined function
        bool checkIfPublisherExiste()
        {
            try
            {
                SqlConnection con = new SqlConnection(srtcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl WHERE publisher_id='"
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
        //go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getPublisherById();
        }
    }
}