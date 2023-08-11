$(document).ready(function () {
    $('#DropDownCatCreate').select2({
        dropdownParent: $('#frmCreateSubCategories')
    });
});




$("#AbrirModalSubCategories").click(function () {
    $('#frmCreateSubCategories').modal('show');
    CleanSubCategories();
    GetSubCategoriesListCreate();
});



function CleanSubCategories() {
    $('#frmCreateSubCategories #scat_Description').val("");
    $('#frmCreateSubCategories #scat_Description').css("border-color", "#eee");
    $('#frmCreateSubCategories #DropDownCatCreate').val(0);
    $('#frmCreateSubCategories #DropDownCatCreate').css("border-color", "#eee");

}

$('#frmCreateSubCategories #CrearSubCategoriaConfirmar').on('click', function () {
    var result = ValiSubCategoriesCreate();
    if (result == true) {
        var data = [
            { name: "scat_Description", value: $("#frmCreateSubCategories #scat_Description").val().trim() },
            { name: "cat_Id", value: $("#frmCreateSubCategories #DropDownCatCreate").val() },
            { name: "scat_IdUserCreate", value: TempUserDefault },
            { name: "scat_IdUserModified", value: null },
        ];
        var descripcion = $("#scat_Description").val().trim();
        var repetido = false;
        $.ajax({
            type: "Get",
            url: BaseUrl + "/SubCategories/SubCategoriesList",
            data: data,
        }).done(function (data, index) {
            if (data.data == null) {
                if (done == false) {
                    //done = NotificationMessage(data.success, "", data.id, data.data, data.type);
                }
            }
            else {
                data.data.forEach(function (item) {
                    if (item.scat_Description.toLowerCase() == descripcion.toLowerCase()) {
                        repetido = true;
                    }
                });
            }
        //}).done(function (data) {
        //    if (data.data != null) {
        //        data.data.forEach(function (item) {
        //            if (item.cou_Description.toLowerCase() == descripcion) {
        //                repetido = true;
        //            }
        //        });
        //    }
        //    else {
        //        data.data.forEach(function (item) {
        //            if (item.cat_Description.toLowerCase() == descripcion.toLowerCase()) {
        //                repetido = true;
        //            }
        //        });
        //    }
            var message = "";
            if (repetido == true) {
                message = '<span style="color: red;">*El Registro ya existe</span><br>';
                $('#frmCreateSubCategories #scat_Description').after($(message).fadeOut(4000));
                $('#frmCreateSubCategories #scat_Description').css("border-color", "red");
            }
            else {
                var data = [
                    { name: "scat_Description", value: $("#frmCreateSubCategories #scat_Description").val().trim() },
                    { name: "cat_Id", value: $("#frmCreateSubCategories #DropDownCatCreate").val() },
                    { name: "scat_IdUserCreate", value: TempUserDefault },
                    { name: "scat_IdUserModified", value: null },
                ];
                //Insertar la subcategoria
                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/SubCategories/Create",
                    data: data,
                }).done(function (data) {
                    iziToast.success({
                        message: 'Registro creado exitosamente'
                    }); setTimeout(function () {
                        location.assign(BaseUrl + "/SubCategories/index");
                    }, 1500)
                }).fail(function (data) {
                    //mostrar alerta en caso de error
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                    //var message = '<span style="color: red;">No se pudo ingresar la Sub-Categoría, comuníquese con el encargado.</span>';
                    //$('#scat_Description').after($(message));
                });
            }   
        });

    }
});

//function ValiSubCategoriesCreate() {
//    var result = true;
//    var message = "";
//    var scat_Description = $('#frmCreateSubCategories #scat_Description');
//    result = MessagesError(scat_Description, 4, 50, 'sub categoria');
//    return result;

//}




function ValiSubCategoriesCreate() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var scat_Description = $('#frmCreateSubCategories #scat_Description');
    var DropDownCatCreate = $('#frmCreateSubCategories #DropDownCatCreate');

    var DivCat = $('#frmCreateSubCategories #DivCat');

    var borCat = $('#frmCreateSubCategories #DivCat span.select2-selection.select2-selection--single');


    result = MessagesError(scat_Description, null, 100, 'descripción');
    if (result == true) { count++; }
 
    result = MessageErrorDrop(DropDownCatCreate, 'categoría', DivCat, borCat);
    if (result == true) { count++; }


    if (count == 2) {
        return result;
    }
    return false;
}

$('#frmCreateSubCategories #scat_Description').on('keypress', function () {
    $('#frmCreateSubCategories #scat_Description').css("border-color", "#eee");
});
$('#frmCreateSubCategories #DropDownCatCreate').on('change', function () {
    $('#frmCreateSubCategories #DropDownCatCreate').css("border-color", "#eee");
});

$('#frmCreateSubCategories #DivCat').on('select2:select', function () {
    $('#frmCreateSubCategories #DivCat span.select2-selection.select2-selection--single').css("border-color", "#eee");
});