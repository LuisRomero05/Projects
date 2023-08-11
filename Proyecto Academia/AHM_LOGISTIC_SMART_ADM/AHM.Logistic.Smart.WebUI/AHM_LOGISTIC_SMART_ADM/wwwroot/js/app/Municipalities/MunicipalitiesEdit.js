$(document).ready(function () {
    $('#DropDownPaisME').select2({
        dropdownParent: $('#EditMunicipalities')
    });
    $('#DropDownDepartmentsME').select2({
        dropdownParent: $('#EditMunicipalities')
    });
});

//almacenar el id del empleado modificado
var mun_Id = 0;
var pais = 0;
//click que abre el modal de crear
function ShowModalEdit(id) {
    /*    limpiar*/
    cleanEditMunici();
    //cambiar el valor de emp_Id
    mun_Id = id;
    //llamar la funció que recupera el detalle

    GetMunicipalitiesDetail(id);
    GposDepartmentsListEditMun();

    //mostrar el modal
    $('#EditMunicipalities').modal('show');
}

$("#EditMunicipalitiesCancelar").click(function () {
    cleanEditMunici();
});

function cleanEditMunici() {
    $('#EditMunicipalities #CodeME').css("border-color", "#eee");
    $('#EditMunicipalities #DescriptionME').css("border-color", "#eee");
    $('#EditMunicipalities #DropDownPaisME').css("border-color", "#eee");
    $('#EditMunicipalities #DropDownDepartmentsME').css("border-color", "#eee");
}
//click que confirma la creación de un minicipio
$("#EditMunicipalities #EditMunicipalitiesConfirmar").click(function (e) {
    e.preventDefault();
    e.stopImmediatePropagation();

    var result = ValidateEditMdlMunicipalies();
    if (result == true) {

        //crear el objeto con los valores seleccionados
        $('#EditMunicipalities').modal('hide');
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
                    { name: "mun_Code", value: $('#EditMunicipalities #CodeME').val() },
                    { name: "mun_Description", value: $("#EditMunicipalities #DescriptionME").val().trim() },
                    { name: "dep_Id", value: $("#EditMunicipalities #DropDownDepartmentsME").val() },
                    { name: "mun_IdUserCreate", value: null },
                    { name: "mun_IdUserModified", value: UserId },
                ];
                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/Muni/Edit?Id=" + mun_Id,
                    data: data,
                }).done(function (data) {
                    var result = true;
                    result = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    if (result) {
                        setTimeout(function () {
                            location.assign(BaseUrl + "/Muni/index");
                        }, 1500)
                    }
                })
            }
            else {
                //mostrar el modal
                $('#EditMunicipalities').modal('show');
            }
        });
    }

});

//funcion para obtener la información del municipio
function GetMunicipalitiesDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Muni/Detail/" + id,
    }).done(function (data) {

        $("#EditMunicipalities #CodeME").val(data.data.mun_Code);
        $("#EditMunicipalities #DescriptionME").val(data.data.mun_Description);
        var dep = 0;
        dep = data.data.dep_Id;

        GetDepartmentsPuente(dep);
    }).fail(function () {
    });
}
//funciones para obtener los listados
function GetDepartmentsPuente(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/Edit/" + id
    }).done(function (data) {
        var pais = 0;
        pais = data.data.cou_Id;
        GetSubPaisListEditMuni(pais, id);
    });
}

function GetSubPaisListEditMuni(selis, algo) {

    $.ajax({
        type: "GET",
        url: BaseUrl + "/Countries/CountriesList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#DropDownPaisME").empty();
        //variable que almacena las opciones

        var NewOption = '<option value=' + 0 + ' selected disabled=""> Por favor seleccione una opción... </option>';

        data.data.forEach(function (item) {
            if (item.cou_Id == selis) {
                NewOption += "<option value=" + item.cou_Id + " selected>" + item.cou_Description + "</option>";
                pais = item.cou_Id;
                GposDepartmentsListEditMun(pais, algo);
            }
            else {
                NewOption += "<option value=" + item.cou_Id + ">" + item.cou_Description + "</option>";
            }
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownPaisME").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPaisDC').after($(message));
    });
}

function GposDepartmentsListEditMun(pais, mun_Id) {
    var seraPais = $('#EditMunicipalities #DropDownPaisME').val();
    if (seraPais != "") {
        pais == seraPais;
    }
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Depart/DepartList",
    }).done(function (data) {

        //Vaciar el dropdownlist
        $("#DropDownDepartmentsME ").empty();

        //variable que almacena las opciones
        var NewOption = '<option value=' + 0 + ' selected disabled=""> Por favor seleccione una opción... </option>';
        data.data.forEach(function (item, index, array) {
            if (mun_Id != null) {
                if (pais == item.cou_Id) {
                    if (item.dep_Id == mun_Id) {
                        NewOption += `<option value="${item.dep_Id}" selected> ${item.dep_Description} </option>`;
                    }
                    else {
                        NewOption += `<option value="${item.dep_Id}"> ${item.dep_Description} </option>`;
                    }
                }
            } else {

                if (pais == item.cou_Id) {
                    NewOption += `<option value="${item.dep_Id}"> ${item.dep_Description} </option>`;
                }
            }
        });

        //Agregar las opciones al dropdownlist
        $("#DropDownDepartmentsME").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#DropDownPaisDC').after($(message));
    });
}
function ValidateEditMdlMunicipalies() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var CodeME = $('#EditMunicipalities #CodeME');
    var DescriptionME = $('#EditMunicipalities #DescriptionME');
    var DropDownPaisME = $('#EditMunicipalities #DropDownPaisME');
    var MessaEditPais = $('#EditMunicipalities #messagEditPais');
    var DropDownDepartmentsME = $('#EditMunicipalities #DropDownDepartmentsME');
    var MessaEditDeper = $('#EditMunicipalities #messaEditDeprt');

    result = MessagesError(CodeME, null, 4, 'Codigo de municipio');
    if (result == true) { count++; }

    result = MessagesError(DescriptionME, null, 100, 'Municipio');
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownPaisME, 'Pais', MessaEditPais);
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownDepartmentsME, 'Departamento', MessaEditDeper);
    if (result == true) { count++; }
    if (count == 4) {
        return result;
    }
    return false;
}
$('#EditMunicipalities #CodeME').on('keypress', function () {
    $('#EditMunicipalities #CodeME').css("border-color", "#eee");
});
$('#EditMunicipalities #DescriptionME').on('keypress', function () {
    $('#EditMunicipalities #DescriptionME').css("border-color", "#eee");
});
$('#EditMunicipalities #DropDownPaisME').on('select2:select', function () {
    $('#EditMunicipalities #DropDownPaisME').css("border-color", "#eee");
    var Id_contr = $("#DropDownPaisME").val();
    GposDepartmentsListEditMun(Id_contr, null);
});
$('#EditMunicipalities #DropDownDepartmentsME').on('select2:select', function () {
    $('#EditMunicipalities #DropDownDepartmentsME').css("border-color", "#eee");
});


$('#EditMunicipalities #CodeME').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});
function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}