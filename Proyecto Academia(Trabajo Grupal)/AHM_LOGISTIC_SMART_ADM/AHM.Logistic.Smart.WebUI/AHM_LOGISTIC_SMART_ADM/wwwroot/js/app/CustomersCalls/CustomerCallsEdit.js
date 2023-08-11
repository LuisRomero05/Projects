var cati_Id = 0;
var cabu_Id = 0;
var caru_Id = 0;
var cus_Id = 0;
var cca_Id = 0;


function ShowModalEditCalls(id) {
    cca_Id = id;
    GetCallsDetails(id);
    $('#EditCalls').modal('show');
}

function timeToSeconds(time) {
    time = time.split(/:/);
    return time[0] * 360 + time[1] * 60 + time[2] * 1;
}


function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}


function GetCallsDetails(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/CustomersCalls/Edit/" + id,
    }).done(function (data) {
        var dateAPI = data.data.cca_Date;
        var date = getFormattedDate(dateAPI);
        var starttime = data.data.cca_StartTime;
        var endtime = data.data.cca_EndTime;
        $("#EditCalls #cca_DateMdl").val(date);
        $("#EditCalls #cca_StartTimeMdl").val(starttime);
        $("#EditCalls #cca_EndTimeMdl").val(endtime);
        cati_Id = 0;
        cabu_Id = 0;
        caru_Id = 0;
        cati_Id = data.data.cca_CallType;
        cabu_Id = data.data.cca_Business;
        caru_Id = data.data.cca_Result;
        cus_Id = data.data.cus_Id;
        GetTList(cati_Id);
        GetBList(cabu_Id);
        GetRList(caru_Id);
    });
}

function GetTList(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/CallType/CallTypeList",
    }).done(function (data, index) {
        //Vaciar el Dropdawn
        $("#CallTypeMdl").empty();

        //variable que almacena las notas
        var Options = "";

        data.data.forEach(function (item) {
            if (item.cati_Id == id) {
                Options += "<option value=" + item.cati_Id + " selected>" + item.cati_Description + "</option>";
            }
            else {
                Options += "<option value=" + item.cati_Id + ">" + item.cati_Description + "</option>";
            }
        });

        $("#CallTypeMdl").append(Options);
    });
}

function GetBList(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/CallBusiness/CallBusinessList",
    }).done(function (data, index) {

        $("#CallBusinessMdl").empty();

        var Options = "";

        data.data.forEach(function (item) {
            if (item.cabu_Id == id) {
                Options += "<option value=" + item.cabu_Id + " selected>" + item.cabu_Description + "</option>";
            }
            else {
                Options += "<option value=" + item.cabu_Id + ">" + item.cabu_Description + "</option>";
            }
        });

        $("#CallBusinessMdl").append(Options);
    });
}

function GetRList(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/CallResult/CallResultList",
    }).done(function (data, index) {

        $("#CallResultMdl").empty();

        var Options = "";

        data.data.forEach(function (item) {
            if (item.caru_Id == id) {
                Options += "<option value=" + item.caru_Id + " selected>" + item.caru_Description + "</option>";
            }
            else {
                Options += "<option value=" + item.caru_Id + ">" + item.caru_Description + "</option>";
            }
        });

        $("#CallResultMdl").append(Options);
    });
}

$('#EditCalls #EditCallConfirm').on('click', function () {
    var result = ValidateEditCalls();
    if (result == true) {
        Swal.fire({
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar los cambios?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonColor: '#6c757d',
            cancelButtonColor: '#001f52',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                data = [
                    { name: "cus_Id", value: cus_Id },
                    { name: "cca_Result", value: $('#EditCalls #CallResultMdl').val() },
                    { name: "cca_StartTime", value: $('#EditCalls #cca_StartTimeMdl').val() },
                    { name: "cca_Date", value: $('#EditCalls #cca_DateMdl').val() },
                    { name: "cca_Business", value: $('#EditCalls #CallBusinessMdl').val() },
                    { name: "cca_CallType", value: $('#EditCalls #CallTypeMdl').val() },
                    { name: "cca_Id", value: cca_Id },
                    { name: "cca_EndTime", value: $('#EditCalls #cca_EndTimeMdl').val() },
                ];
                console.log(data);
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/CustomersCalls/Edit/",
                    data: data,

                }).done(function (data) {
                    location.reload();
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                });
            }
        })
    }
})

function ValidateEditCalls() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;
    var cca_CallType = $('#EditCalls #CallTypeMdl');
    var cca_Business = $('#EditCalls #CallBusinessMdl');
    var cca_Date = $('#EditCalls #cca_DateMdl');
    var cca_StartTime = $('#EditCalls #cca_StartTimeMdl');
    var cca_EndTime = $('#EditCalls #cca_EndTimeMdl');
    var cca_Result = $('#EditCalls #CallResultMdl');

    result = MessagesError(cca_CallType, null, null, 'Tipo de llamada');
    if (result == true) { count++; }

    result = MessagesError(cca_Business, null, null, 'Tipo de negocio');
    if (result == true) { count++; }

    result = MessagesError(cca_Date, null, 10, 'Fecha');
    if (result == true) { count++; }

    result = MessagesError(cca_StartTime, null, 10, 'Hora de inicio');
    if (result == true) { count++; }

    result = MessagesError(cca_EndTime, null, 10, 'Hora de fin');
    if (result == true) { count++; }

    result = MessagesError(cca_Result, null, null, 'Resultado');
    if (result == true) { count++; }

    if (count == 6) {
        return result;
    }
    event.preventDefault();
    return false;
}

