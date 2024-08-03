using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeCove.Order
{
    public partial class orderOption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litStoreName.Text = " ";
            litStoreAdd.Text = " ";
        }
        protected void lbGurney_Click(object sender, EventArgs e)
        {
            litStoreName.Text = "CoffeeCove Gurney Plaza";
            litStoreAdd.Text = "170-G-23,24 Gurney Plaza, Pulau Tikus, 10250 George Town, Penang";
        }
        protected void lbKarpalSingh_Click(object sender, EventArgs e)
        {
            litStoreName.Text = "CoffeeCove Karpal Singh";
            litStoreAdd.Text = "No. 29C, Lot L1-1, L1-2, Maritime, 5, Lebuh Sungai Pinang, 11600 Jelutong, Penang";
        }
        protected void lbQueensBay_Click(object sender, EventArgs e)
        {
            litStoreName.Text = "CoffeeCove QueensBay";
            litStoreAdd.Text = "1-G-01, Jalan Bayan Indah, Queens Waterfront Q1 Commercial, 11900 George Town, Pulau Pinang";
        }
    }
}