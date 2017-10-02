// clipboard
$(function () {
    var clipboard = new Clipboard('#clipboard');

    clipboard.on('success', function (e) {
        console.log(e);
    });

    clipboard.on('error', function (e) {
        console.log(e);
    });
});

$(document).on('click', '#delete-content', function (e) {
    var contentId = $(this).attr("data-content-id");
    var updateTargetId = $(this).attr("data-update-targetId");
    var msg = $(this).attr("data-msg");
    e.preventDefault();
    deleteContent(contentId, updateTargetId, msg);
});

$(document).on('click', '#approve-content', function (e) {
    var contentId = $(this).attr("data-content-id");
    var updateTargetId = $(this).attr("data-update-targetId");
    var msg = $(this).attr("data-msg");
    e.preventDefault();
    approveContent(contentId, updateTargetId, msg);
});

$(document).on('click', '#errorreport', function (e) {
    var contentId = $(this).attr("data-content-id");

    e.preventDefault();

    $.ajax({
        url: abp.appPath + 'Feedback/ErrorReportModal?contentId=' + contentId,
        type: 'POST',
        contentType: 'application/html',
        success: function (content) {
            $('#ModalContent div.modal-content').html(content);
        },
        error: function (e) { }
    });
});

function getControllerName() {
    var url = window.location.pathname.split("/");
    return url[1];
}

function deleteContent(contentId, updateTargetId, msg) {
    abp.message.confirm(
        msg,
        function (isConfirmed) {
            if (isConfirmed) {
                $.ajax({
                    url: abp.appPath + getControllerName() + '/delete/id' + contentId,
                    type: 'POST',
                    contentType: 'application/html',
                    success: function (content) {
                        $('#' + updateTargetId).html(content);
                    },
                    error: function (e) { }
                });
            }
        });
}

function approveContent(contentId, updateTargetId, msg) {
    abp.message.confirm(
        msg,
        function (isConfirmed) {
            if (isConfirmed) {
                $.ajax({
                    url: abp.appPath + getControllerName() + '/approve/id' + contentId,
                    type: 'POST',
                    contentType: 'application/html',
                    success: function (content) {
                        $('#' + updateTargetId).html(content);
                    },
                    error: function (e) { }
                });
            }
        });
}