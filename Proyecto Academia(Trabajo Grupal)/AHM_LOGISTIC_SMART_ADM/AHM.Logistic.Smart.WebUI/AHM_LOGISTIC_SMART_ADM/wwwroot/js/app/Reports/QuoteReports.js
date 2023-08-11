$(document).ready(function () {
    GetCustomersListCreate();
    ReporteDinamico();
});
let name;

function ReporteDinamico() {
    var accion = "pdf";
    var seleccionado = $("#SelectCustomers option:selected").val();
    var fechainicio = $("#fechaInicio").val();
    var fechafinaliza = $("#fechaFinal").val();
    if (seleccionado == null) {
        seleccionado = 0;
    }
    if (fechainicio == "" && fechafinaliza == "") {
        fechainicio = '1950-01-01';
        fechafinaliza = '1950-01-02';
    }
    var data = {
        Id: seleccionado,
        fechainicio: fechainicio,
        fechatermina: fechafinaliza,
        accion: accion
    };


    var params = jQuery.param(data);
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
       .attr("src", "https://localhost:44369/CotizationsDetails/ReportDynamic?" + params).show();
}

function Mostrar() {
    var accion = "pdf";
    var data = {
        accion: accion
    };
    var params = jQuery.param(data);
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
        .attr("src", "https://localhost:44369/CotizationsDetails/LastMonth?" + params).show();
}

function MostrarPorCliente() {
    var seleccionado = $("#SelectCustomers option:selected").val();
    var accion = "pdf";
    var data = {
        Id: seleccionado,
        accion: accion
    };
    var params = jQuery.param(data);
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
        .attr("src", "https://localhost:44369/CotizationsDetails/ByCustomer?" + params).show();

}

function MostrarPorFecha() {
    var fechainicio = $("#fechaInicio").val();
    var fechafinaliza = $("#fechaFinal").val();
    var accion = "pdf";
    var data = {
        fechainicio: fechainicio,
        fechafinaliza: fechafinaliza,
        accion: accion
    };
    if (fechainicio != "" && fechafinaliza != "") {
        $('#validarExistencia').text("");
        if (fechainicio < fechafinaliza) {
            $('#validarExistencia').text("");
    var params = jQuery.param(data);
    $('#divFrame').show();
    $("#frmReporte").show();
    if ($('#frmReporte').is(':visible')) {
        $('#divCarga').css('display', 'block');
    }
    $("#frmReporte")
                .attr("src", "https://localhost:44369/CotizationsDetails/ByMonth?" + params).show();
        }
        else {
            $('#validarExistencia').text('¡La fecha de inicio es superior a la de finalizar!');
        }
    } else {
        $('#validarExistencia').text('¡Todos los campos son necesarios!');
    }

}

$('#TipoReporte').change(function () {
    var seleccionado = $("#TipoReporte option:selected").val();
    TipoReporte(seleccionado);
});

function TipoReporte(valor = '') {
    switch (valor) {
        case 'ByCustomer':
            name = valor;
            $('#div-Fecha').hide();
            $('#div-Cliente').show();
            GetCustomersListCreate();
            //$("#frmReporte").attr("src", "");

            break;
        case 'LastMonth':
            name = valor;
            $('#div-Fecha').hide();
            $('#div-Cliente').hide();
            //$("#frmReporte").attr("src", "");
            Mostrar();
            break;
        case 'ByMonth':
            name = valor;
            $('#div-Cliente').hide();
            $('#div-Fecha').show();
            break;
    }
}

    function GetCustomersListCreate() {
        $.ajax({
            type: "GET",
            url: BaseUrl + "/Customers/CustomersList",
        }).done(function (data, index) {
            //Vaciar el dropdownlist

            $("#SelectCustomers").empty();
            //variable que almacena las opciones
            var NewOption = `<option value="0"> Por favor seleccione una opción... </option>`;

            data.data.forEach(function (item) {
                if (item.cus_Id == 0) {
                    NewOption += "<option  value=" + item.cus_Id + " selected>" + item.cus_Name + "</option>";

                }
                else {
                    NewOption += "<option  value=" + item.cus_Id + ">" + item.cus_Name + "</option>";
                }
            });



            //Agregar las opciones al dropdownlist
            $("#SelectCustomers").append(NewOption);
        }).fail(function () {
            //mostrar alerta en caso de error
            console.log("Error al cargar los Clientes");
        });
    }
function LimpiarFechas() {
    document.getElementById('SelectCustomers').value = 0;
    document.getElementById('fechaInicio').value = '';
    document.getElementById('fechaFinal').value = '';
}
//function LimpiarDropwDown() {
//    document.getElementById('SelectCustomers').value = '';
//}
