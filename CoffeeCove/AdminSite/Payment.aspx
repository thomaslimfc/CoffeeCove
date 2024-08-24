<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="CoffeeCove.AdminSite.Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/paymentAdmin.css" rel="stylesheet" />
    <div id="main" class="main" style="margin-right:1%">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>Payment Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">

                <!-- Date Form -->
                <div class="col-lg-12">
                  <div class="card">
                    <div class="card-body">
                      <h5 class="card-title">Payment</h5>

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
                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="OrderForm" CssClass="btn btn-secondary" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-dark" />
                        </div>
                      </div>

                    </div>
                  </div>
                </div>

                <!-- Message display -->
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
                </div>
                <br />
                <br />

                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                <div class="col-lg-12 d-flex justify-content-end mb-2">
                    <!-- filter -->
                    <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" CssClass="form-select" Width="250px">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="Pending" Value="True" />
                        <asp:ListItem Text="Complete" Value="False" />
                    </asp:DropDownList>
                </div>

                <!-- Category List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <table>
                                <tr>
                                    <td class="col-10">
                                        <h5 class="card-title">Payment List</h5>
                                    </td>
                                </tr>
                            </table>
                    
                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                 Width="100%" AllowSorting="True"
                                AllowPaging="true" OnPageIndexChanging="gvPayment_PageIndexChanging" PageSize="5" EmptyDataText="No categories found.">
                                <Columns>
                                    <asp:TemplateField SortExpression="PaymentId" ItemStyle-Width="10px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="paymentId" runat="server" CommandArgument="PaymentId" CssClass="header-link" ToolTip="Sort">ID</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("PaymentID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="OrderId" ItemStyle-Width="20px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="orderId" runat="server" CommandArgument="OrderId" CssClass="header-link" ToolTip="Sort">Order ID</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("OrderID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount(RM)" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Eval("TotalAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="PaymentMethod" ItemStyle-Width="20px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="paymentMethod" runat="server" CommandArgument="PaymentMethod" CssClass="header-link" ToolTip="Sort">Payment Method</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("PaymentMethod") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Status" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Eval("PaymentStatus").ToString() == "Complete" ? 
                                            "<span class='badge rounded-pill bg-success'>Complete</span>" : 
                                            "<span class='badge rounded-pill bg-danger'>Pending</span>" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="OrderDateTime" ItemStyle-Width="20px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="orderDateTime" runat="server" CommandArgument="OrderDateTime" CssClass="header-link" ToolTip="Sort">Order Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <%# Convert.ToDateTime(Eval("OrderDateTime")).ToString("MM/dd/yyyy") %><br />
                                                <span style="font-size: 0.9em;">
                                                    <%# Convert.ToDateTime(Eval("OrderDateTime")).ToString("hh:mm:ss tt") %>
                                                </span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="UpdateStatus" CommandArgument='<%# Eval("PaymentID") %>' Text="Update" CssClass="btn btn-dark btn-sm" />
                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="CancelPayment" CommandArgument='<%# Eval("PaymentID") %>' Text="Cancel" OnClientClick="return confirmDelete();" CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridview-header" />
                                <PagerStyle CssClass="datatable-pagination" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-5" style="margin-left:45%;margin-bottom:20px">
                        <asp:Button ID="BtnExport" runat="server" Text="Export To PDF" CssClass="btn btn-success"/>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <script>
        function confirmDelete() {
            return confirm("Do you confirm you want to cancel this payment?");
        }
    </script>
</asp:Content>
