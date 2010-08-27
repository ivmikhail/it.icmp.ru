<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
     Model.Name,
    "view",
    "browse",
    new { directory = Model.LinkDir },
    new { title = "Перейти в директорию" }
)%>
