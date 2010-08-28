<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


заканчивается:
<span class="info">
    <% if (Model.ActiveDays == null) { %>
        никогда
    <% } else { %>
        <% if (Model.IsActive) { %>
            <%= Model.EndDate.ToString("dd MMMM yyyy, HH:mm")%>
        <% } else { %>
            уже закончился
        <% } %>
    <% } %>
</span>
