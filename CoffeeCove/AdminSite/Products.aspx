<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="CoffeeCove.AdminSite.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .description-field {
            width:5%;
        }
    </style>
    <div id="main" class="main">
        <div class="pagetitle">
            <br />
            <h3>Products Management</h3>
        </div>
        <section class="section">
            <div class="alert-msg" style="margin-top:20px;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show"></asp:Label>
            </div>
            
            <div class="row" style="margin-top:2%;" >

                <!-- Product Form -->
                <div class="col-lg-4" >
                    <div class="card" >
                        <div class="card-body"  >
                            <h5 class="card-title">Product</h5>
                            <div class="row g-3 ">
                                <div class="col-12">
                                    <label>Product Name</label>
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Product Name." ControlToValidate="txtProductName" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm"/>
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0"/>
                                </div>
                                <div class="col-12">
                                    <label>Product Description</label>
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" placeholder="Enter Product Description"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label>Product Price</label>
                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Unit Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Price (RM)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid price (digits only)." ControlToValidate="txtPrice" CssClass="error" Display="Dynamic" ValidationGroup="ProductForm" ValidationExpression="^\d+(\.\d{1,2})?$" />
                                </div>
                                <div class="col-12">
                                    <label>Category</label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-12">
                                    <label>Product Image</label>
                                    <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);"/>
                                </div>
                                <div class="form-check col-12">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="ProductForm"/>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"/>
                                </div>
                                <div class="col-12">
                                    <asp:Image ID="imgProduct" runat="server" CssClass="img-thumbnail"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Product List -->
                <div class="col-lg-8">
                    <div class="card" >
                        <div class="card-body" >
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
                                    <asp:BoundField DataField="Description" HeaderText="Description"/>
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
