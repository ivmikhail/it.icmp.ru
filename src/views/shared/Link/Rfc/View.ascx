<%@ Control Language="C#" Inherits="ViewUserControl<Rfc>" %>


<a  href="<%= Model.RelativeUrl %>"
    title="Посмотреть полный текст"
    class="main-link"><%= Model.Number %></a>
