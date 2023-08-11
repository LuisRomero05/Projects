var done = false;
$("#AbrirModalCrear").on('click', function () {
    $('#CrearArea').modal('show');
    LimpiarControlesAreasCreate();
});

$('#CrearArea #CrAreaConfirm').on('click', function (e) {
    var result = ValiFrmAreaCreate();
    if (result == true) {

        var area = [
            { name: "are_Description", value: $("#CrearArea #are_Description").val().trim() },
            { name: "are_IdUserCreate", value: UserId },
            { name: "are_IdUserModified", value: null },
        ];


        $.ajax({
            type: "POST",
            url: BaseUrl + "/Areas/Create",
            data: area,
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
    return false;

});

function ValiFrmAreaCreate() {
    var result = true;
    var are_Description = $('#CrearArea #are_Description');
    result = MessagesError(are_Description, null, 40, 'Descripción');
    return result;
}

function LimpiarControlesAreasCreate() {
    $('#CrearArea #are_Description').val("");
    $('#CrearArea #are_Description').css("border-color", "#eee");
}

$('#CrearArea #are_Description').on('keypress', function () {
    $('#CrearArea #are_Description').css("border-color", "#eee");
});