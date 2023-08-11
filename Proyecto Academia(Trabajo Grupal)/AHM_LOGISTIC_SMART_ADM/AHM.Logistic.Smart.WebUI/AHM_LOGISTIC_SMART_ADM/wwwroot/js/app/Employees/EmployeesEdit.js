$(document).ready(function () {
    $('#DropDownPersonas2').select2({
        dropdownParent: $('#EditarEmpleado')
    });
    $('#DropDownOcupaciones2').select2({
        dropdownParent: $('#EditarEmpleado')
    });

    $('#DropDownAreas2').select2({
        dropdownParent: $('#EditarEmpleado')
    });
});

//almacenar el id del empleado modificado
var done = false;
var emp_Id = 0;

//click que abre el modal de crear
function ShowModalEditEmployees(id) {

    //cambiar el valor de emp_Id
    emp_Id = id;
    //llamar la funció que recupera el detalle
    GetEmployeesDetail(id);
    //mostrar el modal
    $('#EditarEmpleado').modal('show');

}


//click que confirma la creación de una persona
$("#EditarEmpleado #EditarPersonaConfirmar").click(function () {
    //crear el objeto con los valores seleccionados

    var result = ValiFrmEmployeesEdit()
    if (result == true) {
        $('#EditarEmpleado').modal('hide');
        //ocultar modal 
        Swal.fire({
            closeOnClickOutside: false,
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar este registro?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#001f52',
            cancelButtonColor: '#6c757d',

        }).then((eliminar) => {
            if (eliminar.isConfirmed) {
                //aqui se inserta la data para editar lol
                var data = [
                    { name: "per_Id", value: $("#EditarEmpleado #DropDownPersonas2").val() },
                    { name: "are_Id", value: $("#EditarEmpleado #DropDownAreas2").val() },
                    { name: "occ_Id", value: $("#EditarEmpleado #DropDownOcupaciones2").val() },
                    { name: "emp_IdUserCreate", value: null },
                    { name: "emp_IdUserModified", value: UserId },
                ];

                //Insertar la persona
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Employers/Edit?Id=" + emp_Id,
                    data: data,
                }).done(function (data) {
                    if (done == false) {
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
            else {
                //mostrar modal again
                $('#EditarEmpleado').modal('show');
            }
        });
    }


});

//funcion para obtener la información del empleado
function GetEmployeesDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Employers/Edit/" + id,
    }).done(function (data) {
        $("#EditarEmpleado #txtNombreCompleto").val(data.data.per_Firstname + " " + data.data.per_LastNames);
        EmployeesGetPersonDetail(data.data.per_Id);
        EmployeesGetOccupationsDetail(data.data.occ_Id);
        EmployeesGetAreasDetail(data.data.are_Id);
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al recuperar la información del empleado");
        NotificationSuccess(data);
    });
}


//funciones para obtener los listados
function EmployeesGetPersonDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Persons/PersonsList",
    }).done(function (data) {
        //Vaciar el dropdownlist
        $("#EditarEmpleado #DropDownPersonas2").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0" disabled=""> Por favor seleccione una opción... </option>`;
        data.data.forEach(function (item, index, array) {
            if (item.per_Id == id)
                /* NewOption += `<option value="${item.per_Id}" selected> ${item.per_Firstname} ${item.per_LastNames} </option>`;*/
                NewOption += `<option value="${item.per_Id}" selected> ${item.per_Firstname} </option>`;
            else
            /*NewOption += `<option value="${item.per_Id}"> ${item.per_Firstname} ${item.per_LastNames} </option>`;*/
                NewOption += `<option value="${item.per_Id}"> ${item.per_Firstname} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#EditarEmpleado #DropDownPersonas2").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPersonas2').after($(message));
    });
}

function EmployeesGetOccupationsDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Occupation/OccupationList",
    }).done(function (data) {
        //Vaciar el dropdownlist
        $("#EditarEmpleado #DropDownOcupaciones2").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0" disabled=""> Por favor seleccione una opción... </option>`;

        data.data.forEach(function (item, index, array) {
            /*            NewOption += `<option value="${item.occ_Id}"> ${item.occ_Description} </option>`;*/
            if (item.occ_Id == id)
                NewOption += `<option value="${item.occ_Id}" selected> ${item.occ_Description} </option>`;
            else
                NewOption += `<option value="${item.occ_Id}"> ${item.occ_Description} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#EditarEmpleado #DropDownOcupaciones2").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownOcupaciones2').after($(message));
    });
}

function EmployeesGetAreasDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Areas/AreasList",
    }).done(function (data) {
        //Vaciar el dropdownlist
        $("#EditarEmpleado #DropDownAreas2").empty();
        //variable que almacena las opciones
        var NewOption = `<option value="0" disabled=""> Por favor seleccione una opción... </option>`;

        data.data.forEach(function (item, index, array) {
            if (item.are_Id == id)
                NewOption += `<option value="${item.are_Id}" selected> ${item.are_Description} </option>`;
            else
                NewOption += `<option value="${item.are_Id}"> ${item.are_Description} </option>`;
        });

        //Agregar las opciones al dropdownlist
        $("#EditarEmpleado #DropDownAreas2").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownAreas2').after($(message));
    });
}

function ValiFrmEmployeesEdit() {
    LimpiarSpanMessa();
    var count = 0;
    var result = true;

    var DropDownPersonas2 = $('#EditarEmpleado #DropDownPersonas2');
    var DropDownOcupaciones2 = $('#EditarEmpleado #DropDownOcupaciones2');
    var DropDownAreas2 = $('#EditarEmpleado #DropDownAreas2');

    var divEditaP = $('#EditarEmpleado #divEditaP');
    var divEditaO = $('#EditarEmpleado #divEditaO');
    var divEditaA = $('#EditarEmpleado #divEditaA');

    var borEditaP = $('#EditarEmpleado #divEditaP span.select2-selection.select2-selection--single');
    var borEditaO = $('#EditarEmpleado #divEditaO span.select2-selection.select2-selection--single');
    var borEditaA = $('#EditarEmpleado #divEditaA span.select2-selection.select2-selection--single');
    
    result = MessageErrorDrop(DropDownPersonas2, 'persona', divEditaP, borEditaP);
    if (result == true) { count++; }
    result = MessageErrorDrop(DropDownOcupaciones2, 'ocupación', divEditaO, borEditaO);
    if (result == true) { count++; }
    result = MessageErrorDrop(DropDownAreas2, 'área', divEditaA, borEditaA);
    if (result == true) { count++; }
    if (count == 3) {
        return result;
    }
    return false;
}

$('#EditarEmpleado #DropDownPersonas2').on('change', function () {
    $('#EditarEmpleado #DropDownPersonas2').css("border-color", "#eee");
});
$('#EditarEmpleado #DropDownAreas2').on('change', function () {
    $('#EditarEmpleado #DropDownAreas2').css("border-color", "#eee");
});
$('#EditarEmpleado #DropDownOcupaciones2').on('change', function () {
    $('#EditarEmpleado #DropDownOcupaciones2').css("border-color", "#eee");
});
