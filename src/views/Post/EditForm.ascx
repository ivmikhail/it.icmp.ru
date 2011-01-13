<%@ Control Language="C#" Inherits="ViewUserControl<PostEditModel>" %>


<h2>
    <%= Html.LabelFor(m => m.Title) %>
    <%= Html.TextBoxFor(m => m.Title, new { maxlength = 128 })%>
    <%= Html.ValidationMessageFor(m => m.Title) %>
</h2>

<% if (CurrentUser.IsAdmin) { %>
    <label>
        <%= Html.CheckBoxFor(m => m.IsAttached)%> прикрепленный пост?
    </label>
<% } %>
<label>
    <%= Html.CheckBoxFor(m => m.IsCommentable)%> разрешить комментарии?
</label>

<%= Html.LabelFor(m => m.Description)%>
<% Html.RenderPartial("EditorToolbar"); %>
<%= Html.TextAreaFor(m => m.Description)%>
<%= Html.ValidationMessageFor(m => m.Description)%>
        
<%= Html.LabelFor(m => m.Text)%>
<% Html.RenderPartial("EditorToolbar"); %>
<%= Html.TextAreaFor(m => m.Text, new { @class = "large" })%>
<%= Html.ValidationMessageFor(m => m.Text)%>

<% Html.RenderPartial("../Picture/Upload", Model); %>

<label>Категории</label>
<%= Html.ValidationMessageFor(m => m.IsSetCategory)%>
<div class="meta">
    Выберите в какие категории будет входить пост. Пожалуйста выбирайте <b class="info">максимум 4 категории</b>.
</div>
<div id="select-categories">
    <% Html.RenderPartial("EditCategories", PostEditCategoriesModel.Current); %>    
</div>
        
<%= Html.LabelFor(m => m.Source)%>
<%= Html.TextBoxFor(m => m.Source, new { maxlength = 1024 })%>