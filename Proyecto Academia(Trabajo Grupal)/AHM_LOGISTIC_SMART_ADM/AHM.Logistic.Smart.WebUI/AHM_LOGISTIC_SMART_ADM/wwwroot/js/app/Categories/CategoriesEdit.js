var done = false;
var cat_Id = 0;
function ShowModalEditCategories(id) {
    CleanCategoriesEdit();
    cat_Id = id;
    GetCategoriesDetail(id);
    $('#EditCategories').modal('show');

}


function CleanCategoriesEdit() {
    $('#EditCategories #cat_Description').val("");
    $('#EditCategories #cat_Description').css("border-color", "#eee");
}

$('#EditCategories #cat_Description').on('keypress', function () {
    $('#EditCategories #cat_Description').css("border-color", "#eee");
});

//funcion para obtener la información del país
function GetCategoriesDetail(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Categories/Edit/" + id,
    }).done(function (data) {
        $("#EditCategories #cat_Description").val(data.data.cat_Description);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#EditCategories #cat_Description').after($(message));
    });
}


$('#EditCategories #EditCategoriesConfirm').on('click', function () {

    var editar = ValiContactsEdit()
    if (editar == true) {
        var data = [
            { name: "cat_Description", value: $("#EditCategories #cat_Description").val().trim() },
            { name: "cat_IdUserCreate", value: null },
            { name: "cat_IdUserModifies", value: TempUserDefault },
        ];
        var descripcion = $("#EditCategories #cat_Description").val().trim();
        var repetido = false;
        $.ajax({
            type: "Get",
            url: BaseUrl + "/Categories/CategoriesList",
            data: data,
        }).done(function (data, index) {
            if (data.data == null) {
                if (done == false) {
                    //done = NotificationMessage(data.success, "", data.id, data.data, data.type);
                }
            }
            else {
                data.data.forEach(function (item) {
                    if (item.cat_Description.toLowerCase() == descripcion.toLowerCase()) {
                        repetido = true;
                    }
                });
            }
            var message = "";
            if (repetido == true) {
                message = '<span style="color: red;">*La Categoría ya existe</span>';
                $('#EditCategories #cat_Description').after($(message).fadeToggle(3000));
                $('#EditCategories #cat_Description').css("border-color", "red");

            } else {

                $('#EditCategories').modal('hide');
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
                            { name: "cat_Description", value: $("#EditCategories #cat_Description").val().trim() },
                            { name: "cat_IdUserCreate", value: null },
                            { name: "cat_IdUserModifies", value: TempUserDefault },
                        ];
                        $.ajax({
                            type: "PUT",
                            url: BaseUrl + "/Categories/Edit?Id=" + cat_Id,
                            data: data,
                        }).done(function (data) {
                            iziToast.success({
                                message: 'Registro actualizado exitosamente'
                            });
                            setTimeout(function () {
                                location.assign(BaseUrl + "/Categories/index");
                            }, 1500)
                        }).fail(function (data) {
                            if (done == false) {
                                NotificationMessage(data.success, "", data.id, data.data, data.type);
                            }
                        });
                    }
                    else {
                        //mostrar modal again
                        $('#EditCategories').modal('show');
                    }
                });


            }
        }).fail(function () {
            //mostrar alerta en caso de error
            var message = '<span style="color: red;">No se pudo ingresar la Categorías, comuníquese con el encargado.</span>';
            $('#cat_Description').after($(message));
        });
    }

});

function ValiContactsEdit() {
    var result = true;
    var cat_Description = $('#EditCategories #cat_Description');
    result = MessagesError(cat_Description, null, 50, 'Categoría');
    return result;
}