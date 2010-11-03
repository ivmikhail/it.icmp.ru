<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.DB.MenuItem>" %>

<% if (Model.Url == "none" || Model.Url == "") { %>
    <%= Model.Name %>
<% } else { %>
    <a  href="<%= Model.Url %>" 
        <%-- if (Model.IsTargetBlank) { %> target="_blank" <% } --%>><%= Model.Name%></a>
<% } %>
