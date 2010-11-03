<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Post>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.TitleFormatted %>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>
            <%= Model.TitleFormatted %>
        </h1>

        <div class="text">
            <%= Model.DescriptionFormatted %>
            <hr id="cut" />
            <%= Model.TextFormatted %>
        </div>

        <div class="meta">
            <% Html.RenderPartial("../Post/ViewMeta", Model); %>
        </div>
    </div>

    <% Html.RenderPartial("../Post/Like", Model); %>

    <% Html.RenderPartial("../Post/Comments", Model); %>

</asp:Content>
