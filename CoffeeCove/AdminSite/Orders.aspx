﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
    <link href="../CSS/orderManagement.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div id="main" class="main">
        <div class="pagetitle" style="color: #fff">
            <br />
            <h3>Orders Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 20px">
                <!-- Date Form -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Order</h5>
                            <div class="row g-3">
                                <div class="col-6">
                                    <label class="label">From:</label>
                                    <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label class="label">To:</label>
                                    <asp:TextBox ID="txtTo" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="OrderForm" CssClass="btn btn-secondary" OnClick="btnSearch_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-dark" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hfStartDate" runat="server" />
                    <asp:HiddenField ID="hfEndDate" runat="server" />
                    <asp:HiddenField ID="hfOrderID" runat="server" />
                </div>
                <br />
                <br />
                <!-- Order List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <table>
                                <tr>
                                    <td class="col-10">
                                        <h5 class="card-title">Order List</h5>
                                    </td>
                                    <!-- Page dropdown -->
                                    <td style="padding-left: 35px; padding-right: 5px">Show</td>
                                    <td class="dataTables_length" id="DataTables_Table_0_length">
                                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" CssClass="form-select form-select-sm" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-left: 5px">entries</td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" PageSize="5" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records" OnRowCommand="gvOrder_RowCommand" DataKeyNames="OrderID" OnSorting="gvOrder_Sorting" OnPageIndexChanging="gvOrder_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField SortExpression="OrderID">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lbStoreID" runat="server" CommandArgument="OrderID" CssClass="header-link" ToolTip="Sort" OnClick="lnkOrder_Click">
                                                Order ID
                                                <asp:Literal ID="litSortIconId" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("OrderID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Username">
                                        <ItemTemplate>
                                            <%# Eval("Username") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lbDate" runat="server" CommandArgument="OrderDateTime" CssClass="header-link" ToolTip="Sort" OnClick="lnkOrder_Click">
                                                Date
                                                <asp:Literal ID="litSortIconDate" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("OrderDateTime")).ToString("dd/MM/yyyy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Method">
                                        <ItemTemplate>
                                            <%# Eval("PaymentMethod") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Total">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lbTotal" runat="server" CommandArgument="TotalAmount" CssClass="header-link" ToolTip="Sort" OnClick="lnkOrder_Click">
                                                Total
                                                <asp:Literal ID="litSortIconTotal" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("TotalAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Status">
                                        <ItemTemplate>
                                            <%# Eval("OrderStatus") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnView" runat="server" CommandName="viewOrder" CommandArgument='<%# Eval("OrderID") %>' Text="View" CssClass="btn btn-dark btn-sm" />
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="deleteOrder" CommandArgument='<%# Eval("OrderID") %>' Text="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirmDelete();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridview-header" />
                                <PagerStyle CssClass="datatable-pagination" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <!-- Order View -->
                <asp:Panel ID="pnlOrderDetail" runat="server" Visible="false">

                <div class="col-lg-12" id="viewOrder">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Order Detail</h5>
                            <div class="col-12">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <table id="orderTable" style="width: 35%">
                                                <tr>
                                                    <th>Order ID:</th>
                                                    <td>
                                                        <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Order Date:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Delivery/Pick-Up:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblDelPick" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Payment Method:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblPaymentMethod" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Order Status:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblOrderStatus" runat="server" Text=""></asp:Label>
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
                                            <table id="customerTable" style="width: 35%">
                                                <tr>
                                                    <th>Username:</th>
                                                    <td>
                                                        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Email:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Pick Up Store:
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblPickUp" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Delivery Address:
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
                                                    <th class="cartLeft">Item</th>
                                                    <th class="cartRight">Price</th>
                                                    <th class="cartRight">Quantity</th>
                                                    <th class="cartRight">Total</th>
                                                </tr>
                                                <!--Repeater-->
                                                <asp:Repeater ID="rptOrdered" runat="server" OnItemDataBound="rptOrdered_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr style="border-bottom: solid 2px #433533">
                                                            <td class="tableItem" style="border-right: none">
                                                                <asp:Label ID="lblName" runat="server" Font-Bold="true" Text='<%# Eval("ProductName") %>' CssClass="itemName" />
                                                                <br />
                                                                <asp:Panel ID="panelTable" runat="server">
                                                                    <table id="excludeTable">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="panelSize" runat="server">
                                                                                    Size:&nbsp&nbsp<asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td>&nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:Panel ID="panelFlavour" runat="server">
                                                                                    Flavour:&nbsp&nbsp<asp:Label ID="lblFlavour" runat="server" Text='<%# Eval("Flavour") %>' />
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="panelIce" runat="server">
                                                                                    Ice Level:&nbsp&nbsp<asp:Label ID="lblIce" runat="server" Text='<%# Eval("IceLevel") %>' />
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td>&nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:Panel ID="panelAddon" runat="server">
                                                                                    Add-Ons:&nbsp&nbsp<asp:Label ID="lblAddOn" runat="server" Text='<%# Eval("AddOn") %>' />
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                            <td style="text-align: center" class="tableItem">
                                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>' />
                                                            </td>
                                                            <td style="text-align: center" class="tableItem">
                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                                                            </td>
                                                            <td style="text-align: center" class="tableItem">
                                                                <asp:Label ID="lblLineTotal" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <table id="cartTotalTable">
                                                <tr class="amtTable">
                                                    <td class="cartLeft">Subtotal</td>
                                                    <td class="cartRight">
                                                        <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="amtTable">
                                                    <td class="cartLeft">Tax 6%</td>
                                                    <td class="cartRight">+
                                        <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="amtTable">
                                                    <td class="cartLeft">Total</td>
                                                    <td class="cartRight">
                                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />

                            <asp:Panel ID="pnlReceipt" runat="server" Visible="false">
                                <div class="col-5" style="margin-left: 45%; margin-bottom: 20px">
                                    <asp:Button ID="BtnReceipt" runat="server" Text="Print Receipt" CssClass="btn btn-success" OnClick="BtnReceipt_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>


</asp:Panel>

                <div class="col-5" style="margin-left: 45%; margin-bottom: 20px">
                    <asp:Button ID="BtnExport" runat="server" Text="Export Report" CssClass="btn btn-success" OnClick="BtnExport_Click" />
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
