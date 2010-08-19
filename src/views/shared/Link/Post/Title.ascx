<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%-- It looks like ActionLink always uses calls HttpUtility.Encode on the link text --%>
<%= Html.ActionLink(
    (Model.Title != "") ? Model.Title : "Без названия",
    "view",
    "post",
    new { id = Model.Id },
    new { @class = Model.IsAttached ? "important-link" : "" }
)%>
