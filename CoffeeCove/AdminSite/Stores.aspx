<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Stores.aspx.cs" Inherits="CoffeeCove.AdminSite.StoreList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
    <div id="main" class="main" style="margin-right:1%">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>Stores Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">
                <!-- Store Form -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Store</h5>
                            <div class="row g-3 ">
                                <div class="col-12">
                                    <label class="label">Store Name</label>
                                    <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control" placeholder="Enter Store Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Store Name." ControlToValidate="txtStoreName" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm" />
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                </div>
                                <div class="col-12">
                                    <label class="label">Store Address</label>
                                    <asp:TextBox ID="txtStoreAddress" runat="server" CssClass="form-control" placeholder="Enter Store Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Store Address." ControlToValidate="txtStoreAddress" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="StoreForm" CssClass="btn btn-secondary" OnClick="btnAdd_Click"/>
                                    &nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-dark" OnClick="btnReset_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
                </div>

                

                <!-- Store List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Store List</h5>
                            <asp:GridView ID="gvStoreList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" PageSize="5" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records" OnRowCommand="gvStoreList_RowCommand" DataKeyNames="StoreID" OnPageIndexChanging="gvStoreList_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField SortExpression="StoreID">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkStoreID" runat="server" CommandArgument="StoreID" CssClass="header-link" ToolTip="Sort" OnClick="lnkStore_Click">No
                                                <asp:Literal ID="litSortIconId" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("StoreID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="StoreName">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkStoreName" runat="server" CommandArgument="StoreName" CssClass="header-link" ToolTip="Sort" OnClick="lnkStore_Click">Store Name
                                                <asp:Literal ID="litSortIconName" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("StoreName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="StoreAddress">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkStoreAddress" runat="server" CommandArgument="StoreAddress" CssClass="header-link" ToolTip="Sort" OnClick="lnkStore_Click">Store Address
                                                <asp:Literal ID="litSortIconAddress" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("StoreAddress") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditStore" CommandArgument='<%# Eval("StoreID") %>' Text="Edit" CssClass="btn btn-dark btn-sm" />
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteStore" CommandArgument='<%# Eval("StoreID") %>' Text="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirmDelete();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="gridview-header" />
                                <PagerStyle CssClass="datatable-pagination" />
                            </asp:GridView>

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
