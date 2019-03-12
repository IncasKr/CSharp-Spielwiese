using BLL;
using DTO;
using System;
using System.Collections.Generic;

namespace WebAppArchitect
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<PersonEntity> list = PersonManager.LoadData();

            foreach (var p in list)
            {
                Response.Write(string.Format("{0}, {1}, {2}<br />", p.ID, p.FirstName, p.LastName));
            }

        }
    }
}