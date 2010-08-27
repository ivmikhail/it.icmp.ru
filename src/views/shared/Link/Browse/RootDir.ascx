<%@ Control Language="C#" Inherits="ViewUserControl<BrowseModel>" %>

<%= Html.ActionLink(
    "..",
    "view",
    "browse",
    new { directory = Model.RootDir.LinkDir },
    new { title = "В корень" }
)%>
