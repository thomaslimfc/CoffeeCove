<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeCove.AdminSite.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main" style="margin-right: 1%">

        <div class="pagetitle" style="color: #fff">
            <br />
            <h3 style="font-weight:600">Dashboard</h3>
        </div>

        <div class="section dashboard">
            <div class="row" style="margin-top: 2%;">
                <div>
                    <div class="row">
                        <h5 style="color:#fff">Todays</h5>
                        <!-- Sales Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card sales-card">
                                <div class="card-body">
                                    <h5 class="card-title">Sales <span>| Today</span></h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-cart"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>145</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Revenue Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card revenue-card">
                                <div class="card-body">
                                    <h5 class="card-title">Revenue <span>| Today</span></h5>

                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-currency-dollar"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>RM 3,264</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Customers Record -->
                        <div class="col-xxl-4 col-xl-5">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Customers <span>| Today</span></h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-people"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>1244</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h5 style="color:#fff">Totals</h5>

                        <!-- Category Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card sales-card">
                                <div class="card-body">
                                    <h5 class="card-title">Categories</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-tags-fill"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>7</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Product Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card revenue-card">
                                <div class="card-body">
                                    <h5 class="card-title">Products</h5>

                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-archive-fill"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>30</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Total Order Record -->
                        <div class="col-xxl-4 col-xl-5">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Total Orders</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-journal-text"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>51</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Delivered Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card sales-card">
                                <div class="card-body">
                                    <h5 class="card-title">Delivered Meals</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-truck"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>45</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Pending Record -->
                        <div class="col-xxl-4 col-md-5">
                            <div class="card info-card revenue-card">
                                <div class="card-body">
                                    <h5 class="card-title">Pending Meals</h5>

                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-clock-history"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>6</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Feedback Record -->
                        <div class="col-xxl-4 col-xl-5">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Feedbacks</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-chat-left-heart-fill"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>3</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                     

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Vendor JS Files -->
    <script src="../Content/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="../Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../Content/vendor/chart.js/chart.umd.js"></script>
    <script src="../Content/vendor/echarts/echarts.min.js"></script>
    <script src="../Content/vendor/quill/quill.js"></script>
    <script src="../Content/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="../Content/vendor/tinymce/tinymce.min.js"></script>
    <script src="../Content/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="../Content/js/main.js"></script>
</asp:Content>
