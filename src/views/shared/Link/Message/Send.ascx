<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "отправить",
    "send",
    "message",
    new { receiver = (Model.IsAnonymous) ? "" : Model.Nick },
    new { title = "Отправить сообщение" }
)%>
