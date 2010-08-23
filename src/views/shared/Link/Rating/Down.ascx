<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


 <%= Ajax.ActionLink(
    "нравится",
    "up",
    "rating",
    new {
        EntityId = Model.EntityId,
        EntityType = Model.EntityType,
    },
    new AjaxOptions { UpdateTargetId = Model.HtmlId },
    new { 
        title = "Нравиться",
        @class = "page-link main-link rating-positive"
    }
)%>