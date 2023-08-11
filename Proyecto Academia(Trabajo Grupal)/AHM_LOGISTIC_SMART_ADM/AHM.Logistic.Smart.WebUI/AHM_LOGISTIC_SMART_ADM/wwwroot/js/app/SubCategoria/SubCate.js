function GetSubCategoriesListCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Categories/CategoriesList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#frmCreateSubCategories #DropDownCatCreate").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item) {
            NewOption += "<option  value=" + item.cat_Id + ">" + item.cat_Description + "</option>";
        });

        //Agregar las opciones al dropdownlist
        $("#frmCreateSubCategories #DropDownCatCreate").append(NewOption);

    }).fail(function () {
        //mostrar alerta en caso de error
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#frmCreateSubCategories #DropDownCatCreate').after($(message));
    });
}