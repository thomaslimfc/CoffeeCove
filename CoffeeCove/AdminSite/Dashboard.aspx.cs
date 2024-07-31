using System;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;

namespace CoffeeCove.AdminSite
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            revenueRepo.NavigateUrl = "~/Reports/RevenueReport.aspx";
            salesRepo.NavigateUrl = "~/Reports/SalesReport.aspx";
            orderRepo.NavigateUrl = "~/Reports/OrderReport.aspx";
        }

        protected void MonthlyRevenue_Load(object sender, EventArgs e)
        {
            monthlyRevenue.Series["barChart"].ChartType = SeriesChartType.Column;
            monthlyRevenue.Series["barChart"].Points.Clear();
            monthlyRevenue.Series["barChart"].Points.AddXY("1", 500);
            monthlyRevenue.Series["barChart"].Points.AddXY("2", 300);
            monthlyRevenue.Series["barChart"].Points.AddXY("3", 400);
            monthlyRevenue.Series["barChart"].Points.AddXY("4", 700);
            monthlyRevenue.Series["barChart"].Points.AddXY("5", 900);
            monthlyRevenue.Series["barChart"].Points.AddXY("6", 1200);
            monthlyRevenue.Series["barChart"].Points.AddXY("7", 1600);
            monthlyRevenue.Series["barChart"].Points.AddXY("8", 1400);
            monthlyRevenue.Series["barChart"].Points.AddXY("9", 2000);
            monthlyRevenue.Series["barChart"].Points.AddXY("10", 1800);
            monthlyRevenue.Series["barChart"].Points.AddXY("11", 1100);
            monthlyRevenue.Series["barChart"].Points.AddXY("12", 1700);

            monthlyRevenue.Titles.Clear();
            monthlyRevenue.Titles.Add("Monthly Revenue");
            monthlyRevenue.ChartAreas[0].AxisX.Title = "Month";
            monthlyRevenue.ChartAreas[0].AxisY.Title = "Revenue";
        }

        protected void CustomerRate_Load(object sender, EventArgs e)
        {
            CustomerRate.Series["lineChart"].ChartType = SeriesChartType.Line;
            CustomerRate.Series["lineChart"].Points.Clear();
            CustomerRate.Series["lineChart"].Points.AddXY("1", 1500);
            CustomerRate.Series["lineChart"].Points.AddXY("2", 2000);
            CustomerRate.Series["lineChart"].Points.AddXY("3", 2500);
            CustomerRate.Series["lineChart"].Points.AddXY("4", 3000);
            CustomerRate.Series["lineChart"].Points.AddXY("5", 4500);

            CustomerRate.Titles.Clear();
            CustomerRate.Titles.Add("Customer Rate");
            CustomerRate.ChartAreas[0].AxisX.Title = "Stars";
            CustomerRate.ChartAreas[0].AxisY.Title = "Customer Rate";
        }
    }
}
