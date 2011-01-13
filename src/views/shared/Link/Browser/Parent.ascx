<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
    "..",
    "files",
    "browser",
    new { link = Model.RelativeLink },
    new { title = "Выше" }
)%>
