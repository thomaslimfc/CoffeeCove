<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div id="main" class="main" style="padding-left:3%;border: solid 2px blue">
    <div class="pagetitle">
        <br />
        <h3>Order Management</h3>
    </div>
    <section class="section">
    <div class="row" style="margin-top:20px" >
        <!-- Category Form -->
        <div class="col-lg-4">

          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Date Range</h5>

              <!-- Vertical Form -->
              <div class="row g-3">
                <div class="col-12">
                    <label class="form-label">From:</label>
                    <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
                </div>
                <div class="col-12">
                    <label class="form-label">To:</label>
                    <asp:TextBox ID="txtTo" runat="server" TextMode="Date" CssClass="form-control dateRange"></asp:TextBox>
                </div>
                <div class="text-center">
                    <button type="submit" class="btn btn-primary">Submit</button>
                    <button type="reset" class="btn btn-secondary">Reset</button>
                </div>
              </div>

            </div>
          </div>
        </div>

        
        <!-- Category List -->
        <div class="col-lg-8">

          <div class="card">
            <div class="card-body">
                <h5 class="card-title">Table with stripped rows</h5>
                wakao
              
            </div>
              </div>
            </div>

        </div>

</section>

</div>

</asp:Content>
