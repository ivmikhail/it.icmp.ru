$(document).ready(function () {
    // прикрипление события для ссылки инвормации о bbcode-е
    $('.tags-info-link').click(function () {
        $('.tags-info', this.parentNode).toggleClass("hide");
        return false;
    });

    // думаю понятно
    $('.delete-link').click(function () {
        return confirm('Точно удалить?');
    });

    // инициализация подсветки кода
    hljs.initHighlightingOnLoad();
});
