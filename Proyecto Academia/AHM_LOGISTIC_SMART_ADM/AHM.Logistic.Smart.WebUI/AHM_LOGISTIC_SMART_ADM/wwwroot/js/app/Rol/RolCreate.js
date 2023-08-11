$("#frmRolCreate").on("submit", function (e) {
    var result = ValidateRolCreate();
    if (result) {
        var rol = $(this).serialize();
        $.ajax({
            type: "Post",
            url: BaseUrl + "/Rol/Create",
            data: rol
        }).done(function (data, index) {
            var result = true;
            result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            if (result) {
                setTimeout(function () {
                    location.assign(BaseUrl + "/rol/index");
                }, 1500)
            }

        });
    }
    e.preventDefault();
    return false;
});

function ValidateRolCreate() {
    var result = true;
    var count = 0;
    var rol_Description = $('#frmRolCreate #rol_Nombre');
    result = MessagesError(rol_Description, null, 50, 'Descripcion');
    if (result == true) { count++; }
    if (count == 1) {
        return result;
    }
    return false;

    return result;
}

$('#frmRolCreate #rol_Nombre').on('keypress', function () {
    $('#frmRolCreate #rol_Nombre').css("border-color", "#eee");
});