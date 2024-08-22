﻿<%@ Page Title="Customer List (Admin)" Language="C#"
    MasterPageFile="../Master/Admin.Master"
    AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs"
    Inherits="CoffeeCove.AdminSite.CustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/Security.css" rel="stylesheet" />

    <!-- Vendor CSS Files -->
    <link href="https://cdn.datatables.net/1.13.4/css/dataTables.bootstrap5.min.css" rel="stylesheet">

    <!-- Required JavaScript Files -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script> <!-- Add this line -->

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.gstatic.com" rel="preconnect">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- Vendor CSS Files -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
    <link href="assets/vendor/quill/quill.snow.css" rel="stylesheet">
    <link href="assets/vendor/quill/quill.bubble.css" rel="stylesheet">
    <link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">
    <link href="assets/vendor/simple-datatables/style.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <link href="assets/css/style.css" rel="stylesheet">

<main id="main" class="main" style="margin-right:1%">
    <section class="section">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>User Management</h3>
            <br />
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <br />
                        <ul class="nav nav-tabs nav-tabs-bordered d-flex" id="borderedTabJustified" role="tablist">
                            <li class="nav-item flex-fill" role="presentation">
                                <button class="nav-link w-100 active" id="home-tab" 
                                    data-bs-toggle="tab" data-bs-target="#bordered-justified-home" 
                                    type="button" role="tab" aria-controls="home" 
                                    aria-selected="true">Admin Account Registration</button>
                            </li>
                            <li class="nav-item flex-fill" role="presentation">
                                <button class="nav-link w-100" id="profile-tab" 
                                    data-bs-toggle="tab" data-bs-target="#bordered-justified-profile" 
                                    type="button" role="tab" aria-controls="profile" 
                                    aria-selected="false">Admin Profile Maintenance</button>
                            </li>
                            <li class="nav-item flex-fill" role="presentation">
                                <button class="nav-link w-100" id="contact-tab" 
                                    data-bs-toggle="tab" data-bs-target="#bordered-justified-contact" 
                                    type="button" role="tab" aria-controls="contact" 
                                    aria-selected="false">User Account Deletion</button>
                            </li>
                        </ul>
                        <div class="tab-content pt-2" id="borderedTabJustifiedContent">
                            <!-- Admin Account Registration -->
                            <div class="tab-pane fade show active" id="bordered-justified-home" role="tabpanel" aria-labelledby="home-tab">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <h5 class="card-title">Register Admin Profile Info</h5>
                                            <div class="row g-3">
                                                <div class="col-8">
                                                    <label for="first-name" 
                                                        class="block text-sm text-muted-foreground">
                                                        Username (Admin)
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="UsernameRegister_CL" 
                                                        ValidationGroup="RegisterAccount" 
                                                        CssClass="mt-1 p-2 border border-border rounded w-full"
                                                        style="width: 300px" 
                                                        runat="server" 
                                                        placeholder="desmundchau7668" 
                                                        title="Username" 
                                                        AutoPostBack="false"></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="UsernameRegister_CL_rqdValidator" runat="server" ControlToValidate="UsernameRegister_CL" ErrorMessage="Username is required." Display="Dynamic" ForeColor="Red" CssClass="rqdValidator"/>
                                                        <asp:RegularExpressionValidator ID="UsernameRegister_CL_regexValidator" runat="server" ControlToValidate="UsernameRegister_CL" ErrorMessage="Must contain >8 letters and numbers only." Display="Dynamic" ForeColor="Red" CssClass="rqdValidator" ValidationExpression="^[a-zA-Z0-9]{8,}$" />
                                                    </div>

                                                    <br />

                                                    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">
                                                        Branch Location
                                                    </label>
                                                    <br />
                                                    <asp:Label ID="lblBranchRegister_CL" runat="server" CssClass="block text-sm text-muted-foreground" />
                                                    
                                                    <asp:DropDownList ID="BranchRegister_CL" runat="server" 
                                                        style="width: 300px"
                                                        CssClass="mt-1 p-2 border border-border rounded w-full">
                                                        <asp:ListItem Text="~ Select a location ~" Value="" />
                                                        <asp:ListItem>Tanjung Tokong</asp:ListItem>
                                                        <asp:ListItem>George Town</asp:ListItem>
                                                        <asp:ListItem>Jelutong</asp:ListItem>
                                                        <asp:ListItem>Butterworth</asp:ListItem>
                                                        <asp:ListItem>Bayan Lepas</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <br />
                                                    
                                                    <asp:RequiredFieldValidator 
                                                        ID="BranchRegister_CL_rqdValidator" runat="server" 
                                                        ControlToValidate="BranchRegister_CL" 
                                                        InitialValue="" 
                                                        ErrorMessage="Branch is required." 
                                                        Display="Dynamic"
                                                        ForeColor="Red"
                                                        CssClass="rqdValidator"
                                                        ValidationGroup="RegisterAccount" />

                                                    <br />
                                                    <br />

                                                    <asp:Button ID="RegisterAcc_CL" 
                                                        runat="server" 
                                                        Text="Save Changes" 
                                                        style="width: 300px"
                                                        CssClass="btn btn-secondary"
                                                        ValidationGroup="RegisterAccount"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                username
                                passworrd
                                gender
                                branch


                            
                            
                            
                            </div>

                            <!-- Admin Profile Maintenance -->
                            <div class="tab-pane fade" id="bordered-justified-profile" role="tabpanel" aria-labelledby="profile-tab">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <h5 class="card-title">Edit Admin Profile Info</h5>
                                            <div class="row g-3">
                                                <div class="col-8">
                                                    <label for="first-name" class="block text-sm text-muted-foreground">
                                                        Username (Admin)
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="UsernameEdit_CL" 
                                                        CssClass="mt-1 p-2 border border-border rounded w-full"
                                                        style="width: 300px" 
                                                        runat="server" 
                                                        placeholder="desmundchau7668" 
                                                        title="Username" 
                                                        AutoPostBack="false"
                                                        ValidationGroup="EditProfile"></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator 
                                                            ID="UsernameEdit_CL_rqdValidator" 
                                                            runat="server" 
                                                            ControlToValidate="UsernameEdit_CL" 
                                                            ErrorMessage="Username is required." 
                                                            Display="Dynamic" 
                                                            ForeColor="Red" 
                                                            CssClass="rqdValidator" />
                                                        <asp:RegularExpressionValidator 
                                                            ID="UsernameEdit_CL_regexValidator" 
                                                            runat="server" 
                                                            ControlToValidate="UsernameEdit_CL" 
                                                            ErrorMessage="Must contain >8 letters and numbers only." 
                                                            Display="Dynamic" 
                                                            ForeColor="Red" 
                                                            CssClass="rqdValidator" 
                                                            ValidationExpression="^[a-zA-Z0-9]{8,}$" />
                                                    </div>
                                                    
                                                    <br />


                                                    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">
                                                        Gender
                                                    </label>

                                                    <asp:DropDownList ID="txtGender" runat="server"
                                                        CssClass="block text-sm text-muted-foreground"
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






                                                    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">
                                                        Branch Location
                                                    </label>
                                                    <br />
                                                    <asp:Label ID="lblBranch" runat="server" CssClass="block text-sm text-muted-foreground" />
                                                    
                                                    <asp:DropDownList ID="txtBranch" runat="server" 
                                                        style="width: 300px"
                                                        CssClass="mt-1 p-2 border border-border rounded w-full">
                                                        <asp:ListItem Text="~ Select a location ~" Value="" />
                                                        <asp:ListItem>Tanjung Tokong</asp:ListItem>
                                                        <asp:ListItem>George Town</asp:ListItem>
                                                        <asp:ListItem>Jelutong</asp:ListItem>
                                                        <asp:ListItem>Butterworth</asp:ListItem>
                                                        <asp:ListItem>Bayan Lepas</asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                    <asp:RequiredFieldValidator 
                                                        ID="txtBranch_rqdValidator" runat="server" 
                                                        ControlToValidate="txtBranch" 
                                                        InitialValue="" 
                                                        ErrorMessage="Branch is required." 
                                                        Display="Dynamic"
                                                        ForeColor="Red"
                                                        CssClass="rqdValidator"
                                                        ValidationGroup="EditProfile" />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <asp:Button ID="SaveChangesBtn_CL" 
                                                runat="server" 
                                                Text="Save Changes" 
                                                style="width: 300px"
                                                CssClass="btn btn-secondary"
                                                ValidationGroup="EditProfile"/>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <!-- User Account Deletion -->
                            <div class="tab-pane fade" id="bordered-justified-contact" role="tabpanel" aria-labelledby="contact-tab">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <h5 class="card-title">Delete User Account</h5>
                                            <div class="row g-3">
                                                <div class="col-8">
                                                    <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">
                                                        Username (Admin / Customer)
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="UsernameDeletion_CL" 
                                                        CssClass="mt-1 p-2 border border-border rounded w-full"
                                                        style="width: 300px" 
                                                        runat="server" 
                                                        placeholder="desmundchau7668" 
                                                        title="Username" 
                                                        AutoPostBack="false"
                                                        ValidationGroup="DeleteAccount"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UsernameDeletion_CL_rqdValidator" 
                                                        runat="server" 
                                                        ControlToValidate="UsernameDeletion_CL" 
                                                        ErrorMessage="Username is required." 
                                                        Display="Dynamic" 
                                                        ForeColor="Red" 
                                                        CssClass="rqdValidator" />
                                                    <asp:RegularExpressionValidator ID="UsernameDeletion_CL_regexValidator" 
                                                        runat="server" 
                                                        ControlToValidate="UsernameDeletion_CL" 
                                                        ErrorMessage="Must contain >8 letters and numbers only." 
                                                        Display="Dynamic" 
                                                        ForeColor="Red" 
                                                        CssClass="rqdValidator" 
                                                        ValidationExpression="^[a-zA-Z0-9]{8,}$" />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <asp:Button ID="DeleteAccBtn_CL" 
                                                runat="server" 
                                                Text="Delete Account" 
                                                style="width: 300px"
                                                CssClass="btn btn-secondary"
                                                ValidationGroup="DeleteAccount"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div><!-- End Bordered Tabs Justified -->
                    </div>
                </div>
            </div>
        
    <!-- Customer List -->
    
            <div class="col-lg-12">
                <div class="pagetitle" style="color:#fff">
                    <br />
                    <h3>Customer List</h3>
                    <br>
                </div>
                <!-- DataTable -->
                <table class="table datatable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Username</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email Address</th>
                            <th>Contact No</th>
                            <th>Gender</th>
                            <th>Date of Birth</th>
                            <th>State</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCustomerList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("cusID") %></td>
                                    <td><%# Eval("Username") %></td>
                                    <td><%# Eval("FirstName") %></td>
                                    <td><%# Eval("LastName") %></td>
                                    <td><%# Eval("EmailAddress") %></td>
                                    <td><%# Eval("ContactNo") %></td>
                                    <td><%# Eval("Gender") %></td>
                                    <td><%# Eval("DateOfBirth", "{0:yyyy/MM/dd}") %></td>
                                    <td><%# Eval("ResidenceState") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <br />
        <br />
    </section>
