﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
<div id="main" class="main">
    <div class="pagetitle" style="color:#fff">
        <br />
        <h3>Orders Management</h3>
    </div>
    <section class="section">
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

        <div class="col-lg-12">
        <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
        </div>
        <br />
        <br />

        <!--Search Bar-->
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="search-bar">
            <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search..." CssClass="datatable-input"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
                EnableCaching="false" CompletionInterval="100" CompletionSetCount="10" MinimumPrefixLength="1" ServiceMethod="GetItemList">
            </asp:AutoCompleteExtender>
            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" />
        </div>
        <br />
        <br />
        
        <!-- Order List -->
        <div class="col-lg-12">

            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Order List</h5>
                
                    <asp:GridView ID="gvOrder" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="OrderID" CssClass="table table-striped" PageSize="10" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records" OnRowCommand="gvOrder_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Date" SortExpression="OrderDate" />
                            <asp:BoundField DataField="DeliveryNo" HeaderText="DeliveryNo" SortExpression="DeliveryNo" />
                            <asp:BoundField DataField="PickUpNo" HeaderText="Pick-UpNo" SortExpression="PickUpNo " />
                            <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" SortExpression="TotalAmount" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnView" runat="server" CommandName="viewOrder" CommandArgument='<%# Eval("OrderId") %>' Text="View" CssClass="btn btn-primary btn-sm" OnClientClick="viewOrder(event);"/>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="deleteOrder" CommandArgument='<%# Eval("OrderId") %>' Text="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirmDelete();"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridview-header" />
                        <PagerStyle CssClass="datatable-pagination" />
                    </asp:GridView>
              
              
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [OrderID], [OrderDate], [TotalAmount], [DeliveryNo], [PickUpNo] FROM [Order]"></asp:SqlDataSource>
              
              
                </div>
            </div>
        </div>


    </div>

</section>

</div>

        <script>
        function confirmDelete() {
            return confirm("Do you confirm you want to delete this store from store list?");
        }
        function viewOrder(event) {
            document.getElementById("viewOrder").style.display = 'block';
            event.preventDefault();
        }

        </script>
</asp:Content>
