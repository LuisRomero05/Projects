$(document).ready(function () {
    $('#DropDownPersonas').select2({
        dropdownParent: $('#CrearEmpleado')
    });
    $('#DropDownOcupaciones').select2({
        dropdownParent: $('#CrearEmpleado')
    });

    $('#DropDownAreas').select2({
        dropdownParent: $('#CrearEmpleado')
    });
});


var done = false;
//click que abre el modal de crear

$("#AbrirModalEmployees").click(function () {

    //llamar las funciones que cargan los dropdownlist
    EmployeesGetPersonCreate();
    EmployeesGetOccupationsCreate();
    EmployeesGetAreasCreate();
    //mostrar el modal
    $('#CrearEmpleado').modal('show');
    CleanEmployees();
});

function CleanEmployees() {
    $('#CrearEmpleado #DropDownPersonas').val("");
    $('#CrearEmpleado #DropDownPersonas').css("border-color", "#eee");
    $('#CrearEmpleado #DropDownAreas').val("");
    $('#CrearEmpleado #DropDownAreas').css("border-color", "#eee");
    $('#CrearEmpleado #DropDownOcupaciones').val("");
    $('#CrearEmpleado #DropDownOcupaciones').css("border-color", "#eee");
}

//click que confirma la creación de una persona
$('#CrearEmpleado #CrearPersonaConfirmar').on('click', function () {
    var result = ValiFrmEmployeesCreate()
    if (result == true) {
        var data = [
            { name: "per_Id", value: $("#CrearEmpleado #DropDownPersonas").val() },
            { name: "are_Id", value: $("#CrearEmpleado #DropDownAreas").val() },
            { name: "occ_Id", value: $("#CrearEmpleado #DropDownOcupaciones").val() },
            { name: "emp_IdUserCreate", value: UserId },
            { name: "emp_IdUserModified", value: null },
        ];

        //Insertar la persona
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Employers/Create",
            data: data,
        })
            .done(function (data) {
            if (data.data == null) {
                if (done == false) {
                    done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                }
                //setTimeout(function () {
                //    location.assign(BaseUrl + "/employers/index");
                //}, 1500)
            }
            else {
                done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                setTimeout(function () {
                    location.assign(BaseUrl + "/employers/index");
                }, 1500)
            }
        }).fail(function (data) {
            if (done == false) {
                NotificationMessage(data.success, "", data.id, data.data, data.type);
            }
        });
    }
})

//funciones para obtener los listados
function EmployeesGetPersonCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Persons/PersonsList",
    }).done(function (data) {
        //Vaciar el dropdownlist
        $("#CrearEmpleado #DropDownPersonas").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0"> Por favor seleccione una opción... </option>`;

        data.data.forEach(function (item, index, array) {
            /*NewOption += `<option value="${item.per_Id}"> ${item.per_Firstname} ${item.per_LastNames} </option>`;*/
            NewOption += `<option value="${item.per_Id}"> ${item.per_Firstname} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#CrearEmpleado #DropDownPersonas").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPersonas').after($(message));
    });
}

function EmployeesGetOccupationsCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Occupation/OccupationList",
    }).done(function (data) {

        //Vaciar el dropdownlist
        $("#CrearEmpleado #DropDownOcupaciones").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0"> Por favor seleccione una opción... </option>`;

        data.data.forEach(function (item, index, array) {
            NewOption += `<option value="${item.occ_Id}"> ${item.occ_Description} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#CrearEmpleado #DropDownOcupaciones").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownOcupaciones').after($(message));
    });
}

function EmployeesGetAreasCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Areas/AreasList",
    }).done(function (data) {

        //Vaciar el dropdownlist
        $("#CrearEmpleado #DropDownAreas").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0"> Por favor seleccione una opción... </option>`;

        data.data.forEach(function (item, index, array) {
            NewOption += `<option value="${item.are_Id}"> ${item.are_Description} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#CrearEmpleado #DropDownAreas").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownAreas').after($(message));
    });
}

function ValiFrmEmployeesCreate() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;
    var DropDownPersonas = $('#CrearEmpleado #DropDownPersonas');
    var DropDownOcupaciones = $('#CrearEmpleado #DropDownOcupaciones');
    var DropDownAreas = $('#CrearEmpleado #DropDownAreas');

    var divPersonas = $('#CrearEmpleado #divPersonas');
    var divPuesto = $('#CrearEmpleado #divPuesto');
    var divAreas = $('#CrearEmpleado #divAreas');

    var borPersonas = $('#CrearEmpleado #divPersonas span.select2-selection.select2-selection--single');
    var borPuestos = $('#CrearEmpleado #divPuesto span.select2-selection.select2-selection--single');
    var borAreas = $('#CrearEmpleado #divAreas span.select2-selection.select2-selection--single');


    result = MessageErrorDrop(DropDownPersonas, 'persona', divPersonas, borPersonas);
    if (result == true) { count++; }
    result = MessageErrorDrop(DropDownOcupaciones, 'ocupación', divPuesto, borPuestos);
    if (result == true) { count++; }
    result = MessageErrorDrop(DropDownAreas, 'área', divAreas, borAreas);
    if (result == true) { count++; }
    if (count == 3) {
        return result;
    }
    return false;
}

$('#CrearEmpleado #DropDownPersonas').on('change', function () {
    $('#CrearEmpleado #DropDownPersonas').css("border-color", "#eee");
});
$('#CrearEmpleado #DropDownAreas').on('change', function () {
    $('#CrearEmpleado #DropDownAreas').css("border-color", "#eee");
});
$('#CrearEmpleado #DropDownOcupaciones').on('change', function () {
    $('#CrearEmpleado #DropDownOcupaciones').css("border-color", "#eee");
});
