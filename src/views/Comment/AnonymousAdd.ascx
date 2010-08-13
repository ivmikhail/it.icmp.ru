<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.Models.Comment.AnonymousAddModel>" %>


<h3 id="add-comment">Вы - anonymous, <% Html.RenderPartial("Link/User/Login"); %>?</h3>

<%--
    <% Html.RenderPartial("EditorToolbar", "adding-comment"); %>
--%>

<% using (Html.BeginForm("anonymousadd", "comment")) { %>

    <%= Html.HiddenFor(m => m.PostId) %>

    <% Html.RenderPartial("ITCaptcha", Model); %>

    <%= Html.TextAreaFor(m => m.Text)%>

    <input type="submit" value="добавить" />                

<% } %>