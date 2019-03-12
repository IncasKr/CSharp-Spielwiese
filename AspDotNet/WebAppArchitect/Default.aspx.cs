using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAppArchitect
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = LoadData();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(dt.Rows[i][j].ToString());
                }
                Response.Write("<br />");
            }
        }

        private DataTable LoadData()
        {
            DataTable dt = new DataTable();

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
                        if (rdr.Read())
                        {
                            dt.Load(rdr);
                        }
                    }
                }
            }

            return dt;
        }
    }
}