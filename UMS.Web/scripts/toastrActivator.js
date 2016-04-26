function showNotification(model) {
    if (model != null && model.Message && model.MessageTitle) {
        toastr.options = {
            closeButton: true,
            debug: false,
            progressBar: true,
            positionClass: "toast-top-right",
            onclick: null,
            showDuration: 400,
            hideDuration: 1000,
            timeOut: 10000,
            extendedTimeOut: 700,
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        }
        switch (model.ToastrType) {
            case 1:
                toastr.success(model.Message, model.MessageTitle);
                break;
            case 2:
                toastr.info(model.Message, model.MessageTitle);
                break;
            case 3:
                toastr.warning(model.Message, model.MessageTitle);
                break;
            case 4:
                toastr.error(model.Message, model.MessageTitle);
                break;
        }
    }
}