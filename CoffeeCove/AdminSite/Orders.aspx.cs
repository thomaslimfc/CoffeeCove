﻿using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoffeeCove.Securities;
using System.Data.Entity.Core.Common.CommandTrees;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iText.Kernel.Pdf;
using static System.Data.Entity.Infrastructure.Design.Executor;
using iText.Layout.Properties;

namespace CoffeeCove.AdminSite
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        string cs = Global.CS;
        DateTime startDate;
        DateTime endDate;

        int quantity = 0;
        decimal price = 0;
        decimal subTotal = 0;
        decimal linePrice = 0;
        decimal tax = 0;
        decimal total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                conditionalBind();
            }
            
        }

        protected void conditionalBind()
        {
            if (string.IsNullOrEmpty(hfStartDate.Value) || string.IsNullOrEmpty(hfEndDate.Value)) //means it not search with range
            {
                BindGridView();
            }
            else
            {
                bindGridViewWithDate();
            }
        }

        protected void rptOrdered_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            System.Web.UI.WebControls.Label lblQuantity = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblQuantity");
            quantity = int.Parse(lblQuantity.Text);
            System.Web.UI.WebControls.Label lblPrice = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrice");
            price = decimal.Parse(lblPrice.Text);
            System.Web.UI.WebControls.Label lblLineTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblLineTotal");

            System.Web.UI.WebControls.Label lblSize = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblSize");

            Panel panelSize = (Panel)e.Item.FindControl("panelSize");
            Panel panelFlavour = (Panel)e.Item.FindControl("panelFlavour");
            Panel panelIce = (Panel)e.Item.FindControl("panelIce");
            Panel panelAddon = (Panel)e.Item.FindControl("panelAddon");

            Panel panelTable = (Panel)e.Item.FindControl("panelTable");

            if (string.IsNullOrEmpty(lblSize.Text))
            {
                panelTable.Visible = false;
                panelSize.Visible = false;
                panelFlavour.Visible = false;
                panelIce.Visible = false;
                panelAddon.Visible = false;
            }

            linePrice = quantity * price;
            subTotal += linePrice;

            tax = subTotal * (decimal)0.06;
            total = subTotal + tax;

            lblSubtotal.Text = subTotal.ToString("C"); // "C" formats the number as currency
            lblTax.Text = tax.ToString("C");
            lblTotal.Text = total.ToString("C");
            lblLineTotal.Text = linePrice.ToString("C");
        }

        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT O.OrderID, O.OrderDateTime, O.TotalAmount, P.PaymentMethod, O.OrderStatus, C.Username
                    FROM OrderPlaced O 
                    JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    JOIN Customer C ON O.CusID = C.CusID";

                if (!string.IsNullOrEmpty(SortExpression))
                {
                    sql += $" ORDER BY {SortExpression} {SortDirection}";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvOrder.DataSource = dt;
                        gvOrder.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }

                }
                UpdateSortIcons();
            }
        }

        private void BindGridView(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT O.OrderID, O.OrderDateTime, O.TotalAmount, P.PaymentMethod, O.OrderStatus, C.Username
                    FROM OrderPlaced O 
                    JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    JOIN Customer C ON O.CusID = C.CusID
                    WHERE O.OrderDateTime BETWEEN @startDate AND @endDate";

                if (!string.IsNullOrEmpty(SortExpression))
                {
                    sql += $" ORDER BY {SortExpression} {SortDirection}";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();

                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvOrder.DataSource = dt;
                        gvOrder.DataBind();
                        
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }
                UpdateSortIcons();
            }
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewOrder")
            {
                pnlOrderDetail.Visible = true;
                string orderId = e.CommandArgument.ToString();
                hfOrderID.Value = orderId;
                bindRepeater(orderId);
                displayDetail(orderId);
                
            }
            else if(e.CommandName == "deleteOrder")
            {
                string orderId = e.CommandArgument.ToString();
                deleteOrder(orderId);
            }
        }

        private void deleteOrder(string orderId)
        {
            //delete store then reset the identity() to max num
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string paymentId = "";
                //get paymentID
                string sql5 = @"SELECT PaymentID
                                FROM PaymentDetail
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql5, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                    
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            paymentId = dr["PaymentID"].ToString();

                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }

                string sql = @"DELETE FROM OrderedItem
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }

                //delete review first then only payment
                string sql4 = @"DELETE FROM Review
                                WHERE PaymentID = @paymentId";
                using (SqlCommand cmd = new SqlCommand(sql4, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@paymentId", paymentId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }

                string sql2 = @"DELETE FROM PaymentDetail
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql2, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }

                string sql3 = @"DELETE FROM OrderPlaced
                                WHERE OrderID = @orderId";
                using (SqlCommand cmd = new SqlCommand(sql3, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }

                lblMsg.Text = "Order Deleted Successfully";
                lblMsg.Visible = true;

            }

            //rebind the gridview
            conditionalBind();
        }

        private void bindRepeater(string orderId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT * 
                            FROM OrderedItem I JOIN Product P 
                            ON I.ProductId = P.ProductId
                            WHERE OrderId = @orderId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        conn.Open();

                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        //clear the repeater
                        rptOrdered.DataSource = null;
                        rptOrdered.DataBind();

                        rptOrdered.DataSource = ds;
                        rptOrdered.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }
                }
                    
            }
                
        }

        private void displayDetail(string orderId)
        {
            //get data from db
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"SELECT O.OrderDateTime, O.OrderType, O.DeliveryAddress, O.StoreID, P.PaymentMethod, C.Username, C.EmailAddress, O.OrderStatus
                    FROM OrderPlaced O 
                    JOIN PaymentDetail P ON O.OrderID = P.OrderID
                    JOIN Customer C ON O.CusID = C.CusID
                    WHERE O.OrderID = @orderId";


                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                    
                        conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            lblOrderNo.Text = orderId;
                            DateTime orderDate = Convert.ToDateTime(dr["OrderDateTime"]);
                            lblDate.Text = orderDate.ToString("dd/MM/yyyy");
                            string orderType = dr["OrderType"].ToString();


                            lblOrderStatus.Text = dr["OrderStatus"].ToString();
                            lblPaymentMethod.Text = dr["PaymentMethod"].ToString();
                            lblUsername.Text = dr["Username"].ToString();
                            lblEmail.Text = dr["EmailAddress"].ToString();

                            //if it is delivery then display delivery and vice versa
                            if (orderType == "Delivery")//if store ID = null, means that it is using delivery
                            {
                                lblDelPick.Text = orderType;
                                lblDelivery.Text = dr["DeliveryAddress"].ToString();
                                lblPickUp.Text = "-";
                            }
                            else if (orderType == "Pick Up")
                            {
                                lblDelPick.Text = orderType;
                                string storeID = dr["StoreID"].ToString();

                                dr.Close();
                                //find store name based on id
                                string sql1 = @"SELECT StoreName
                                            FROM Store
                                            WHERE StoreID = @storeId";
                                using (SqlCommand cmd1 = new SqlCommand(sql1, conn))
                                {
                                    try
                                    {
                                        cmd1.Parameters.AddWithValue("@storeId", storeID);
                                    
                                        SqlDataReader dr1 = cmd1.ExecuteReader();

                                        if (dr1.Read())
                                        {
                                            lblPickUp.Text = dr1["StoreName"].ToString();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Write("Oops! An error occurred: " + ex.Message);
                                    }

                                }

                                lblDelivery.Text = "-";
                            }
                            else
                            {
                                lblDelPick.Text = " ";
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Oops! An error occurred: " + ex.Message);
                    }

                }
            }

            pnlReceipt.Visible = true;


        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGridViewWithDate();
        }

        protected void bindGridViewWithDate()
        {
            try
            {
                startDate = DateTime.Parse(txtFrom.Text);
                endDate = DateTime.Parse(txtTo.Text).AddDays(1);

                if (endDate <= startDate)
                {
                    lblErrorMsg.Text = "End Date must be after the Start Date.";
                    lblErrorMsg.Visible = true;
                    return;
                }

                hfStartDate.Value = startDate.ToString("dd/MM/yyyy");
                hfEndDate.Value = endDate.ToString("dd/MM/yyyy");

                BindGridView(startDate, endDate);
            }
            catch (FormatException ex)
            {
                lblErrorMsg.Text = "Please enter date range";
                lblErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write("Oops! An error occurred: " + ex.Message);
            }
        }
        //sorting function from jinhuei
        protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvOrder.PageIndex = e.NewPageIndex;

            conditionalBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected page size from the dropdown list
            gvOrder.PageSize = int.Parse(ddlPageSize.SelectedValue);
            conditionalBind();
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "OrderID"; }
            set { ViewState["SortExpression"] = value; }
        }

        protected void gvOrder_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            SortExpression = e.SortExpression;

            conditionalBind();
        }

        protected void lnkOrder_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton linkButton)
            {
                SortExpression = linkButton.CommandArgument;

                SortDirection = (SortDirection == "ASC" && SortExpression == linkButton.CommandArgument) ? "DESC" : "ASC";

                conditionalBind();
            }
        }

        private void UpdateSortIcons()
        {
            Literal litSortIconId = gvOrder.HeaderRow.FindControl("litSortIconId") as Literal;
            Literal litSortIconDate = gvOrder.HeaderRow.FindControl("litSortIconDate") as Literal;
            Literal litSortIconTotal = gvOrder.HeaderRow.FindControl("litSortIconTotal") as Literal;

            string defaultIcon = "<i class='bi bi-caret-up-fill'></i>";
            string ascendingIcon = "<i class='bi bi-caret-up-fill'></i>";
            string descendingIcon = "<i class='bi bi-caret-down-fill'></i>";

            litSortIconId.Text = defaultIcon;
            litSortIconDate.Text = defaultIcon;
            litSortIconTotal.Text = defaultIcon;

            if (SortExpression == "OrderID")
            {
                litSortIconId.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "OrderDateTime")
            {
                litSortIconDate.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }
            else if (SortExpression == "TotalAmount")
            {
                litSortIconTotal.Text = (SortDirection == "ASC") ? ascendingIcon : descendingIcon;
            }

        }

        //export to pdf function from jinhuei
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // Set up PDF response properties
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=OrderReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            DataTable dt = new DataTable();

            //if date selected --> got value
            if (!string.IsNullOrEmpty(hfStartDate.Value) && !string.IsNullOrEmpty(hfEndDate.Value))
            {
                DateTime fromDate = DateTime.Parse(hfStartDate.Value);
                DateTime toDate = DateTime.Parse(hfEndDate.Value);

                string sql = @"SELECT *
                        FROM OrderPlaced O 
                        JOIN PaymentDetail P ON O.OrderID = P.OrderID
                        JOIN Customer C ON O.CusID = C.CusID
                        WHERE O.OrderDateTime BETWEEN @startDate AND @endDate";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@startDate", fromDate);
                            cmd.Parameters.AddWithValue("@endDate", toDate);
                            con.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            dt.Load(dr);
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Oops! An error occurred: " + ex.Message);
                        }
                    }
                }

                // Set up iTextSharp PDF document
                Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
                pdfdoc.Open();

                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph($"Order Summary Report From {fromDate:dd/MM/yyyy} To {toDate:dd/MM/yyyy}", FontFactory.GetFont("Arial", 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                pdfdoc.Add(title);
                pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

                int totalOrders = dt.Rows.Count;

                iTextSharp.text.Paragraph exportInfo = new iTextSharp.text.Paragraph($"ExportDate: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nTotal Orders: {totalOrders}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
                exportInfo.Alignment = Element.ALIGN_LEFT;

                pdfdoc.Add(exportInfo);

                pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

                PdfPTable pdfTable = new PdfPTable(6);
                pdfTable.WidthPercentage = 100;
                pdfTable.SetWidths(new float[] { 1f, 1f, 1f, 2f, 1f, 2f });
                BaseColor lightGrey = new BaseColor(211, 211, 211);

                // table headers
                pdfTable.AddCell(new PdfPCell(new Phrase("Order ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Date")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Order Type")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Payment Method")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Total Amount")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Status")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

                // Initialize sums
                int totalDelivery = 0;
                int totalPickUp = 0;
                decimal totalSales = 0;

                foreach (DataRow row in dt.Rows)
                {
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderID"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(row["OrderDateTime"]).ToString("dd/MM/yyyy"))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderType"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["PaymentMethod"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["TotalAmount"]).ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderStatus"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });

                    if (row["OrderType"].ToString() == "Delivery")
                    {
                        totalDelivery++;
                    }
                    else if (row["OrderType"].ToString() == "Pick Up")
                    {
                        totalPickUp++;
                    }

                    totalSales += Convert.ToDecimal(row["TotalAmount"]);
                }

                pdfdoc.Add(pdfTable);

                iTextSharp.text.Paragraph totalReport = new iTextSharp.text.Paragraph($"Total 'Delivery' Orders: {totalDelivery.ToString()}\nTotal 'Pick Up' Orders: {totalPickUp.ToString()}\nTotal Sales: {totalSales.ToString("C")}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
                totalReport.Alignment = Element.ALIGN_LEFT;

                pdfdoc.Add(totalReport);

                pdfdoc.Close();
                Response.Write(pdfdoc);
                Response.End();

            }
            else
            {
                //if date not selected
                //get all the data
                string sql = @"SELECT *
                        FROM OrderPlaced O 
                        JOIN PaymentDetail P ON O.OrderID = P.OrderID
                        JOIN Customer C ON O.CusID = C.CusID";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        try
                        {
                            con.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            dt.Load(dr);
                        }
                        catch (Exception ex) 
                        {
                            Response.Write("Oops! An error occurred: " + ex.Message);
                        }
                    }
                }

                // Set up iTextSharp PDF document
                Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
                pdfdoc.Open();

                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Order Summary Report", FontFactory.GetFont("Arial", 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                pdfdoc.Add(title);
                pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

                int totalOrders = dt.Rows.Count;

                iTextSharp.text.Paragraph exportInfo = new iTextSharp.text.Paragraph($"ExportDate: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nTotal Orders: {totalOrders}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
                exportInfo.Alignment = Element.ALIGN_LEFT;

                pdfdoc.Add(exportInfo);

                pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

                PdfPTable pdfTable = new PdfPTable(6);
                pdfTable.WidthPercentage = 100;
                pdfTable.SetWidths(new float[] { 1f, 1f, 1f, 2f, 1f, 2f });
                BaseColor lightGrey = new BaseColor(211, 211, 211);

                // table headers
                pdfTable.AddCell(new PdfPCell(new Phrase("Order ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Date")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Order Type")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Payment Method")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Total Amount")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
                pdfTable.AddCell(new PdfPCell(new Phrase("Status")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

                // Initialize sums
                int totalDelivery = 0;
                int totalPickUp = 0;
                decimal totalSales = 0;

                foreach (DataRow row in dt.Rows)
                {
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderID"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(row["OrderDateTime"]).ToString("dd/MM/yyyy"))) { HorizontalAlignment = Element.ALIGN_LEFT });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderType"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["PaymentMethod"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["TotalAmount"]).ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                    pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderStatus"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });

                    if (row["OrderType"].ToString() == "Delivery")
                    {
                        totalDelivery++;
                    }
                    else if (row["OrderType"].ToString() == "Pick Up")
                    {
                        totalPickUp++;
                    }

                    totalSales += Convert.ToDecimal(row["TotalAmount"]);
                }

                pdfdoc.Add(pdfTable);

                iTextSharp.text.Paragraph totalReport = new iTextSharp.text.Paragraph($"Total 'Delivery' Orders: {totalDelivery.ToString()}\nTotal 'Pick Up' Orders: {totalPickUp.ToString()}\nTotal Sales: {totalSales.ToString("C")}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
                totalReport.Alignment = Element.ALIGN_LEFT;

                pdfdoc.Add(totalReport);

                pdfdoc.Close();
                Response.Write(pdfdoc);
                Response.End();
            }
            
            
        }

        protected void BtnReceipt_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string orderId = hfOrderID.Value;
            string sql = @"
        SELECT OI.Quantity, P.ProductName, OI.Price, 
               (OI.Quantity * OI.Price) AS Subtotal,
               OP.OrderDateTime, 
               C.Username, C.EmailAddress, C.ContactNo
        FROM OrderedItem OI
        INNER JOIN Product P ON OI.ProductID = P.ProductID
        INNER JOIN OrderPlaced OP ON OI.OrderID = OP.OrderID
        INNER JOIN Customer C ON OP.CusID = C.CusID
        WHERE OI.OrderID = @OrderID";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }

            // Set up iTextSharp PDF document
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();

            // Title
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Invoice", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));
            // User Information
            if (dt.Rows.Count > 0)
            {
                DataRow userInfo = dt.Rows[0]; // Get user info from the first row
                iTextSharp.text.Paragraph exportInfo = new iTextSharp.text.Paragraph($"Order ID: {orderId}\n" +
                    $"Export Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n\n" +
                    $"Username: {userInfo["Username"]} \n" +
                    $"Email: {userInfo["EmailAddress"]} \n" +
                    $"Contact No: {userInfo["ContactNo"]} \n", FontFactory.GetFont("Arial", 12, Font.NORMAL));

                exportInfo.Alignment = Element.ALIGN_LEFT;
                pdfdoc.Add(exportInfo);
            }

            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));
            // Create the PDF table
            PdfPTable pdfTable = new PdfPTable(4); // 4 columns
            pdfTable.WidthPercentage = 100;
            pdfTable.SetWidths(new float[] { 1f, 2f, 1f, 1f });
            BaseColor lightGrey = new BaseColor(211, 211, 211);

            // Table headers
            pdfTable.AddCell(new PdfPCell(new Phrase("Quantity")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Product Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Price (RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Subtotal (RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            // Initialize sums
            decimal totalSubtotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(row["Quantity"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString())) { HorizontalAlignment = Element.ALIGN_LEFT });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["Price"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["Subtotal"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });

                totalSubtotal += Convert.ToDecimal(row["Subtotal"]);
            }
            // Calculate tax (6%) and total
            decimal tax = totalSubtotal * 0.06m;
            decimal grandTotal = totalSubtotal + tax;

            // Add rows for subtotal, tax, and grand total
            pdfTable.AddCell(new PdfPCell(new Phrase("Subtotal")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase($"RM{totalSubtotal:F2}")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            pdfTable.AddCell(new PdfPCell(new Phrase("Tax (6%)")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase($"RM{tax:F2}")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            pdfTable.AddCell(new PdfPCell(new Phrase("Total")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase($"RM{grandTotal:F2}")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfdoc.Add(pdfTable);
            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }
    }


}
