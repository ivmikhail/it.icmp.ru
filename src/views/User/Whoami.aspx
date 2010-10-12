<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<dynamic>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Кто я? 
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ваши HTTP-заголовки</h1>
    <p class="block meta">
        На данной странице представлена информация о вас, отправляемая вашим браузером с запросом.
    </p>    
    <ul>
        <% foreach (var header in (List<KeyValuePair<String, String>>)ViewData["headers"]) { %>
            <li class="block">
                <h4><%= Html.Encode(header.Key) %></h4>
                <p class="info"><%= Html.Encode(header.Value) %></p>
            </li>
        <% } %>
    </ul>
</asp:Content>
