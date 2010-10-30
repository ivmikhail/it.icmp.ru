<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.ActiveDays != null) { %>
    <% if (Model.IsActive) { %>
        закончится:
    <% } else { %>
        закончился:
    <% } %>
    <b class="info">
        <%= Html.Date(Model.EndDate)%>
    </b>
<% } else { %>
    не известно когда закончится
<% } %>
