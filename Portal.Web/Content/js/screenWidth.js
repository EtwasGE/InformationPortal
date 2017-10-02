// передача на сервер ширины окна браузера
$(document).ready(function () {
    screen_width();
});
$(window).resize(function () {
    screen_width();
});

function screen_width() {
    var w = window,
        d = document,
        e = d.documentElement,
        g = d.getElementsByTagName('body')[0],
        x = w.innerWidth || e.clientWidth || g.clientWidth;

    $.ajax({
        type: "POST",
        url: "/layout/updatescreenwidth",
        data: { value: x },
        dataType: "json"
    });
}