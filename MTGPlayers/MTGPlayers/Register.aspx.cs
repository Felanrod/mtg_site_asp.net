using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MTGPlayers
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if page was already loaded
            if (IsPostBack == false)
            {
                getProvinces();
                getCities();
                getStores();
                getPlayed();
            }
        }

        //calls upon the getTerritory method in CTerritory.cs to fill in the
        //textbox and ddl with the values of the territory selected
        protected void getProvinces()
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            //objPlayer.ProvinceID = Convert.ToString(Request.QueryString["ProvinceID"]);

            ddlProvince.DataSource = objPlayer.getProvinces();
            ddlProvince.DataBind();

            /*txtTerritoryName.Text = objTerritory.TerritoryName;
            if (objTerritory.RegionID != 0)
            {
                ddlRegion.SelectedValue = Convert.ToString(objTerritory.RegionID);
            }*/

        }

        //calls upon the getTerritory method in CTerritory.cs to fill in the
        //textbox and ddl with the values of the territory selected
        protected void getCities()
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            //objPlayer.ProvinceID = Convert.ToString(Request.QueryString["ProvinceID"]);
            string provinceID = ddlProvince.SelectedValue;
            ddlCity.DataSource = objPlayer.getCities(provinceID);
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("Different City", "0"));
            ddlCity.DataBind();

        }

        
        protected void getStores()
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            int cityID = Convert.ToInt32(ddlCity.SelectedValue);
            ddlStore.DataSource = objPlayer.getStores(cityID);
            ddlStore.Items.Clear();
            ddlStore.Items.Add(new ListItem("NA", "0"));
            ddlStore.DataBind();

        }

        protected void getPlayed()
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            ddlPlayed.DataSource = objPlayer.getPlayed();
            ddlPlayed.DataBind();

        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCities();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStores();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            int showEmail;
            if (cbEmail.Checked)
            {
                showEmail = 1;
            }
            else
            {
                showEmail = 0;
            }

            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            objPlayer.register(txtPlayerName.Text, ddlProvince.SelectedValue, Convert.ToInt32(ddlCity.SelectedValue),
                 Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt32(ddlPlayed.SelectedValue), txtEmail.Text, showEmail,
                  txtAbout.Text, txtPass.Text, rblRole.SelectedValue);

            Response.Redirect("Login.aspx", true);
        }
    }
}