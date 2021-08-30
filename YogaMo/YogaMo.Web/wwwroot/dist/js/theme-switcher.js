function setTheme(theme) {
    if (theme === "dark") {
        $('body').addClass('dark-mode');
        $('#navbar').removeClass('navbar-light').addClass('navbar-dark');
        $('#main-sidebar').removeClass('sidebar-light-primary').addClass('sidebar-dark-primary');
        $('#theme-switch').removeClass('fa-moon').addClass('fa-lightbulb');
        $.cookie('dark_mode', true, {
            path: '/',
            expires: 3650
        });
    }
    else if (theme === "light") {
        $('body').removeClass('dark-mode');
        $('#navbar').addClass('navbar-light').removeClass('navbar-dark');
        $('#main-sidebar').addClass('sidebar-light-primary').removeClass('sidebar-dark-primary');
        $('#theme-switch').addClass('fa-moon').removeClass('fa-lightbulb');

        $.cookie('dark_mode', false, {
            path: '/',
            expires: 3650
        });

    }
}

$('#theme-switch').click(function () {
    var dark_mode = $.cookie('dark_mode');
    if (dark_mode === "true") {
        setTheme("light");
    }
    else {
        setTheme("dark");
    }
});