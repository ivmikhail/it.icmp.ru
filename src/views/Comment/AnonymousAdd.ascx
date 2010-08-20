<%@ Control Language="C#" Inherits="ViewUserControl<AnonymousCommentAddModel>" %>


<h3 id="add-comment">Вы - anonymous, <% Html.RenderPartial("Link/User/Login"); %>?</h3>

<% using (Html.BeginForm("anonymousadd", "comment")) { %>

    <%= Html.HiddenFor(m => m.PostId) %>

    <% Html.RenderPartial("ITCaptcha", Model); %>

    <% Html.RenderPartial("EditorToolbar"); %>

    <%= Html.TextAreaFor(m => m.Text) %>
    <%= Html.ValidationMessageFor(m => m.Text) %>

    <input type="submit" value="добавить" />                

<% } %>