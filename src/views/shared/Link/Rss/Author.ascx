<%@ Control Language="C#" Inherits="ViewUserControl<SyndicationPerson>" %>


<% if (Model.Uri != null) { %>
    <a href="<%= Model.Uri%>" title="Посетить страницу - <%= Model.Name%>" class="user-link">
        <%= Model.Name%></a>
<% } else { %>
    <a href="#" title="Ссылка не задана" class="user-link">
    <%= Model.Name%></a>
<% } %>