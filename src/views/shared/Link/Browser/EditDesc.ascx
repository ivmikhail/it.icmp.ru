<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<%= Html.ActionLink(
    "edit",
    "editdesc",
    "browser",
    new { link = Model.RelativeLink },
    new { title = "Редактировать описание" }
)%>
