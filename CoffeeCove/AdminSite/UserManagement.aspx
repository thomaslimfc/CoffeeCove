<%@ Page Title="User Management (Admin)" Language="C#" 
    MasterPageFile="../Master/Admin.Master" 
    AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" 
    Inherits="CoffeeCove.AdminSite.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
<div class="p-6 bg-background rounded-lg shadow-md dark:bg-card">
  <h2 class="text-2xl font-semibold text-primary dark:text-primary-foreground">Enhance Your Data with Datatables 🚀</h2>
  <p class="text-muted-foreground dark:text-muted-foreground my-4">
    Elevate your project with the Simple DataTables library. Simply add the .datatable class to any table for instant magic. Explore more examples below!
  </p>
  <div class="flex flex-col md:flex-row md:justify-between mb-4">
    <div class="mb-2 md:mb-0">
      <label for="entries" class="text-muted-foreground dark:text-muted-foreground">Show</label>
      <select id="entries" class="ml-2 border border-border rounded p-1 dark:border dark:border-border">
        <option>10</option>
        <option>25</option>
        <option>50</option>
        <option>100</option>
      </select>
      <span class="text-muted-foreground dark:text-muted-foreground ml-2">entries per page</span>
    </div>
    <div>
      <input type="text" placeholder="Search..." class="border border-border rounded p-1 dark:border dark:border-border" />
    </div>
  </div>
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
  <div class="flex justify-between items-center mt-4 text-muted-foreground dark:text-muted-foreground">
    <div>Showing 1 to 10 of 100 entries</div>
    <div>
      <button class="bg-primary text-primary-foreground px-4 py-2 rounded-md dark:bg-primary-foreground dark:text-primary">Previous</button>
      <div class="flex items-center">
        <button class="bg-primary text-primary-foreground px-2 py-1 rounded-md mx-1 dark:bg-primary-foreground dark:text-primary">1</button>
        <button class="bg-primary text-primary-foreground px-2 py-1 rounded-md mx-1 dark:bg-primary-foreground dark:text-primary">2</button>
        <button class="bg-primary text-primary-foreground px-2 py-1 rounded-md mx-1 dark:bg-primary-foreground dark:text-primary">3</button>
        <button class="bg-primary text-primary-foreground px-2 py-1 rounded-md mx-1 dark:bg-primary-foreground dark:text-primary">4</button>
        <button class="bg-primary text-primary-foreground px-2 py-1 rounded-md mx-1 dark:bg-primary-foreground dark:text-primary">5</button>
      </div>
      <button class="bg-primary text-primary-foreground px-4 py-2 rounded-md ml-2 dark:bg-primary-foreground dark:text-primary">Next</button>
    </div>
  </div>
</div>
</asp:Content>
