using CoffeeCove.Master;
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

        //protected void DeleteUserBtn_CL_Click(object sender, EventArgs e)
        //{
        //    // Logic to delete a user from the database
        //    string username = Username_SI.Text;

        //    // Add your deletion logic here. This could involve calling a method from a data access layer.
        //    bool isDeleted = DeleteUserByUsername(username);

        //    // Provide feedback to the user based on whether the deletion was successful
        //    if (isDeleted)
        //    {
        //        Response.Write("<script>alert('User deleted successfully.');</script>");
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('User deletion failed. Please try again.');</script>");
        //    }
        //}

        //private bool DeleteUserByUsername(string username)
        //{
        //    // Implement the logic to delete a user by their username from your database.
        //    // This is a placeholder and should be replaced with actual code.
        //    // Return true if the deletion is successful, otherwise false.
        //    return true; // Replace with actual implementation
        //}

        //private Admin GetAdminByUsername(string username)
        //{
        //    // Your logic to fetch admin data from the database
        //}

        //private bool UpdateAdminBranch(string username, string branch)
        //{
        //    // Your logic to update the admin's branch in the database
        //}

    }
}
