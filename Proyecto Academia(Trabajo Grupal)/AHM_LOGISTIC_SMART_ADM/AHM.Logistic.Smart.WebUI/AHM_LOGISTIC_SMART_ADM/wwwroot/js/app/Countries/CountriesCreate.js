var done = false;
$("#AbrirModalCountries").click(function () {
    $('#CreateCountries').modal('show');
    CleanCountries();
});

$('#CreateCountries #CreateCountriesConfirm').on('click', function () {

    var result = ValiContriesCreate();
    if (result == true) {
        var countries = [
            { name: "cou_Description", value: $("#CreateCountries #cou_Description").val().trim() },
            { name: "cou_IdUserCreate", value: UserId },
            { name: "cou_IdUserModified", value: null },
        ];

        //crear el objeto con los valores seleccionados
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Countries/Create",
            data: countries,
        }).done(function (data) {
            var result = true;
            result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            if (result) {
                setTimeout(function () {
                    location.assign(BaseUrl + "/countries/index");
                }, 1500)
            }
        });
    }
    return false;
});

function ValiContriesCreate() {
    LimpiarSpanMessa();
    var result = true;
    var cou_Description = $('#CreateCountries #cou_Description');
    result = MessagesError(cou_Description, null, 100, 'Descripción');
    return result;
}

function CleanCountries() {
    $('#CreateCountries #cou_Description').val("");
    $('#CreateCountries #cou_Description').css("border-color", "#eee");
}

$('#CreateCountries #cou_Description').on('keypress', function () {
    $('#CreateCountries #cou_Description').css("border-color", "#eee");
});
