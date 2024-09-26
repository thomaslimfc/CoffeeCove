<%@ Page Title="User Profile" Language="C#" 
    MasterPageFile="../Master/Customer2.Master" 
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" 
    Inherits="CoffeeCove.UserManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../CSS/UserManagement.css" rel="stylesheet" />
    <link href="../CSS/Security.css" rel="stylesheet" />
    <script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
    <script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
    <br /><br /><br /><br />

    <center>
        <div class="profileContainer">
        <br /> 
        <table style="user-select: none; margin-left: 25px;">
            <tr>
                <!-- LEFT PORTION -->
                <td style="width: 400px">
                    <center>
                    <div>
                        <table>
                            <tr>
                                <td class="blankCol" colspan="4">
                                    <center>
                                        <div style="width: 200px">
                                            <asp:Image ID="imgProfilePicture" runat="server" 
                                                Width="200px" Height="200px"
                                                style="margin-right: 15px; border-radius: 20px;" />
                                        </div>
                                    </center>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fuProfilePicture" runat="server"
                                        style="margin-bottom: 10px" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                    <asp:Button ID="EditPictureBtn_UP"
                                        runat="server"
                                        Text="Edit Picture"
                                        style="margin-top: 10px"
                                        width="150px"
                                        CssClass="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                                        OnClick="EditPictureBtn_UP_Click"
                                        Visible="false"/>
                                    <asp:Button ID="UploadBackBtn_UP" 
                                        runat="server" 
                                        Text="Back" 
                                        style="margin-top: 10px"
                                        width="150px"
                                        CssClass="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                                        OnClick="UploadBackBtn_UP_Click"
                                        Visible="false"/>
                                    <asp:Button ID="UploadPictureBtn_UP" 
                                        runat="server" 
                                        Text="Upload Picture" 
                                        style="margin-top: 10px"
                                        width="150px"
                                        CssClass="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                                        OnClick="UploadPictureBtn_UP_Click"
                                        Visible="false"/>
                                    <asp:Button ID="RemovePictureBtn_UP"
                                        runat="server"
                                        Text="Remove Picture"
                                        style="margin-top: 10px"
                                        width="150px"
                                        CssClass="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                                        OnClick="RemovePictureBtn_UP_Click"
                                        Visible="false"/>
                                    <br />
                                    <asp:Label ID="lblUploadMessage" runat="server"
                                        CssClass="text-success" Visible="false"></asp:Label>
                                    <asp:Label ID="lblRemoveMessage" runat="server"
                                        CssClass="text-success" Visible="false"></asp:Label>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </div>
                    </center>
                </td>
                <!-- RIGHT PORTION -->
                <td style="width: 1200px;">
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
                            <td class="contentCol" style="margin-right: 50px;">Username</td>
                            <td class="contentCol">Gender</td>
                            <td class="blankCol"></td>
                        </tr>
                        <tr class="evenRow">
                            <td class="blankCol"></td>
                            <td class="contentCol" style="height: 40px">
                                <asp:Label ID="lblUsername" 
                                    runat="server"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450"/>
                                <asp:TextBox ID="txtUsername" runat="server" 
                                    CssClass="mt-1 p-2 border border-border rounded w-full" 
                                    Visible="false"
                                    placeholder="desmundchau7668" />
                                <asp:RequiredFieldValidator 
                                    ID="txtUsername_rqdValidator" runat="server" 
                                    ControlToValidate="txtUsername" 
                                    ErrorMessage="Username is required." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator"
                                    ValidationGroup="SaveProfile" />
                                <asp:RegularExpressionValidator 
                                    ID="txtUsername_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="txtUsername" 
                                    ErrorMessage="Must contain >8 letters, numbers, and underscores only." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^[a-zA-Z0-9_]{8,}$" />
                            </td>
                            <td class="contentCol">
                                <asp:Label ID="lblGender" runat="server" 
                                    CssClass="block text-sm text-muted-foreground"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450"/>
                                
                                <asp:DropDownList ID="txtGender" runat="server"
                                    CssClass="mt-1 p-2 border border-border rounded w-full"
                                    Visible="false">
                                    <asp:ListItem Text="~ Select your gender ~" Value="" />
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                    ID="txtGender_rqdValidator" runat="server" 
                                    ControlToValidate="txtGender" 
                                    InitialValue="" 
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
                                <asp:Label ID="lblEmail" runat="server"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450"/>
                                <asp:Label ID="lblEmailDisplayMode" runat="server" />
                            </td>
                            <td class="contentCol">
                                <!-- Date of Birth -->
                                <asp:Label ID="lblDOB" runat="server" 
                                    class="block text-sm text-muted-foreground"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450; padding-top: 15px"/>
                                <div class="mb-4">
                                    <asp:TextBox ID="txtDOB" 
                                        CssClass="mt-1 p-2 border border-border rounded w-full" 
                                        runat="server" 
                                        TextMode="Date"
                                        placeholder="YYYY/MM/DD"
                                        Visible="false" />
                                    <asp:CompareValidator 
                                        ID="DOB_compareValidator" runat="server" 
                                        ControlToValidate="txtDOB" 
                                        ErrorMessage="Please enter a valid date." 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        CssClass="rqdValidator" 
                                        Operator="DataTypeCheck" Type="Date" 
                                        ValidationGroup="SaveProfile" />
                                </div>
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
                                <asp:Label ID="lblContactNo" runat="server"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450"/>
                                <asp:TextBox ID="txtContactNo" runat="server" 
                                    CssClass="mt-1 p-2 border border-border rounded w-full"  
                                    Visible="false"
                                    placeholder="012-3456789" />
                                <asp:RequiredFieldValidator 
                                    ID="ContactNo_rqdValidator" runat="server" 
                                    ControlToValidate="txtContactNo" 
                                    ErrorMessage="Contact Number is required." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator"
                                    ValidationGroup="SaveProfile" />
                                <asp:RegularExpressionValidator 
                                    ID="txtContactNo_regexValidator" 
                                    runat="server" 
                                    ControlToValidate="txtContactNo" 
                                    ErrorMessage="Must start with 0 in front with '-'." 
                                    Display="Dynamic" 
                                    ForeColor="Red" 
                                    CssClass="rqdValidator" 
                                    ValidationExpression="^01[0-9]-[0-9]{7,9}$" />
                            </td>
                            <td class="contentCol">
                                <asp:Label ID="lblResidenceState" runat="server" CssClass="block text-sm text-muted-foreground"
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                        color: black; font-size: 12pt; font-weight: 450" />
                                
                                <asp:DropDownList ID="txtResidenceState" runat="server" 
                                    CssClass="mt-1 p-2 border border-border rounded w-full" 
                                    style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                        color: black; font-size: 12pt; font-weight: 450"                               
                                    ForeColor="Black" Visible="false">
                                    <asp:ListItem Text="~ Select a location ~" Value=""
                                        style="font-family: 'Work Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';
                                            color: black; font-size: 12pt; font-weight: 450"/>
                                    <asp:ListItem>Batu Feringghi</asp:ListItem>
                                    <asp:ListItem>Tanjung Bungah</asp:ListItem>
                                    <asp:ListItem>Tanjung Tokong</asp:ListItem>
                                    <asp:ListItem>Pulau Tikus</asp:ListItem>
                                    <asp:ListItem>George Town</asp:ListItem>
                                    <asp:ListItem>Batu Lancang</asp:ListItem>
                                    <asp:ListItem>Air Itam</asp:ListItem>
                                    <asp:ListItem>Paya Terubong</asp:ListItem>
                                    <asp:ListItem>Jelutong</asp:ListItem>
                                    <asp:ListItem>Gelugor</asp:ListItem>
                                    <asp:ListItem>Batu Maung</asp:ListItem>
                                    <asp:ListItem>Bayan Lepas</asp:ListItem>
                                </asp:DropDownList>
                            
                                <asp:RequiredFieldValidator 
                                    ID="ResidenceState_rqdValidator" runat="server" 
                                    ControlToValidate="txtResidenceState" 
                                    InitialValue="" 
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
                            <td class="trMarginBottom20"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="relative mb-4">
                        <asp:Button ID="EditBtn_UP" 
                            runat="server" 
                            class="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                            style="cursor: pointer; width: 250px;"
                            Text="Edit Info"
                            CausesValidation="false" 
                            OnClick="EditBtn_UP_Click"/>
                        <asp:Button ID="SaveBtn_UP"
                            runat="server"
                            class="w-full bg-primary text-primary-foreground p-2 rounded-md hover:bg-primary/80"
                            style="cursor: pointer; width: 250px;"
                            Text="Save Now"
                            CausesValidation="true"
                            OnClick="SaveBtn_UP_Click"/>
                    </div>               
                </td>
            </tr>
        </table>
        </div>
    </center>
    <br /><br /><br />

