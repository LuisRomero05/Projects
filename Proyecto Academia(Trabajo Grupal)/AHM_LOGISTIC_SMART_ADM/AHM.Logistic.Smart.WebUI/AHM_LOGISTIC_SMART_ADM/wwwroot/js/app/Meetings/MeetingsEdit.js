
var done = false;
var url = window.location.pathname;
var IdEnc = url.substring(url.lastIndexOf('/') + 1);


function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}
$(document).ready(function () {
    var setDate = $("#met_Date").val();
    $("#setMet_Date").val(getFormattedDate(setDate));
});

var desabilita = 0;

$("#met_Title").click(function () {
    $("#btn_addInv").prop("disabled", true);
    if (desabilita == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Presiona aceptar para guardar los cambios del encabezado!'
        });
        desabilita = 1;
    }
});
$("#met_MeetingLink").click(function () {
    $("#btn_addInv").prop("disabled", true);
    if (desabilita == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Presiona aceptar para guardar los cambios del encabezado!'
        });
        desabilita = 1;
    }
});
$("#met_StartTime").click(function () {
    $("#btn_addInv").prop("disabled", true);
    if (desabilita == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Presiona aceptar para guardar los cambios del encabezado!'
        });
        desabilita = 1;
    }
});
$("#met_EndTime").click(function () {
    $("#btn_addInv").prop("disabled", true);
    if (desabilita == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Presiona aceptar para guardar los cambios del encabezado!'
        });
        desabilita = 1;
    }
});
$("#setMet_Date").click(function () {
    $("#btn_addInv").prop("disabled", true);
    if (desabilita == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Presiona aceptar para guardar los cambios del encabezado!'
        });
        desabilita = 1;
    }
});


function AddInvitadoEdit() {
    $("#btn_addInv").prop("disabled", true);
    var dropDown = document.getElementById("invitados");
    var NameCus = dropDown.options[dropDown.selectedIndex].text;
    var idCus = $("#invitados").val();
    var confirm = -1;
    detallesTabla.forEach(function (detail) {
        if (detail.mix_Id == idCus) {
            confirm = 0;
        }
    });
    if (idCus != 0 && confirm == -1) {
        InsertInvitado = {
            "met_Id": IdEnc,
            "mix_Id": idCus,
            "cus_Id": 0,
            "emp_Id": 0,
            "cont_Id": 0,
            "mde_IdUserCreate": TempUserDefault,
            "mde_IdUserModified": 0,
        };
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Meetings/CreateDetails",
            data: InsertInvitado,
            dataType: "json",
        }).done(function (data) {
            iziToast.success({
                displayMode: 1,
                message: '¡Se ha invitado a ' + NameCus + '!'
            });
            setTimeout(function () {
                location.reload();
            }, 1500)
        }).fail(function (data) {
            iziToast.warning({
                title: 'Advertencia',
                displayMode: 1,
                message: 'Ha ocurrido un error! Comuniquese con el encargado'
            });
            setTimeout(function () {
                location.reload();
            }, 1500)
        });
    } else if (idCus == 0) {
        $("#btn_addInv").prop("disabled", false);
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Por favor seleccione un invitado!'
        });
    } else if (confirm >= 0) {
        $("#btn_addInv").prop("disabled", false);
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡' + NameCus + ' ya esta invitado!'
        });
    }

}

function EliminarInvitado(idDelete) {
    $.ajax({
        type: "DELETE",
        url: BaseUrl + "/Meetings/DeleteDetails?Id=" + idDelete,
        success: function (response) {
            iziToast.success({
                displayMode: 1,
                message: '¡El invitado se ha eliminado correctamente!'
            });
            setTimeout(function () {
                location.reload();
            }, 1500)
        }
    });
}

