<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.DB.MenuItem>" %>


<%= Html.ActionLink(
    "редактировать",
    "list",
    "menuitem",
    new { id = Model.Id },
    new { title = "Редактировать ссылку менюшки" }
)%>
