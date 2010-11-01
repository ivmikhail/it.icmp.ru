<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
    "..",
    "files",
    "browser",
    new { link = Model.Link },
    new { title = "Выше" }
)%>
