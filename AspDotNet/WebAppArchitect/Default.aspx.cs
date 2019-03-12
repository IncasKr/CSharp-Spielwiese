using BLL;
using DTO;
using System;
using System.Collections.Generic;

namespace WebAppArchitect
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<PersonEntity> List = PersonManager.LoadData();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
    }
}