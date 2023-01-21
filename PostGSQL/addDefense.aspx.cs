﻿using System;
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
    public partial class addDefense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGSQL"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            if (RadioButtonList1.SelectedValue == "1")
            {
                SqlCommand AddDefenseGucian = new SqlCommand("AddDefenseGucian", conn);
                AddDefenseGucian.CommandType = CommandType.StoredProcedure;
                AddDefenseGucian.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = ThesisSSN.Text;
                AddDefenseGucian.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.DateTime)).Value = dateDefense.Text;
                AddDefenseGucian.Parameters.Add(new SqlParameter("@DefenseLocation", SqlDbType.VarChar)).Value = host.Text;
                conn.Open();
                try
                {
                    AddDefenseGucian.ExecuteNonQuery();
                    Response.Write("Gucian defense added");

                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.StartsWith("The INSERT statement conflicted with the FOREIGN KEY"))
                    {
                        Response.Write("<script>alert('Invalid Thesis ID For Guican Students');</script>");

                    }
                    else
                    {
                        if (sqlEx.Message.StartsWith("Violation of PRIMARY"))
                        {
                            Response.Write("<script>alert('Defense with the samte date was already added to this thesis');</script>");

                        }
                        else
                        {
                            if (sqlEx.Message.StartsWith("Failed to convert parameter value from a String to a DateTime"))
                            {
                                Response.Write("<script>alert('Date format should be mm/dd/yyyy ');</script>");

                            }

                        }

                    }

                }
                catch (System.FormatException sFe)
                {
                    if (sFe.Message.StartsWith("Failed to convert parameter value from a String to a DateTime"))
                    {
                        Response.Write("<script>alert('Date must be written in mm/dd/yyyy format');</script>");

                    }
                }
                conn.Close();






            }
            else
            {
                SqlCommand AddDefenseNonGucian = new SqlCommand("AddDefenseNonGucian", conn);
                AddDefenseNonGucian.CommandType = CommandType.StoredProcedure;
                AddDefenseNonGucian.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = ThesisSSN.Text;
                AddDefenseNonGucian.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.DateTime)).Value = dateDefense.Text;
                AddDefenseNonGucian.Parameters.Add(new SqlParameter("@DefenseLocation", SqlDbType.VarChar)).Value = host.Text;
                conn.Open();
                try
                {
                    AddDefenseNonGucian.ExecuteNonQuery();
                    Response.Write("NonGucian defense added");

                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.StartsWith("The INSERT statement conflicted with the FOREIGN KEY"))
                    {
                        Response.Write("<script>alert('Invalid Thesis ID for NonGucianStudents');</script>");

                    }
                    else
                    {
                        if (sqlEx.Message.StartsWith("Violation of PRIMARY"))
                        {
                            Response.Write("<script>alert('Defense with the samte date was already added to this thesis');</script>");

                        }

                    }

                }
                catch (System.FormatException sFe)
                {
                    if (sFe.Message.StartsWith("Failed to convert parameter value from a String to a DateTime"))
                    {
                        Response.Write("<script>alert('Date must be written in mm/dd/yyyy format');</script>");

                    }
                }
                conn.Close();



            }


        }


    }
}