﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="CoffeeCove.Categories" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="../CSS/ProductCategory.css" rel="stylesheet" />

    <div id="main" class="main" style="margin-right: 1%">
        <div class="pagetitle" style="color: #fff">
            <br />
            <h3>Categories Management</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">

                <!-- Category Form -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Category</h5>
                            <div class="row g-3 ">
                                <div class="col-8">
                                    <label class="label">Category Name</label>
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Enter Category Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Category Name." ControlToValidate="txtCategoryName" CssClass="error" Display="Dynamic" ValidationGroup="CategoryForm" />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtCategoryName" CssClass="error" Display="Dynamic" ErrorMessage="Category already exists." OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="CategoryForm"></asp:CustomValidator>
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                </div>
                                <div class="col-8">
                                    <label class="label">Category Image</label>
                                    <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select Photo." ControlToValidate="fuCategoryImage" CssClass="error" Display="Dynamic" ValidationGroup="CategoryForm"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Only JPG and PNG are allowed." ControlToValidate="fuCategoryImage" CssClass="error" Display="Dynamic" ValidationExpression=".+\.(jpg|png)$" ValidationGroup="CategoryForm"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-check col-8">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CategoryForm" CssClass="btn btn-secondary" />
                                    &nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-dark" />
                                </div>
                                <div class="col-8">
                                    <asp:Image ID="imgCategory" runat="server" CssClass="img-box" Width="200px" Height="200px" />
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
                <table style="margin-left: 10px; margin-bottom: 10px">
                    <tr>
                        <td class="search-bar">
                            <!-- AJAX tools:Search -->
                            <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search ID, Name" CssClass="datatable-input"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
                                EnableCaching="false" CompletionInterval="100" CompletionSetCount="10" MinimumPrefixLength="1" ServiceMethod="GetItemList">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-light" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-secondary" />

                        </td>
                        <td>
                            <!-- filter -->
                            <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" CssClass="form-select" Width="80%">
                                <asp:ListItem Text="All" Value="All" />
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <!-- Category List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <table>
                                <tr>
                                    <td class="col-10">
                                        <h5 class="card-title">Category List</h5>
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

                            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                OnRowCommand="gvCategory_RowCommand" Width="100%" AllowSorting="True" OnSorting="gvCategory_Sorting"
                                AllowPaging="true" OnPageIndexChanging="gvCategory_PageIndexChanging" PageSize="5" EmptyDataText="No categories found.">
                                <Columns>
                                    <asp:TemplateField SortExpression="CategoryId" ItemStyle-Width="10px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkCategoryId" runat="server" CommandArgument="CategoryId" CssClass="header-link"
                                                ToolTip="Sort" OnClick="lnkCategory_Click">
                                                ID
                                                <asp:Literal ID="litSortIconId" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("CategoryId") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="CategoryName" ItemStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkCategoryName" runat="server" CommandArgument="CategoryName" CssClass="header-link"
                                                ToolTip="Sort" OnClick="lnkCategory_Click">
                                                Name
                                                <asp:Literal ID="litSortIconName" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("CategoryName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <img src='<%# Eval("CategoryImageUrl") %>' width="50" height="50" />
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
                                                ToolTip="Sort" OnClick="lnkCategory_Click">
                                                CreatedDate
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

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditCategory" CommandArgument='<%# Eval("CategoryId") %>' Text="Edit" CssClass="btn btn-dark btn-sm" />
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryId") %>' Text="Delete" OnClientClick="return confirmDelete();" CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridview-header" />
                                <PagerStyle CssClass="datatable-pagination" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-5" style="margin-left: 45%; margin-bottom: 20px">
                        <asp:Button ID="BtnExport" runat="server" Text="Export Report" CssClass="btn btn-success" OnClick="BtnExport_Click" />
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
                    document.getElementById('<%= imgCategory.ClientID %>').src = e.target.result;
                    document.getElementById('<%= imgCategory.ClientID %>').width = 200;
                    document.getElementById('<%= imgCategory.ClientID %>').height = 200;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function confirmDelete() {
            return confirm("Do you confirm you want to delete this category?");
        }
    </script>

</asp:Content>
