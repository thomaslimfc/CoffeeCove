using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

namespace CoffeeCove.AdminSite
{
    public partial class StoreList : System.Web.UI.Page
    {
        string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PositionGlyph(gvStoreList, SortExpression, SortDirection);
            }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "StoreID"; }
            set { ViewState["SortExpression"] = value; }
        }

        protected void gvStoreList_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortExpression = e.SortExpression;

            SqlDataSource1.SelectCommand = $"SELECT [StoreID], [StoreName], [StoreAddress] FROM [Store] ORDER BY {SortExpression} {SortDirection}";

            gvStoreList.DataBind();

            PositionGlyph(gvStoreList, SortExpression, SortDirection);
        }


        private void PositionGlyph(GridView gridView, string currentSortColumn, string currentSortDirection)
        {
            if (gridView.HeaderRow == null)
                return;

            // Remove existing glyphs
            foreach (TableCell cell in gridView.HeaderRow.Cells)
            {
                foreach (Control ctrl in cell.Controls)
                {
                    if (ctrl is Image img && img.ID == "sortGlyph")
                        cell.Controls.Remove(ctrl);
                }
            }

            // Create new glyphs for each sortable column
            foreach (TableCell cell in gridView.HeaderRow.Cells)
            {
                if (cell.Controls.OfType<LinkButton>().Any())
                {
                    LinkButton linkButton = cell.Controls.OfType<LinkButton>().First();
                    Image glyph = new Image
                    {
                        ID = "sortGlyph",
                        Width = Unit.Pixel(10),
                        Height = Unit.Pixel(10)
                    };

                    if (string.Compare(currentSortColumn, linkButton.CommandArgument, true) == 0)
                    {
                        glyph.ImageUrl = currentSortDirection == "ASC" ? "~/img/up.png" : "~/img/down.png";
                        glyph.AlternateText = currentSortDirection == "ASC" ? "Ascending" : "Descending";
                    }
                    else
                    {
                        glyph.ImageUrl = "~/img/up.png";
                        glyph.AlternateText = "Ascending";
                    }

                    cell.Controls.Add(glyph);
                }
            }
        }

        protected void gvStoreList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string StoreId = (string)e.CommandArgument;
                LoadStoreForEdit(StoreId);
            }
            else if (e.CommandName == "Delete")
            {
                int StoreId = (int)e.CommandArgument;
                //DeleteStore(StoreId);
            }
        }

        private void LoadStoreForEdit(string StoreId)
        {
            string sql = "SELECT * FROM Store WHERE StoreID = @StoreId";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StoreId", StoreId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtStoreName.Text = dr["StoreName"].ToString();
                        txtStoreAddress.Text = dr["StoreAddress"].ToString();
                        txtPostCode.Text = dr["StorePostCode"].ToString();
                        hdnId.Value = StoreId;
                    }
                }
            }
            btnAdd.Text = "Update";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stores.aspx");
        }
    }

}