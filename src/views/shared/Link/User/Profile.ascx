<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>

<% if (Model.IsAnonymous) { %>
    <%= Html.ActionLink(
        "Anonymous",
        "anonymous",
        "user",
        null,
        new { @class = "user-link", title = "Посетить страницу Anonymous"}
    )%>
<% } else { %>
    <%= Html.ActionLink(
        Model.Nick,
        "profile",
        "user",
        new { nick = Model.Nick },
        new { @class = "user-link", title = "Посетить страницу " + Model.Nick}
    )%>
<% } %>