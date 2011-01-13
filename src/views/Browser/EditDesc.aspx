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
       
        <h2>
            <label for="desc">Редактировать описание</label>
            <%= Html.TextBox("desc", Model.Description)%>
        </h2>

        <input type="submit" value="сохранить" />

    <% } %>
    
</asp:Content>
