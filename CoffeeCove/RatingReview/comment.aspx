<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Customer.Master" AutoEventWireup="true" CodeFile="comment.aspx.cs" Inherits="CoffeeCove.RatingReview.comment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <link href="../CSS/ratingBootstrap.css" rel="stylesheet" />
    <link href="../CSS/LeaveAComment.css" rel="stylesheet" />

    <div class="tab-pane fade active show" id="pills-reviews" role="tabpanel" aria-labelledby="pills-reviews-tab">
        <div id="rateContainer" class="bg-white rounded shadow-sm p-4 mb-5 rating-review-select-page">
            <h3 class="mb-4">Leave Comment</h3>
            <p class="mb-2">Rate the Place</p>
            <div class="mb-4">
                <span class="star-rating">
                         <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                         <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                         <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                         <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                         <a href="#"><i class="icofont-ui-rating icofont-2x"></i></a>
                         </span>
            </div>
            <form>
                <div class="form-group">
                    <label>Your Comment</label>
                    <textarea class="form-control fixed-height"></textarea>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btnCont" Text="Submit Comment" OnClick="btnSubmit_Click" />
                </div>
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>