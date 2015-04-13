using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MTGPlayers
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getPlayer();
                getProvinces();
                getCities();
                getStores();
                getPlayed();
                
            }
        }

        protected void getProvinces()
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            //objPlayer.ProvinceID = Convert.ToString(Request.QueryString["ProvinceID"]);

            ddlProvince.DataSource = objPlayer.getProvinces();
            ddlProvince.DataBind();

        }

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

        protected void getPlayer()
        {

            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            objPlayer.PlayerID = Convert.ToInt32(Request.QueryString["PlayerID"]);
            objPlayer.getPlayer();

            txtPlayerName.Text = objPlayer.PlayerName;
            ddlProvince.SelectedValue = objPlayer.ProvinceID;
            ddlCity.SelectedValue = Convert.ToString(objPlayer.CityID);
            ddlStore.SelectedValue = Convert.ToString(objPlayer.StoreID);
            ddlPlayed.SelectedValue = Convert.ToString(objPlayer.PlayedID);
            txtEmail.Text = objPlayer.Email;
            if (objPlayer.ShowEmail == 1)
            {
                cbEmail.Checked = true;
            }
            else
            {
                cbEmail.Checked = false;
            }
            txtAbout.Text = objPlayer.About;
            if (objPlayer.Role == "User")
            {
                rblRole.SelectedIndex = 0;
            }
            else
            {
                rblRole.SelectedIndex = 1;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            /* //create an instance of player class
             BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

             //populate the supplier properties with values from the ID
             objPlayer.PlayerID = Convert.ToInt32(Request.QueryString["PlayerID"]);
             objPlayer.PlayerName = txtPlayerName.Text;
             objPlayer.ProvinceID = ddlProvince.SelectedValue;
             objPlayer.CityID = Convert.ToInt32(ddlCity.SelectedValue);
             objPlayer.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
             objPlayer.PlayedID = Convert.ToInt32(ddlPlayed.SelectedValue);
             objPlayer.Email = txtEmail.Text;
             if (cbEmail.
             //invoke the save method in the class library
             objPlayer.savePlayer();*/

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
            objPlayer.savePlayer(txtPlayerName.Text, ddlProvince.SelectedValue, Convert.ToInt32(ddlCity.SelectedValue),
                 Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt32(ddlPlayed.SelectedValue), txtEmail.Text, showEmail,
                  txtAbout.Text, rblRole.SelectedValue);

            //take the user back to the updated list
            Response.Redirect("Players.aspx", true);
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCities();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStores();
        }
    }
}