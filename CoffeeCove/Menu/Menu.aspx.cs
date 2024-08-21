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
            if (!IsPostBack)
            {
                BindCategory();

                string categoryId = Request.QueryString["CategoryId"];
                if (!string.IsNullOrEmpty(categoryId))
                {
                    BindProducts(categoryId); 
                }
                else
                {
                    BindProducts("1"); 
                }
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
                            lblSize.Visible = false;
                            lblFlavour.Visible = false;
                            lblIceLevel.Visible = false;
                            lblAddOn.Visible = false; 
                            ddlSize.Visible = false;
                            ddlFlavour.Visible = false;
                            ddlIceLevel.Visible = false;
                            ddlAddOn.Visible = false;
                            txtSpecialInstructions.Visible = true;
                            
                        }
                        else if (categoryId == "4")
                        {
                            lblSize.Visible = true;
                            lblFlavour.Visible = true;
                            lblIceLevel.Visible = true;
                            lblAddOn.Visible = false;
                            ddlSize.Visible = true;
                            ddlFlavour.Visible = true;
                            ddlIceLevel.Visible = true;
                            ddlAddOn.Visible = false;
                            txtSpecialInstructions.Visible = true;
                        }
                        else
                        {
                            lblSize.Visible = true;
                            lblFlavour.Visible = true;
                            lblIceLevel.Visible = true;
                            lblAddOn.Visible = true;
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

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity < 10)
            {
                txtQuantity.Text = (quantity + 1).ToString();
                UpdatePrice(sender, e); 
            }
            
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity > 1)
            {
                txtQuantity.Text = (quantity - 1).ToString();
                UpdatePrice(sender, e); 
            }
        }

        private int GetQuantity()
        {
            int quantity;
            int.TryParse(txtQuantity.Text, out quantity);
            return quantity > 0 ? quantity : 1; // Quantity at least 1
        }

        protected void UpdatePrice(object sender, EventArgs e)
        {
            decimal basePrice = Convert.ToDecimal(ViewState["BasePrice"]);
            decimal finalPrice = basePrice;

            if (ddlSize.SelectedValue == "Large") finalPrice += 1.50m;
            if (ddlFlavour.SelectedValue == "Cold") finalPrice += 1.50m;
            if (ddlAddOn.SelectedValue == "1EspressoShot") finalPrice += 2.50m;
            if (ddlAddOn.SelectedValue == "2EspressoShots") finalPrice += 5.00m;

            int quantity = GetQuantity();
            lblPrice.Text = $"Price: RM {finalPrice * quantity:N2}";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
           // Clear dropdown selections
            ddlSize.SelectedIndex = -1;
            ddlFlavour.SelectedIndex = -1;
            ddlIceLevel.SelectedIndex = -1;
            ddlAddOn.SelectedIndex = -1;
            txtQuantity.Text = "1";

            // Reset the price to the original price
            decimal basePrice = Convert.ToDecimal(ViewState["BasePrice"]);
            lblPrice.Text = $"Price: RM {basePrice:N2}";
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlOrderForm.Visible = false;
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {

        }
    }
}