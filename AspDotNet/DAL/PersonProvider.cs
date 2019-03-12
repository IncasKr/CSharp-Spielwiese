using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class PersonProvider
    {
        public List<PersonEntity> LoadData()
        {
            List<PersonEntity> list = new List<PersonEntity>();

            // Test
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new PersonEntity() { ID = i.ToString(), FirstName = $"Firstname{i}", LastName = $"Lastname{i}" });
            }
            return list;

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