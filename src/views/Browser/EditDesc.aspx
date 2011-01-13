<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<BrowseItem>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование описания
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <% foreach (var parent in Model.Parents) { %>
            <% Html.RenderPartial("Link/Browser/Dir", parent); %>
            /
        <% } %>
        <%= Model.Name %>
    </h1>
 
    <% using (Html.BeginForm()) { %>
       
        <label for="desc">Редактировать описание</label>
        <span class="meta">Максимум 64 символов, <b class="info">&lt;</b> и <b class="info">&gt;</b> экранируются </span>
        <%= Html.TextBox("desc", Model.Description, new { maxlength = 64 })%>

        <input type="submit" value="сохранить" />

    <% } %>
    
</asp:Content>
