<%@ Page Title="User Profile" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" 
    Inherits="CoffeeCove.UserManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <br /><br /><br /><br />
    
    <!--<div>
        <h3 class="textAlignCenter">Your current Club tier</h3>
        <h2 class="textAlignCenter">Point Balance: 199</h2>
        <h1 class="textAlignCenter">Bronze</h1>
    </div>-->
    <br />
    <center>
        <div class="profileContainer">
        <br />
        <table>
            <tr><td><br /></td></tr>
            <tr>
                <td class="blankCol"></td>
                <th colspan="2">Personal Information</th>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol" >Username</td>
                <td class="contentCol" >Gender</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol"><asp:Label ID="lblUsername" runat="server" /></td>
                <td class="contentCol"><asp:Label ID="lblGender" runat="server" /></td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol" >Email Address</td>
                <td class="contentCol" >Date of Birth</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol"><asp:Label ID="lblEmail" runat="server" /></td>
                <td class="contentCol"><asp:Label ID="lblDOB" runat="server" /></td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol" >Contact Number</td>
                <td class="contentCol" >Residence State</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol"><asp:Label ID="lblContactNo" runat="server" /></td>
                <td class="contentCol"><asp:Label ID="lblResidenceState" runat="server" /></td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>
        </table>
        <br />
        </div>
    </center>

</asp:Content>