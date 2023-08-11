var id = $("#frmEditProducts #subcategories").val();
var categorie = null;
var done = false;
$(document).ready(function () {
    GetDropdowns();
    GetCategoriesListCreate();
    GetSubcategoriesListCreate();

});

function GetCategoriesListCreate(categorie) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Categories/CategoriesList",
    }).done(function (data, index) {
        $("#SelectCategories").empty();

        var NewOption = "<option value=" + 0 + " disabled> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.cat_Id == categorie) {
                    NewOption += "<option  value=" + item.cat_Id + " selected>" + item.cat_Description + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.cat_Id + ">" + item.cat_Description + "</option>";
                }
            });
        }

        $("#SelectCategories").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectCategories').after($(message));
    });
}

$("#SelectCategories").on('change', function () {
    $("#SelectSubcategories").attr('disabled', false);
    id = 0;
    GetSubcategoriesListCreate();
});

function GetSubcategoriesListCreate() {
    var value = $("#SelectCategories").val();
    if (value == null) {
        $.ajax({
            type: "GET",
            url: BaseUrl + "/SubCategories/SubCategoriesList",
        }).done(function (data, index) {
            if (data.data == null) {
                if (done == false) {
                    done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                }
            }
            else {
                data.data.forEach(function (item) {
                    if (item.scat_Id == id) {
                        categorie = item.cat_Id
                        GetCategoriesListCreate(categorie)
                    }
                });
            }
        });
    }
    $.ajax({
        type: "GET",
        url: BaseUrl + "/SubCategories/SubCategoriesList",
    }).done(function (data, index) {
        $("#SelectSubcategories").empty();

        var NewOption = "<option value=" + 0 + " disabled> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
                done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
            }
        }
        else {
            data.data.forEach(function (item) {
                if (item.scat_Id == id) {
                    NewOption += "<option  value=" + item.scat_Id + "  selected>" + item.scat_Description + "</option>";
                }
                else {
                    if (item.cat_Id == value) {
                        NewOption += "<option  value=" + item.scat_Id + ">" + item.scat_Description + "</option>";
                    }
                }
            });
        }
        $("#SelectSubcategories").append(NewOption);

    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#SelectSubcategories').after($(message));
    });
}

$('#frmEditProducts').on('submit', function () {
    LimpiarSpanMessa();
    var result = ValidateFrmEditProducts();
    var pro_PurchasePrice = $('#frmEditProducts #pro_PurchasePrice').val();
    var pro_SalesPrice = $('#frmEditProducts #pro_SalesPrice').val();
    var pro_Stock = $('#frmEditProducts #pro_Stock').val();
    if (pro_PurchasePrice != null && pro_SalesPrice != null && pro_Stock != null) {
        if (pro_PurchasePrice.includes('Lps')) {
            pro_PurchasePrice = pro_PurchasePrice.slice(4, -3);
            pro_PurchasePrice = pro_PurchasePrice.replace(/,/g, "");
        }
        if (pro_SalesPrice.includes('Lps')) {
            pro_SalesPrice = pro_SalesPrice.slice(4, -3);
            pro_SalesPrice = pro_SalesPrice.replace(/,/g, "");
        }
        pro_Stock = pro_Stock.replace(/,/g, "");
    }

    if (result == true) {
        Swal.fire({
            width: '20%',
            height: '20%',
            text: "¿Estás seguro que deseas guardar los cambios?",
            icon: 'info',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            confirmButtonColor: '#6c757d',
            cancelButtonColor: '#001f52',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                var products = [
                    { name: "pro_Description", value: $('#frmEditProducts #pro_Description').val().trim() },
                    { name: "pro_PurchasePrice", value: pro_PurchasePrice },
                    { name: "pro_SalesPrice", value: pro_SalesPrice },
                    { name: "pro_Stock", value: pro_Stock },
                    { name: "pro_ISV", value: $('#frmEditProducts #pro_ISV').val() },
                    { name: "uni_Id", value: $('#frmEditProducts #uni_Id').val() },
                    { name: "scat_Id", value: $('#frmEditProducts #SelectSubcategories').val() },
                    { name: "pro_Id", value: $('#frmEditProducts #pro_Id').val() },
                    { name: "pro_IdUserCreate", value: UserId },
                    { name: "pro_IdUserModified", value: UserId },
                ];

                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/Products/Edit",
                    data: products,
                }).done(function (data) {
                    if (done == false) {
                        done = NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    }
                    setTimeout(function () {
                        location.assign(BaseUrl + "/products/index");
                    }, 1500)
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, data.message, data.id, data.data, data.type);
                    }
                });
            }
        });
    }
    return false;
});

$("input[data-type='currency']").on({
    keyup: function () {
        formatCurrency($(this));
    },
    blur: function () {
        formatCurrency($(this), "blur");
    }
});


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}


