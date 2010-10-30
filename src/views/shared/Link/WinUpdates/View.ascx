<%@ Control Language="C#" Inherits="ViewUserControl<WsusFile>" %>


<a  href="winupdates/file?name=<%= Server.UrlEncode(Model.Name) %>"
    title="Скачать"
    class="main-link"
    target="_blank"><%= Model.Name %></a>
