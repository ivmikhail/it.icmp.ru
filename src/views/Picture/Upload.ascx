<%@ Control Language="C#" Inherits="ViewUserControl<PictureUploadModel>" %>


<% Html.RenderPartial("../Picture/List", Model); %>
<%= Html.HiddenFor(m => m.Path) %>

<%= Html.LabelFor(m => m.Picture) %>
<input type="file" id="Picture" name="Picture" />
<%= Html.ValidationMessageFor(m => m.Picture) %>

<%--<input type="submit" id="UploadPicture" name="UploadPicture" value="загрузить" />--%>
