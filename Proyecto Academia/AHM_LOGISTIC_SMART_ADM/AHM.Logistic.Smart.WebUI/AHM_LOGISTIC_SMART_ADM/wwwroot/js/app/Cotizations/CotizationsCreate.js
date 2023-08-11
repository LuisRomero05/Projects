function ValidateFrmCotizations() {
    var result = false;
    var count = 0;
    var div_Clientes = $('#DivCustomers');
    var cot_Clientes = $('#DivCustomers #SelectCustomers');
    var cot_FechaExpiracion = $('#fechaexpiracion');
    var cot_Tabla = $('#tableContent');

    var borCus = $('#DivCustomers span.select2-selection.select2-selection--single');

    result = MessageErrorDrop(cot_Clientes, 'Cliente', div_Clientes, borCus);
    if (result == true) { count++; }
    result = MessagesError(cot_FechaExpiracion, null, null, 'Fecha de Expiración');
    if (result == true) { count++; }
    if (tablaPro != "") { count++; } else {
        MessagesError(cot_Tabla, null, null, 'Detalle');
    }
    if (count == 3) {
        return result;
    }
    return false;
}

function ValidateFrmCotizationsDetails() {
    var result = false;
    var count = 0;
    var div_Producto = $('#divPro');
    var cot_Producto = $('#divPro #SelectProduct');
    var cot_Cantidad = $('#Cant');
    var borPro = $('#divPro span.select2-selection.select2-selection--single');

    result = MessageErrorDrop(cot_Producto, 'Producto', div_Producto, borPro);
    if (result == true) { count++; }
    result = MessagesError(cot_Cantidad, 1, 20, 'Cantidad');
    if (result == true) { count++; }
    if (count == 2) {
        return result;
    }
    return false;
}
$('#SelectProduct').on('select2:select', function () {
    $('#divPro span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#SelectCustomers').on('select2:select', function () {
    $('#DivCustomers span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#fechaexpiracion').on('change', function () {
    $('#fechaexpiracion').css("border-color", "#eee");
});
$('#Cant').on('keypress', function () {
    $('#Cant').css("border-color", "#eee");
});