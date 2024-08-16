<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="CoffeeCove.AdminSite.OrderManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div id="main" class="main" style="padding-left:3%;border: solid 2px blue">
    <div class="pagetitle">
        <br />
        <h3>Order Management</h3>
    </div>
    <section class="section">
    <div class="row" style="margin-top:20px" >
        <!-- Date Form -->
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
                    <h5 class="card-title">Order History</h5>
                
                    <asp:GridView ID="gvOrder" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="OrderID" Width="100%" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
                            <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" SortExpression="OrderDate" />
                            <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" SortExpression="TotalAmount" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditCategory" CommandArgument='<%# Eval("OrderId") %>' Text="Edit" />
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("OrderId") %>' Text="Delete"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
              
              
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT [OrderID], [CustomerID], [OrderDate], [TotalAmount] FROM [Order]"></asp:SqlDataSource>
              
              
                </div>
            </div>
        </div>

    </div>

</section>

</div>

</asp:Content>
