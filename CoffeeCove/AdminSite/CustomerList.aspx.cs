using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class CustomerList : System.Web.UI.Page
    {
        string cs = Global.CS; // Assumes Global.CS is correctly configured
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomerList();
            }
        }

        private void BindCustomerList()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT cusID, Username, FirstName, LastName, EmailAddress, ContactNo, Gender, DateOfBirth, ResidenceState FROM [dbo].[Customer]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                rptCustomerList.DataSource = dt;
                rptCustomerList.DataBind();
            }
        }
    }
}
