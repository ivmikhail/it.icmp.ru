<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% using (Ajax.BeginForm("upload", "picture", null, new AjaxOptions { HttpMethod = "Post", UpdateTargetId = "PictureUpload" }, new { enctype = "multipart/form-data" })) { %>

    <label for="picture">Выберите рисунок для загрузки</label>

    <input type="file" id="picture" name="picture" />

    <%= Html.ValidationSummary() %>

    <input type="submit" value="загрузить" />

<% } %> 
