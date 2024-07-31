<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CoffeeCove.Menu.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    
    <!-- Menu Poster -->
    <div id="menuPoster">
        <img src="/img/poster.jpg" style="width: 102%" />
        <div id="menuPosterText">WELCOME TO OUR MENU</div>
    </div>

    <!-- Category Menu -->
    <div id="categoryContainer">
        <asp:Menu ID="Menu1" runat="server" Font-Size="17px" Orientation="Horizontal" >
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
            <td class="item" style="height: 250px">
                <asp:ImageButton ID="Img1" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato1.jpg" Width="200px" CommandArgument="Hot Honey Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit1" runat="server" Text="Hot Honey Affogato"></asp:Literal>
            </td>
            <td style="width: 60px; height: 250px;"></td>
            <td class="item" style="height: 250px">
                <asp:ImageButton ID="Img2" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato2.jpg" Width="200px" CommandArgument="Whiskey Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="lit2" runat="server" Text="Whiskey Affogato"></asp:Literal>
            </td>
            <td style="width: 60px; height: 250px;"></td>
            <td class="item" style="height: 250px">
                <asp:ImageButton ID="Img3" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato3.jpg" Width="200px" CommandArgument="Caramel Mocha Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit3" runat="server" Text="Caramel Mocha Affogato"></asp:Literal>
            </td>
            <td style="width: 60px; height: 250px;"></td>
            <td class="item" style="height: 250px">
                <asp:ImageButton ID="Img4" runat="server" CssClass="product" ImageUrl="~/imgProductItems/affogato4.jpg" Width="200px" CommandArgument="Caramel Mocha Affogato|5.00" CommandName="SelectItem" /><br />
                <asp:Literal ID="Lit4" runat="server" Text="Caramel Mocha Affogato"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="item">
                <asp:ImageButton ID="Img5" runat="server" CssClass="product" ImageUrl="~/imgProductItems/coffee1.jpg" Width="200px" CommandArgument="Hot Honey Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit5" runat="server" Text="Hot Honey Affogato"></asp:Literal>
            </td>
            <td style="width: 60px"></td>
            <td class="item">
                <asp:ImageButton ID="Img6" runat="server" CssClass="product" ImageUrl="~/imgProductItems/coffee2.jpg" Width="200px" CommandArgument="Whiskey Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit6" runat="server" Text="Whiskey Affogato"></asp:Literal>
            </td>
            <td style="width: 60px"></td>
            <td class="item">
                <asp:ImageButton ID="Img7" runat="server" CssClass="product" ImageUrl="~/imgProductItems/coffee3.jpg" Width="200px" CommandArgument="Caramel Mocha Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit7" runat="server" Text="Caramel Mocha Affogato"></asp:Literal>
            </td>
            <td style="width: 60px"></td>
            <td class="item">
                <asp:ImageButton ID="Imge8" runat="server" CssClass="product" ImageUrl="~/imgProductItems/coffee4.jpg" Width="200px" CommandArgument="Caramel Mocha Affogato|5.00" CommandName="SelectItem"/><br />
                <asp:Literal ID="Lit8" runat="server" Text="Caramel Mocha Affogato"></asp:Literal>
            </td>
        </tr>
    </table>

<!-- Form Container -->
<asp:Panel ID="pnlOrderForm" runat="server" Visible="false">
    <h2>
        <asp:Label ID="itemNameLabel" runat="server" Text="Item Name"></asp:Label>
    </h2>
    <asp:DropDownList ID="ddlSize" runat="server">
        <asp:ListItem Text="Small" Value="Small" />
        <asp:ListItem Text="Medium" Value="Medium" />
        <asp:ListItem Text="Large" Value="Large" />
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSugarLevel" runat="server">
        <asp:ListItem Text="No Sugar" Value="None" />
        <asp:ListItem Text="Light" Value="Light" />
        <asp:ListItem Text="Regular" Value="Regular" />
        <asp:ListItem Text="Extra" Value="Extra" />
    </asp:DropDownList>
    <asp:DropDownList ID="ddlIceLevel" runat="server">
        <asp:ListItem Text="No Ice" Value="None" />
        <asp:ListItem Text="Light" Value="Light" />
        <asp:ListItem Text="Regular" Value="Regular" />
        <asp:ListItem Text="Extra" Value="Extra" />
    </asp:DropDownList>
    <asp:TextBox ID="txtInstructions" runat="server" TextMode="MultiLine" />
    <div>
        Price: <asp:Label ID="lblPrice" runat="server">$0.00</asp:Label>
    </div>
    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"/>
    <asp:Button ID="btnClose" runat="server" Text="Close"/>
</asp:Panel>

        
        
</asp:Content>
