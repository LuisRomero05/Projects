$(document).ready(function () {
    GetCustomersListCreate();
    ReporteDinamico();
//$('#div-Fecha').show();
//$('#div-Cliente').show();
});
let name;

////function MostrarRepoeGeneral() {
////    varccion = "pdf";
//    var data = {
////      acci: accion
////    };
////    var para = jQuery.param(data);
////  $('#divFrame').show();
////  $("#frmReporte").show();
////    if ($('#mReporte').is(':visible')) {
////        $('#divrga').c('display', 'block');///    }
////    $("#frmReporte")
////        .attr("src", "https://localhost:44369/OrdsRepos/General?" + params).show();
//}

////function MostrUltimoMes() {
///   var accion = "pdf";
//    vadata = {
////        accion: accion
//    };
////    var params jQuery.param(data);
////    '#divFrame').show();
////    $("#frmReporte.show();
////    if ($('#frmReporte').is(':visib')) {
//        $('#divCarga.css('display', 'block');
////    }
////    $("#frmReporte")
////        .attr("src", "hps://calhost:44369/OrdersReports/Lastnth?" + params).show();
////}

////function MostrarPorCliente() {///    var seleccionado =("#SelectCustomersption:selected").val();
//    var accion = "pdf";///    v data = {
////        Id: seleccionad
////        accion: accion////    };
////    var params jQuery.param(data);
////    $('#divFrame')how();
////    $("#frmReporte").show();
////   f ($('#mReporte').is(':visibl)) {
////        $('#divCarga').css('display', 'block');
////    }
////    $("#frmReporte
///       .attr("src", "https://lalhost:44369/OrdersReports/ByClient?" + params).ow();

////}

////function MostrarPorFecha() 
////    var fechainicio $("#fechaInicio").l();
////    var fechafinaliza = $#fechaFinal").val();
////    var accio= "pdf";
////    var da = {
//        fechainicio: fechainicio,
////        fechafaliza: fechafinaliza,
////        accion: cion
////    };
////    if (fechainicio !=" && fechafinaliza != "") {
////        $('#vadarExistencia').text("");
////       f (fechainicio < fechafinali) {
////            $('#validExistencia').text("");
////    var params =Query.param(data);
////    $('#divFrame').show()
////  $("#frmReporte").show(
////    if ($('#frmReporte').is(':visible')) {
////        $('#divCarga').css('display 'block');///    }
////  $("#frmReporte")
////        .attr("src", "https://localhost:44369/OrdersReports/ByMonth?" + pars).show();///        }
//        else {
////            $('#validarExistencia').text('¡La fecha denicio esuperr a la de finalizar!');
////        }////    } else {
////        $('#validarExistencia').text('¡Todos locampos son necesarios!');
////    }
////}

//function MostrarPorClienteyFecha() {
////    vaseleccionado = $("#Selectstomers option:selted").val();
////    var fhainicio = $("#fechaInicio").val();////    var fechafinaliza = $("#fechaFil").val();
////    var cion = "f";
////    var data = {
////        Id: selecciona,
////        fechainicio: fechainicio,
//        fechafinaliza: fechafinaliza,
////      accion: accion
////    };
////    if (fhainicio != "" && fechafinaliza != "") {
////      $('#validarExistencia').text(";
////        if (fechainicio < fechanaliza) {
////            $('#validarExistencia').tt("");
////            var params = jQuery.param(data);///            '#divFrame').show();
////          $("#frmReporte").show();
////            if ($('#frmReporte').is(':visible')) {
////              $('#divCarg).css('display',block');
////            }
////            $("#frmReporte")
////                .attr("src", "tps://localst:44369/Ordereports/ByMonthAndClient?" + params).show();
////        }
////        else
////        $('#validarExistencia').text('¡La fecha dinicio es superior a la de finalizar!');
////        }
////    } else {////        $('#validarExisteia').text('¡Todos los mpos son necesarios!');
////  }
////}

//// function MorarPorClientUltimoMes() {
////        var seleccionad= $("#SelectCustomers option:selted").val();
////        var acci = "pdf";
////        var data = {
////          Id: seleccionado,
////            accion: accion///        
////        var params =Query.param(data);
////        $('#divFrame').show();
////        $("#frmReporte").show();
////      if ($(frmReporte').is(':visible')) {
//            $('#divCarga').css('display', 'block');
////      }
////        $("#frmReporte")
///           .at("src", "https://localhost:44369/OrdersRepor/LastMohClient + params).show();
////    }

///('#Mes').change(function () {
////    if (document.getElementBd("Mes").checked == true) {
////      $('#div-checox-fecha').hide();
////    } else {///      $('#d-checkbox-fecha').show();

////  }
////});

////$('#cliente').change(function () {
////  if (document.getElementById("cliente").ccked == true) {
////        $('#diCliente').show;

////    } else {
////        $('#d-Cliente').hide();

////    }
//});

//$('#fha').change(function () {
//    if (document.getElementById("fecha").checked == true) {
////        $('#div-checkbox-mes').hide();
////      $('#div-Fecha').show();

////    } se {
////        $('#div-checkbox-mes').show();
////        $('#div-Fecha').hide();

////    }
////});

////functioLlamarReporte() {
////    if (documt.getElementById("Mes").checked == true && document.getElementById("cliente").checked == true) {
////        MostrarPorCenteyUltimoMes();
////    } se if (document.getElementById("cliente").checked == true && document.getElementById("fecha").checked == true) {
////      MostrarPorClienteyFecha();///    } elsef (document.getElementById("Me).checked= truinicio);
//    console.log(fechafinaliza);
//    var params = jQuery.param(data);
//    $('#divFrame').show();
//    $("#frmReporte").show();
//    if ($('#frmReporte').is(':visible')) {
//        $('#divCarga').css('display', 'block');
//    }
//    $("#frmReporte")
//        .attr("src", "https://localhost:44369/OrdersReports/OrdersReportsDynamic?" + params).show();
//}
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
        .attr("src", "https://localhost:44369/OrdersReports/OrdersReportsDynamic?" + params).show();
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
