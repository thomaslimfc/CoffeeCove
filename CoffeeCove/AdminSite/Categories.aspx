<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="CoffeeCove.AdminSite.Categories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main">
        <div class="pagetitle">
            <br />
            <h3>Categories Management</h3>
        </div>
        <section class="section">
            <div class="alert-msg" style="margin-top:20px;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show"></asp:Label>
            </div>
            
            <div class="row" style="margin-top:2%;" >
                <!-- Category Form -->
                <div class="col-lg-4" >
                    <div class="card" >
                        <div class="card-body"  >
                            <h5 class="card-title">Category</h5>
                            <div class="row g-3 ">
                                <div class="col-12">
                                    <label>Category Name</label>
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Enter Category Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Category Name." ControlToValidate="txtCategoryName" CssClass="error" Display="Dynamic" ValidationGroup="CategoryForm"/>
                                    <asp:HiddenField ID="hdnId" runat="server" Value="0"/>
                                </div>
                                <div class="col-12">
                                    <label>Category Image</label>
                                    <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);"/>
                                </div>
                                <div class="form-check col-12">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="CategoryForm"/>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"/>
                                </div>
                                <div class="col-12">
                                    <asp:Image ID="imgCategory" runat="server" CssClass="img-thumbnail"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Category List -->
                <div class="col-lg-8">
                    <div class="card" >
                        <div class="card-body" >
                            <h5 class="card-title">Category List</h5>
                            <asp:GridView ID="GridViewCategory" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewCategory_RowCommand" Width="100%" CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="CategoryId" HeaderText="ID" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                    <asp:TemplateField HeaderText="Category Image">
                                        <ItemTemplate>
                                            <img src='<%# Eval("CategoryImageUrl") %>' width="50" height="50" />
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
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditCategory" CommandArgument='<%# Eval("CategoryId") %>' Text="Edit" />
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("CategoryId") %>' Text="Delete" OnClientClick="return confirmDelete();" />
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
