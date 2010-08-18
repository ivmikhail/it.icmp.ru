<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<User>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Профиль - <%= Model.Nick %>
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("UserMenu", Model); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Профиль - <%= Model.Nick %></h1>

    <h2 class="light-block">Зарегистрировался - <%= Model.CreateDate.ToString("dd MMMM yyyy") %></h2>

    <h2 class="light-block">Написал постов - <%= Model.PostsCount %></h2>

    <h2 class="light-block">Написал комментариев - <%= Model.CommentsCount %></h2>

</asp:Content>
