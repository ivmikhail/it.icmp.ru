<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.Models.Comment.AddModel>" %>


<h3 id="add-comment"><%= CurrentUser.User.Nick %>, напиши комментарий!</h3>                        

<%-- 
    <% Html.RenderPartial("EditorToolbar", "adding-comment"); %>
--%>

<% using (Html.BeginForm("add", "comment")) { %>

    <%= Html.HiddenFor(m => m.PostId) %>

    <%= Html.TextAreaFor(m => m.Text) %>

    <input type="submit" value="добавить" />                

<% } %>