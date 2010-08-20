<%@ Control Language="C#" Inherits="ViewUserControl<CommentEditModel>" %>


<h3 id="add-comment"><%= CurrentUser.User.Nick %>, напиши комментарий!</h3>                        

<% using (Html.BeginForm("add", "comment")) { %>

    <%= Html.HiddenFor(m => m.PostId) %>

    <% Html.RenderPartial("EditorToolbar"); %>

    <%= Html.TextAreaFor(m => m.Text) %>
    <%= Html.ValidationMessageFor(m => m.Text) %>

    <input type="submit" value="добавить" />                

<% } %>