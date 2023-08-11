
var done = false;
$("#AbrirModalCategories").click(function () {
    $('#CreateCategories').modal('show');
    CleanCategories();
});

function CleanCategories() {
    $('#CreateCategories #cat_Description').val("");
    $('#CreateCategories #cat_Description').css("border-color", "#eee");
}

$('#CreateCategories #cat_Description').on('keypress', function () {
    $('#CreateCategories #cat_Description').css("border-color", "#eee");
});

$('#CreateCategories #CreateCategoriesConfirm').on('click', function () {

    var result = ValiCategoriesCreate();
    if (result == true) {
        var data = [
            { name: "cat_Description", value: $("#CreateCategories #cat_Description").val().trim() },
            { name: "cat_IdUserCreate", value: TempUserDefault },
            { name: "cat_IdUserModified", value: null },
        ];

        var descripcion = $("#cat_Description").val().trim();
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
                $('#CreateCategories #cat_Description').after($(message).fadeToggle(3000));
                $('#CreateCategories #cat_Description').css("border-color", "red");

            } else {
                var data = [
                    { name: "cat_Description", value: $("#CreateCategories #cat_Description").val().trim() },
                    { name: "cat_IdUserCreate", value: TempUserDefault },
                    { name: "cat_IdUserModified", value: null },
                ];
                //Insertar la país
                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Categories/Create",
                    data: data,
                }).done(function (data) {
                    iziToast.success({
                        message: 'Registro creado exitosamente'
                    });
                    setTimeout(function () {
                        location.assign(BaseUrl + "/categories/index");
                    }, 1500)
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                });
            }
        }).fail(function () {
            //mostrar alerta en caso de error
            var message = '<span style="color: red;">No se pudo ingresar la categoría, comuníquese con el encargado.</span>';
            $('#cat_Description').after($(message));
        });
    }
});

function ValiCategoriesCreate() {
    var result = true;
    var message = "";
    var cat_Description = $('#CreateCategories #cat_Description');
    result = MessagesError(cat_Description, null, 50, 'Categoría');
    return result;
}