using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iTextSharp.text.pdf;
using iTextSharp.text;

namespace CoffeeCove.Order
{
    public partial class OrderInvoice : System.Web.UI.Page
    {
        string cs = Global.CS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["OrderID"];
                if (!string.IsNullOrEmpty(orderId))
                {
                    // Set the OrderID in the literal
                    OrderIdLiteral.Text = $"{orderId}";

                    LoadOrderDetails(orderId);
                }
                else
                {
                    // Handle the case where no OrderID is passed
                    OrderIdLiteral.Text = "Invalid Order ID";
                }
            }
        }

        private void LoadOrderDetails(string orderId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                // Modify the query to also select OrderDateTime from OrderPlaced
                string query = @"
            SELECT OI.Quantity, P.ProductName, OI.Price, 
                   (OI.Quantity * OI.Price) AS Subtotal,
                   OP.OrderDateTime,
                   C.Username,
                   C.EmailAddress,
                   C.ContactNo
            FROM OrderedItem OI
            INNER JOIN Product P ON OI.ProductID = P.ProductID
            INNER JOIN OrderPlaced OP ON OI.OrderID = OP.OrderID
            INNER JOIN Customer C ON OP.CusID = C.CusID
            WHERE OI.OrderID = @OrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        decimal totalSubtotal = 0;
                        List<object> productList = new List<object>(); // Temporary storage for product data

                        while (reader.Read())
                        {
                            // Retrieve user information
                            UsernameLiteral.Text = reader["Username"].ToString();
                            EmailLiteral.Text = reader["EmailAddress"].ToString();
                            PhoneLiteral.Text = reader["ContactNo"].ToString();

                            // Calculate subtotal
                            decimal subtotal = Convert.ToDecimal(reader["Subtotal"]);
                            totalSubtotal += subtotal;

                            // Store data for binding later
                            productList.Add(new
                            {
                                Quantity = reader["Quantity"],
                                ProductName = reader["ProductName"],
                                Price = reader["Price"],
                                Subtotal = subtotal
                            });

                            // Retrieve OrderDateTime
                            DateTime orderDateTime = Convert.ToDateTime(reader["OrderDateTime"]);
                            InvoiceDateLiteral.Text = orderDateTime.ToString("MM/dd/yyyy");
                        }

                        // Bind the products to the Repeater
                        ProductTable.DataSource = productList;
                        ProductTable.DataBind();

                        // Calculate Tax and Total
                        decimal tax = totalSubtotal * 0.06m;
                        decimal total = totalSubtotal + tax;

                        // Update footer with calculated values
                        InvoiceSubtotal.Text = $"RM{totalSubtotal:F2}";
                        InvoiceTax.Text = $"RM{tax:F2}";
                        InvoiceTotal.Text = $"RM{total:F2}";
                    }
                    else
                    {
                        // Handle if no products found for the order
                        NoProductsMessage.Visible = true;
                    }
                }
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            string orderId = OrderIdLiteral.Text;
            Response.Redirect($"OrderTracking.aspx?OrderID={orderId}");
        }



        protected void PrintButton_Click(object sender, EventArgs e)
        {
            GeneratePdf();
        }

        private void GeneratePdf()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string orderId = OrderIdLiteral.Text;
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
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();

            // Title
            Paragraph title = new Paragraph("Invoice", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new Paragraph(" "));
            // User Information
            if (dt.Rows.Count > 0)
            {
                DataRow userInfo = dt.Rows[0]; // Get user info from the first row
                Paragraph exportInfo = new Paragraph($"Order ID: {orderId}\n" +
                    $"Export Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} \n\n" +
                    $"Username: {userInfo["Username"]} \n" +
                    $"Email: {userInfo["EmailAddress"]} \n" +
                    $"Contact No: {userInfo["ContactNo"]} \n", FontFactory.GetFont("Arial", 12, Font.NORMAL));

                exportInfo.Alignment = Element.ALIGN_LEFT;
                pdfdoc.Add(exportInfo);
            }

            pdfdoc.Add(new Paragraph(" "));
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
