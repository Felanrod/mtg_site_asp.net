using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MTGPlayers
{
    public partial class Players : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getPlayers();
            }
        }

        protected void getPlayers()
        {
            //Instantiating an instance of our cPlayer class
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            //Get the data reader and attach to grid
            gvPlayers.DataSource = objPlayer.getPlayersGV();
            gvPlayers.DataBind();

        }


        protected void gvPlayers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[10].Attributes.Add("onClick", "return confirm('Are you sure?');");
        }

        protected void gvPlayers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            objPlayer.PlayerID = Convert.ToInt32(gvPlayers.DataKeys[e.RowIndex].Values["PlayerID"]);

            objPlayer.deletePlayer();

            //repopulate the grid with our existing function
            getPlayers();
        }

    }
}