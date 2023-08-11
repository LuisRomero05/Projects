var are_Id = 0;
function ShowModalEditAreas(id) {
    are_Id = id;
    GetAreasDetail(id);
    $('#EditArea').modal('show');
    LimpiarControles();

}

function GetAreasDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Areas/Edit/" + id,
    }).done(function (data) {
        if(data.data != null)
            $("#EditArea #are_Description").val(data.data.are_Description);
        if (data.data == null)
            NotificationMessage(data.success, data.message, data.id, data.data, data.type);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#EditArea #are_Description').after($(message));
    });
}


$('#EditArea #EditAreaConfirm').on('click', function () {
    var result = ValiFrmAreaEdit()
    if (result == true) {
        var areas = [
            { name: "are_Description", value: $("#EditArea #are_Description").val().trim() },
            { name: "are_IdUserCreate", value: UserId },
            { name: "are_IdUserModified", value: UserId },
        ];

        $('#EditArea').modal('hide');
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

                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Areas/Edit?Id=" + are_Id,
                    data: areas,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/areas/index");
                        }, 1500)
                    }
                });
            }
            else {
                $('#EditArea').modal('show');
            }
        });
    }
});



function ValiFrmAreaEdit() {
    var result = true;
    var are_Description = $('#EditArea #are_Description');
    result = MessagesError(are_Description, null, 40, 'Descripción');
    return result;
}

function LimpiarControles() {
    $('#EditArea #are_Description').css("border-color", "#eee");
}

$('#EditArea #are_Description').on('keypress', function () {
    $('#EditArea #are_Description').css("border-color", "#eee");
});