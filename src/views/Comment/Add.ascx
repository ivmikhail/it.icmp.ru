<%@ Control Language="C#" Inherits="ViewUserControl<CommentEditModel>" %>


<h3><%= CurrentUser.User.Nick %>, напиши комментарий!</h3>                        

<% using (Ajax.BeginForm("add", "comment", new AjaxOptions() { HttpMethod = "post", OnSuccess = "checkAddedComment", UpdateTargetId = "add-comment" })) { %>

    <%= Html.HiddenFor(m => m.PostId) %>

    <% Html.RenderPartial("EditorToolbar"); %>

    <%= Html.TextAreaFor(m => m.Text) %>
    <%= Html.ValidationMessageFor(m => m.Text) %>

    <input type="submit" value="добавить" />                

<% } %>