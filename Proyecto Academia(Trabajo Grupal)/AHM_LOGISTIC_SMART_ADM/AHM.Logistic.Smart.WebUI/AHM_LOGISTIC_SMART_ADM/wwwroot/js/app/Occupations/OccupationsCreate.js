var done = false;
//click que abre el modal de crear
$("#OpenModalCreateOccupation").click(function () {
    //mostrar el modal
    $('#CreateOccupation').modal('show');
    CleanOccupation();
});
//click que confirma la creación del registro
$("#CreateOccupation #CreateOccupatioConfirm").click(function () {
    var result = ValidationOccupationCreate();
    if (result == true) {

        var occupation = [
            { name: "occ_Description", value: $("#CreateOccupation #occ_Description").val().trim() },
            { name: "occ_IdUserCreate", value: UserId },
            { name: "occ_IdUserModified", value: null },
        ];

        //crear el objeto con los valores seleccionados
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Occupation/Create",
            data: occupation,
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
    return false;
});

function ValidationOccupationCreate() {
    LimpiarSpanMessa();
    var result = true;
    var occ_Description = $('#CreateOccupation #occ_Description');
    result = MessagesError(occ_Description, null, 100, 'Descripción');
    return result;
}

function CleanOccupation() {
    $('#CreateOccupation #occ_Description').val("");
    $('#CreateOccupation #occ_Description').css("border-color", "#eee");
}

$('#CreateOccupation #occ_Description').on('keypress', function () {
    $('#CreateOccupation #occ_Description').css("border-color", "#eee");
});