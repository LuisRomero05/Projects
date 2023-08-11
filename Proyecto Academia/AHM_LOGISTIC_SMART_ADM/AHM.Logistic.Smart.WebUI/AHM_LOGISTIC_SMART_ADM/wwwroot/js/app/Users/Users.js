var id = $('#formulario #usu_Id').val();
var valEditUnique = $('#formulario #isEditUnique').val();
if (valEditUnique === 'True') {
    var isEditUnique = true;
} else {
    var isEditUnique = false;
}

$(document).ready(function () {
    GetDropdowns();
    UsersGetRolesCreate(isEditUnique);
});


function UsersGetRolesCreate(isEditUnique) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Rol/RolList",
    }).done(function (data) {
        var NewOption = "";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        }
        else if (id == 0) {
            NewOption += '<option value="0"> Por favor seleccione una opción... </option>';
            data.data.forEach(function (item, index, array) {
                NewOption += `<option value="${item.rol_Id}"> ${item.rol_Description} </option>`;
            });
        } else if (id > 0) {
            var idrol = $("#formulario #rolId").val();
            data.data.forEach(function (item, index, array) {
                if (!isEditUnique) {
                    if (item.rol_Id == idrol) {
                        NewOption += `<option value="${item.rol_Id}" selected> ${item.rol_Description} </option>`;
                    }
                    else {
                        NewOption += `<option value="${item.rol_Id}"> ${item.rol_Description} </option>`;
                    }
                } else {
                    if (item.rol_Id == idrol) {
                        NewOption += `<option value="${item.rol_Id}" selected> ${item.rol_Description} </option>`;
                    }
                }
            });
        }

        $("#formulario #DropDownRoles").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownRoles').after($(message));
    });
}

$('#btnCrear').click(function () {
    window.location = (BaseUrl + "/user/CreateUsers")
});

function GetDropdowns() {
    $('#DropDownPersonas').select2();
    $('#DropDownRoles').select2();
}
