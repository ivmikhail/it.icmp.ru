<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% Html.RenderPartial("Sidebar/LastComments"); %>

<% Html.RenderPartial("Sidebar/DiscussiblePosts"); %>

<% Html.RenderPartial("Sidebar/PopularPosts"); %>

<% Html.RenderPartial("Sidebar/Categories"); %>