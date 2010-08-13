<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<ITCommunity.Models.Comment.AddModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление комментария
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1><%= Html.ValidationSummary() %></h1>
    
    <% Html.RenderPartial("Add", Model); %>

</asp:Content>
