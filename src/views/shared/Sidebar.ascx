<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% Html.RenderPartial("Sidebar/Banner"); %>

<% Html.RenderPartial("Sidebar/LastComments"); %>

<% Html.RenderPartial("Sidebar/RatedPosts"); %>

<% Html.RenderPartial("Sidebar/PopularPosts"); %>

<%-- Html.RenderPartial("Sidebar/DiscussiblePosts"); --%>

<% Html.RenderPartial("Sidebar/Categories"); %>