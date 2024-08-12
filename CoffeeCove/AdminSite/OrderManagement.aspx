<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div id="main" class="main" style="margin-left:9%">
    <div class="pagetitle">
        <br />
        <h3>Orders Management</h3>
    </div>
    <section class="section">
        <div class="row" style="margin-top:2%;" >
            <!-- Category Form -->
            <div class="col" >
                <div class="card" >
                    <div class="card-body"  >
                        <h5 class="card-title">Orders History</h5>
                        <div class="row g-3 ">
                            <div class="col-12">
                                <label>Category Name</label>
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Enter Category Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Category Name." ControlToValidate="txtCategoryName" CssClass="error" Display="Dynamic" ValidationGroup="CategoryForm"/>
                                <asp:HiddenField ID="hdnId" runat="server" Value="0"/>
                            </div>
                            <div class="col-12">
                                <label>Category Image</label>
                                <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control"/>
                            </div>
                            <div class="form-check col-12">
                                <asp:CheckBox ID="cbIsActive" runat="server" Text="IsActive" />
                            </div>
                            
                            <div class="col-12">
                                <asp:Image ID="imgCategory" runat="server" CssClass="img-thumbnail"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            
        </div>
    </section>
</div>

</asp:Content>