function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }
    if (input_val.includes("Lps")) {
        input_val = input_val.substring(4);
        if (/^0/.test(input_val)) {
            input_val = input_val.replace(/^0/, "")
        }
    }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(".") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered
        var decimal_pos = input_val.indexOf(".");

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);

        // validate right side
        right_side = formatNumber(right_side);

        // On blur make sure 2 numbers after decimal
        if (blur === "blur") {
            right_side += "00";
        }

        // Limit decimal to only 2 digits
        right_side = right_side.substring(0, 2);

        // join number by .
        input_val = "Lps " + left_side + "." + right_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        input_val = "Lps " + input_val;

        // final formatting
        if (blur === "blur") {
            input_val += ".00";
        }
    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}

$('#frmEditProducts #pro_Stock').keyup(function (e) {
    if (e.which >= 37 && e.which <= 40) {
        e.preventDefault();
    }

    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/([0-9])([0-9]{3})$/, '$1,$2')
            .replace(/\B(?=(\d{3})+(?!\d),?)/g, ",")
            ;
    });
});
$('#frmEditProducts #pro_ISV').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});


function ValidateFrmEditProducts() {
    var result = true;
    var count = 0;
    var pro_Description = $('#frmEditProducts #pro_Description');
    var pro_PurchasePrice = $('#frmEditProducts #pro_PurchasePrice');
    var pro_SalesPrice = $('#frmEditProducts #pro_SalesPrice');
    var pro_Stock = $('#frmEditProducts #pro_Stock');
    var pro_ISV = $('#frmEditProducts #pro_ISV');
    var uni_Id = $('#frmEditProducts #uni_Id');
    var SelectCategories = $('#frmEditProducts #SelectCategories');
    var SelectSubcategories = $('#frmEditProducts #SelectSubcategories');

    var divUnidad = $('#frmEditProducts #divUnidad');
    var divCategoria = $('#frmEditProducts #divCategoria');
    var divSubCat = $('#frmEditProducts #divSubCat');

    var borUni = $('#frmEditProducts #divUnidad span.select2-selection.select2-selection--single');
    var borCat = $('#frmEditProducts #divCategoria span.select2-selection.select2-selection--single');
    var borSub = $('#frmEditProducts #divSubCat span.select2-selection.select2-selection--single');

    result = MessagesError(pro_Description, null, 200, 'Descripcion');
    if (result == true) { count++; }
    result = MessagesError(pro_PurchasePrice, null, null, 'Precio de Compra');
    if (result == true) { count++; }
    result = MessagesError(pro_SalesPrice, null, null, 'Precio de Venta');
    if (result == true) { count++; }
    result = MessagesError(pro_Stock, null, null, 'Stock');
    if (result == true) { count++; }
    result = MessagesError(pro_ISV, null, 2, 'ISV');
    if (result == true) { count++; }
    result = MessageErrorDrop(uni_Id, 'Unidad de medida', divUnidad, borUni);
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectCategories, 'Categorías', divCategoria, borCat);
    if (result == true) { count++; }
    result = MessageErrorDrop(SelectSubcategories, 'Subcategorías', divSubCat, borSub);
    if (result == true) { count++; }
    if (count == 8) {
        return result;
    }
    return false;

    return result;
}

$('#frmEditProducts #pro_Description').on('keypress', function () {
    $('#frmEditProducts #pro_Description').css("border-color", "#eee");
});
$('#frmEditProducts #pro_PurchasePrice').on('keypress', function () {
    $('#frmEditProducts #pro_PurchasePrice').css("border-color", "#eee");
});
$('#frmEditProducts #pro_SalesPrice').on('keypress', function () {
    $('#frmEditProducts #pro_SalesPrice').css("border-color", "#eee");
});
$('#frmEditProducts #pro_Stock').on('keypress', function () {
    $('#frmEditProducts #pro_Stock').css("border-color", "#eee");
});
$('#frmEditProducts #pro_ISV').on('keypress', function () {
    $('#frmEditProducts #pro_ISV').css("border-color", "#eee");
});
$('#frmEditProducts #SelectSubcategories').on('select2:select', function () {
    $('#frmEditProducts #divSubCat span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmEditProducts #SelectCategories').on('select2:select', function () {
    $('#frmEditProducts #divCategoria span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmEditProducts #uni_Id').on('select2:select', function () {
    $('#frmEditProducts #divUnidad span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$("#frmEditProducts #pro_PurchasePrice").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmEditProducts #pro_SalesPrice").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmEditProducts #pro_Stock").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmEditProducts #pro_ISV").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})

function GetDropdowns() {
    $('#uni_Id').select2();
    $('#SelectCategories').select2();
    $('#SelectSubcategories').select2();
}
