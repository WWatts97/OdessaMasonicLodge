$(document).ready(function () {
    if (window.innerWidth < 992) {
        $('.smallhide').addClass('visually-hidden');
    }
});

$(document).ready(function () {
    if (window.innerWidth > 992) {
        $('.smallhide').removeClass('visually-hidden');
    }
});

$(window).resize(function () {
    if (window.innerWidth < 992) {
        $('.smallhide').addClass('visually-hidden');
    } else {
        $('.smallhide').removeClass('visually-hidden');
    }
})