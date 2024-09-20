<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="CoffeeCove.AdminSite.Products" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/ProductCategory.css" rel="stylesheet" />
    <div id="main" class="main" style="margin-right:1%">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>Products Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">
                <!-- Product Form -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Product</h5>
                            <div class="row g-3 ">
                                <div class="col-6">
                                    <label class="label">Product Name</label>
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Product Name." ControlToValidate="txtProductName" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtProductName" CssClass="error" Display="Dynamic" ErrorMessage="Product already exists." OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="ProductForm"></asp:CustomValidator>
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                </div>
                                <div class="col-6">
                                    <label class="label">Product Description</label>
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" placeholder="Enter Product Description" MaxLength="30"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label class="label">Product Price (RM)</label>
                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Unit Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Price (RM)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid price (digits only)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" ValidationExpression="^\d+(\.\d{1,2})?$" />
                                </div>
                                <div class="col-6">
                                    <label class="label">Category</label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select a category." ControlToValidate="ddlCategory" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                </div>
                                <div class="col-6">
                                    <label class="label">Product Image</label>
                                    <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                </div>
                                <div class="form-check col-8">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="ProductForm" CssClass="btn btn-secondary" />
                                    &nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-dark" />
                                </div>
                                <div class="col-6">  
                                    <asp:Image ID="imgProduct" runat="server" CssClass="img-box" Width="200px" Height="200px" />
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

                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                <table style="margin-left: 10px; margin-bottom: 10px">
                    <tr>
                        <td class="search-bar"><!--AJAX tools: Search-->
                            <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search ID, Name" CssClass="datatable-input"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
                                EnableCaching="false" CompletionInterval="100" CompletionSetCount="10" MinimumPrefixLength="1" ServiceMethod="GetItemList">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-light" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-secondary" />
                        </td>
                        <td><!-- filter -->
                            <asp:DropDownList ID="ddlFilterCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterCategory_SelectedIndexChanged" CssClass="form-select" Width="80%"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFilterActive" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterActive_SelectedIndexChanged" CssClass="form-select" Width="80%">
                                <asp:ListItem Text="All" Value="All" />
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <!-- Product List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <table>
                                <tr>
                                    <td class="col-10">
                                        <h5 class="card-title">Product List</h5>
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

                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                OnRowCommand="gvProduct_RowCommand" Width="100%" AllowSorting="True" OnSorting="gvProduct_Sorting"
                                AllowPaging="true" OnPageIndexChanging="gvProduct_PageIndexChanging" PageSize="5" EmptyDataText="No Products found.">
                                <Columns>
                                    <asp:TemplateField SortExpression="ProductId" ItemStyle-Width="10px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkProductId" runat="server" CommandArgument="ProductId" CssClass="header-link" 
                                                ToolTip="Sort" OnClick="lnkProduct_Click">ID
                                                <asp:Literal ID="litSortIconId" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductId") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="ProductName" ItemStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkProductName" runat="server" CommandArgument="ProductName" CssClass="header-link" 
                                                ToolTip="Sort" OnClick="lnkProduct_Click">Name
                                                <asp:Literal ID="litSortIconName" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("ProductName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="UnitPrice" ItemStyle-Width="30px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkUnitPrice" runat="server" CommandArgument="UnitPrice" CssClass="header-link" 
                                                ToolTip="Sort" OnClick="lnkProduct_Click">Price(RM)
                                                <asp:Literal ID="litSortIconPrice" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("UnitPrice", "{0:N2}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <img src='<%# Eval("ImageUrl") %>' width="50" height="50" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Active" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Eval("IsActive", "{0}") == "True" ? 
                                            "<span class='badge rounded-pill bg-success'>Active</span>" : 
                                            "<span class='badge rounded-pill bg-danger'>InActive</span>" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="CreatedDate" ItemStyle-Width="20px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandArgument="CreatedDate" CssClass="header-link" 
                                                ToolTip="Sort" OnClick="lnkProduct_Click">CreatedDate
                                                <asp:Literal ID="litSortIconDate" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("dd/MM/yyyy") %><br />
                                                <span style="font-size: 0.9em;">
                                                    <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("hh:mm:ss tt") %>
                                                </span>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150px" />

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditProduct" CommandArgument='<%# Eval("ProductId") %>' Text="Edit" CssClass="btn btn-dark btn-sm" />
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' Text="Delete" OnClientClick="return confirmDelete();" CssClass="btn btn-danger btn-sm" />
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
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= imgProduct.ClientID %>').src = e.target.result;
                    document.getElementById('<%= imgProduct.ClientID %>').width = 200;
                    document.getElementById('<%= imgProduct.ClientID %>').height = 200;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function confirmDelete() {
            return confirm("Do you confirm you want to delete this product?");
        }
    </script>
</asp:Content>