function ActualizarMeeting() {
    $("#btn_addInv").prop("disabled", false);
    InsertMeet = [];
    var valida = ValiFrmMeetingsEdit();
    var titulo = $("#met_Title").val();
    var MeetLink = $("#met_MeetingLink").val();
    var StarTine = $("#met_StartTime").val();
    var EndTime = $("#met_EndTime").val();
    var IdCustomer = $("#cus_Id").val();
    var met_date = new Date($("#Met_DateEdit").val()).toISOString().split('T')[0];
    var fecha = Date.now();
    var fechaAhora = new Date(fecha);
    var hoy_Day = fechaAhora.getDate();
    var hoy_Month = fechaAhora.getMonth() + 1;
    var hoy_Year = fechaAhora.getFullYear();
    var met_Day = parseInt(met_date.split('-')[2]);
    var met_Month = parseInt(met_date.split('-')[1]);
    var met_Year = parseInt(met_date.split('-')[0]);
    if (StarTine.toString() == "00:00") {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡El tiempo de inicio no es valido!'
        });
    }
    else if (EndTime.toString() == "00:00") {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡El tiempo de fin no es valido!'
        });
    }
    else if (StarTine >= EndTime) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡El tiempo de fin de la reunión no puede ser menor o igual que el tiempo de inicio, cámbielo, por favor!'
        });
    } else {

        if (met_Day < hoy_Day && met_Month < hoy_Month && hoy_Year < met_Year) {
            iziToast.warning({
                title: 'Advertencia',
                displayMode: 1,
                message: '¡No puedes agendar la reunión para una fecha pasada, cámbiela, por favor!'
            });
        } else {
            if (valida == true) {

                var InsertMeet = {
                    "cus_Id": IdCustomer,
                    "met_Id": IdEnc,
                    "met_Title": titulo,
                    "met_MeetingLink": MeetLink,
                    "met_StartTime": StarTine,
                    "met_EndTime": EndTime,
                    "met_Date": met_date,
                    "met_IdUserCreate": 0,
                    "met_IdUserModified": TempUserDefault,
                    "met_details": null,
                };
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Meetings/Edit",
                    data: InsertMeet,
                    dataType: "json",
                }).done(function (data) {
                    if (data.data == null) {
                        if (done == false) {
                            done = NotificationMessage(data.success, "", data.id, data.data, data.type);
                            setTimeout(function () {
                                location.reload();
                            }, 1500)
                        }
                    }
                    else {
                        if (done == false) {
                            done = NotificationMessage(data.success, "Resgistro actualizado exitosamente.", data.id, data.data, data.type);
                        }
                        setTimeout(function () {
                            location.reload();
                        }, 1500)
                    }
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                });
            }
        }
    }
}











function ValiFrmMeetingsEdit() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;
    var met_Title = $('#met_Title');
    var met_MeetingLink = $('#met_MeetingLink');
    var met_StartTime = $('#met_StartTime');
    var met_EndTime = $('#met_EndTime');
    var met_date = $("#setMet_Date");

    result = MessagesError(met_Title, null, 100, 'titulo de la reunion');
    if (result == true) { count++; }
    result = MessagesError(met_MeetingLink, 3, null, 'link de reunion');
    if (result == true) { count++; }
    result = MessagesError(met_StartTime, 5, null, 'tiempo de inicio');
    if (result == true) { count++; }
    result = MessagesError(met_EndTime, 5, null, 'tiempo de fin');
    if (result == true) { count++; }
    result = MessagesError(met_date, 1, null, 'fecha');
    if (result == true) { count++; }
    result
    if (count == 5) {
        return result;
    }
    event.preventDefault();
    return false;
}


$('#met_Title').on('change', function () {
    $('#met_Title').css("border-color", "#eee");
});
$('#met_MeetingLink').on('change', function () {
    $('#met_MeetingLink').css("border-color", "#eee");
});
$('#met_StartTime').on('change', function () {
    $('#met_StartTime').css("border-color", "#eee");
});
$('#met_EndTime').on('change', function () {
    $('#met_EndTime').css("border-color", "#eee");
});
$('#setMet_Date').on('change', function () {
    $('#setMet_Date').css("border-color", "#eee");
});