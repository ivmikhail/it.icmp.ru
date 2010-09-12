$(document).ready(function () {
    // прикрипление события для ссылки инвормации о bbcode-е
    $('.tags-info-link').click(function () {
        $('.tags-info', this.parentNode).toggleClass('hide');
        return false;
    });

    // думаю понятно
    $('.delete-link').click(function () {
        return confirm('Точно удалить?');
    });

    // инициализация подсветки кода
    hljs.initHighlightingOnLoad();

    // Вставка загруженного изображения
    $('.pictures-thumbs a').click(function () {
        var textarea = $(this.title);

        var thumbUrl = this.href;
        var fullUrl = thumbUrl.replace('thumb', 'full');
        var text = textarea.attr('value');
        var align = $(this).attr('class');

        textarea.attr('value', text + '[' + align + '][img=' + fullUrl + ']' + thumbUrl + '[/img][/' + align + ']');

        return false;
    });
});
