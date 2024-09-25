using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;

namespace CoffeeCove.AdminSite
{
    public partial class Payment : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                gvPayment.RowDataBound += gvPayment_RowDataBound;
                BindPayment();
            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the buttons
                LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
                LinkButton lnkCancel = (LinkButton)e.Row.FindControl("lnkCancel");

                // Get the PaymentStatus and PaymentMethod values
                string paymentStatus = DataBinder.Eval(e.Row.DataItem, "PaymentStatus").ToString().Trim();
                string paymentMethod = DataBinder.Eval(e.Row.DataItem, "PaymentMethod").ToString().Trim();

                // Check the status and control the visibility of buttons
                if (paymentStatus == "Pending")
                {
                    // Show lnkCancel button if PaymentMethod is NULL or empty
                    if (string.IsNullOrEmpty(paymentMethod))
                    {
                        lnkUpdate.Visible = false;
                        lnkCancel.Visible = true;
                    }
                    else
                    {
                        lnkUpdate.Visible = true;
                        lnkCancel.Visible = true;
                    }
                }
                else
                {
                    lnkUpdate.Visible = false;
                    lnkCancel.Visible = false;
                }
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
            string filterCondition = ddlFilter.SelectedValue;

            // Save filter condition in ViewState
            ViewState["FilterCondition"] = filterCondition;

