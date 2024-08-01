<%@ Page Title="User Profile" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" 
    Inherits="CoffeeCove.UserManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <br /><br /><br /><br />
    
    <div>
        <h3 class="textAlignCenter">Your current Club tier</h3>
        <h2 class="textAlignCenter">Point Balance: 199</h2>
        <h1 class="textAlignCenter">Bronze</h1>
    </div>
    <br />
    <center>
        <div class="profileContainer">
        <br />
        <table>
            <tr class="underlineRow">
                <th colspan="2">Personal Information</th>
            </tr>
            <tr class="oddRow">
                <td>Username</td>
                <td>Gender</td>
            </tr>
            <tr class="evenRow">
                <td>thomas7296</td>
                <td>Male</td>
            </tr>

            <tr class="oddRow">
                <td>Email Address</td>
                <td>Date of Birth</td>
            </tr>
            <tr class="evenRow">
                <td>thomaslim@gmail.com</td>
                <td>21 - 02 - 2003</td>
            </tr>

            <tr class="oddRow">
                <td>Residence State</td>
                <td></td>
            </tr>
            <tr class="evenRow">
                <td>Penang</td>
                <td></td>
            </tr>
        </table>
        <br />
        </div>
    </center>

</asp:Content>