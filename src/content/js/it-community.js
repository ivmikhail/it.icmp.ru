$(document).ready(function () {
    // прикрипление события для ссылки информации о bbcode-е
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
        var textarea = $('#' + this.title);

        var thumbUrl = this.href;
        var is_full = thumbUrl.indexOf('thumb') == -1;
        var fullUrl = thumbUrl.replace('thumb', 'full');
        var text = textarea.attr('value');
        var align = $(this).attr('class');

        if (is_full) {
            textarea.attr('value', text + '[' + align + '][img]' + fullUrl + '[/img][/' + align + ']');
        } else {
            textarea.attr('value', text + '[' + align + '][img=' + fullUrl + ']' + thumbUrl + '[/img][/' + align + ']');
        }

        return false;
    });
});

function checkAddedComment() {
    if ($('#add-comment').html().length == 0) {
        location.reload(true);
    }
}