            // Call the method to bind filtered payment data
            BindPaymentWithFilter(filterCondition);
        }

        protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvPayment.PageIndex = e.NewPageIndex;

            // Check if a filter condition is stored in ViewState
            if (ViewState["FilterCondition"] != null)
            {
                string filterCondition = ViewState["FilterCondition"].ToString();
                BindPaymentWithFilter(filterCondition);
            }
            else
            {
                // If no filter is selected, bind the normal payment data
                BindPayment();
            }
        }

        private void BindPaymentWithFilter(string filterCondition)
        {
            string sql = @"
        SELECT pd.PaymentID, pd.PaymentMethod, pd.PaymentStatus, pd.OrderID, op.OrderDateTime, op.TotalAmount
        FROM PaymentDetail pd
        INNER JOIN OrderPlaced op ON pd.OrderID = op.OrderID";

            // Apply the filter condition
            if (filterCondition == "True")
            {
                sql += " WHERE pd.PaymentStatus = 'Pending'";
            }
            else if (filterCondition == "False")
            {
                sql += " WHERE pd.PaymentStatus = 'Complete'";
            }
            else if (filterCondition == "Cancelled")
            {
                sql += " WHERE pd.PaymentStatus = 'Cancel'";
            }

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

        protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                // Get the PaymentID from the CommandArgument
                int paymentId = Convert.ToInt32(e.CommandArgument);

                // Call method to update the PaymentStatus to 'Complete' and OrderStatus to 'Order Delivered'
                UpdatePaymentAndOrderStatus(paymentId);
            }
            else if (e.CommandName == "CancelPayment")
            {
                // Get the PaymentID from the CommandArgument
                int paymentId = Convert.ToInt32(e.CommandArgument);

                // Call method to cancel the payment and update OrderStatus
                CancelPayment(paymentId);
            }
        }

        private void UpdatePaymentAndOrderStatus(int paymentId)
        {
            string updateSql = @"
        UPDATE PaymentDetail SET PaymentStatus = 'Complete' WHERE PaymentID = @PaymentID;
        UPDATE OrderPlaced SET OrderStatus = 'Order Delivered' 
        WHERE OrderID = (SELECT OrderID FROM PaymentDetail WHERE PaymentID = @PaymentID)";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(updateSql, con))
                {
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Rebind the GridView to reflect the changes
            BindPayment();

            // Optionally, show a success message
            lblMsg.Text = "Payment status updated to Complete and order marked as Delivered successfully.";
            lblMsg.CssClass = "alert alert-success";
            lblMsg.Visible = true;
        }

        private void CancelPayment(int paymentId)
        {
            string updateSql = @"
        UPDATE PaymentDetail SET PaymentStatus = 'Cancel' WHERE PaymentID = @PaymentID;
        UPDATE OrderPlaced SET OrderStatus = 'Order Cancelled' 
        WHERE OrderID = (SELECT OrderID FROM PaymentDetail WHERE PaymentID = @PaymentID)";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(updateSql, con))
                {
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Rebind the GridView to reflect the changes
            BindPayment();

            // Optionally, show a success message
            lblMsg.Text = "Payment status cancelled successfully.";
            lblMsg.CssClass = "alert alert-success";
            lblMsg.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the dates from the textboxes
            DateTime fromDate;
            DateTime toDate;

            // Check if the dates are valid
            if (DateTime.TryParse(txtFrom.Text, out fromDate) && DateTime.TryParse(txtTo.Text, out toDate))
            {
                // Validate that fromDate is not greater than toDate
                if (fromDate > toDate)
                {
                    // Show an error message if fromDate is greater than toDate
                    lblMsg.Text = "End date must be after the start date.";
                    lblMsg.CssClass = "alert alert-danger";
                    lblMsg.Visible = true;
                }
                else
                {
                    // Call a method to bind the payments based on the date range
                    BindPaymentByDate(fromDate, toDate);

                    // Optionally hide the message if the search is successful
                    lblMsg.Visible = false;
                }
            }
            else
            {
                // Optionally display an error message
                lblMsg.Text = "Please enter valid dates.";
                lblMsg.CssClass = "alert alert-danger";
                lblMsg.Visible = true;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Clear the textboxes
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;

            // Rebind the original payment data
            BindPayment();
        }

        private void BindPaymentByDate(DateTime fromDate, DateTime toDate)
        {
            string sql = @"
        SELECT pd.PaymentID, pd.PaymentMethod, pd.PaymentStatus, pd.OrderID, op.OrderDateTime, op.TotalAmount
        FROM PaymentDetail pd
        INNER JOIN OrderPlaced op ON pd.OrderID = op.OrderID
        WHERE op.OrderDateTime BETWEEN @FromDate AND @ToDate";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

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

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // Set up PDF response properties
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PaymentReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string sql = @"SELECT pd.PaymentID, pd.PaymentMethod, pd.PaymentStatus, 
            pd.OrderID, op.OrderDateTime, op.TotalAmount,
            (SELECT COUNT(*) FROM PaymentDetail WHERE PaymentStatus = 'Pending') AS PendingCount,
            (SELECT COUNT(*) FROM PaymentDetail WHERE PaymentStatus = 'Complete') AS CompleteCount,
            (SELECT COUNT(*) FROM PaymentDetail WHERE PaymentStatus = 'Cancel') AS CancelCount FROM 
            PaymentDetail pd INNER JOIN 
            OrderPlaced op ON pd.OrderID = op.OrderID";


            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }

            // Set up iTextSharp PDF document
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();

            Paragraph title = new Paragraph("Payment Summary Report", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new Paragraph(" "));

            int totalPayments = dt.Rows.Count;
            decimal totalAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));

            // Get the status counts
            int pendingCount = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["PendingCount"]) : 0;
            int completeCount = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["CompleteCount"]) : 0;
            int cancelCount = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["CancelCount"]) : 0;

            Paragraph exportInfo = new Paragraph($"ExportDate: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\nTotal Payments: {totalPayments}\nTotal Amount: RM {totalAmount:F2}", FontFactory.GetFont("Arial", 12, Font.NORMAL));
            exportInfo.Alignment = Element.ALIGN_LEFT;
            pdfdoc.Add(exportInfo);

            pdfdoc.Add(new Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(6); // 6 columns
            pdfTable.WidthPercentage = 100;
            pdfTable.SetWidths(new float[] { 1f, 1f, 1f, 2f, 1f, 1f });
            BaseColor lightGrey = new BaseColor(211, 211, 211);

            // Table headers
            pdfTable.AddCell(new PdfPCell(new Phrase("Payment ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Order ID")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total Amount (RM)")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Payment Method")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Payment Status")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Order Date")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(new PdfPCell(new Phrase(row["PaymentID"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["OrderID"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["TotalAmount"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["PaymentMethod"].ToString())) { HorizontalAlignment = Element.ALIGN_LEFT });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["PaymentStatus"].ToString())) { HorizontalAlignment = Element.ALIGN_LEFT });
                pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDateTime(row["OrderDateTime"]).ToString("dd/MM/yyyy"))) { HorizontalAlignment = Element.ALIGN_CENTER });
            }

            pdfdoc.Add(pdfTable);

            // Add payment status counts below the table
            pdfdoc.Add(new Paragraph($"Total Pending Count: {pendingCount}", FontFactory.GetFont("Arial", 12, Font.NORMAL)));
            pdfdoc.Add(new Paragraph($"Total Complete Count: {completeCount}", FontFactory.GetFont("Arial", 12, Font.NORMAL)));
            pdfdoc.Add(new Paragraph($"Total Cancel Count: {cancelCount}", FontFactory.GetFont("Arial", 12, Font.NORMAL)));

            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }
    }
}