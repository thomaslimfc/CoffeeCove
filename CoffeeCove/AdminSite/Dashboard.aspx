﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeCove.AdminSite.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h2>Today's Sales</h2>
    <h3>Sales Summary</h3>
    
     <!-- Total Record -->
    <table id="totalContainer">
        <tr>
            <td colspan="2" class="record">
                <asp:HyperLink ID="revenueRepo" runat="server">
                    <div class="recordContent">
                        <img src="../img/revenue.png" class="totalImg" id="totalImg1"/>
                        <div class="recordText">
                            <asp:Label ID="labRevenue" runat="server" Text="Total Revenue" Font-Size="17px" ></asp:Label>
                            <asp:Literal ID="litRevenue" runat="server" Text="0"></asp:Literal>
                        </div>
                    </div>
                </asp:HyperLink>
            </td>
            <td style="width: 50px"></td>
            <td colspan="2" class="record">
                <asp:HyperLink ID="salesRepo" runat="server">
                    <div class="recordContent">
                        <img src="../img/sales.png" class="totalImg" id="totalImg2"/>
                        <div class="recordText">
                            <asp:Label ID="labSales" runat="server" Text="Total Sales" Font-Size="17px"></asp:Label>
                            <asp:Literal ID="litSales" runat="server" Text="0"></asp:Literal>
                        </div>
                    </div>
                </asp:HyperLink>
            </td>
            <td style="width: 50px"></td>
            <td colspan="2" class="record">
                <asp:HyperLink ID="orderRepo" runat="server">
                    <div class="recordContent">
                        <img src="../img/order.png" class="totalImg" id="totalImg3"/>
                        <div class="recordText">
                            <asp:Label ID="labOrder" runat="server" Text="Total Order" Font-Size="17px"></asp:Label>
                            <asp:Literal ID="litOrder" runat="server" Text="0" ></asp:Literal>
                        </div>
                    </div>
                </asp:HyperLink>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <!-- Statistics -->
    <table id="statistic">
        <tr>
            <td>
                <asp:Chart ID="monthlyRevenue" runat="server" OnLoad="MonthlyRevenue_Load" BorderlineColor="Silver" BorderlineDashStyle="Solid" Palette="Pastel" Width="800px" Height="400px">
                    <Series>
                        <asp:Series Name="barChart" ChartType="Column"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            <td style="width: 50px"></td>
            <td>
                <asp:Chart ID="CustomerRate" runat="server" OnLoad="CustomerRate_Load" BorderlineColor="Silver" BorderlineDashStyle="Solid" Palette="EarthTones" Height="400px" Width="400px">
                    <Series>
                        <asp:Series Name="lineChart" ChartType="Line"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
    </table>



</asp:Content>