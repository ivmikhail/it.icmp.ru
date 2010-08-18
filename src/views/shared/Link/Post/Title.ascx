<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    (Model.TitleFormatted != "") ? Model.TitleFormatted : "Без названия",
    "view",
    "post",
    new { id = Model.Id },
    new { @class = Model.IsAttached ? "important-link" : "" }
)%>
