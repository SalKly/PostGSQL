using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostGSQL
{
    public partial class loggedSupervisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void listPublications(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGSQL"].ToString();

            SqlConnection conn = new SqlConnection(connStr);
            String SID = studentID.Text;
            SqlCommand ViewAStudentPublications = new SqlCommand("ViewAStudentPublications", conn);
            ViewAStudentPublications.CommandType = CommandType.StoredProcedure;
            ViewAStudentPublications.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int)).Value = SID;

            conn.Open();
            SqlDataReader rdr = ViewAStudentPublications.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable table = new DataTable();
            table.Columns.Add("title");
            table.Columns.Add("date");
            table.Columns.Add("place");
            table.Columns.Add("accepted");
            table.Columns.Add("host");
            Boolean flag = false;

            while (rdr.Read())
            {
                flag = true;

                DataRow dataRow = table.NewRow();
                String Ptitle = rdr.GetString(rdr.GetOrdinal("title"));
                DateTime Pdate = rdr.GetDateTime(rdr.GetOrdinal("date"));
                String Pplace = rdr.GetString(rdr.GetOrdinal("place"));
                Boolean Paccepted = rdr.GetBoolean(rdr.GetOrdinal("accepted"));
                String Phost = rdr.GetString(rdr.GetOrdinal("host"));


                dataRow["title"] = Ptitle;
                dataRow["date"] = Pdate;
                dataRow["place"] = Pplace;
                dataRow["accepted"] = Paccepted;
                dataRow["host"] = Phost;

                table.Rows.Add(dataRow);





            }
            if (flag == false)
               
            {
                Response.Write("<script>alert('Invalid Studnet ID');</script>");
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = table;
                GridView1.DataBind();
            }


        }

        protected void cancelThesis(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGSQL"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand CancelThesis = new SqlCommand("CancelThesis", conn);
            CancelThesis.CommandType = CommandType.StoredProcedure;
            CancelThesis.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = ThesisSN.Text;
            conn.Open();
           // try
            //{
                int Check = CancelThesis.ExecuteNonQuery();
            if (Check <= 0)
            {
                Response.Write("<script>alert('The following Thesis number is not suited for cancelation as the last progress report evaluation is higher than zero ');</script>");


            }
            else
            {
                Response.Write("<h1>Thesis is Canceled successfuly</h1>");


            }

        }
    }
}