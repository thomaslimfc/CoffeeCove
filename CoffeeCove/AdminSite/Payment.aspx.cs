using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.AdminSite
{
    public partial class Payment : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                BindPayment();
            }
        }

        private void BindPayment()
        {
            string sql = @"
                SELECT pd.PaymentID, pd.PaymentMethod, pd.PaymentStatus, pd.OrderID, op.OrderDateTime , op.TotalAmount
                FROM PaymentDetail pd
                INNER JOIN OrderPlaced op ON pd.OrderID = op.OrderID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvPayment.DataSource = dt;
                        gvPayment.DataBind();
                    }
                }
            }
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPayment();
        }

        protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvPayment.PageIndex = e.NewPageIndex;

            BindPayment();
        }
    }
}