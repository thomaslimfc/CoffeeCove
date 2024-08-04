<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    
    <div id="menuContainer">
        <!-- Category -->
        <div id="categoryContainer">
            <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_itemCommand" >
                <ItemTemplate>
                    <asp:LinkButton ID="linkCategory" runat="server" CssClass="categoryLink" CommandArgument='<%# Eval("CategoryId") %>' CommandName="Select" >
                        <%# Eval("CategoryName") %>
                    </asp:LinkButton>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Product -->
        <div id="productItem">
            <asp:Repeater ID="rptProduct" runat="server">
                <ItemTemplate>
                    <div style="width: 20%; height: 50%;margin: 1%; box-sizing: border-box;">
                        <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' style="width: 100%; height: auto" />
                        <div><strong><%# Eval("ProductName") %></strong></div>
                        <div>Price: RM <%# Eval("UnitPrice", "{0:N2}") %></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
