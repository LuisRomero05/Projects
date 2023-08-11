

function ValidateFrmCreateCustomerCalls() {
    LimpiarSpanMessa();
    var StartTime = $('#cca_StartTime').val();
    var EndTime = $('#cca_EndTime').val();

    if (StartTime.toString() == "00:00") {
        event.preventDefault();
        iziToast.warning({
            message: '¡La hora de inicio no es valida!'
        });
    }
    else if (EndTime.toString() == "00:00") {
        event.preventDefault();
        iziToast.warning({
            message: '¡La hora de fin no es valida!'
        });
    }
    else if (StartTime > EndTime) {
        event.preventDefault();
        iziToast.warning({
            message: '¡La hora de fin de la llamada no puede ser menor que la hora de inicio, cámbielo, por favor!'
        });
    }
    var result = true;
    var count = 0;
    var cca_CallType = $('#frmCreateCustomerCalls #VCallType');
    var cca_Business = $('#frmCreateCustomerCalls #VCallBusiness');
    var cca_Date = $('#frmCreateCustomerCalls #cca_Date');
    var cca_StartTime = $('#frmCreateCustomerCalls #cca_StartTime');
    var cca_EndTime = $('#frmCreateCustomerCalls #cca_EndTime');
    var cca_Result = $('#frmCreateCustomerCalls #VCallResult');

    var divCallType = $('#frmCreateCustomerCalls #CallType');
    var divCallBuss = $('#frmCreateCustomerCalls #CallBusiness');
    var divCallResult = $('#frmCreateCustomerCalls #CallResult');

    var BordCallType = $('#frmCreateCustomerCalls #CallType #divPrioridad span.select2-selection.select2-selection--single');
    var BordCallBuss = $('#frmCreateCustomerCalls #CallBusiness #divPrioridad span.select2-selection.select2-selection--single');
    var BordCallResult = $('#frmCreateCustomerCalls #CallResult #divPrioridad span.select2-selection.select2-selection--single');

    result = MessageErrorDrop(cca_CallType, 'tipo de llamada', divCallType, BordCallType);
    if (result == true) { count++; }

    result = MessageErrorDrop(cca_Business, 'tipo de negocio', divCallBuss, BordCallBuss);
    if (result == true) { count++; }

    result = MessagesError(cca_Date, null, 10, 'fecha');
    if (result == true) { count++; }

    result = MessagesError(cca_StartTime, null, 5, 'hora de inicio');
    if (result == true) { count++; }

    result = MessagesError(cca_EndTime, null, 5, 'hora de fin');
    if (result == true) { count++; }

    result = MessageErrorDrop(cca_Result, 'resultado', divCallResult, BordCallResult);
    if (result == true) { count++; }

    if (count == 6) {
        return result;
    }
    event.preventDefault();
    return false;
}

$('#frmCreateCustomerCalls #cca_Date').on('keypress', function () {
    $('#frmCreateCustomerCalls #cca_Date').css("border-color", "#eee");
});
$('#frmCreateCustomerCalls #cca_StartTime').on('keypress', function () {
    $('#frmCreateCustomerCalls #cca_StartTime').css("border-color", "#eee");
});
$('#frmCreateCustomerCalls #cca_EndTime').on('keypress', function () {
    $('#frmCreateCustomerCalls #cca_EndTime').css("border-color", "#eee");
});
$('#frmCreateCustomerCalls #CallResult').on('keypress', function () {
    $('#frmCreateCustomerCalls #CallResult').css("border-color", "#eee");
});