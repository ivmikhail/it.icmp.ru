<%@ Control Language="C#" Inherits="ViewUserControl<WsusFile>" %>


<a  href="winupdates/file?name=<%= Server.UrlEncode(Model.Name) %>"
    title="Скачать"
    class="main-link"><%= Model.Name %></a>