<script type="text/javascript">
    window.tailwind.config = {
        darkMode: ['class'],
        theme: {
            extend: {
                colors: {
                    border: 'hsl(var(--border))',
                    input: 'hsl(var(--input))',
                    ring: 'hsl(var(--ring))',
                    background: 'hsl(var(--background))',
                    foreground: 'hsl(var(--foreground))',
                    primary: {
                        DEFAULT: 'hsl(var(--primary))',
                        foreground: 'hsl(var(--primary-foreground))'
                    },
                    secondary: {
                        DEFAULT: 'hsl(var(--secondary))',
                        foreground: 'hsl(var(--secondary-foreground))'
                    },
                    destructive: {
                        DEFAULT: 'hsl(var(--destructive))',
                        foreground: 'hsl(var(--destructive-foreground))'
                    },
                    muted: {
                        DEFAULT: 'hsl(var(--muted))',
                        foreground: 'hsl(var(--muted-foreground))'
                    },
                    accent: {
                        DEFAULT: 'hsl(var(--accent))',
                        foreground: 'hsl(var(--accent-foreground))'
                    },
                    popover: {
                        DEFAULT: 'hsl(var(--popover))',
                        foreground: 'hsl(var(--popover-foreground))'
                    },
                    card: {
                        DEFAULT: 'hsl(var(--card))',
                        foreground: 'hsl(var(--card-foreground))'
                    },
                },
            }
        }
    }
