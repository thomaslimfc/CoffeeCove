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
        <section class="section" style="margin-left: 20px;">
            <h3>Customer List</h3>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="card" style="width: 1300px;">
                        <br />
                        <!-- Search input for filtering -->
                        <!-- <input type="text" id="tableSearch" placeholder="Search here..." class="form-control mb-3"> -->
                        <div class="card-body">
                            <!-- DataTable -->
                            <table class="table datatable">
                                <thead>
                                    <tr>
                                        <th>Customer ID</th>
                                        <th>Username</th>
                                        <th>First Name</th>
                                        <th>Last Name</th>
                                        <th>Email Address</th>
                                        <th>Contact Number</th>
                                        <th>Gender</th>
                                        <th>Date of Birth</th>
                                        <th>Residence State</th>
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
                </div>
            </div>

            <br /><br />
            <div class="row">
                <h3>User Profile Maintenance</h3>
                <div class="col-lg-12">
                    <div class="card" style="width: 1300px;">
                        <div class="card-body">
                            ss
                        </div>
                    </div>
                </div>
            </div>

            <br /><br />
            <div class="row">
                <h3>User Deletion</h3>
                <div class="col-lg-12">
                    <div class="card" style="width: 1300px;">
                        <div class="card-body">
                            <br />
                            <!-- Username -->
                            <div class="mb-4">
                                <label for="first-name" class="block text-sm text-muted-foreground" style="padding-bottom: 3px;">Username:</label>
                                <asp:TextBox ID="Username_SI" 
                                    CssClass="datatable-search--box" 
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
                "margin-top": "5px"
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
</asp:Content>
