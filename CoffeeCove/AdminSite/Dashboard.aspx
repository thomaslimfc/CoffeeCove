<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeCove.AdminSite.Dashboard" %>
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

</asp:Content>
