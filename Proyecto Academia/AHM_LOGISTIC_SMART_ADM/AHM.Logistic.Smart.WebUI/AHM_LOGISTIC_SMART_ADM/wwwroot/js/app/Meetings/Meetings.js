$(document).ready(function () {
    //INICIAR FUNCIONES
    GetInvitadosList();
});

var Id_cus = $("#cus_Id").val();
var done = false;

function EditMeeting(id) {
    location.assign(BaseUrl + "/meetings/edit/" + id);
}

function GetInvitadosList() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Meetings/ListCusEmp",
    }).done(function (data, index) {
        $("#invitados").empty();
        var Options = "";
        if (data.data != null) {
            Options = "<option value='0' selected> Por favor seleccione una opcion... </option>";
            data.data.forEach(function (item2) {
                Options += "<option value=" + item2.id + ">" + item2.name + "</option>";
            });
        } else {
            Options = "<option value='0' selected> No hay registros </option>";
        }
        $("#invitados").append(Options);
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar las prioridades");
    });
}


var tablaI = "";
var invitados = [];

function AddInvitados() {
    var idCus = $("#invitados").val();
    if (idCus == 0) {
        $("#btn_addInv").prop("disabled", false);
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Por favor seleccione un invitado!'
        });
    } else {
        var dropDown = document.getElementById("invitados");
        var NameCus = dropDown.options[dropDown.selectedIndex].text;
        var idConvert = Number(idCus);
        var agrega = true;
        var nombre = "";
        invitados.forEach(function (item) {
            if (idConvert == Number(item.ID) || idConvert == 0) {
                agrega = false;
                nombre = item.name_cus;
            }
        });
        if (agrega == true) {
            invitados.push({ ID: idCus, name_cus: NameCus });
            tablaI = "";
            $("#TbInvitaciones").empty();
            invitados.forEach(function (item) {
                tablaI += "<tr><td>" + item.ID + "</td><td>" + item.name_cus + "</td><td><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarInvitado(" + item.ID + ")'><span class='btn-inner'><svg style='color: white;' width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a></td></tr>"
            });
            $("#TbInvitaciones").append(tablaI);
            iziToast.success({
                displayMode: 2,
                message: '¡Se ha invitado a ' + NameCus + '!'
            });
        } else if (agrega == false) {
            iziToast.warning({
                title: 'Advertencia',
                displayMode: 1,
                message: '¡' + nombre + ' ya esta invitado!'
            });
        }
        agrega = true;
    }
    GetInvitadosList();
}

function EliminarInvitado(id) {
    var cont = 0;
    invitados.forEach(function (item) {
        if (item.ID == id) {
            invitados.splice(cont, 1);
        }
        cont++
    });

    tablaI = "";
    $("#TbInvitaciones").empty();
    invitados.forEach(function (item) {
        tablaI += "<tr><td>" + item.ID + "</td><td>" + item.name_cus + "</td><td><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarInvitado(" + item.ID + ")'><span class='btn-inner'><svg style='color: white;' width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a></td></tr>"
    });
    $("#TbInvitaciones").append(tablaI);
}

//REALIZAR REGISTRO A LA BASE DE DATOS
function InsertarMeeting() {
    var valida = ValiFrmMeetingsEdit();
    var StarTine = $("#met_StartTime").val();
    var EndTime = $("#met_EndTime").val();
    var met_date = new Date($("#met_Date").val());
    var fecha = Date.now();
    var fechaAhora = new Date(fecha);
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
            message: '¡El tiempo de fin de la reunión no puede ser menor o igual que el tiempo de inicio, cámbiela, por favor!'
        });
    }
    else if (invitados.length == 0) {
        iziToast.warning({
            title: 'Advertencia',
            displayMode: 1,
            message: '¡Tiene que invitar al menos a una persona para crear la reunion!'
        });
    }
    else {
        if (met_date < fechaAhora) {
            iziToast.warning({
                title: 'Advertencia',
                displayMode: 1,
                message: '¡No puedes agendar la reunión para una fecha pasada, cámbiela, por favor!'
            });
            event.preventDefault();
        }
        else {
            if (valida == true) {

                var titulo = $("#met_Title").val();
                var MeetLink = $("#met_MeetingLink").val();
                var StarTine = $("#met_StartTime").val();
                var EndTime = $("#met_EndTime").val();
                var met_date = $("#met_Date").val();
                var InsertInv = [];
                invitados.forEach(function (item) {
                    InsertInv.push({
                        "met_Id": 0,
                        "mix_Id": item.ID,
                        "mde_IdUserCreate": TempUserDefault,
                        "mde_IdUserModified": 0,
                    });
                });
                var InsertMeet = {
                    "cus_Id": Id_cus,
                    "met_Title": titulo,
                    "met_MeetingLink": MeetLink,
                    "met_Date": met_date,
                    "met_StartTime": StarTine,
                    "met_EndTime": EndTime,
                    "met_IdUserCreate": 1,
                    "met_IdUserModified": 0,
                    "met_details": InsertInv
                };

                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Meetings/create",
                    data: InsertMeet,
                    dataType: "json",
                }).done(function (data) {
                    location.reload();
                }).fail(function (data) {
                    location.reload();
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
    var met_date = $('#met_Date');

    result = MessagesError(met_Title, null, 100, 'titulo de la reunion');
    if (result == true) { count++; }
    result = MessagesError(met_MeetingLink, null, null, 'link de la reunion');
    if (result == true) { count++; }
    result = MessagesError(met_StartTime, null, null, 'tiempo de inicio');
    if (result == true) { count++; }
    result = MessagesError(met_EndTime, null, null, 'tiempo de fin');
    if (result == true) { count++; }
    result = MessagesError(met_date, null, null, 'fecha');
    if (result == true) { count++; }

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
$('#met_Date').on('change', function () {
    $('#met_Date').css("border-color", "#eee");
});
