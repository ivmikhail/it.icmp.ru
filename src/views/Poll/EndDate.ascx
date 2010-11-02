<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


дата окончания голосования:
<b class="info">
    <% if (Model.ActiveDays != null) { %>
        <%= Html.Date(Model.EndDate)%>
    <% } else { %>
        бессрочный
    <% } %>
</b>