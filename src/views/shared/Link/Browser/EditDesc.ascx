<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
    "edit",
    "editdesc",
    "browser",
    new { path = Uri.EscapeDataString(Model.RelativeLink) },
    new { title = "Редактировать описание" }
)%>
