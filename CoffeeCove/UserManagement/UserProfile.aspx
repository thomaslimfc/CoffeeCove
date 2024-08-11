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
            <tr>
                <td class="blankCol"></td>
                <td class="blankCol" colspan="4">
                    <asp:Image ID="imgProfilePicture" runat="server" Width="150px" Height="150px" />
                    <br />
                    <asp:FileUpload ID="fuProfilePicture" runat="server"
                        style="margin-top: 10px" />
                    <br />
                    <asp:Button ID="UploadPictureBtn_UP" runat="server" Text="Upload Picture" 
                        style="margin-top: 10px"
                        CssClass="btn btn-primary" OnClick="UploadPictureBtn_UP_Click" />
                    <br />
                    <asp:Label ID="lblUploadMessage" runat="server"
                        CssClass="text-success" Visible="false"></asp:Label>
                </td>
                <td class="blankCol"></td>
            </tr>

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
                <td class="contentCol" style="height: 40px">
                    <asp:Label ID="lblUsername" runat="server" />
                    <asp:TextBox ID="txtUsername" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Username_rqdValidator" runat="server" 
                        ControlToValidate="txtUsername" 
                        ErrorMessage="Username is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblGender" runat="server" />
                    <asp:TextBox ID="txtGender" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Gender_rqdValidator" runat="server" 
                        ControlToValidate="txtGender" 
                        ErrorMessage="Gender is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
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
                <td class="contentCol" style="height: 40px">
                    <asp:Label ID="lblEmail" runat="server" />
                    <asp:TextBox ID="txtEmail" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="Email_rqdValidator" runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Email Address is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
                    <asp:RegularExpressionValidator 
                        ID="Email_regexValidator" runat="server" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Invalid email format." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" 
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        ValidationGroup="SaveProfile" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblDOB" runat="server" />
                    <asp:TextBox ID="txtDOB" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="DOB_rqdValidator" runat="server" 
                        ControlToValidate="txtDOB" 
                        ErrorMessage="Date of Birth is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
                    <asp:CompareValidator 
                        ID="DOB_compareValidator" runat="server" 
                        ControlToValidate="txtDOB" 
                        ErrorMessage="Please enter a valid date." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator" 
                        Operator="DataTypeCheck" Type="Date" 
                        ValidationGroup="SaveProfile" />
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
                <td class="contentCol" style="height: 40px">
                    <asp:Label ID="lblContactNo" runat="server" />
                    <asp:TextBox ID="txtContactNo" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="ContactNo_rqdValidator" runat="server" 
                        ControlToValidate="txtContactNo" 
                        ErrorMessage="Contact Number is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
                </td>
                <td class="contentCol">
                    <asp:Label ID="lblResidenceState" runat="server" />
                    <asp:TextBox ID="txtResidenceState" runat="server" 
                        CssClass="SizeEditTextbox" Visible="false" />
                    <asp:RequiredFieldValidator 
                        ID="ResidenceState_rqdValidator" runat="server" 
                        ControlToValidate="txtResidenceState" 
                        ErrorMessage="Residence State is required." 
                        Display="Dynamic" 
                        ForeColor="Red" 
                        CssClass="rqdValidator"
                        ValidationGroup="SaveProfile" />
                </td>
                <td class="blankCol"></td>
            </tr>
            <tr><td><br /></td></tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="EditBtn_UP" runat="server" 
                    Text="Edit Info" CssClass="btn btn-success"
                    style="width: 150px;"
                    OnClick="EditBtn_UP_Click"
                    CausesValidation="false" />
                <asp:Button ID="SaveBtn_UP" runat="server" 
                    Text="Save Now" CssClass="btn btn-primary" 
                    style="width: 150px;" Visible="false" 
                    OnClick="SaveBtn_UP_Click"
                    CausesValidation="true" />
            </td>
        </tr>
        <tr>
            <td class="trMarginBottom20"></td>
        </tr>
    </table>
    </div>
    </center>
    <br /><br /><br />
</asp:Content>
