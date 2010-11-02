<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<a  href="<%= Url.Action("view", "post", new { id = Model.Id }) %>"
    title="Просмотров: <%= Model.ViewsCount %>"
    >
    <%= Model.TitleFormatted %></a>
