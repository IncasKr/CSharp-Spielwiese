using BLL;
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
            DataTable dt = PersonManager.LoadData();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(dt.Rows[i][j].ToString());
                }
                Response.Write("<br />");
            }
        }
    }
}