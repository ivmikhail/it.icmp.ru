<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<a  href="<%= Url.Action("receivedlist", "message") %>"
    title="Новых сообщений: <%= CurrentUser.User.UnreadMessagesCount %>"
    <% if (CurrentUser.User.UnreadMessagesCount > 0) { %>
        class="important-link"
    <% } %>>
    сообщения</a>
