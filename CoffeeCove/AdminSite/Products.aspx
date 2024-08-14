<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="CoffeeCove.AdminSite.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main">
        <div class="pagetitle">
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
                                    <label>Product Name</label>
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Product Name." ControlToValidate="txtProductName" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtProductName" CssClass="error" Display="Dynamic" ErrorMessage="Product already exists." OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="ProductForm"></asp:CustomValidator>
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                </div>
                                <div class="col-6">
                                    <label>Product Description</label>
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" placeholder="Enter Product Description" MaxLength="30"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <label>Product Price (RM)</label>
                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Unit Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Price (RM)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid price (digits only)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" ValidationExpression="^\d+(\.\d{1,2})?$" />
                                </div>
                                <div class="col-6">
                                    <label>Category</label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select a category." ControlToValidate="ddlCategory" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" />
                                </div>
                                <div class="col-6">
                                    <label>Product Image</label>
                                    <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                </div>
                                <div class="form-check col-8">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                                </div>
                                <div class="col-8">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="ProductForm" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                                </div>
                                <div class="col-6">
                                    <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
                </div>

                <!-- Product List -->
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Product List</h5>
                            <asp:GridView ID="GridViewProduct" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewProduct_RowCommand" Width="100%" CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="ProductId" HeaderText="ID" />
                                    <asp:BoundField DataField="ProductName" HeaderText="Name" />
                                    <asp:BoundField DataField="UnitPrice" HeaderText="Price(RM)" DataFormatString="{0:N2}" />
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <img src='<%# Eval("ImageUrl") %>' width="50" height="50" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Active">
                                        <ItemTemplate>
                                            <%# Eval("IsActive", "{0}") == "True" ? 
                                                    "<span class='badge rounded-pill bg-success'>Active</span>" : 
                                                    "<span class='badge rounded-pill bg-danger'>InActive</span>" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditProduct" CommandArgument='<%# Eval("ProductId") %>' Text="Edit" />
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductId") %>' Text="Delete" OnClientClick="return confirmDelete();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
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
