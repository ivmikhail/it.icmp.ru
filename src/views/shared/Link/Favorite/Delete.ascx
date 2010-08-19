<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "избранное",
    "delete",
    "favorite",
    new { id = Model.Id },
    new { title = "Убрать из избранных", @class="none-active" }
)%>
