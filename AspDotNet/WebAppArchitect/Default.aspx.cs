using BLL;
using DTO;
using System;
using System.Collections.Generic;
using WcfServiceLibrary;

namespace WebAppArchitect
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<PersonEntity> List = PersonManager.LoadData();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
            IThreeTierServiceLibrary client = new ThreeTierServiceLibrary();
            PersonEntity pers = client.GetPersonByName("Lastname4");
            Result1.Text = $"ID = {pers.ID} | Firstname = {pers.FirstName} | Lastname = {pers.LastName}";
        }
    }
}