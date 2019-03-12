using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using SQLServer = System.Data.SqlClient;
/*
 * Spacenames supported with using class "DbProviderFactory":
 * System.Data.EntityClient.EntityProviderFactory ;
 * System.Data.Odbc.OdbcFactory ;
 * System.Data.OleDb.OleDbFactory ;
 * System.Data.OracleClient.OracleClientFactory ;
 * System.Data.SqlClient.SqlClientFactory;
*/

namespace DAL
{
    public class PersonProvider
    {
        public List<PersonEntity> LoadData()
        {
            List<PersonEntity> list = new List<PersonEntity>();
            // Creation de la fabrique
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ProviderName);
            
            // Test
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new PersonEntity() { ID = i.ToString(), FirstName = $"Firstname{i}", LastName = $"Lastname{i}" });
            }
            return list;

            // Objet connection
            using (IDbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
                cn.Open();

                using (IDbCommand cmd = factory.CreateCommand())
                {
                    cmd.CommandText = "Select * from [Persons]";
                    cmd.Connection = cn;

                    using (IDataReader rdr = cmd.ExecuteReader())
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