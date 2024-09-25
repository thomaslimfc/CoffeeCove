using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Master
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = (string)Session["Username"];
            Session["statusTFA"] = "done";
            string statusTFA = (string)Session["statusTFA"];
            if (username != null && statusTFA == "done")
            {
                lblUserName.Text = username;
            }
            else
            {
                lblUserName.Text = "Register Now";
            }

            if (IsPostBack)
            {
                pnlDropdownMenu.Visible = ViewState["DropdownVisible"] != null && (bool)ViewState["DropdownVisible"];
            }
        }

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the dropdown menu panel
            pnlDropdownMenu.Visible = !pnlDropdownMenu.Visible;
            ViewState["DropdownVisible"] = pnlDropdownMenu.Visible;
        }

    }
}