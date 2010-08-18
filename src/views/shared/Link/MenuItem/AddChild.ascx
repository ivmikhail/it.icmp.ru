<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.Db.MenuItem>" %>


<%= Html.ActionLink(
    "добавить",
    "list",
    "menuitem",
    new { parentid = Model.Id },
    new { title = "Добавить ссылку для этого родительского элемента" }
)%>
