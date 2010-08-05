<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ITCommunity.Db.Category>" %>


<%= Html.ActionLink(
    Model.Name,
    "category",
    "posts",
    new { id = Model.Id },
    new { title = "Всего " + Model.PostsCount + " постов", @class="category-link" }
)%>