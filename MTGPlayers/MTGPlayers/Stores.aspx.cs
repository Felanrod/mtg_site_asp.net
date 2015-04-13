using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MTGPlayers
{
    public partial class Stores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getStores();
            }
        }

        protected void getStores()
        {
            //Instantiating an instance of our cPlayer class
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            //Get the data reader and attach to grid
            gvStores.DataSource = objPlayer.getStoresGV();
            gvStores.DataBind();

        }

        protected void gvStores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessRules.CPlayer objPlayer = new BusinessRules.CPlayer();

            objPlayer.StoreID = Convert.ToInt32(gvStores.DataKeys[e.RowIndex].Values["StoreID"]);

            objPlayer.deleteStore();

            //repopulate the grid with our existing function
            getStores();
        }

        protected void gvStores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Attributes.Add("onClick", "return confirm('Are you sure?');");
        }
    }
}