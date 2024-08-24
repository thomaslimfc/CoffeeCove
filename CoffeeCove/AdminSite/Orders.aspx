﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
    <link href="../CSS/orderManagement.css" rel="stylesheet" />
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
              </div>

            </div>
          </div>
        </div>

        <div class="col-lg-12">
        <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
        </div>

        <!-- Order View -->
        <div class="col-lg-12" id="viewOrder" style="display:none">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Order Detail</h5>
                        <div class="col-12">
                            <table style="width:100%">
                                <tr>
                                    <td>
                                        <table id="orderTable" style="width:35%">
                                            <tr>
                                                <th> Order ID:</th>
                                                <td>
                                                    #<asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Order Date:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Delivery/Pick-Up:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblDelPick" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Payment Method:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblPaymentMethod" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h5 class="card-title">Customer Information</h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table id="customerTable" style="width:35%">
                                            <tr>
                                                <th>Username:</th>
                                                <td>
                                                    <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Email:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    Pick Up Store:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblPickUp" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <th>
                                                    Delivery Address:
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblDelivery" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h5 class="card-title">Product Ordered</h5>
                                    </td>
                                </tr>

                                <!--Display cart-->
                                <tr>
                                    <td colspan="2">
                                        <table id="cartItemTable">
                                            <tr id="cartTitle" style="border-bottom: solid 3px #433533">
                                                <th class="cartLeft" colspan="2">Item</th>
                                                <th class="cartRight">Price</th>
                                                <th class="cartRight">Quantity</th>
                                                <th class="cartRight">Total</th>
                                            </tr>

                                            <!--Repeater-->
                                            <asp:Repeater ID="rptOrdered" runat="server" OnItemDataBound="rptOrdered_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr style="border-bottom: solid 2px #433533">
                                                        <td class="tableItem">
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("ProductId") %>' Font-Bold="True" />
                                                        </td>
                                                        <td class="tableItem">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="true" Text='<%# Eval("ProductName") %>' CssClass="itemName" />
                                                            <table id="excludeTable">
                                                                <tr>
                                                                    <td>
                                                                        Size:&nbsp&nbsp<asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp
                                                                    </td>
                                                                    <td>
                                                                        Flavour:&nbsp&nbsp<asp:Label ID="lblFlavour" runat="server" Text='<%# Eval("Flavour") %>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Ice Level:&nbsp&nbsp<asp:Label ID="lblIce" runat="server" Text='<%# Eval("IceLevel") %>' />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp
                                                                    </td>
                                                                    <td>
                                                                        Add-Ons:&nbsp&nbsp<asp:Label ID="lblAddOn" runat="server" Text='<%# Eval("AddOn") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align:center" class="tableItem">
                                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>' />
                                                        </td>
                                                        <td style="text-align:center" class="tableItem">
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                                                        </td>
                                                        <td style="text-align:center" class="tableItem">
                                                            <asp:Label ID="lblLineTotal" runat="server" Text="" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>

                                        <table id="cartTotalTable">
                                            <tr class="amtTable">
                                                <td class="cartLeft">Subtotal [RM]</td>
                                                <td class="cartRight">
                                                <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="amtTable">
                                                <td class="cartLeft">Tax [6%]</td>
                                                <td class="cartRight">+
                                                <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="amtTable">
                                                <td class="cartLeft">Total [RM]</td>
                                                <td class="cartRight">
                                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                    
                            </table>
                        </div>
                    
                </div>
            </div>
        </div>


        <!-- Order List -->
        <div class="col-lg-12">

            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Order List</h5>
                
                    <asp:GridView ID="gvOrder" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="OrderID" CssClass="table table-striped" PageSize="10" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records" OnRowCommand="gvOrder_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" SortExpression="OrderID" />
                            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                            <asp:BoundField DataField="OrderDateTime" HeaderText="Date" SortExpression="OrderDateTime" />
                            <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" SortExpression="PaymentMethod" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total" SortExpression="TotalAmount" />
                            <asp:BoundField DataField="PaymentStatus" HeaderText="Status" SortExpression="PaymentStatus" />
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
              
              
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT OrderPlaced.OrderID, OrderPlaced.OrderDateTime, OrderPlaced.TotalAmount, PaymentDetail.PaymentMethod, PaymentDetail.PaymentStatus, Customer.Username FROM OrderPlaced INNER JOIN PaymentDetail ON OrderPlaced.OrderID = PaymentDetail.OrderID INNER JOIN Customer ON OrderPlaced.CusID = Customer.CusID"></asp:SqlDataSource>
              
              
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
﻿