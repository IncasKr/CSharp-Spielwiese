using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public static class PersonManager
    {
        public static DataTable LoadData()
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
