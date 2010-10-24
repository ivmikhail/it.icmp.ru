<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<RfcListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Поиск RFC
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Введите номер RFC или ключевое слово</h1>
    
    <% using (Html.BeginForm("search", "rfc", null, FormMethod.Get, new { enctype = "multipart/form-data" })) { %>           
       
        <input type="text" name="q" value="<%= Html.Encode(Model.Query) %>" />
    
        <input type="submit" value="найти" />

    <% } %>

    <% if(Model.List != null) { %>
    <ul class="text">
        <% if(Model.List.Count == 0) { %>
            <li>
                Ничего не найдено ...
            </li>
        <% } %> 
        <% foreach (var rfc in Model.List) { %>
            <li>
                <% Html.RenderPartial("Link/Rfc/View", rfc); %>
                <p class="info"><%= Html.Encode(rfc.Title) %></p>
            </li>
        <% } %>
    </ul>
    <% } %>
</asp:Content>