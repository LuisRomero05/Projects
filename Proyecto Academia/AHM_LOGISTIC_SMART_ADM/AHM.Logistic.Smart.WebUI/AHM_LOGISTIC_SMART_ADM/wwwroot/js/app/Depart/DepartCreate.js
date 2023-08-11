$(document).ready(function () {
    $('#DropDownPaisDC').select2({
        dropdownParent: $('#CrearDepart')
    });
    console.log("Crear dep");
});

var done = false;

//click que abre el modal de crear
$("#AbrirModalCrearDepart").click(function () {
    cleanCrearDepart();
    GetSubPaisListCreateCount();
    //mostrar el modal
    $('#CrearDepart').modal('show');
    //$('#CrearEmpleado').modal('hide');
});

$("#CrearDepartCancelar").click(function () {
    cleanCrearDepart();
});

function cleanCrearDepart() {
    $('#CrearDepart #dep_CodeDC').val("");
    $('#CrearDepart #dep_CodeDC').css("border-color", "#eee");
    $('#CrearDepart #dep_DescriptionDC').val("");
    $('#CrearDepart #dep_DescriptionDC').css("border-color", "#eee");
    $('#CrearDepart #DropDownPaisDC').val(0);
    $('#CrearDepart #DropDownPaisDC').css("border-color", "#eee");
}

/*click que confirma la creación del Departamento*/
$("#CrearDepart #CrearDepartConfirmar").click(function () {
    var result = ValidateCreateMdlDepart();
    if (result == true) {

        var depa = [
            { name: "dep_Code", value: $("#CrearDepart #dep_CodeDC").val() },
            { name: "dep_Description", value: $("#CrearDepart #dep_DescriptionDC").val().trim() },
            { name: "cou_Id", value: $("#CrearDepart #DropDownPaisDC").val() },
            { name: "dep_IdUserCreate", value: UserId },
            { name: "dep_IdUserModified", value: null },
        ];
        //Insertar el dato
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Depart/Create",
            data: depa,
        }).done(function (data) {
            var result = true;
            result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            if (result) {
                setTimeout(function () {
                    location.assign(BaseUrl + "/Depart/index");
                }, 1500)
            }
        });
    }
    return false;
});

function GetSubPaisListCreateCount() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Countries/CountriesList",
    }).done(function (data, index) {
        /*        Vaciar el dropdownlist*/
        $("#DropDownPaisDC").empty();
        /*        variable que almacena las opciones*/
        var NewOption = '<option value=' + 0 + ' selected disabled=""> Por favor seleccione una opción... </option>';

        data.data.forEach(function (item) {
            NewOption += "<option value=" + item.cou_Id + ">" + item.cou_Description + "</option>";
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownPaisDC").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPaisDC').after($(message));
    });
}
function ValidateCreateMdlDepart() {
    LimpiarSpanMessa();
    secciones = "";
    var result = false;
    var count = 0;

    var dep_CodeDC = $('#CrearDepart #dep_CodeDC');
    var dep_DescriptionDC = $('#CrearDepart #dep_DescriptionDC');
    var DropDownPaisDC = $('#CrearDepart #DropDownPaisDC');
    var divMesa = $('#CrearDepart #messageCre');
    var el = $("#CrearDepart #messageCre span.select2-selection.select2-selection--single");

    result = MessagesError(dep_CodeDC, null, 4, 'código de departemento');
    if (result == true) { count++; }

    result = MessagesError(dep_DescriptionDC, null, 100, 'departamento');
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownPaisDC, 'país', divMesa,el);
    if (result == true) { count++; }

    if (count == 3) {
        return result;
    }
    return false;
}

$('#CrearDepart #dep_CodeDC').on('keypress', function () {
    $('#CrearDepart #dep_CodeDC').css("border-color", "#eee");
});
$('#CrearDepart #dep_DescriptionDC').on('keypress', function () {
    $('#CrearDepart #dep_DescriptionDC').css("border-color", "#eee");
});
$('#CrearDepart #DropDownPaisDC').on('select2:select', function () {
    $('#CrearDepart #messageCre span.select2-selection.select2-selection--single').css("border-color", "#eee");
});

$('#CrearDepart #dep_CodeDC').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});
function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}