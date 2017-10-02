(function ($) {
    var _$modal = $('#ModalContent');
    var _$form = $('form[name=ErrorReportForm]');

    _$form.validate({
        errorClass: "has-error",
        validClass: "has-success",
        
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').addClass(errorClass).removeClass(validClass);
            
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) { }
    });

    function send() {

        if (!_$form.valid()) {
            return;
        }

        var contentId = _$form.find('input[name="ContentId"]').val();
        var subject = _$form.find('select[name="Subject"]').val();
        var comment = _$form.find('textarea[name="Comment"]').val();

        $.ajax({
            url: abp.appPath + 'Feedback/AddErrorReport',
            type: 'POST',
            data: { 'ContentId': contentId, 'Subject': subject, 'Comment': comment },
            success: function () {
                _$modal.modal('hide');
            },
            error: function (e) { }
        });
    }

    //Handle send button click
    _$form.closest('div.modal-content').find("#send-button").click(function (e) {
        e.preventDefault();
        send();
    });

    $.AdminBSB.input.activate(_$form);

})(jQuery);