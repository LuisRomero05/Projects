$(document).ready(function () {

    GetClientsListCreate2();
    GetOccupationsListCreate2();
});

function GetOccupationsListCreate2(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Occupation/OccupationList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#frmCreateContacts #SelectOccupations").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        data.data.forEach(function (item) {
            if (item.occ_Id == id) {
                NewOption += "<option  value=" + item.occ_Id + " selected>" + item.occ_Description + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.occ_Id + ">" + item.occ_Description + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#frmCreateContacts #SelectOccupations").append(NewOption);


    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#frmCreateContacts #SelectOccupations').after($(message));
    });
}

function GetClientsListCreate2(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Customers/CustomersList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#frmCreateContacts #SelectCustomers").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item) {
            if (item.cus_Id == id) {
                NewOption += "<option  value=" + item.cus_Id + " selected>" + item.cus_Name + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.cus_Id + ">" + item.cus_Name + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#frmCreateContacts #SelectCustomers").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#frmCreateContacts #SelectCustomers').after($(message));
    });
}


















