(function () {
    $(function () {
        var url = window.location.href;
        $('ul.navbar-nav li a').filter(function () {
            return url.indexOf(this.href) !== -1;
        }).parent().addClass('active');
        $('ul.flex-column li a').filter(function () {
            return url.indexOf(this.href) !== -1;
        }).addClass('active');
    });
})();
///if the nav need the class [active]