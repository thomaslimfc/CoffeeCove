<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    
    <!-- Menu Poster -->
    <div id="menuPoster">
        <img src="/img/poster.jpg" />
        <div id="menuPosterText">WELCOME TO OUR MENU</div>
    </div>
   
    <div id="productContainer">
        <!-- Category Menu -->
        <div id="categoryContainer">
            <asp:Menu ID="Menu1" runat="server" Font-Size="17px" >
                <StaticMenuItemStyle CssClass="categoryItem" />
                <Items>
                    <asp:MenuItem Text="All Product" Value="All Product"></asp:MenuItem>
                    <asp:MenuItem Text="Espresso & Coffee" Value="Espresso & Coffee"></asp:MenuItem>
                    <asp:MenuItem Text="Frappuccino" Value="Frappuccino"></asp:MenuItem>
                    <asp:MenuItem Text="Coffee Cocktail" Value="Coffee Cocktail"></asp:MenuItem>
                    <asp:MenuItem Text="Breakfast" Value="Breakfast"></asp:MenuItem>
                    <asp:MenuItem Text="Lunch" Value="Lunch"></asp:MenuItem>
                    <asp:MenuItem Text="Dessert" Value="Dessert"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>

        <!-- Product Items -->
  
            <table id="productItem">
                <tr>
                    <td class="item">
                        <asp:Image ID="Img1" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato1.jpg" Width="200px" /><br />
                        <asp:Literal ID="Lit1" runat="server" Text="Hot Honey Affogato"></asp:Literal>
                    </td>
                    <td style="width: 60px"></td>
                    <td class="item">
                        <asp:Image ID="Img2" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato2.jpg" Width="200px"/><br />
                        <asp:Literal ID="lit2" runat="server" Text="Whiskey Affogato"></asp:Literal>
                    </td>
                    <td style="width: 60px"></td>
                    <td class="item">
                        <asp:Image ID="Img3" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato3.jpg" Width="200px"/><br />
                        <asp:Literal ID="Lit3" runat="server" Text="Caramel Mocha Affogato"></asp:Literal>
                    </td>
                </tr>
            </table>
 
    </div>
</asp:Content>
