<%@ Page Title="Profile Edit" Language="C#" 
    MasterPageFile="~/Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="ProfileEdit.aspx.cs" 
    Inherits="CoffeeCove.User_Management.ProfileEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/ProfileEdit.css" rel="stylesheet" />
    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <br /><br /><br /><br /><br /><br /><br /><br />
    s
    <div>
        <h3 class="textAlignCenter">Edit your</h3>
        <h1 class="textAlignCenter">Profile Information</h1>
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
