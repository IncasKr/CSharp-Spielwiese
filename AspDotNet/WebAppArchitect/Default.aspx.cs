using System;
using System.Collections.Generic;
using WAA.BLL;
using WAA.DTO;
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
            PersonEntity pers = client.GetPersonByName("Alexander");
            Result1.Text = $"ID = {pers?.ID} | Firstname = {pers?.FirstName} | Lastname = {pers?.LastName}";
        }
    }
}