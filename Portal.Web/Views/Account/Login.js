(function () {

    $(function () {
        var $loginForm = $('#LoginForm');

        $loginForm.submit(function (e) {
            e.preventDefault();

            if (!$loginForm.valid()) {
                return;
            }
            var loginButton = $loginForm.find('#LoginButton')
            loginButton.addClass('running');
            
            abp.ajax({
                contentType: 'application/x-www-form-urlencoded',
                url: $loginForm.attr('action'),
                data: $loginForm.serialize(),
                error: function() {
                    loginButton.removeClass('running');
                }
            });
        });
        
        $('#ReturnUrlHash').val(location.hash);

        $('#LoginForm input:first-child').focus();
    });

    $(function () {
        // checkbox iCheck
        $('input[type="checkbox"], input[type="radio"]').iCheck({
            checkboxClass: "icheckbox_square-red",
            radioClass: "iradio_square-red",
            increaseArea: '20%'
        });
    });

})();