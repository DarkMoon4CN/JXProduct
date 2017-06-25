//common fun
String.prototype.isnum = function () {
    return /^[0-9]*$/.test(this);
};
String.prototype.format = String.prototype.f = function () {
    var s = this,
        i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}

$(document).ready(function ($) {
    var $left = $('#l-index-nav');
    var indexLeftNav = $left.find('dt');
    indexLeftNav.each(function () { this.bBtn = !$(this).hasClass('active'); });
    indexLeftNav.on('click', function () {
        indexLeftNav.removeClass('active');
        if (this.bBtn) {
            if ($(this).find('span').length > 1) {
                $(this).find('span:last').html('-');
            }
            $(this).addClass('active');
            $(this).parent().find('dd').removeAttr('class');
        } else {
            if ($(this).find('span').length > 1) {
                $(this).find('span:last').html('+');
            }
            $(this).removeClass('active');
            $(this).parent().find('dd').removeAttr('class').attr('class', 'hide');
        }
        this.bBtn = !this.bBtn;
    });
    $('.header-comm-table a').on('click', function () {
        $('.header-comm-table a').removeClass('active');
        $(this).addClass('active');
        $('.setCommConterTab').hide().eq($(this).index()).show();
    });

    var height = $(window).height();
    height = height > 500 ? height : 500;
    $left.css("min-height", height - 80);

    $('.goback').on('click', function () {
        history.go(-1);
    });
});