</script>
    <style type="text/tailwindcss">
        @layer base {
            :root {
                --background: 0 0% 100%;
                --foreground: 240 10% 3.9%;
                --card: 0 0% 100%;
                --card-foreground: 240 10% 3.9%;
                --popover: 0 0% 100%;
                --popover-foreground: 240 10% 3.9%;
                --primary: 240 5.9% 10%;
                --primary-foreground: 0 0% 98%;
                --secondary: 240 4.8% 95.9%;
                --secondary-foreground: 240 5.9% 10%;
                --muted: 240 4.8% 95.9%;
                --muted-foreground: 240 3.8% 46.1%;
                --accent: 240 4.8% 95.9%;
                --accent-foreground: 240 5.9% 10%;
                --destructive: 0 84.2% 60.2%;
                --destructive-foreground: 0 0% 98%;
                --border: 240 5.9% 90%;
                --input: 240 5.9% 90%;
                --ring: 240 5.9% 10%;
                --radius: 0.5rem;
            }
            .dark {
                --background: 240 10% 3.9%;
                --foreground: 0 0% 98%;
                --card: 240 10% 3.9%;
                --card-foreground: 0 0% 98%;
                --popover: 240 10% 3.9%;
                --popover-foreground: 0 0% 98%;
                --primary: 0 0% 98%;
                --primary-foreground: 240 5.9% 10%;
                --secondary: 240 3.7% 15.9%;
                --secondary-foreground: 0 0% 98%;
                --muted: 240 3.7% 15.9%;
                --muted-foreground: 240 5% 64.9%;
                --accent: 240 3.7% 15.9%;
                --accent-foreground: 0 0% 98%;
                --destructive: 0 62.8% 30.6%;
                --destructive-foreground: 0 0% 98%;
                --border: 240 3.7% 15.9%;
                --input: 240 3.7% 15.9%;
                --ring: 240 4.9% 83.9%;
            }
        }

        .hidden {
            display: none;
        }
    </style>
</asp:Content>
