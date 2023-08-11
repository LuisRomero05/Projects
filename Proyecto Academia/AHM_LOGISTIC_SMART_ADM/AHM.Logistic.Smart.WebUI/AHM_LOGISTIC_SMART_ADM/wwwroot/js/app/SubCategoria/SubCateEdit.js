var scat_Id = 0;
var cat_Id = 0;

$(document).ready(function () {
    $('#DropDownCatEdit').select2({
        dropdownParent: $('#frmEditSubCategories')
    });
});

function ShowModalfrmEditSubCategories(id) {
    scat_Id = id;
    GetSubCategoriesListEdit(id);
    GetSubCategoriesDetail(id);
    $('#frmEditSubCategories').modal('show');
    CleanSubCategoriesEdit();
}


function CleanSubCategoriesEdit() {
    $('#frmEditSubCategories #scat_Description').val("");
    $('#frmEditSubCategories #scat_Description').css("border-color", "#eee");
    $('#frmEditSubCategories #DropDownCatEdit').val("");
    $('#frmEditSubCategories #DropDownCatEdit').css("border-color", "#eee");
}


$('#frmEditSubCategories #scat_Description').on('keypress', function () {
    $('#frmEditSubCategories #scat_Description').css("border-color", "#eee");
    $('#frmEditSubCategories #DropDownCatEdit').css("border-color", "#eee");

});

//funcion para obtener la información del país
function GetSubCategoriesDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/SubCategories/Edit/" + id,
    }).done(function (data) {
        cat_Id = data.data.cat_Id;
        $("#frmEditSubCategories #scat_id").val(id);
        $("#frmEditSubCategories #scat_Description").val(data.data.scat_Description);
        $("#frmEditSubCategories #cat_Id").val(data.data.cat_Id);
        var sele = 0;
        sele = data.data.scat_Id;
        GetSubCategoriesListEdit(sele);
    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#frmEditSubCategories #scat_Description').after($(message));
    });
}

function GetSubCategoriesListEdit(sele) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Categories/CategoriesList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#frmEditSubCategories #DropDownCatEdit").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        data.data.forEach(function (item) {
            if (item.cat_Id == cat_Id) {
                NewOption += "<option  value=" + item.cat_Id + " selected>" + item.cat_Description + "</option>";
            }
            else {
                NewOption += "<option  value=" + item.cat_Id + ">" + item.cat_Description + "</option>";
            }

        });




        //Agregar las opciones al dropdownlist
        $("#frmEditSubCategories #DropDownCatEdit").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#frmEditSubCategories #DropDownCatEdit').after($(message));
    });
}




$('#frmEditSubCategories #EditarSubCategoriaConfirmar').on('click', function () {
  
    var result = ValiSubCateEdit()
    if (result == true) {
        var data = [
            { name: "scat_Description", value: $("#frmEditSubCategories #scat_Description").val().trim() },
            { name: "cat_Description", value: $("#frmEditSubCategories #DropDownCategoria").val() },

            { name: "scat_IdUserCreate", value: null },
            { name: "scat_IdUserModifies", value: TempUserDefault },
        ];
        var descripcion = $("#frmEditSubCategories #scat_Description").val().trim();
        var repetido = false;
        $.ajax({
            type: "Get",
            url: BaseUrl + "/SubCategories/SubCategoriesList",
            data: data,
        }).done(function (data, index) {

            data.data.forEach(function (item) {
                if (item.scat_Description.toLowerCase() == descripcion.toLowerCase()) {
                    repetido = true;
                }
            });
            var message = "";
            if (repetido == true) {
                message = '<span style="color: red;">*La Subcategoría ya existe</span>';
                $('#frmEditSubCategories #scat_Description').after($(message).fadeOut(4000));
                $('#frmEditSubCategories #scat_Description').css("border-color", "red");

            }
            else {
                $('#frmEditSubCategories').modal('hide');
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
                            { name: "scat_Id", value: scat_Id },
                            { name: "scat_Description", value: $("#frmEditSubCategories #scat_Description").val().trim() },
                            { name: "cat_Id", value: $("#frmEditSubCategories #DropDownCatEdit").val() },
                            { name: "scat_IdUserCreate", value: null },
                            { name: "scat_IdUserModified", value: TempUserDefault },
                        ];
                        $.ajax({
                            type: "PUT",
                            url: BaseUrl + "/SubCategories/Edit",
                            data: data,
                        }).done(function (data) {
                            iziToast.success({
                                message: 'Registro actualizado exitosamente'
                            }); setTimeout(function () {
                                location.assign(BaseUrl + "/SubCategories/index");
                            }, 1500)
                        }).fail(function (data) {
                            if (done == false) {
                                NotificationMessage(data.success, "", data.id, data.data, data.type);
                            }
                        });
                    }
                    else {
                        //mostrar modal again
                        $('#frmEditSubCategories').modal('show');
                    }
                });


            }
        }).fail(function () {
            //mostrar alerta en caso de error
            NotificationMessage(false);

            var message = '<span style="color: red;">No se pudo cargar, comuníquese con el encargado.</span>';
            $('#frmEditSubCategories #DropDownCatEdit').after($(message));
        });
    }

});

function ValiSubCateEdit() {
    var result = false;
    var count = 0;
    var scat_Description = $('#frmEditSubCategories #scat_Description');
    var DropDownCatEdit = $('#frmEditSubCategories #DropDownCatEdit');

    var DivCatEd = $('#frmEditSubCategories #DivCatEd');

    var borEditaA = $('#frmEditSubCategories #DivCatEd span.select2-selection.select2-selection--single');



    result = MessagesError(scat_Description, null, 100, 'Descripción');
    if (result == true) { count++; }

    result = MessageErrorDrop(DropDownCatEdit, 'categoría', DivCatEd, borEditaA);
    if (result == true) { count++; }




    if (count == 2) {
        return result;
    }
    return false;
}

$('#frmEditSubCategories #scat_Description').on('keypress', function () {
    $('#frmEditSubCategories #scat_Description').css("border-color", "#eee");
});
$('#frmEditSubCategories #DropDownCatEdit').on('change', function () {
    $('#frmEditSubCategories #DropDownCatEdit').css("border-color", "#eee");
});
$('#frmEditSubCategories #DivCatEd').on('select2:select', function () {
    $('#frmEditSubCategories #DivCatEd span.select2-selection.select2-selection--single').css("border-color", "#eee");
});