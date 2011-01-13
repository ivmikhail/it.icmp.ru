<%@ Control Language="C#" Inherits="ViewUserControl<AnonymousCommentAddModel>" %>


<h3>Вы - anonymous, <% Html.RenderPartial("Link/User/Login"); %>?</h3>

<% using (Ajax.BeginForm("anonymousadd", "comment", new AjaxOptions() { HttpMethod = "post", OnSuccess = "checkAddedComment", UpdateTargetId = "add-comment" })) { %>

    <%= Html.HiddenFor(m => m.PostId)%>

    <% Html.RenderPartial("../Captcha/Captcha", Model); %>

    <% Html.RenderPartial("EditorToolbar"); %>

    <%= Html.TextAreaFor(m => m.Text)%>
    <%= Html.ValidationMessageFor(m => m.Text)%>

    <input type="submit" value="добавить" />

<% } %>