(function () {
    $(function () {
        var url = window.location.href;
        $('ul.navbar-nav li a').filter(function () {
            return url.indexOf(this.href) !== -1;
        }).parent().addClass('active');
    });
})();