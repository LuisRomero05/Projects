var cou_Id = 0;
var done = false;
function ShowModalEditCountries(id) {
    cou_Id = id;
    GetCountriesDetail(id);
    $('#EditCountries').modal('show');
    CleanCountriesEdit();

}

//funcion para obtener la información del país
function GetCountriesDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Countries/Edit/" + id,
    }).done(function (data) {
        $("#EditCountries #cou_Description").val(data.data.cou_Description);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudo ingresar la descripción, comuníquese con el encargado.</span>';
        $('#cou_Description').after($(message));
    });
}

$('#EditCountries #EditCountriesConfirm').on('click', function () {

    var result = ValiContriesEdit()
    if (result == true) {
        var countries = [
            { name: "cou_Description", value: $("#EditCountries #cou_Description").val().trim() },
            { name: "cou_IdUserCreate", value: null },
            { name: "cou_IdUserModified", value: UserId },
        ];

        $('#EditCountries').modal('hide');
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
                    url: BaseUrl + "/Countries/Edit?Id=" + cou_Id,
                    data: countries,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/Countries/index");
                        }, 1500)
                    }
                });
            }
            else {
                //mostrar modal again
                $('#EditCountries').modal('show');
            }
        });
    }
})

function ValiContriesEdit() {
    LimpiarSpanMessa();
    var result = true;
    var cou_Description = $('#EditCountries #cou_Description');
    result = MessagesError(cou_Description, null, 100, 'Descripción');
    return result;
}

function CleanCountriesEdit() {
    $('#EditCountries #cou_Description').css("border-color", "#eee");
}

$('#EditCountries #cou_Description').on('keypress', function () {
    $('#EditCountries #cou_Description').css("border-color", "#eee");
});
