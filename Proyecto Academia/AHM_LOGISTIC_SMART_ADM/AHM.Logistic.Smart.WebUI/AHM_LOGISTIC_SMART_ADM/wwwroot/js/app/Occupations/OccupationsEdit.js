var done = false;
//almacenar el id del registro modificado
var occ_Id = 0;

//click que abre el modal de crear
function ShowModalEditOccupation(id) {
    //cambiar el valor de emp_Id
    occ_Id = id;
    //llamar la funció que recupera el detalle
    GetOccupationDetail(id);
    //mostrar el modal
    $('#EditOccupation').modal('show');
    CleanOccupationEdit();
}

//funcion para obtener la información del empleado
function GetOccupationDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Occupation/Edit/" + id,
    }).done(function (data) {
        $("#EditOccupation #occ_Description").val(data.data.occ_Description);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudo ingresar la descripción, comuníquese con el encargado.</span>';
        $('#occ_Description').after($(message));
    });
}


//click que confirma la creación de un Occupation
$("#EditOccupation #EditOccupationConfirmar").click(function (e) {
    var result = ValidationOccupationEdit()
    if (result == true) {
        var occupations = [
            { name: "occ_Description", value: $("#EditOccupation #occ_Description").val().trim() },
            { name: "occ_IdUserCreate", value: null },
            { name: "occ_IdUserModifies", value: UserId },

        ];

        $('#EditOccupation').modal('hide');
        //ocultar modal 
        Swal.fire({
            closeOnClickOutside: false,
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar este registro?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonColor: '#6c757d',
            cancelButtonColor: '#001f52',
            confirmButtonText: 'Aceptar'

        }).then((eliminar) => {
            if (eliminar.isConfirmed) {
                //aqui se inserta la data para editar lol
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Occupation/Edit?Id=" + occ_Id,
                    data: occupations,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/Occupation/index");
                        }, 1500)
                    }
                });
            }
            else {
                //mostrar modal again
                $('#EditOccupation').modal('show');
            }
        });
    }

});

function ValidationOccupationEdit() {
    LimpiarSpanMessa();
    var result = true;
    var occ_Description = $('#EditOccupation #occ_Description');
    result = MessagesError(occ_Description, null, 100, 'Descripción');
    return result;
}

function CleanOccupationEdit() {
    $('#EditOccupation #occ_Description').css("border-color", "#eee");
}

$('#EditOccupation #occ_Description').on('keypress', function () {
    $('#EditOccupation #occ_Description').css("border-color", "#eee");
});
