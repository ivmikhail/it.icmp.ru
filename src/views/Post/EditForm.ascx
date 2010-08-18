<%@ Control Language="C#" Inherits="ViewUserControl<PostEditModel>" %>


<h2>
    <%= Html.LabelFor(m => m.Title) %>
    <%= Html.TextBoxFor(m => m.Title) %>
    <%= Html.ValidationMessageFor(m => m.Title) %>
</h2>

<% if (CurrentUser.IsAdmin) { %>
    <label>
        <%= Html.CheckBoxFor(m => m.IsAttached)%> прикрепленный пост?
    </label>
<% } %>

<%= Html.LabelFor(m => m.Description)%>
<% Html.RenderPartial("EditorToolbar"); %>
<%= Html.TextAreaFor(m => m.Description)%>
<%= Html.ValidationMessageFor(m => m.Description)%>
        
<%= Html.LabelFor(m => m.Text)%>
<% Html.RenderPartial("EditorToolbar"); %>
<%= Html.TextAreaFor(m => m.Text, new { @class = "large" })%>
<%= Html.ValidationMessageFor(m => m.Text)%>

<label>Категории</label>
<div class="meta">выберите в какие категории будет входить пост</div>
<div id="select-categories">
    <% Html.RenderPartial("EditCategories", PostEditCategoriesModel.Current); %>    
</div>
        
<%= Html.LabelFor(m => m.Source)%>
<%= Html.TextBoxFor(m => m.Source)%>