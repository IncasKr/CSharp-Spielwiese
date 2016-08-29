using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNoSecure
{
    enum sqlType
    {
        table,
        record,
        value
    }
    
    struct Table
    {
        public struct Column
        {
            public string Name;
            public int Width;

            public Column(string name)
            {
                Name = name;
                Width = name.Length;
            }
        }

        public int ColumnCount;
        public int RowCount;
        public Column[] Heads;
        public List<string[]> Rows;

        public Table(int columnNumber)
        {
            ColumnCount = columnNumber;
            RowCount = 0;
            Heads = new Column[columnNumber];
            Rows = new List<string[]>();
        }   
    }
    class Data
    {
        private SqlConnection conIC;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        public string DataSource { get; set; }
        private DataSet ds;
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public Table Result;
        private List<string> allToList;

        public Data(string dataSource = "Douabalet-NB", string initialCatalog = "NDSoft", bool integratedSecurity = true)
        {
            conIC = new SqlConnection();
            cmd = new SqlCommand();
            da = new SqlDataAdapter();
            ds = new DataSet();
            conIC.StateChange += new StateChangeEventHandler(OnStateChange);

            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            IntegratedSecurity = integratedSecurity;
        }        

        public void Connect()
        {
            if (conIC.State == ConnectionState.Closed)
            {
                conIC.ConnectionString = "Data Source=Douabalet-NB; Initial Catalog=NDSoft; integrated security=true";
                conIC.Open();
            }
        }

        private void OnStateChange(object sender, StateChangeEventArgs e)
        {
            switch(conIC.State)
            {
                case ConnectionState.Closed:
                    Console.WriteLine("\u0054he databank is closed."); // \u0054 representiert das Unicode zeichen T 
                    break;
                case ConnectionState.Open:
                    Console.WriteLine($"The databank ({InitialCatalog}) is open.");
                    break;
            }
        }        

        public void Disconnect()
        {
            if(conIC.State == ConnectionState.Open)
            {
                conIC.Close();
            }
        }


        public void GetData(string topTableName, string request)
        {
            Connect();
            cmd.Connection = conIC;
            cmd.CommandText = request;
            SqlDataReader sdr = cmd.ExecuteReader();
            Result = new Table(sdr.FieldCount);
            for (int i = 0; i < Result.ColumnCount; i++)
            {
                Result.Heads[i].Name = sdr.GetName(i).Trim();
                Result.Heads[i].Width = sdr.GetName(i).Trim().Length;
            }
            while (sdr.Read())
            {
                string[] tmp = new string[Result.ColumnCount];
                for (int i = 0; i < Result.ColumnCount; i++)
                {
                    tmp[i] = sdr.GetValue(i).ToString().Trim();
                    if(tmp[i].Length > Result.Heads[i].Width)
                        Result.Heads[i].Width = tmp[i].Length; 
                                       
                }
                Result.Rows.Add(tmp);
                Result.RowCount++;
            }
            sdr.Close();
            Order(topTableName);
            Disconnect();           
        }

        private void Order(string topTableName)
        {
            string space = " ";

            for (int i = 0; i < Result.ColumnCount; i++)           
            {
                Result.Heads[i].Name += new string(space[0], Result.Heads[i].Width - Result.Heads[i].Name.Length);
            }

            Console.WriteLine($"The Result on table '{topTableName}' has {Result.ColumnCount} Column(s) and {Result.RowCount} Row(s)");
            for (int i = 0; i < Result.ColumnCount; i++)
            {
                for (int j = 0; j < Result.RowCount; j++)
                {
                    Result.Rows[j][i] += new string(space[0], Result.Heads[i].Width - Result.Rows[j][i].Length);
                }
            }
        }

        public SqlDataReader Display(string request)
        {

            Connect();
            cmd.Connection = conIC;
            cmd.CommandText = request;
            return cmd.ExecuteReader();
        }

        private string Request()
        {
            return "SELECT StadtID, Name, Einwohner FROM Stadt WHERE Einwohner > 0";
        }

        public List<string> toList()
        {           
            return allToList;
        }
    }
}
