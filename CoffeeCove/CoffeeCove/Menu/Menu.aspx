<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <!-- Menu Poster -->
    <div id="menuPoster">
        <img src="img/menuPoster.jpg" />
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
        <div id="productItem">
            <div class="item">
                <asp:Image ID="Img1" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato1.jpg" />
                <asp:Label ID="Label1" runat="server" Text="Hot Honey Affogato" CssClass="productName"></asp:Label>
            </div>
            <div class="item">
                <asp:Image ID="Img2" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato2.jpg" />
                <asp:Label ID="Label2" runat="server" Text="Whiskey Barrel-Aged Affogato" CssClass="productName"></asp:Label>
            </div>
            <div class="item">
                <asp:Image ID="Img3" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato3.jpg" />
                <asp:Label ID="Label3" runat="server" Text="Caramel Mocha Drizzle Affogato" CssClass="productName"></asp:Label>
            </div>
            <div class="item">
                <asp:Image ID="Img4" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato4.jpg" />
                <asp:Label ID="Label4" runat="server" Text="Caramel Mocha Drizzle Affogato" CssClass="productName"></asp:Label>
            </div>
            <div class="item">
                <asp:Image ID="Img5" runat="server" CssClass="product" ImageUrl="~/imgProductItems/coffee1.jpg" />
                <asp:Label ID="Label5" runat="server" Text="Caramel Mocha Drizzle Affogato" CssClass="productName"></asp:Label>
            </div>
        </div>

    </div>
</asp:Content>
