<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% Html.RenderPartial("SidebarItems/PopularPosts"); %>

<% Html.RenderPartial("SidebarItems/DiscussiblePosts"); %>

<% Html.RenderPartial("SidebarItems/LastComments"); %>

<% Html.RenderPartial("SidebarItems/Categories"); %>