<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<WinUpdatesListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Обновления windows
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Поиск обновлений windows</h1>
    <p class="meta">
        Поиск производится по названию файла(обновления), аргументы 1 и 2 это строки которые содержатся в имени файла
    </p>
    <p class="meta">
        Пожалуйста указывайте более конкретные критерии поиска, иначе поиск может затянуться...
    </p>
    <% using (Html.BeginForm("search", "winupdates", null, FormMethod.Get, new { enctype = "multipart/form-data" })) { %>           
       
        <%= Html.LabelFor(m => m.Start) %>        
	    <p class="meta">Cтрока, с которой должно начинаться имя файла, например "windows6.0"</p>
        <%= Html.TextBoxFor(m => m.Start)%>

        <%= Html.LabelFor(m => m.Q1) %>
	    <p class="meta">например "rus"</p>
        <%= Html.TextBoxFor(m => m.Q1)%>    
         
        <%= Html.LabelFor(m => m.Q2) %>	    
        <p class="meta">например "x64"</p>
        <%= Html.TextBoxFor(m => m.Q2)%>    

        <input type="submit" value="найти" />

    <% } %>

    <% if(Model.List != null) { %>
    <ul class="text">
        <% if(Model.List.Count == 0) { %>
            <li>
                Ничего не найдено ...
            </li>
        <% } %> 
        <% foreach (var update in Model.List) { %>
            <li>
                <% Html.RenderPartial("Link/WinUpdates/View", update); %>
                <p class="info"><%= Html.Encode(update.Description) %></p>
            </li>
        <% } %>
    </ul>
    <% } %>
</asp:Content>