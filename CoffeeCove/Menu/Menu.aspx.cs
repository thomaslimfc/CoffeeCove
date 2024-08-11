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

                        // Bind ingredient options 
                        string categoryId = GetProductCategoryId(productId);
                        BindIngredientOptions(categoryId);

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

        private void BindIngredientOptions(string categoryId)
        {
            string sizeSql = "SELECT * FROM Ingredient WHERE IngredientType = 'Size'";
            string flavourSql = "SELECT * FROM Ingredient WHERE IngredientType = 'Flavour'";
            string iceSql = "SELECT * FROM Ingredient WHERE IngredientType = 'Ice'";
            string addOnSql = "SELECT * FROM Ingredient WHERE IngredientType = 'Add on'";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();

                DataSet ds = new DataSet();

                // Size options
                da.SelectCommand = new SqlCommand(sizeSql, con);
                da.Fill(ds, "Size");

                // Flavour options
                da.SelectCommand.CommandText = flavourSql;
                da.Fill(ds, "Flavour");

                // Ice options
                da.SelectCommand.CommandText = iceSql;
                da.Fill(ds, "Ice");

                // Add-on options
                da.SelectCommand.CommandText = addOnSql;
                da.Fill(ds, "AddOn");

                // Bind to controls
                rblSize.DataSource = ds.Tables["Size"];
                rblSize.DataTextField = "IngredientName";
                rblSize.DataBind();
                rblSize.Visible = true;

                rblFlavour.DataSource = ds.Tables["Flavour"];
                rblFlavour.DataTextField = "IngredientName";
                rblFlavour.DataBind();
                rblFlavour.Visible = true;

                rblIceLevel.DataSource = ds.Tables["Ice"];
                rblIceLevel.DataTextField = "IngredientName";
                rblIceLevel.DataBind();
                rblIceLevel.Visible = true;

                rblAddOns.DataSource = ds.Tables["AddOn"];
                rblAddOns.DataTextField = "IngredientName";
                rblAddOns.DataBind();
                rblAddOns.Visible = true;
            }
        }


        private decimal CalculatePrice()
        {
            decimal basePrice = GetBasePrice();
            decimal sizePrice = GetSelectedPrice(rblSize);
            decimal flavourPrice = GetSelectedPrice(rblFlavour);
            decimal icePrice = GetSelectedPrice(rblIceLevel);
            decimal addOnPrice = GetSelectedAddOnsPrice();

            return basePrice + sizePrice + flavourPrice + icePrice + addOnPrice;
        }

        private decimal GetBasePrice()
        {
            return 10.00m;
        }

        private decimal GetSelectedPrice(RadioButtonList rbl)
        {
            if (rbl.SelectedItem != null)
            {
                return GetIngredientPrice(rbl.SelectedValue);
            }
            return 0.00m;
        }

        private decimal GetSelectedAddOnsPrice()
        {
            decimal totalPrice = 0.00m;
            foreach (ListItem item in rblAddOns.Items)
            {
                if (item.Selected)
                {
                    totalPrice += GetIngredientPrice(item.Value);
                }
            }
            return totalPrice;
        }

        private decimal GetIngredientPrice(string ingredientId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = "SELECT Price FROM Ingredient WHERE IngredientId = @IngredientId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@IngredientId", ingredientId);
                    con.Open();
                    return (decimal)cmd.ExecuteScalar();
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlOrderForm.Visible = false;
        }

    }
}
