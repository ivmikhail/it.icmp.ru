<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "сообщения",
    "unreads",
    "message",
    null,
    new { 
        title = "Новых сообщений: " + CurrentUser.User.UnreadMessagesCount,
        @class = (CurrentUser.User.UnreadMessagesCount == 0) ? "" : "important-link"
    }
)%>
