<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% Html.RenderPartial("Sidebar/LastRegisteredUsers"); %>

<% Html.RenderPartial("Sidebar/ActivePosters"); %>

<% Html.RenderPartial("Sidebar/ActiveCommentators"); %>

<% Html.RenderPartial("Sidebar/TopPosters"); %>

<% Html.RenderPartial("Sidebar/TopCommentators"); %>
