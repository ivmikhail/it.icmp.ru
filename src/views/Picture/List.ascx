<%@ Control Language="C#" Inherits="ViewUserControl<String>" %>


<label>Загруженные рисунки</label>
<div class="meta">Не добавленные в новость картинки будут удалены автоматически. Чтобы добавить картинку в пост кликните по соответствующей ссылке.</div>

<ul class="pictures-thumbs">
    <% foreach (var picture in Picture.GetList(Model)) { %>
        <li class="light-block">
            <img src="<%= picture.ThumbUrl %>" alt="<%= picture.Name %>" />
            <ul class="right-list meta">
                <li class="info">
                    вставить в описание:
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Description" class="left">слева</a>
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Description" class="center">по центру</a>
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Description" class="right">справа</a>
                </li>
            </ul>
            <ul class="right-list meta">
                <li class="info">
                    вставить в текст:
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Text" class="left">слева</a>
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Text" class="center">по центру</a>
                </li>
                <li>
                    <a href="<%= picture.ThumbUrl %>" title="#Text" class="right">справа</a>
                </li>
            </ul>
            <div class="clear"></div>
        </li>
    <% } %>
</ul>

<div class="clear"></div>

