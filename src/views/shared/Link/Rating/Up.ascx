<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


<%= Ajax.ActionLink(
    "не нравится",
    "down",
    "rating",
    new {
        EntityId = Model.EntityId,
        EntityType = Model.EntityType,
    },
    new AjaxOptions { UpdateTargetId = Model.HtmlId },
    new {
        title = "Не равиться",
        @class = "page-link rating-negative"
    }
)%>