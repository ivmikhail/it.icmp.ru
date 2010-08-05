<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ITCommunity.Db.User>" %>


<%= Html.ActionLink(
    Model.Nick,
    "view",
    "user",
    new { nick = Model.Nick },
    new { @class = "user-link", title = "Посетить страницу пользователя" }
)%>
