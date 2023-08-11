var done = false;
$('#frmCreateCampaigns').on('submit', function () {
    var result = ValiFrmCampaignCreate();
    if (result == true) {
        var campaign = [
            { name: "cam_Nombre", value: $('#frmCreateCampaigns #cam_Nombre').val().trim() },
            { name: "cam_Descripcion", value: $('#frmCreateCampaigns #cam_Descripcion').val().trim() },
            { name: "cam_IdUserCreate", value: UserId },
            { name: "cam_Html", value: CKEDITOR.instances['cam_Html'].getData() },
        ];
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Campaign/Create",
            data: campaign,
        }).done(function (data) {
            if (data.data == null) {
                if (done == false) {
                    done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    setTimeout(function () {
                        location.assign(BaseUrl + "/campaign/index");
                    }, 1500)
                }
            }
            else {
                if (done == false) {
                    done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                }
                setTimeout(function () {
                    location.assign(BaseUrl + "/campaign/index");
                }, 1500)
            }
        }).fail(function (data) {
            if (done == false) {
                NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            }
            setTimeout(function () {
                location.assign(BaseUrl + "/campaign/index");
            }, 1500)
        });

    }
    return false;
});


function ValiFrmCampaignCreate() {
    var result = true;
    var count = 0;
    var cam_Nombre = $('#frmCreateCampaigns #cam_Nombre');
    var cam_Descripcion = $('#frmCreateCampaigns #cam_Descripcion');
    var cam_Html = CKEDITOR.instances['cam_Html'].getData();
    result = MessagesError(cam_Nombre, null, 100, 'Nombre');
    if (result == true) { count++; }
    result = MessagesError(cam_Descripcion, null, 500, 'Description');
    if (result == true) { count++; }
    if (cam_Html == "") {
        var message = 'Una presentación es requerida';
        $('#frmCreateCampaigns #Errorcam_Html').text(message).fadeToggle(3000);
        count++;
    }
    if (count == 2) {
        return result;
    }

    return false;

};

$('#frmCreateCampaigns #cam_Nombre').on('keypress', function () {
    $('#frmCreateCampaigns #cam_Nombre').css("border-color", "#eee");
});
$('#frmCreateCampaigns #cam_Descripcion').on('keypress', function () {
    $('#frmCreateCampaigns #cam_Descripcion').css("border-color", "#eee");
});