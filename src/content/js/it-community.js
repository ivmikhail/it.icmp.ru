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
    $('.add-picture-bbcode').click(function () {
        var textarea = $(this.title);

        var thumbUrl = this.href;
        var fullUrl = thumbUrl.replace('thumb', 'full');
        var text = textarea.attr('value');

        textarea.attr('value', text + '[img=' + fullUrl + ']' + thumbUrl + '[/img]');

        return false;
    });
});
