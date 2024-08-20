<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
<div id="main" class="main">
    <div class="pagetitle">
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

              <!-- Vertical Form -->
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
              </div>

            </div>
          </div>
        </div>

        <div class="col-lg-12">
        <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
        </div>
        <br />
        <br />
        
        <!-- Category List -->
        <div class="col-lg-12">

            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Order List</h5>
                
                    <asp:GridView ID="gvOrder" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="OrderID" Width="100%" CssClass="table table-striped gridview" PageSize="10" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
                            <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" SortExpression="OrderDate" />
                            <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" SortExpression="TotalAmount" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("OrderId") %>' Text="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirmDelete();"/>
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
        </script>
</asp:Content>
