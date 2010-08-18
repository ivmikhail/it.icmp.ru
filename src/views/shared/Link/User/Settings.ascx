<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "настройки",
    "settings",
    "user",
    null,
    new { title = "Настроить профиль" }
)%>
