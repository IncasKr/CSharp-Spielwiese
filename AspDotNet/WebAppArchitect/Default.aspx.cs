using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebAppArchitect
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ChaineDeConnexion"].ConnectionString;
                cn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Select * from [Persons]";
                    cmd.Connection = cn;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                if (rdr[i] != DBNull.Value)
                                    Response.Write(rdr[i].ToString());
                                else
                                    Response.Write("NULL");
                                if (i < rdr.FieldCount)
                                    Response.Write("|");
                            }
                            Response.Write("<br />");
                        }
                    }
                }
            }
        }
    }
}