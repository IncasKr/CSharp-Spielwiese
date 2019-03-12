using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BLL
{
    public static class PersonManager
    {
        public static List<PersonEntity> LoadData()
        {
            List<PersonEntity> list = new List<PersonEntity>();

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
                            PersonEntity p = new PersonEntity();
                            p.ID = rdr["PersonId"] == DBNull.Value ? string.Empty : rdr["PersonId"].ToString();
                            p.LastName = rdr["LastName"] == DBNull.Value ? string.Empty : rdr["LastName"].ToString();
                            p.FirstName = rdr["FirstName"] == DBNull.Value ? string.Empty : rdr["FirstName"].ToString();

                            list.Add(p);
                        }
                    }
                }
            }

            return list;
        }
    }
}
