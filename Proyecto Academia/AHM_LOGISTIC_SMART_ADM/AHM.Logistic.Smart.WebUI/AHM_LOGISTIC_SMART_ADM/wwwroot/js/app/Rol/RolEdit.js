var done = false;
$("#frmEditRoles").on("submit", function (e) {
    var result = ValidateRolEdit();
    if (result) {
        Swal.fire({
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar los cambios?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonColor: '#6c757d',
            cancelButtonColor: '#001f52',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                var rol = $(this).serialize();
                $.ajax({
                    type: "Post",
                    url: BaseUrl + "/Edit/roles",
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
            };
        })
    }
    e.preventDefault();
    return false;
});

function ValidateRolEdit() {
    var result = true;
    var count = 0;
    var rol_Description = $('#frmEditRoles #rol_Nombre');
    result = MessagesError(rol_Description, null, 50, 'Descripcion');
    if (result == true) { count++; }
    if (count == 1) {
        return result;
    }
    return false;

    return result;
}

$('#frmEditRoles #rol_Nombre').on('keypress', function () {
    $('#frmEditRoles #rol_Nombre').css("border-color", "#eee");
});