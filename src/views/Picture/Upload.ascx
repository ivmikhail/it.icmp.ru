<%@ Control Language="C#" Inherits="ViewUserControl<PictureUploadModel>" %>


<% Html.RenderPartial("../Picture/List", Model.Path); %>

<%= Html.LabelFor(m => m.Picture) %>
<input type="file" id="Picture" name="Picture" />
<%= Html.ValidationMessageFor(m => m.Picture) %>

<input type="submit" name="UploadPicture" value="загрузить" />
