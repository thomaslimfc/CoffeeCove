<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeCove.Admin.Index" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContent" runat="server">
    <!-- CSS style -->
    <link href="../CSS/Dashboard.css" rel="stylesheet" />
    
    <h2>Today's Sales</h2>
    <h3>Sales Summary</h3>
    
     <!-- Total Record -->
    <div id="totalContainer">
        <div class="total">
            <img src="../img/revenue.png" alt="total revenue" class="lblImg" id="lblImg1"/>
            <div class="record">
                <h4>Total Revenue</h4>
                <asp:Label ID="lblTotalRevenue" runat="server" Text="0" Font-Size="30px" CssClass="lblText"></asp:Label>
            </div>
        </div>
        <div class="total">
            <img src="../img/sales.png" alt="total sales" class="lblImg" id="lblImg2"/>
            <div class="record">
                <h4>Total Sales</h4>
                <asp:Label ID="lblTotalSales" runat="server" Text="0" Font-Size="30px" CssClass="lblText"></asp:Label>
            </div>
        </div>
        <div class="total">
            <img src="../img/order.png" alt="total order" class="lblImg" id="lblImg3"/>
            <div class="record">
                <h4>Total Order</h4>
                <asp:Label ID="lblTotalOrder" runat="server" Text="0" Font-Size="30px" CssClass="lblText"></asp:Label>
            </div>
        </div>
        <div class="total">
            <img src="../img/customer.png" alt="total customer" class="lblImg" id="lblImg4"/>
            <div class="record">
                <h4>Total Customer</h4>
                <asp:Label ID="lblTotalCustomer" runat="server" Text="0" Font-Size="30px" CssClass="lblText"></asp:Label>
            </div>
        </div>
    </div>
    
    <!-- Statistics -->
    <div id="statisticsContainer">
        <!-- Year Revenue -->
        <div class="statistic">
            <h3>Revenue by Month</h3>
            <div class="statRecord">
                <asp:Chart ID="RevenueChart" runat="server">
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
        
        <!-- Customer Satisfaction -->
        <div class="statistic">
            <h3>Customer Satisfaction</h3>
            <div class="statRecord">
                <asp:Chart ID="StatChart" runat="server">
                    <Series>
                        <asp:Series Name="Series1"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </div>

</asp:Content>
