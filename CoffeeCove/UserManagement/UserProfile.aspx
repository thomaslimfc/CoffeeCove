<%@ Page Title="User Profile" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" 
    Inherits="CoffeeCove.UserManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <br /><br /><br /><br /><br /><br /><br /><br />
    
    <div>
        <h3 class="textAlignCenter">Your current Club tier</h3>
        <h2 class="textAlignCenter">Point Balance: 199</h2>
        <h1 class="textAlignCenter">Bronze</h1>
    </div>
    <br />
    <center>
        <table>
            <tr class="oddRow">
                <td class="tdFirstCol">Username: </td>
                <td class="tdSecondCol">thomas7296</td>
            </tr>
            <tr class="evenRow">
                <td class="tdFirstCol">Email Address: </td>
                <td class="tdSecondCol">thomaslimfc@gmail.com</td>
            </tr>
            <tr class="oddRow">
                <td class="tdFirstCol">Date of Birth: </td>
                <td class="tdSecondCol">22 - 02 - 2003</td>
            </tr>
            <tr class="evenRow">
                <td class="tdFirstCol">Gender: </td>
                <td class="tdSecondCol">Female</td>
            </tr>
            <tr class="oddRow">
                <td class="tdFirstCol">Residence State: </td>
                <td class="tdSecondCol">Penang</td>
            </tr>
        </table>
    </center>

</asp:Content>
