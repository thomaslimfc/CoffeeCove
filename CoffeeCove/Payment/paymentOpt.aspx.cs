﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Payment
{
    public partial class paymentOpt : System.Web.UI.Page
    {
        public string PayPalClientId { get; set; } = "";
        public string PayPalClientSecret { get; set; } = "";
        public string PayPalMode { get; set; } = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnCreditCardPayment_Click(object sender, EventArgs e)
        {
            // Check if all the validators on the page are valid
            if (Page.IsValid)
            {
                // If valid, redirect to payment success page
                Response.Redirect("~/Payment/paymentSuccess.aspx");
            }
        }

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payment/paymentSuccess.aspx");
        }

        protected void btnPaypal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payment/paymentSuccess.aspx");
        }
    }
}