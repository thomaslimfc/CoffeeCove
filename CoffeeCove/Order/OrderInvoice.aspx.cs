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
            DataTable dt = new DataTable();
            string invoiceNum = "";
            string orderId = OrderIdLiteral.Text;
            //if date not selected
            //get all the data
            string sql = @"SELECT *
                        FROM OrderPlaced O 
                        JOIN PaymentDetail P ON O.OrderID = P.OrderID
                        JOIN Customer C ON O.CusID = C.CusID
                        JOIN OrderedItem OI ON O.OrderID = OI.OrderID
                        JOIN Product PR ON OI.ProductID = PR.ProductID
                        WHERE O.OrderID = @orderID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@orderID", orderId);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        //generate invoice number
                        DateTime orderDate = Convert.ToDateTime(dr["OrderDateTime"]);
                        string cusID = dr["CusID"].ToString();
                        string orderType = dr["OrderType"].ToString();
                        if (orderType == "Delivery")
                        {
                            invoiceNum = "D" + orderDate.ToString("ddMMyyyy") + orderId + cusID;
                        }
                        else if (orderType == "Pick Up")
                        {
                            invoiceNum = "P" + orderDate.ToString("ddMMyyyy") + orderId + cusID;
                        }


                    }
                    dt.Load(dr);

                }
            }



            // Set up iTextSharp PDF document
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            pdfdoc.Open();

            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("COFFEECOVE", FontFactory.GetFont("Arial", 23, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title);
            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));
            
            iTextSharp.text.Paragraph title1 = new iTextSharp.text.Paragraph("INVOICE", FontFactory.GetFont("Arial", 18, Font.BOLD));
            title1.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(title1);
            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));



            DataRow row1 = dt.Rows[0];
            iTextSharp.text.Paragraph exportInfo = new iTextSharp.text.Paragraph();

            exportInfo.Add(new iTextSharp.text.Phrase($"Print Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase("Invoice No: ", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase(invoiceNum + "\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase("Order No: ", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase(row1["OrderID"].ToString() + "\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase("Customer No: ", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase(row1["CusID"].ToString() + "\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase("Order Type: ", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase(row1["OrderType"].ToString() + "\n", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase("Payment Method: ", FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));
            exportInfo.Add(new iTextSharp.text.Phrase(row1["PaymentMethod"].ToString(), FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)));



            exportInfo.Alignment = Element.ALIGN_LEFT;

            pdfdoc.Add(exportInfo);

            pdfdoc.Add(new iTextSharp.text.Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(4);
            pdfTable.WidthPercentage = 100;
            pdfTable.SetWidths(new float[] { 3f, 1f, 1f, 1f });
            BaseColor lightGrey = new BaseColor(211, 211, 211);

            // table headers
            pdfTable.AddCell(new PdfPCell(new Phrase("Item")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Price")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Quantity")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            // Initialize sums
            decimal subTotal = 0;
            decimal tax = 0;
            decimal total = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal lineTotal = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"]);


                pdfTable.AddCell(new PdfPCell(new Phrase(row["ProductName"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["Price"]).ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(row["Quantity"].ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTable.AddCell(new PdfPCell(new Phrase(lineTotal.ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER });

                subTotal += lineTotal;
            }
            tax = subTotal * (decimal)0.06;
            total = subTotal + tax;

            pdfTable.AddCell(new PdfPCell(new Phrase("SubTotal")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(subTotal.ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Tax 6%")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(tax.ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase("Total")) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });
            pdfTable.AddCell(new PdfPCell(new Phrase(total.ToString("C"))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = lightGrey });

            pdfdoc.Add(pdfTable);

            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
        }
    }
}
