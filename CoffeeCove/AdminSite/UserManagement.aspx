<%@ Page Title="User Management (Admin)" Language="C#" 
    MasterPageFile="../Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.UserManagement" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <!-- DataTables -->
  <link rel="stylesheet" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.min.css">
  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
  <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>

  <script>
      $(document).ready(function () {
          $('.datatable').DataTable({
              "paging": true,
              "searching": true,
              "ordering": true
          });
      });
  </script>

  <div class="overflow-x-auto">
    <table class="min-w-full divide-y divide-border datatable dark:divide-border">
      <thead class="bg-card dark:bg-card-foreground">
        <tr>
          <th class="px-4 py-2 text-left text-primary dark:text-primary-foreground">Name</th>
          <th class="px-4 py-2 text-left text-primary dark:text-primary-foreground">Ext.</th>
          <th class="px-4 py-2 text-left text-primary dark:text-primary-foreground">City</th>
          <th class="px-4 py-2 text-left text-primary dark:text-primary-foreground">Start Date</th>
          <th class="px-4 py-2 text-left text-primary dark:text-primary-foreground">Completion</th>
        </tr>
      </thead>
      <tbody class="bg-card-foreground dark:bg-card">
        <tr>
          <td class="px-4 py-2">Unity Pugh</td>
          <td class="px-4 py-2">9958</td>
          <td class="px-4 py-2">Curicó</td>
          <td class="px-4 py-2">2005/02/11</td>
          <td class="px-4 py-2">37%</td>
        </tr>
      </tbody>
    </table>
  </div>
</asp:Content>
