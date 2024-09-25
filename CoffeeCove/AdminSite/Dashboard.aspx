<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeCove.AdminSite.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main" style="margin-right: 1%">

        <div class="pagetitle" style="color: #fff">
            <br />
            <h3 style="font-weight: 600">Dashboard</h3>
        </div>

        <div class="section dashboard">
            <div class="row" style="margin-top: 2%;">
                <div>
                    <div class="row">
                        <h4 style="color: #fff">Todays</h4>
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
                                            <h6>
                                                <asp:Label ID="lblSalesToday" runat="server" Text="0"></asp:Label></h6>
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
                                            <h6>
                                                <asp:Label ID="lblRevenueToday" runat="server" Text="RM 0.00"></asp:Label></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Order Record -->
                        <div class="col-xxl-4 col-xl-5">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Orders <span>| Today</span></h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-people"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6>
                                                <asp:Label ID="lblOrdersToday" runat="server" Text="0"></asp:Label></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h4 style="color: #fff">Totals</h4>

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
                                            <h6>
                                                <asp:Label ID="lblTotalCategory" runat="server" Text="0"></asp:Label></h6>
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
                                            <h6>
                                                <asp:Label ID="lblTotalProduct" runat="server" Text="0"></asp:Label></h6>
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
                                            <h6>
                                                <asp:Label ID="lblTotalFeedback" runat="server" Text="0"></asp:Label></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
     <div class="card-body pb-0">
         <div id="chart_div" style="width: 100%; height: 500px;"></div>
     </div>
 </div>

 <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
 <script type="text/javascript">
     google.charts.load("current", { packages: ["corechart"] });
     google.charts.setOnLoadCallback(drawChart);

     function drawChart() {
         var data = google.visualization.arrayToDataTable(chartData);

         var options = {
             title: 'Monthly Revenue',
             legend: { position: 'none' },
             hAxis: { title: 'Month' },
             vAxis: { title: 'Total Revenue', format: 'RM#,##0.00' },
         };

         var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
         chart.draw(data, options);
     }
 </script>

  <div class="col-12">
      <div class="card-body pb-0">
          <h4 style="color: #fff">Top Selling</h4>

          <asp:GridView ID="gvTopSellingProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-borderless" EmptyDataText="No products found">
              <Columns>
                  <asp:TemplateField HeaderText="Image">
                      <ItemTemplate>
                          <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' style="width: 50px; height: auto;" />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                  <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}" />
                  <asp:BoundField DataField="TotalSold" HeaderText="Total Sold" />
                  <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}" />
              </Columns>
          </asp:GridView>
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
