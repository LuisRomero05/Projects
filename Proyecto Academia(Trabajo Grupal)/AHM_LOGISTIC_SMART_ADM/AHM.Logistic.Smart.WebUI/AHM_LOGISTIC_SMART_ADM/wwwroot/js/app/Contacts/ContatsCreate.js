//input que desea agregarles las cualidades del jQuery
var cont_Phone = document.querySelector("#cont_Phone");

//Paramtros sobre las cualidades del input type tel
var cont_PhoneIti = window.intlTelInput(cont_Phone, {
    preferredCountries: ['hn', 'sv', 'gt', 'ni', 'pa', 'cr'],
    /*    separateDialCode: true,*/
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.0.3/js/utils.js"
});

//El numero de area al selecionar el pais, sera guardado en la variable
var contPhone = "";
cont_Phone.addEventListener("countrychange", function () {
    var countryData = cont_PhoneIti.getSelectedCountryData();
    contPhone = "+" + countryData.dialCode + " ";
});

//el numero de area al cargar
var countryData = cont_PhoneIti.getSelectedCountryData();
var ariaInicioCont = "+" + countryData.dialCode + " ";

$(document).ready(function () {
    GetDropdowns();
});

$('#frmCreateContacts').on('submit', function () {
    var result = ValidateFrmContacts();
    if (result == true) {
        var telefonoFinal = "";
        if (contPhone === "") {
            telefonoFinal = ariaInicioCont + cont_Phone.value;
        } else {
            telefonoFinal = contPhone + cont_Phone.value;
        }
        var data = [
            { name: "cont_Name", value: $("#frmCreateContacts #cont_Name").val().trim() },
            { name: "cont_LastName", value: $("#frmCreateContacts #cont_LastName").val().trim() },
            { name: "cont_Email", value: $("#frmCreateContacts #cont_Email").val().trim() },
            { name: "cont_Phone", value: telefonoFinal.trim() },
            { name: "occ_Id", value: $("#frmCreateContacts #SelectOccupations").val() },
            { name: "cus_Id", value: $("#frmCreateContacts #SelectCustomers").val() },

            { name: "cont_IdUserCreate", value: TempUserDefault },
            { name: "cont_IdUserModified", value: null },
        ];

        $.ajax({
            type: "POST",
            url: BaseUrl + "/Contacts/Create",
            data: data,
        }).done(function (data) {
            iziToast.success({
                message: 'Registro creado exitosamente'
            });
            setTimeout(function () {
                location.assign(BaseUrl + "/contacts/index");
            }, 1500)
        }).fail(function (data) {
            iziToast.warning({
                message: 'Ha ocurrido un error! Comuniquese con el encargado'
            });
        });

    }

    return false;

});

var telVali = false;
function talvalidad() {
    var iti = intlTelInput(input);
    var isValid = iti.isValidNumber();
    return isValid;
};

function ValidateFrmContacts() {

    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var cont_Name = $('#frmCreateContacts #cont_Name');
    var cont_LastName = $('#frmCreateContacts #cont_LastName');
    var cont_Email = $('#frmCreateContacts #cont_Email');
    var cont_Phone = $('#frmCreateContacts #cont_Phone');
    var cont_PhoneMess = $("#frmCreateContacts #cont_PhoneMess");
    var divMessaOccu = $('#frmCreateContacts #messageCre')
    var divMessaCust = $('#frmCreateContacts #messageCre2')



    var occ_Id = $('#frmCreateContacts #SelectOccupations');
    var cus_Id = $('#frmCreateContacts #SelectCustomers');

    result = MessagesError2(occ_Id, null, null, 'Ocupación', divMessaOccu);
    if (result == true) { count++; }

    result = MessagesError2(cus_Id, null, null, 'Cliente', divMessaCust);
    if (result == true) { count++; }

    result = MessagesError(cont_Name, null, 100, 'Nombre');
    if (result == true) { count++; }
    result = MessagesError(cont_LastName, null, 100, 'Apellido');
    if (result == true) { count++; }

    if (cont_Email.val().indexOf('@', 0) == -1 || cont_Email.val().indexOf('.com', 0) == -1) {
        message = '<span style="color: red;" name="Mesas">El campo de email es inválido</span>';
        cont_Email.after($(message).fadeToggle(3000));
        cont_Email.css("border-color", "red");
        count++;
    }

    result = telMessagesError(cont_Phone, 'Teléfono', cont_PhoneIti, cont_PhoneMess);
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

$('#frmCreateContacts #cont_Name').on('keypress', function () {
    $('#frmCreateContacts #cont_Name').css("border-color", "#eee");
});
$('#frmCreateContacts #cont_LastName').on('keypress', function () {
    $('#frmCreateContacts #cont_LastName').css("border-color", "#eee");
});
$('#frmCreateContacts #cont_Email').on('keypress', function () {
    $('#frmCreateContacts #cont_Email').css("border-color", "#eee");
});
$('#frmCreateContacts #cont_Phone').on('keypress', function () {
    $('#frmCreateContacts #cont_Phone').css("border-color", "#eee");
});
$('#frmCreateContacts #SelectOccupations').on('change', function () {
    $('#frmCreateContacts #SelectOccupations').css("border-color", "#eee");
});
$('#frmCreateContacts #SelectCustomers').on('change', function () {
    $('#frmCreateContacts #SelectCustomers').css("border-color", "#eee");
});

$('#frmCreateContacts #cont_Phone').keyup(function (e) {
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

