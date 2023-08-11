var done = false;
var isEdit = false;

$("#formulario").on('submit', function (e) {
    e.preventDefault();
    var id = $('#formulario #usu_Id').val();
    var rolId = $('#formulario #usu_Id').val();
    var result = ValidateFrmUsers(id);
    if (isEdit && result) {
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
                var user = new FormData(this);
                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Usuario/Acciones",
                    data: user,
                    cache: false,
                    contentType: false,
                    processData: false
                }).done(function (data) {
                    var result = false;
                    if (id == 0) {
                        result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                        if (result) {
                            setTimeout(function () {
                                location.assign(BaseUrl + "/user/index");
                            }, 1500)
                        }
                    }
                    if (id > 0) {
                        result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                        if (result) {
                            setTimeout(function () {
                                location.assign(BaseUrl + "/user/index");
                            }, 1500)
                        }
                    }

                });
            }
        });
    }
    else if (result) {
        var user = new FormData(this);
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Usuario/Acciones",
            data: user,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (data) {
            var result = false;
            if (id == 0) {
                result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                if (result) {
                    setTimeout(function () {
                        location.assign(BaseUrl + "/user/index");
                    }, 1500)
                }
            }
            if (id > 0) {
                result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                if (result) {
                    setTimeout(function () {
                        location.assign(BaseUrl + "/user/index");
                    }, 1500)
                }
            }
        });
    }
    return false;
});

function ValidateFrmUsers(id) {
    var result = false;
    var count = 0;
    if (id > 0)
        isEdit = true;
    var usu_UserName = $('#formulario #inputusua');
    if (id == 0) {
        var usu_Password = $('#formulario #usu_Password');
    }
    var DropDownPersonas = $("#formulario #DropDownPersonas");
    var DropDownRoles = $('#formulario #DropDownRoles');

    var divPersons = $('#formulario #divPersons');
    var divRoles = $('#formulario #divRoles');

    var borPersons = $('#formulario #divPersons span.select2-selection.select2-selection--single');
    var borRoles = $('#formulario #divRoles span.select2-selection.select2-selection--single');

    result = MessagesError(usu_UserName, null, 20, 'Nombre de Usuario');
    if (result == true) { count++; }
    if (id == 0) {
        result = MessagesError(usu_Password, 8, null, 'Contraseña');
        if (result == true) { count++; }
    } else {
        count++;
    }

    result = MessageErrorDrop(DropDownPersonas, 'Empleados', divPersons, borPersons);
    if (result == true) { count++; }
    result = MessageErrorDrop(DropDownRoles, 'Roles', divRoles, borRoles);
    if (result == true) { count++; }
    if (count == 4) {
        return result;
    }
    return false;
}



$('#formulario #inputusua').on('keypress', function () {
    $('#formulario #inputusua').css("border-color", "#eee");
});
$('#formulario #usu_Password').on('keypress', function () {
    $('#formulario #usu_Password').css("border-color", "#eee");
});
$('#formulario #DropDownPersonas').on('select2:select', function () {
    $('#formulario #divPersons span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#formulario #DropDownRoles').on('select2:select', function () {
    $('#formulario #divRoles span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

