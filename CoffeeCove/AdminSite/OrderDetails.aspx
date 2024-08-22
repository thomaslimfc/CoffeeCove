<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagetitle" style="color:#fff">
        <br />
        <h3>Orders Details</h3>
    </div>
    <div class="row" style="margin-top:20px" >
    <!-- Date Form -->
    <div class="col-lg-12">
      <div class="card">
        <div class="card-body">
          <h5 class="card-title">Order</h5>

          <div class="row g-3">
            <div class="col-12">
                <label class="label">From:</label>
                <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
            </div>
            <div class="col-12">
                <label class="label">To:</label>
                <asp:TextBox ID="txtTo" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
            </div>
            <div class="col-8">
                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="OrderForm" CssClass="btn btn-secondary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-dark" />
            </div>
            <div class="col-8">
                <table id="viewOrder" style="display:none">
                    <tr>
                        <td>
                            OrderNo:
                        </td>
                        <td>
                            <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date:
                        </td>
                        <td>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Delivery/Pick-Up No:
                        </td>
                        <td>
                            <asp:Label ID="lblDelPick" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Amount:
                        </td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
          </div>

        </div>
      </div>
    </div>
        </div>
</asp:Content>
