using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeCove.Master;
using System.Security.Policy;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using CoffeeCove.Order;

namespace CoffeeCove.Menu
{
    public partial class Menu : System.Web.UI.Page
    {
        string cs = Global.CS;
        private List<int> top3Products = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategory();

                int categoryId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["CategoryId"]))
                {
                    int.TryParse(Request.QueryString["CategoryId"], out categoryId);
                }

                if (categoryId == 0)
                {
                    lblCategoryName.Text = "All Categories";
                }
                else
                {
                    SetCategoryName(categoryId);
                }
                BindProducts(categoryId);
            }
        }

        private void BindCategory()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId, CategoryName FROM Category WHERE IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    rptCategory.DataSource = dr;
                    rptCategory.DataBind();
                }
            }
        }

        private void BindProducts(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = @"SELECT p.*, COALESCE(SUM(oi.Quantity), 0) AS TotalSold 
                                FROM Product p LEFT JOIN OrderedItem oi ON p.ProductId = oi.ProductID 
                                INNER JOIN Category c ON p.CategoryId = c.CategoryId WHERE c.IsActive = 1 AND p.IsActive = 1";

                if (categoryId != 0)
                {
                    sql += " AND p.CategoryId = @CategoryId";
                }

                sql += " GROUP BY p.ProductId, p.ProductName, p.Description, p.ImageUrl, p.UnitPrice, p.CategoryId, p.IsActive, p.CreatedDate ORDER BY TotalSold DESC, p.ProductId ASC";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (categoryId != 0)
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    }
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    top3Products.Clear();
                    // store top 3 product IDs
                    if (categoryId == 0)
                    {
                        int count = 0;
                        while (dr.Read() && count < 3)
                        {
                            int totalSold = Convert.ToInt32(dr["TotalSold"]);

                            // Only add products to top3 if TotalSold > 0
                            if (totalSold > 0)
                            {
                                top3Products.Add(Convert.ToInt32(dr["ProductId"]));
                                count++;
                            }
                        }
                        dr.Close();
                        dr = cmd.ExecuteReader();
                    }
                    rptProduct.DataSource = dr;
                    rptProduct.DataBind();
                }
            }
        }

        protected void rptCategory_itemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int categoryId = Convert.ToInt32(e.CommandArgument);
                SetCategoryName(categoryId);
                BindProducts(categoryId);
            }

        }

        private void SetCategoryName(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryName FROM Category WHERE CategoryId = @CategoryId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lblCategoryName.Text = result.ToString();
                    }
                    else
                    {
                        lblCategoryName.Text = "Category not found";
                    }
                }
            }
        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                ShowOrderForm(productId);
            }
        }

        private void ShowOrderForm(int productId)
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
                        lblProductName.Text = dr["ProductName"].ToString();
                        lblProductDescription.Text = dr["Description"].ToString();
                        lblPrice.Text = $"Price: RM {dr["UnitPrice"]:0.00}";
                        ViewState["BasePrice"] = dr["UnitPrice"];

                        hfProductId.Value = dr["ProductId"].ToString();
                        hfProductName.Value = dr["ProductName"].ToString();
                        hfSize.Value = "Regular";
                        hfFlavour.Value = "Hot";
                        hfIceLevel.Value = "No Ice";
                        hfAddOn.Value = ddlAddOn.SelectedValue;
                        hfSpecialInstructions.Value = txtSpecialInstructions.Text;
                        hfQuantity.Value = txtQuantity.Text;
                        hfUpdatedPrice.Value = lblPrice.Text;

                        // Get the category for the product
                        int categoryId = GetProductCategoryId(productId);

                        // Show or hide form elements based on category
                        if (categoryId == 1 || categoryId == 2)
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
                        else if (categoryId == 3)
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
                            // only show special instructions
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

                        // Show the panel
                        pnlOrderForm.Visible = true;
                    }
                }
            }
        }

        private int GetProductCategoryId(int productId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT CategoryId FROM Product WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if result is null, and return 0 if no result is found
                    if (result != null && int.TryParse(result.ToString(), out int categoryId))
                    {
                        return categoryId;
                    }
                    else
                    {
                        return 0; // if no category found
                    }
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
            if (ddlAddOn.SelectedValue == "1 Espresso Shot") finalPrice += 2.50m;
            if (ddlAddOn.SelectedValue == "2 Espresso Shots") finalPrice += 5.00m;

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
            //if first time click without select orderOption
            if (Session["orderOpt"] == null)
            {
                Response.Redirect("../Order/OrderOption.aspx");
            }

            // Retrieve values from form controls
            string productId = hfProductId.Value;
            string productName = lblProductName.Text;
            string size = ddlSize.Visible ? ddlSize.SelectedValue : null;
            string flavour = ddlFlavour.Visible ? ddlFlavour.SelectedValue : null;
            string iceLevel = ddlIceLevel.Visible ? ddlIceLevel.SelectedValue : null;
            string addOn = ddlAddOn.Visible ? ddlAddOn.SelectedValue : null;
            string specialInstructions = txtSpecialInstructions.Visible ? txtSpecialInstructions.Text : null;
            int quantity = GetQuantity();

            // Calculate the updated price
            decimal basePrice = Convert.ToDecimal(ViewState["BasePrice"]);
            decimal finalPrice = basePrice;

            if (ddlSize.Visible && ddlSize.SelectedValue == "Large") finalPrice += 1.50m;
            if (ddlFlavour.Visible && ddlFlavour.SelectedValue == "Cold") finalPrice += 1.50m;
            if (ddlAddOn.Visible && ddlAddOn.SelectedValue == "1 Espresso Shot") finalPrice += 2.50m;
            if (ddlAddOn.Visible && ddlAddOn.SelectedValue == "2 Espresso Shots") finalPrice += 5.00m;

            //finalPrice *= quantity; // Total price based on quantity

            string orderId = GetCurrentOrderId();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Check if the order exists, insert if not
                    string checkOrderSql = "SELECT COUNT(*) FROM OrderPlaced WHERE OrderID = @OrderID";
                    using (SqlCommand checkCmd = new SqlCommand(checkOrderSql, con, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@OrderID", orderId);
                        int orderCount = (int)checkCmd.ExecuteScalar();

                        if (orderCount == 0)
                        {
                            string insertOrderSql = "INSERT INTO OrderPlaced (OrderID, OrderDateTime) VALUES (@OrderID, @OrderDateTime)";
                            using (SqlCommand insertCmd = new SqlCommand(insertOrderSql, con, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@OrderID", orderId);
                                insertCmd.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert into OrderedItem table
                    string sql = "INSERT INTO OrderedItem (ProductID, OrderID, Quantity, Size, Flavour, IceLevel, AddOn, Instruction, Price) " +
                                 "VALUES (@ProductID, @OrderID, @Quantity, @Size, @Flavour, @IceLevel, @AddOn, @Instruction, @Price)";
                    using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        if (ddlSize.Visible)
                            cmd.Parameters.AddWithValue("@Size", size);
                        else
                            cmd.Parameters.AddWithValue("@Size", DBNull.Value);

                        if (ddlFlavour.Visible)
                            cmd.Parameters.AddWithValue("@Flavour", flavour);
                        else
                            cmd.Parameters.AddWithValue("@Flavour", DBNull.Value);

                        if (ddlIceLevel.Visible)
                            cmd.Parameters.AddWithValue("@IceLevel", iceLevel);
                        else
                            cmd.Parameters.AddWithValue("@IceLevel", DBNull.Value);

                        if (ddlAddOn.Visible)
                            cmd.Parameters.AddWithValue("@AddOn", addOn);
                        else
                            cmd.Parameters.AddWithValue("@AddOn", DBNull.Value);

                        if (txtSpecialInstructions.Visible)
                            cmd.Parameters.AddWithValue("@Instruction", specialInstructions);
                        else
                            cmd.Parameters.AddWithValue("@Instruction", DBNull.Value);

                        cmd.Parameters.AddWithValue("@Price", finalPrice);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    // Rollback the transaction if error occurs
                    transaction.Rollback();
                    throw;
                }
            }
            Response.Redirect(Request.RawUrl);
            pnlOrderForm.Visible = false;
        }

        private string GetCurrentOrderId()
        {
            if (Session["OrderID"] != null)
            {
                return Session["OrderID"].ToString();
            }
            else
            {
                // if OrderID is missing
                Response.Redirect("../Order/OrderOption.aspx");
                return null;
            }
        }

        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblMostPopular = (Label)e.Item.FindControl("lblMostPopular");
                int productId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ProductId"));

                // Show label only if product ID is in the top 3
                if (top3Products.Contains(productId))
                {
                    lblMostPopular.Visible = true;
                }
                else
                {
                    lblMostPopular.Visible = false;
                }
            }
        }
    }
}


