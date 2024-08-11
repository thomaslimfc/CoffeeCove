<%@ Page Title="User Profile" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" 
    Inherits="CoffeeCove.UserManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <!-- Bootstrap CSS Files -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <br /><br /><br /><br />
    
    <center>
        <div class="profileContainer">
        <br />
        <table style="user-select: none">
            <tr><td><br /></td></tr>
            <tr>
                <td class="blankCol"></td>
                <th colspan="2">Personal Information</th>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol">Username</td>
                <td class="contentCol">Gender</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol">
                    <asp:Label ID="lblUsername" runat="server" />
                    <asp:TextBox ID="txtUsername" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Username_rqdValidator" runat="server" 
                        ControlToValidate="txtUsername" 
                        ErrorMessage="Username is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblGender" runat="server" />
                    <asp:TextBox ID="txtGender" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Gender_rqdValidator" runat="server" 
                        ControlToValidate="txtGender" 
                        ErrorMessage="Gender is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                </td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol">Email Address</td>
                <td class="contentCol">Date of Birth</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol">
                    <asp:Label ID="lblEmail" runat="server" />
                    <asp:TextBox ID="txtEmail" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Email_rqdValidator" runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Email Address is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                    <asp:RegularExpressionValidator 
                        ID="Email_regexValidator" runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Invalid email format." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" 
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblDOB" runat="server" />
                    <asp:TextBox ID="txtDOB" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="DOB_rqdValidator" runat="server" 
                        ControlToValidate="txtDOB" 
                        ErrorMessage="Date of Birth is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                    <asp:CompareValidator 
                        ID="DOB_compareValidator" runat="server" 
                        ControlToValidate="txtDOB" 
                        ErrorMessage="Please enter a valid date." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" 
                        Operator="DataTypeCheck" Type="Date" />
                </td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>

            <tr class="oddRow">
                <td class="blankCol"></td>
                <td class="contentCol">Contact Number</td>
                <td class="contentCol">Residence State</td>
                <td class="blankCol"></td>
            </tr>
            <tr class="evenRow">
                <td class="blankCol"></td>
                <td class="contentCol">
                    <asp:Label ID="lblContactNo" runat="server" />
                    <asp:TextBox ID="txtContactNo" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="ContactNo_rqdValidator" runat="server" 
                        ControlToValidate="txtContactNo" 
                        ErrorMessage="Contact Number is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblResidenceState" runat="server" />
                    <asp:TextBox ID="txtResidenceState" runat="server" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="ResidenceState_rqdValidator" runat="server" 
                        ControlToValidate="txtResidenceState" 
                        ErrorMessage="Residence State is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" />
                </td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnEdit" runat="server" 
                    Text="Edit Info" CssClass="btn btn-success"
                    style="width: 150px;"
                    OnClick="btnEdit_Click" />
                <asp:Button ID="btnSave" runat="server" 
                    Text="Save Now" CssClass="btn btn-danger"
                    style="width: 150px;"
                    OnClick="btnSave_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
        </table>
        <br />
        </div>
    </center>

</asp:Content>
