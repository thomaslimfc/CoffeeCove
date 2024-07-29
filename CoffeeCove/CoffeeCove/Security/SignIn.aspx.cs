using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Security
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeForm();
            }
        }

        private void InitializeForm()
        {
            username2.Text = string.Empty;
            password2.Text = string.Empty;
        }

        protected void Password2_TextChanged(object sender, EventArgs e)
        {
            // Add your logic here to handle the text changed event for the password TextBox
        }

        protected void Username_TextChanged2(object sender, EventArgs e)
        {
            // Add your logic here to handle the text changed event for the username TextBox
        }
    }
}