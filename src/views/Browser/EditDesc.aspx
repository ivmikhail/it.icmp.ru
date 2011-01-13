<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<BrowseItem>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование описания
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Редактирование описания</h1>
 
    <% using (Html.BeginForm()) { %>
       
        <h2>
            <label for="desc"><%= Model.Name %></label>
            <%= Html.TextBox("desc", Model.Description)%>
        </h2>

        <input type="submit" value="сохранить" />

    <% } %>
    
</asp:Content>
