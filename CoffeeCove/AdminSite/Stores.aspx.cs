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

    }

}