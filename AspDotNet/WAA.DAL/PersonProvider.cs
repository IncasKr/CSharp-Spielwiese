using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Timers;
using WAA.DTO;
using SQLServer = System.Data.SqlClient;
/*
 * Spacenames supported with using class "DbProviderFactory":
 * System.Data.EntityClient.EntityProviderFactory ;
 * System.Data.Odbc.OdbcFactory ;
 * System.Data.OleDb.OleDbFactory ;
 * System.Data.OracleClient.OracleClientFactory ;
 * System.Data.SqlClient.SqlClientFactory;
*/

namespace WAA.DAL
{
    public class PersonProvider
    {
        private List<PersonEntity> list;
        private Timer updateTimer;
        
        private void RefreshDataCache(object sender, ElapsedEventArgs e)
        {
            List<PersonEntity> listTmp = new List<PersonEntity>();
            updateTimer.Enabled = false;
            // Creation de la fabrique
            int index = ConfigurationManager.ConnectionStrings.Count > 1 ? 1 : 0;
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[index].ProviderName);

            // Objet connection
            using (IDbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings[index].ConnectionString;
                cn.Open();

                using (IDbCommand cmd = factory.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Student";
                    cmd.Connection = cn;

                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            PersonEntity p = new PersonEntity
                            {
                                ID = rdr["ID"] == DBNull.Value ? string.Empty : rdr["ID"].ToString(),
                                LastName = rdr["LastName"] == DBNull.Value ? string.Empty : rdr["LastName"].ToString(),
                                FirstName = rdr["FirstName"] == DBNull.Value ? string.Empty : rdr["FirstName"].ToString()
                            };

                            listTmp.Add(p);
                        }                    
                    }
                }
            }
            list = listTmp;
            updateTimer.Enabled = true;
        }

        public PersonProvider()
        {
             list = new List<PersonEntity>();
            updateTimer = new Timer()
            {
                AutoReset = true,
                Interval = 60000
            };
            updateTimer.Elapsed += new ElapsedEventHandler(RefreshDataCache);
            updateTimer.Start();
        }

        public List<PersonEntity> LoadData()
        {
            while (!updateTimer.Enabled || list.Count == 0) ;
            return list;
        }
    }
}