<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<a  href="<%= Url.Action("view", "post", new { id = Model.Id }) %>"
    title="<%= Model.ViewsCount %> просмотров"
    >
    <%= Model.TitleFormatted %></a>
