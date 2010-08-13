<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    Model.TitleFormatted,
    "view",
    "post",
    new { id = Model.Id },
    new { title = Model.ViewsCount + " просмотров", @class = "sidebar-link" }
)%>
