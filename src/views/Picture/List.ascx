<%@ Control Language="C#" Inherits="ViewUserControl<PictureUploadModel>" %>


<label>Загруженные рисунки</label>
<div class="meta">Не добавленные в новость картинки будут удалены автоматически. Чтобы добавить картинку в пост кликните по соответствующей ссылке.</div>

<ul class="pictures-thumbs">
    <% foreach (var picture in Picture.GetList(Model.Path)) { %>
        <li class="light-block">
            <img src="<%= picture.ThumbUrl %>" alt="<%= picture.Name %>" />

            <% foreach (var textarea in Model.PictureTextareas) { %>
                <h3>в <%= textarea.Value%></h3>
                <ul class="right-list meta">
                    <li class="info">thumb: </li>
                    <li><a href="<%= picture.ThumbUrl %>" title="<%= textarea.Key %>" class="left">слева</a> </li>
                    <li><a href="<%= picture.ThumbUrl %>" title="<%= textarea.Key %>" class="center">по центру</a> </li>
                    <li><a href="<%= picture.ThumbUrl %>" title="<%= textarea.Key %>" class="right">справа</a> </li>
                </ul>
                <ul class="right-list meta">
                    <li class="info">full: </li>
                    <li><a href="<%= picture.FullUrl %>" title="<%= textarea.Key %>" class="left">слева</a> </li>
                    <li><a href="<%= picture.FullUrl %>" title="<%= textarea.Key %>" class="center">по центру</a> </li>
                    <li><a href="<%= picture.FullUrl %>" title="<%= textarea.Key %>" class="right">справа</a> </li>
                </ul>
            <% } %>
            <div class="clear"></div>
        </li>
    <% } %>
</ul>

<div class="clear"></div>

