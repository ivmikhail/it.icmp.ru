<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
     Model.Name,
    "files",
    "browser",
    new { link = Model.RelativeLink },
    new { title = Model.Description }
)%>
