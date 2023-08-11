$(document).ready(function () {
    $('#DropDownPaisDE').select2({
        dropdownParent: $('#EditarDepart')
    });
});

//almacenar el id del registro modificado
var dep_Id = 0;

//click que abre el modal de crear
function ShowModalEditDepart(id) {
    //cambiar el valor de emp_Id
    dep_Id = id;
    //llamar la funció que recupera el detalle
    GetDepartDetails(id);
    GetSubPaisListEditDepart(id);
    //mostrar el modal
    $('#EditarDepart').modal('show');
    cleanEditarDepart();
}
$("#EditarDepartaCancelar").click(function () {
    cleanEditarDepart();
});

function cleanEditarDepart() {
    $('#EditarDepart #dep_CodeDE').css("border-color", "#eee");
    $('#EditarDepart #dep_DescriptionDE').css("border-color", "#eee");
    $('#EditarDepart #DropDownPaisDE').css("border-color", "#eee");
}

//click que confirma la creación de un area
$("#EditarDepart #EditarDepartConfirmar").click(function (e) {
    e.preventDefault();
    e.stopImmediatePropagation();
    var result = ValidateEditMdlDepart();
    if (result == true) {
        //crear el objeto con los valores seleccionados 

        //crear el objeto con los valores seleccionados
        $('#EditarDepart').modal('hide');
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

        }).then((Edit) => {
            if (Edit.isConfirmed) {


                var data = [
                    { name: "dep_Code", value: $("#EditarDepart #dep_CodeDE").val() },
                    { name: "dep_Description", value: $("#EditarDepart #dep_DescriptionDE").val().trim() },
                    { name: "cou_Id", value: $("#EditarDepart #DropDownPaisDE").val() },
                    { name: "dep_IdUserCreate", value: null },
                    { name: "dep_IdUserModified", value: UserId },
                ];
                //Insertar la persona
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Depart/Edit?Id=" + dep_Id,
                    data: data,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/Depart/index");
                        }, 1500)
                    }
                })

            }
            else {
                //mostrar el modal
                $('#EditarDepart').modal('show');
            }
        });



    }
});


//funcion para obtener la información del empleado
function GetDepartDetails(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/Edit/" + id,
    }).done(function (data) {
        $("#EditarDepart #dep_CodeDE").val(data.data.dep_Code);
        $("#EditarDepart #dep_DescriptionDE").val(data.data.dep_Description);
        var sele = 0;
        sele = data.data.cou_Id;
        GetSubPaisListEditDepart(sele);

    }).fail(function () {
    });
}
function GetSubPaisListEditDepart(selec) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Countries/CountriesList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#DropDownPaisDE").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + " disabled> Por favor seleccione una pais... </option>";

        data.data.forEach(function (item) {
            if (item.cou_Id == selec) {
                NewOption += "<option value=" + item.cou_Id + " selected>" + item.cou_Description + "</option>";
            }
            else {
                NewOption += "<option value=" + item.cou_Id + ">" + item.cou_Description + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownPaisDE").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPaisDE').after($(message));
    });
}

function ValidateEditMdlDepart() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var CodeDE = $('#EditarDepart #dep_CodeDE');
    var DescriptionDE = $('#EditarDepart #dep_DescriptionDE');
    var DropDownPaisDE = $('#EditarDepart #DropDownPaisDE');
    var divMesa = $('#EditarDepart #messaEdi');
    var el = $("#EditarDepart #messaEdi span.select2-selection.select2-selection--single");

    result = MessagesError(CodeDE, null, 4, 'código departemento');
    if (result == true) { count++; }

    result = MessagesError(DescriptionDE, null, null, "departamento");
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownPaisDE, 'pais', divMesa,el);
    if (result == true) { count++; }
    if (count == 3) {
        return result;
    }
    return false;
}

$('#EditarDepart #dep_CodeDE').on('keypress', function () {
    $('#EditarDepart #dep_CodeDE').css("border-color", "#eee");
});
$('#EditarDepart #dep_DescriptionDE').on('keypress', function () {
    $('#EditarDepart #dep_DescriptionDE').css("border-color", "#eee");
});
$('#EditarDepart #DropDownPaisDE').on('select2:select', function () {
    $("#EditarDepart #messaEdi span.select2-selection.select2-selection--single").css("border-color", "#eee");
});


$('#EditarDepart #dep_CodeDE').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});
function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}