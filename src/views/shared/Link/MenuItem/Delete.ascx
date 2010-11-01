<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.DB.MenuItem>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "menuitem",
    new { id = Model.Id },
    new { title = "Удалить ссылку менюшки", @class = "delete-link" }
)%>
