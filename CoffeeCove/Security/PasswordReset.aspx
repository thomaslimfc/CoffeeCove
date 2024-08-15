<%@ Page Title="Reset Password" Language="C#" 
    MasterPageFile="../Master/Customer.Master" 
    AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" 
    Inherits="CoffeeCove.Security.PasswordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/Security.css" rel="stylesheet" />
    <br /><br /><br /><br /><br /><br /><br /><br />

    <div id="signIn_container" class="sign_container">
        <table>
            <tr>
                <td id="passwordReset_td">
                    <h2 style="font-size: 24px; font-weight: bold">
                        Reset Password</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="passwordIcon" src="../img/lock_icon.png" 
                                        alt="Enter your new password" />
                                </td>
                                <td>
                                    <asp:TextBox ID="Password_PR" CssClass="securityInput" 
                                        runat="server" placeholder="New Password" 
                                        title="New Password" TextMode="Password" 
                                        AutoPostBack="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignLeft" colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="Password_PR_rqdValidator" runat="server" 
                                        ControlToValidate="Password_PR" 
                                        ErrorMessage="Password is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="rqdValidator" />
                                    <asp:RegularExpressionValidator 
                                        ID="Password_PR_regexValidator" 
                                        runat="server" 
                                        ControlToValidate="Password_PR" 
                                        ErrorMessage="Must contain >10 letters, numbers, and symbols." 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        CssClass="rqdValidator" 
                                        ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{10,}$" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="trMarginBottom20"></td>
            </tr>
            <tr>
                <td id="password_td">
                    <div class="input_div">
                        <table>
                            <tr>
                                <td>
                                    <img class="passwordIcon" src="../img/lock_icon.png" 
                                        alt="Confirm your new password" />
                                </td>
                                <td>
                                    <asp:TextBox ID="PasswordConfirm_PR" CssClass="securityInput" 
                                        runat="server" placeholder="Confirm New Password" 
                                        title="Confirm New Password" TextMode="Password"
                                        AutoPostBack="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignLeft" colspan="2">
                                    <asp:RequiredFieldValidator 
                                        ID="PasswordConfirm_PR_rqdValidator" runat="server" 
                                        ControlToValidate="PasswordConfirm_PR" 
                                        ErrorMessage="Password Confirmation is required." 
                                        Display="Dynamic" ForeColor="Red" 
                                        CssClass="rqdValidator" />
                                    <asp:CompareValidator 
                                        ID="PasswordConfirm_PR_compareValidator" 
                                        runat="server" 
                                        ControlToValidate="PasswordConfirm_PR" 
                                        ControlToCompare="Password_PR" 
                                        ErrorMessage="Passwords do not match." 
                                        Display="Dynamic" 
                                        ForeColor="Red" 
                                        CssClass="rqdValidator" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="trMarginBottom20"></td>
            </tr>

            <tr>
                <td id="resetPassword_td">
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<link href="../CSS/Security.css" rel="stylesheet" />
<script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
<script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init></script>
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
</style>

<!-- Password Toggle Tool -->


<!--- reCAPTCHA -->
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
<br /><br /><br /><br /><br /><br />



<div class="max-w-md mx-auto p-6 bg-card rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-foreground mb-4">Sign Up</h2>
    
    <!-- Password -->
    <div class="mb-4">
        <label for="password" class="block text-sm text-muted-foreground">Password</label>
        <asp:TextBox ID="Password_SU" 
            CssClass="mt-1 p-2 border border-border rounded w-full" 
            runat="server" 
            placeholder="**********" 
            title="Password"></asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="Password_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="Password_SU" 
            ErrorMessage="Password is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator" />
        <asp:RegularExpressionValidator 
            ID="Password_SU_regexValidator" 
            runat="server" 
            ControlToValidate="Password_SU" 
            ErrorMessage="Must contain >10 letters, numbers, and symbols." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" 
            ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{10,}$" />
    </div>
    
    <!-- Re-enter Password -->
    <div class="mb-4">
        <label for="reenter-password" class="block text-sm text-muted-foreground">Re-enter Password</label>

        <asp:TextBox ID="PasswordReenter_SU" 
            CssClass="mt-1 p-2 border border-border rounded w-full" 
            runat="server" 
            placeholder="**********" 
            title="Re-enter Password"></asp:TextBox>
        <asp:RequiredFieldValidator 
            ID="PasswordReenter_SU_rqdValidator" 
            runat="server" 
            ControlToValidate="PasswordReenter_SU" 
            ErrorMessage="Password Re-enter is required." 
            Display="Dynamic" ForeColor="Red" 
            CssClass="rqdValidator" />
        <asp:CompareValidator 
            ID="PasswordReenter_SU_compareValidator" 
            runat="server" 
            ControlToValidate="PasswordReenter_SU" 
            ControlToCompare="Password_SU" 
            ErrorMessage="Passwords do not match." 
            Display="Dynamic" 
            ForeColor="Red" 
            CssClass="rqdValidator" />

    <!-- Reset Password -->
        <asp:Button ID="ResetPasswordBtn_PR" 
            runat="server" Text="Reset Password"
            CssClass="securityPrimaryBtn"
            CausesValidation="true" 
            OnClick="ResetPasswordBtn_PR_Click"/>
        <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>

    <div class="flex justify-center space-x-4">
        <asp:Button ID="SignInUsernameBtn_SU" 
            runat="server" 
            CssClass="bg-secondary text-secondary-foreground p-2 rounded w-full" 
            Text="Sign In with Username"
            style="cursor: pointer;"
            OnClick="SignUpUsernameBtn_SU_Click"/>

        <asp:Button ID="SignUpEmailBtn_SU" 
            runat="server" 
            CssClass="bg-secondary text-secondary-foreground p-2 rounded w-full" 
            Text="Sign In with Email"
            style="cursor: pointer;"
            OnClick="SignUpEmailBtn_SU_Click"/>
    </div>
</div>
<br />
<br />
</asp:Content>