</main>

    <style>
        #DataTables_Table_0_length > label,
        #DataTables_Table_0_filter > label,
        #DataTables_Table_0_info {
            color: white;
        }
    </style>

    <!-- Sorting, Filtering, and Pagination Initialization -->
    <script>
        $(document).ready(function () {
            var table = $('.datatable').DataTable({
                "ordering": true,
                "searching": true,
                "paging": true,
                "pageLength": 10,
                "lengthChange": true,
                "language": {
                    "search": "Filter records:"
                },
                "columnDefs": [{
                    "targets": 'nosort',  // Disable sorting for specific columns when admin needs it
                    "orderable": false
                }]
            });

            // Search-Box CSS
            $('.dataTables_filter input[type="search"]').css({
                "width": "300px",
                "display": "inline-block",
                "margin-bottom": "15px"
            });

            // Add margin-top to "Showing 1 to X of X entries" text
            $('.dataTables_info').css({
                "margin-top": "5px",
                "color:": "white"
            });

            // Pagination Button Margin
            $('.dataTables_paginate').css({
                "margin-top": "30px"
            });
        });
    </script>

    <style>
        .datatable-search-box {
            width: 300px;
            display: inline-block;
            margin-bottom: 15px;
            padding: 2px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
    </style>

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

    <!--
    <script src="https://cdn.tailwindcss.com?plugins=forms,typography"></script>
    <script src="https://unpkg.com/unlazy@0.11.3/dist/unlazy.with-hashing.iife.js" defer init=""></script>
     -->
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

    <!-- Tab Switching -->

    <!-- Vendor JS Files -->
    <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/chart.js/chart.umd.js"></script>
    <script src="assets/vendor/echarts/echarts.min.js"></script>
    <script src="assets/vendor/quill/quill.js"></script>
    <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="assets/vendor/tinymce/tinymce.min.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>
</asp:Content>
