<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeFile="AdminReview.aspx.cs" Inherits="CoffeeCove.AdminSite.AdminReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/AdminReview.css" rel="stylesheet" />
        <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
    
        <asp:Button ID="Btnback" runat="server" Text="Back" PostBackUrl="~/AdminSite/Review.aspx" CssClass="backBtn"/>

        <div id="rateContainer" class="bg-white rounded shadow-sm p-4 mb-5 rating-review-select-page">
            <h3 class="mb-4">Reply Customer</h3>
            
            <form>
                <div class="form-group">
                    <label>Your Comment</label>
                    <textarea class="form-control fixed-height" id="txtComment" runat="server"></textarea>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnCont" Text="Submit Comment" OnClick="btnSubmit_Click" />
                </div>
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
