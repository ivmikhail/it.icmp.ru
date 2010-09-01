<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
     Model.Name,
    "files",
    "browser",
    new { link = Model.Link },
    new { title = Model.Description }
)%>
