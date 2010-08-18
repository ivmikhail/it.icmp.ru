$(document).ready(function () {
    $('.tags-info-link').click(function () {
        $('.tags-info', this.parentNode).toggleClass("hide");
        return false;
    });
});
