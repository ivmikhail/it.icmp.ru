<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.ActiveDays != null) { %>
    <% if (Model.IsActive) { %>
        закончится:
    <% } else { %>
        закончился:
    <% } %>
    <span class="info">
        <%= Model.EndDate.ToString("dd MMMM yyyy, HH:mm")%>
    </span>
<% } %>
