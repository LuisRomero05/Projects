function ValidateFrmOrders() {
    var result = false;
    var count = 0;
    var cot_Clientes = $('#SelectCustomers'); 
    var cot_Tabla = $('#tableContent');

    result = MessagesError(cot_Clientes, null, null, 'Cliente');
    if (result == true) { count++; }
    if (tablaPro != "") { count++; } else {
        MessagesError(cot_Tabla, null, null, 'Detalle');
    }
    if (count == 2) {
        return result;
    }
    return false;
}

function ValidateFrmOrdersDetails() {
    var result = false;
    var count = 0;
    var cot_Producto = $('#SelectProduct');
    var cot_Cantidad = $('#Cant');

    result = MessagesError(cot_Producto, null, null, 'Producto');
    if (result == true) { count++; }
    result = MessagesError(cot_Cantidad, 1, 20, 'Cantidad');
    if (result == true) { count++; }
    if (count == 2) {
        return result;
    }
    return false;
}
$('#SelectProduct').on('change', function () {
    $('#SelectProduct').css("border-color", "#eee");
});
$('#SelectCustomers').on('change', function () {
    $('#SelectCustomers').css("border-color", "#eee");
});
$('#Cant').on('keypress', function () {
    $('#Cant').css("border-color", "#eee");
});