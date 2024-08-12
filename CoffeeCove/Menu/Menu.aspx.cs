using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Menu
{
    public partial class Menu : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategory();
                BindProducts("1"); // Default category to show all products
            }
        }
        private void BindCategory()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName FROM Category";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptCategory.DataSource = dr;
                    rptCategory.DataBind();
                }
            }
        }

        private void BindProducts(string categoryId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = categoryId == "1"
                    ? "SELECT * FROM Product"
                    : "SELECT * FROM Product WHERE CategoryId = @CategoryId";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (categoryId != "1")
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    }
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptProduct.DataSource = dr;
                    rptProduct.DataBind();
                }
            }
        }

        protected void rptCategory_itemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string categoryId = e.CommandArgument.ToString();
                BindProducts(categoryId);
            }

        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectProduct")
            {
                string productId = e.CommandArgument.ToString();
                ShowOrderForm(productId);
            }
        }

        private void ShowOrderForm(string productId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT * FROM Product WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        // Set the product details
                        imgProduct.ImageUrl = dr["ImageUrl"].ToString();
                        lblProductID.Text = dr["ProductId"].ToString();
                        lblProductName.Text = dr["ProductName"].ToString();
                        lblProductDescription.Text = dr["Description"].ToString();
                        lblPrice.Text = $"Price: RM {dr["UnitPrice"]:0.00}";
                        ViewState["BasePrice"] = dr["UnitPrice"];

                        // Get the category for the product
                        string categoryId = GetProductCategoryId(productId);

                        // Show or hide form elements based on category
                        if (categoryId == "5" || categoryId == "6" || categoryId == "7")
                        {
                            // For categories 5, 6, 7: only show special instructions
                            lbSize.Visible = false;
                            lbFlavour.Visible = false;
                            lbIceLevel.Visible = false;
                            lbAddOn.Visible = false; 
                            ddlSize.Visible = false;
                            ddlFlavour.Visible = false;
                            ddlIceLevel.Visible = false;
                            ddlAddOn.Visible = false;
                            txtSpecialInstructions.Visible = true;
                        }
                        else
                        {
                            // For other categories (2, 3, 4): show all form elements
                            ddlSize.Visible = true;
                            ddlFlavour.Visible = true;
                            ddlIceLevel.Visible = true;
                            ddlAddOn.Visible = true;
                            txtSpecialInstructions.Visible = true;
                        }

                        // Show the panel
                        pnlOrderForm.Visible = true;
                    }
                }
            }
        }



        private string GetProductCategoryId(string productId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId FROM Product WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

        protected void UpdatePrice(object sender, EventArgs e)
        {
            decimal basePrice = Convert.ToDecimal(ViewState["BasePrice"]);
            decimal finalPrice = basePrice;

            if (ddlSize.SelectedValue == "Large") finalPrice += 1.50m;
            if (ddlFlavour.SelectedValue == "Cold") finalPrice += 1.50m;
            if (ddlAddOn.SelectedValue == "1EspressoShot") finalPrice += 2.50m;
            if (ddlAddOn.SelectedValue == "2EspressoShots") finalPrice += 5.00m;

            lblPrice.Text = $"Price: RM {finalPrice:N2}";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
           // Clear dropdown selections
            ddlSize.SelectedIndex = -1;
            ddlFlavour.SelectedIndex = -1;
            ddlIceLevel.SelectedIndex = -1;
            ddlAddOn.SelectedIndex = -1;

            // Reset the price to the original base price
            decimal basePrice = Convert.ToDecimal(ViewState["BasePrice"]);
            lblPrice.Text = $"Price: RM {basePrice:N2}";
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlOrderForm.Visible = false;
        }

    }
}