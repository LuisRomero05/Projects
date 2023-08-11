
function ValidateFrmCreateCustomerNote() {
    LimpiarSpanMessa();
    document.getElementById('cun_IdUserCreate').value = UserId;
    var result = true;
    var count = 0;
    var cun_Descripcion = $('#frmCreateCustomerNote #cun_Descripcion');
    var cun_ExpirationDate = $('#frmCreateCustomerNote #cun_ExpirationDate');
    var pry_Id = $('#frmCreateCustomerNote #priorities');
    var divMesa = $("#frmCreateCustomerNote #divPrioridad");

    var BordMesa = $("#frmCreateCustomerNote #divPrioridad span.select2-selection.select2-selection--single");

    result = MessagesError(cun_Descripcion, 2, null, 'descripción');
    if (result == true) { count++; }

    result = MessagesError(cun_ExpirationDate, null, 10, 'fecha de expiracion');
    if (result == true) { count++; }

    result = MessageErrorDrop(pry_Id, 'prioridad', divMesa, BordMesa);
    if (result == true) { count++; }

    if (count == 3) {
        return result;
    }
    $(function () {
        $('body').find('input[type=submit]').each(function () {
            $(this).attr('disabled', 'disabled');
        });
    });
    event.preventDefault();
    return false;
}
$('#frmCreateCustomerNote #cun_Descripcion').on('keypress', function () {
    $('#frmCreateCustomerNote #cun_Descripcion').css("border-color", "#eee");
});
$('#frmCreateCustomerNote #cun_ExpirationDate').on('keypress', function () {
    $('#frmCreateCustomerNote #cun_ExpirationDate').css("border-color", "#eee");
});
$('#frmCreateCustomerNote #priorities').on('keypress', function () {
    $('#frmCreateCustomerNote #priorities').css("border-color", "#eee");
});
$('#frmCreateCustomerNote #divPrioridad').on('keypress', function () {
    $('#frmCreateCustomerNote #divPrioridad').css("border-color", "#eee");
});