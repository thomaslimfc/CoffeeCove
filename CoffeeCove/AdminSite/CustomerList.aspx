<%@ Page Title="Customer List (Admin)" Language="C#"
    MasterPageFile="../Master/Admin.Master"
    AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs"
    Inherits="CoffeeCove.AdminSite.CustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Vendor CSS Files -->
    <link href="https://cdn.datatables.net/1.13.4/css/dataTables.bootstrap5.min.css" rel="stylesheet">

    <!-- Required JavaScript Files -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js"></script>

    <main id="main" class="main">
        <div class="pagetitle" style="color:#fff">
            <br />
            <h3>Customer List</h3>
        </div>
        <section class="section">
            <div class="row" style="margin-top: 2%;">
                <div class="col-lg-12">
                    <div class="card">

                        <!-- Search input for filtering -->
                        <!-- <input type="text" id="tableSearch" placeholder="Search here..." class="form-control mb-3"> -->


                        <div class="card-body">
                            <h5 class="card-title">User deletion</h5>
                            <div class="row g-3 ">
                                <div class="col-8">
                                    <label for="first-name" class="block text-sm text-muted-foreground"
                                        style="padding-bottom: 3px;">
                                        Username</label>
                                    <asp:TextBox ID="Username_SI"
                                        CssClass="form-control mb-3"
                                        Style="width: 300px"
                                        runat="server"
                                        placeholder="desmundchau7668"
                                        title="Username"
                                        AutoPostBack="false">
                                    </asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator
                                        ID="Username_SI_rqdValidator" runat="server"
                                        ControlToValidate="Username_SI"
                                        ErrorMessage="Username is required."
                                        Display="Dynamic" ForeColor="Red"
                                        CssClass="rqdValidator" />
                                    <asp:RegularExpressionValidator
                                        ID="Username_SI_regexValidator"
                                        runat="server"
                                        ControlToValidate="Username_SI"
                                        ErrorMessage="Must contain >8 letters and numbers only."
                                        Display="Dynamic"
                                        ForeColor="Red"
                                        CssClass="rqdValidator"
                                        ValidationExpression="^[a-zA-Z0-9]{8,}$" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="card-body">
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



                <br />
                <br />
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">User Profile Maintenance</h5>

                            ss
                        </div>
                    </div>
                </div>
            </div>
   

            <br />
            <br />


        </section>
    </main>

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
</asp:Content>
