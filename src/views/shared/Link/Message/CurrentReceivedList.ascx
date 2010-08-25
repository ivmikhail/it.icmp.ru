<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "сообщения",
    "receivedlist",
    "message",
    null,
    new { 
        title = "Новых сообщений: " + CurrentUser.User.UnreadMessagesCount,
        @class = (CurrentUser.User.UnreadMessagesCount == 0) ? "" : "important-link"
    }
)%>
