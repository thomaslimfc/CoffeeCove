<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu1.aspx.cs" Inherits="CoffeeCove.Menu.Menu1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div>
        <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_itemCommand">
            <ItemTemplate>
                <asp:LinkButton ID="linkCategory" runat="server" CommandArgument='<%# Eval("CategoryId") %>' CommandName="Select">
                    <%# Eval("CategoryName") %>
                </asp:LinkButton>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div>
        <asp:Repeater ID="rptProduct" runat="server">
            <ItemTemplate>
                <div style="float:left; width: 23%; margin: 1%; box-sizing: border-box;">
                    <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' style="width: 100%; height: auto;" />
                    <div><strong><%# Eval("ProductName") %></strong></div>
                    <div>Price: <%# Eval("UnitPrice", "{0:C}") %></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
