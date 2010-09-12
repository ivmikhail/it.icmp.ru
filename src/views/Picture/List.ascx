<%@ Control Language="C#" Inherits="ViewUserControl<String>" %>

<label>Загруженные рисунки</label>
<div class="meta">Не добавленные в новость картинки будут удалены автоматически. Чтобы добавить картинку в пост кликните по соответствующей ссылке.</div>

<ul class="pictures-thumbs">
    <% foreach (var picture in Picture.GetList(Model)) { %>
        <li>
            <img src="<%= picture.ThumbUrl %>" alt="<%= picture.Name %>" />
            <a href="<%= picture.ThumbUrl %>" title="#Description" class="add-picture-bbcode">вставить в описание</a>
            <a href="<%= picture.ThumbUrl %>" title="#Text" class="add-picture-bbcode">вставить в текст</a>
        </li>
    <% } %>
</ul>

<div class="clear"></div>

