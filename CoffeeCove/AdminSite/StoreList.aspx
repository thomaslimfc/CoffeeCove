<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="StoreList.aspx.cs" Inherits="CoffeeCove.AdminSite.StoreList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/storeList.css" rel="stylesheet" />
<div id="main" class="main">
    <div class="pagetitle">
        <br />
        <h3>Store Management</h3>
    </div>
    <section class="section">
        <div class="row" style="margin-top:2%;" >

            <!-- Product Form -->
            <div class="col-lg-4" >
                <div class="card" >
                    <div class="card-body"  >
                        <h5 class="card-title">Store</h5>
                        <div class="row g-3 ">
                            <div class="col-12">
                                <label>Store Name</label>
                                <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control" placeholder="Enter Store Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Store Name." ControlToValidate="txtStoreName" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm"/>
                                <asp:HiddenField ID="hdnId" runat="server" Value="0"/>
                            </div>
                            <div class="col-12">
                                <label>Store Address</label>
                                <asp:TextBox ID="txtStoreAddress" runat="server" CssClass="form-control" placeholder="Enter Store Address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Store Address." ControlToValidate="txtStoreAddress" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12">
                                <label>PostCode</label>
                                <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" placeholder="Enter PostCode"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter PostCode." ControlToValidate="txtPostCode" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm"/>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid PostCode (5 digits only)." ControlToValidate="txtPostCode" CssClass="error" Display="Dynamic" ValidationGroup="StoreForm" ValidationExpression="^\d{5}$" />
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="StoreForm"/>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" Text="Reset" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Store List -->
            <div class="col-lg-8">
                <div class="card" >
                    <div class="card-body" >
                        <h5 class="card-title">Product List</h5>
                        <asp:GridView ID="gvStoreList" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="StoreID" CssClass="table table-striped gridview" PageSize="10" AllowPaging="true" AllowSorting="true" EmptyDataText="There are no data records">
                            <Columns>
                                <asp:BoundField DataField="StoreID" HeaderText="StoreID" ReadOnly="True" SortExpression="StoreID" />
                                <asp:BoundField DataField="StoreName" HeaderText="StoreName" SortExpression="StoreName" />
                                <asp:BoundField DataField="StoreAddress" HeaderText="StoreAddress" SortExpression="StoreAddress" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("StoreID") %>' Text="Edit"/>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("StoreID") %>' Text="Delete"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row"/>
                            <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                        </asp:GridView>
                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [StoreID], [StoreName], [StoreAddress] FROM [Store]"></asp:SqlDataSource>
                        
                        
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</asp:Content>
