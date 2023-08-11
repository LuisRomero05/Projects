//input que desea agregarles las cualidades del jQuery
var cont_PhoneEd = document.querySelector("#cont_Phone");

//Paramtros sobre las cualidades del input type tel
var cont_PhoneItiEd = window.intlTelInput(cont_Phone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});


// Ulilizada para elegir que area va enviar.
var ValidarTele = true;
//El numero de area al selecionar el pais, sera guardado en la variable
var contPhoneEd = "";
cont_PhoneEd.addEventListener("countrychange", function () {
    var countryData = cont_PhoneItiEd.getSelectedCountryData();
    ValidarTele = false;
    contPhoneEd = "+" + countryData.dialCode + " ";
});

//el numero de area al cargar
var countryDataEd = cont_PhoneItiEd.getSelectedCountryData();
var ariaInicioContEd = "+" + countryDataEd.dialCode + " ";


var occ_Id = $("#IdOcc").val();
var cusId = $("#IdCus").val();
var contId = $("#IdCont").val();
//EJECUTAR FUNCIONES EN LA PRIMER CARGA DE LA PAGINA
$(document).ready(function () {
    GetOccupationsListEdit();
    GetClientsListEdit();
    GetDropdowns();
});

function GetOccupationsListEdit() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Occupation/OccupationList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectOccupations").empty();
        //variable que almacena las opciones
        var NewOptionOccupation = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        data.data.forEach(function (item) {
            if (item.occ_Id == occ_Id) {
                NewOptionOccupation += "<option  value=" + item.occ_Id + " selected>" + item.occ_Description + "</option>";
            }
            else {
                NewOptionOccupation += "<option  value=" + item.occ_Id + ">" + item.occ_Description + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#SelectOccupations").append(NewOptionOccupation);


    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectOccupations').after($(message));
    });
}

function GetClientsListEdit(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Customers/CustomersList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectCustomers").empty();
        //variable que almacena las opciones
        var NewOption2 = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item2) {
            if (item2.cus_Id == cusId) {
                NewOption2 += "<option  value=" + item2.cus_Id + " selected>" + item2.cus_Name + "</option>";
            }
            else {
                NewOption2 += "<option  value=" + item2.cus_Id + ">" + item2.cus_Name + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#SelectCustomers").append(NewOption2);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectCustomers').after($(message));
    });
}

$('#frmEditContacts').on('submit', function (id) {

    //Aqui se edita el registro con validaciones
    var result = ValidateEditFrmContacts();
    if (result == true) {
        var telefonoFinal = "";
        if (ValidarTele === true) {
            telefonoFinal = ariaInicioContEd + cont_PhoneEd.value;
        } else {
            telefonoFinal = contPhoneEd + cont_Phone.value;
        }
        var data = [
            { name: "cont_Id", value: contId },
            { name: "cont_Name", value: $('#frmEditContacts #cont_Name').val().trim()},
            { name: "cont_LastName", value: $('#frmEditContacts #cont_LastName').val().trim() },
            { name: "cont_Email", value: $('#frmEditContacts #cont_Email').val().trim() },
            { name: "cont_Phone", value: telefonoFinal.trim()},
            { name: "occ_Id", value: $("#frmEditContacts #SelectOccupations").val() },
            { name: "cus_Id", value: $("#frmEditContacts #SelectCustomers").val() },
            { name: "cont_IdUserCreate", value: TempUserDefault },
            { name: "cont_IdUserModified", value: TempUserDefault },
        ];
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Contacts/Edit",
            data: data,
        }).done(function (data) {
            Swal.fire({
                closeOnClickOutside: false,
                width: '20%',
                height: '20%',
                text: "¿Estás seguro que deseas guardar este registro?",
                icon: 'info',
                showCancelButton: true,
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#6c757d',
                cancelButtonColor: '#001f52',
            }).then((result) => {
                if (result.isConfirmed) {
                    iziToast.success({
                        message: 'Registro actualizado exitosamente'
                    });
                    setTimeout(function () {
                        location.assign(BaseUrl + "/contacts/index");
                    }, 1500)
                }
            })

        }).fail(function (data) {
            iziToast.warning({
                message: 'Ha ocurrido un error! Comuniquese con el encargado'
            });
        });
    }
    return false;

});

function ValidateEditFrmContacts() {
    var result = false;
    var count = 0;
    var cont_Name = $('#frmEditContacts #cont_Name');
    var cont_LastName = $('#frmEditContacts #cont_LastName');
    var cont_Email = $('#frmEditContacts #cont_Email');
    var cont_Phone = $('#frmEditContacts #cont_Phone');
    var cont_PhoneMess = $("#frmEditContacts #cont_PhoneMessEd");
    var divMessaOccu = $('#frmEditContacts #messageCre')
    var divMessaCust = $('#frmEditContacts #messageCre2')


    var occ_Id = $('#frmEditContacts #SelectOccupations');
    var cus_Id = $('#frmEditContacts #SelectCustomers');

    result = MessagesError2(occ_Id, null, null, 'Ocupación', divMessaOccu);
    if (result == true) { count++; }

    result = MessagesError2(cus_Id, null, null, 'Cliente', divMessaCust);
    if (result == true) { count++; }

    result = MessagesError(cont_Name, null, 100, 'Nombre');
    if (result == true) { count++; }
    result = MessagesError(cont_LastName, null, 100, 'Apellido');
    if (result == true) { count++; }

    if (cont_Email.val().indexOf('@', 0) == -1 || cont_Email.val().indexOf('.com', 0) == -1) {
        message = '<span style="color: red;">El campo de email es inválido</span>';
        cont_Email.after($(message).fadeToggle(3000));
        cont_Email.css("border-color", "red");
        count++;
    }

    result = telMessagesError(cont_Phone, 'Teléfono', cont_PhoneItiEd, cont_PhoneMess);
    if (result == true) { count++; }
    //result = MessagesError(occ_Id, null, null, 'Ocupaciones');
    //if (result == true) { count++; }
    //result = MessagesError(cus_Id, null, null, 'Clientes');
    //if (result == true) { count++; }

    if (count == 5) {
        return result;
    }
    return false;
}

$('#frmEditContacts #cont_Name').on('keypress', function () {
    $('#frmEditContacts #cont_Name').css("border-color", "#eee");
});
$('#frmEditContacts #cont_LastName').on('keypress', function () {
    $('#frmEditContacts #cont_LastName').css("border-color", "#eee");
});
$('#frmEditContacts #cont_Email').on('keypress', function () {
    $('#frmEditContacts #cont_Email').css("border-color", "#eee");
});
$('#frmEditContacts #cont_Phone').on('keypress', function () {
    $('#frmEditContacts #cont_Phone').css("border-color", "#eee");
});
$('#frmEditContacts #SelectOccupations').on('change', function () {
    $('#frmEditContacts #SelectOccupations').css("border-color", "#eee");
});
$('#frmEditContacts #SelectCustomers').on('change', function () {
    $('#frmEditContacts #SelectCustomers').css("border-color", "#eee");
});

$('#frmEditContacts #cont_Phone').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});
function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}

function GetDropdowns() {
    $("#SelectOccupations").select2();
    $("#SelectCustomers").select2();
    //$('#SelectUsuario2').select2();
    //$('#tyCh_Id').select2();
}

