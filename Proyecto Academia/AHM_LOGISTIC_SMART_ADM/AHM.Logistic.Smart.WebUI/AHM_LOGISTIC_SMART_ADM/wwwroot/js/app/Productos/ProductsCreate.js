var id = $("#subcategories").val();
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

        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
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
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        if (data.data == null) {
            if (done == false) {
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

$('#frmCreateProducts').on('submit', function (e) {
    LimpiarSpanMessa();
    var result = ValidateFrmProducts();
    var pro_PurchasePrice = $('#frmCreateProducts #pro_PurchasePrice').val();
    pro_PurchasePrice = pro_PurchasePrice.slice(4, -3);
    pro_PurchasePrice = pro_PurchasePrice.replace(/,/g, "");
    var pro_SalesPrice = $('#frmCreateProducts #pro_SalesPrice').val();
    pro_SalesPrice = pro_SalesPrice.slice(4, -3);
    pro_SalesPrice = pro_SalesPrice.replace(/,/g, "");
    var pro_Stock = $('#frmCreateProducts #pro_Stock').val();
    pro_Stock = pro_Stock.replace(/,/g, "");
    if (result == true) {
        var products = [
            { name: "pro_Description", value: $('#frmCreateProducts #pro_Description').val().trim() },
            { name: "pro_PurchasePrice", value: Number(pro_PurchasePrice) },
            { name: "pro_SalesPrice", value: Number(pro_SalesPrice) },
            { name: "pro_Stock", value: Number(pro_Stock) },
            { name: "pro_ISV", value: $('#frmCreateProducts #pro_ISV').val() },
            { name: "uni_Id", value: $('#frmCreateProducts #uni_Id').val() },
            { name: "scat_Id", value: $('#frmCreateProducts #SelectSubcategories').val() },
            { name: "pro_IdUserCreate", value: UserId },
            { name: "pro_IdUserModified", value: null },
        ];
        $.ajax({
            type: "POST",
            url: BaseUrl + "/Products/Create",
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
    return false;
});

$("input[data-type='currency']").on({
    keyup: function () {
        formatCurrency($(this));
    },
    keydown: function () {
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

$('#frmCreateProducts #pro_Stock').keyup(function (e) {
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

$('#frmCreateProducts #pro_ISV').keyup(function (e) {
    if (/\D/g.test(this.value)) {
        // Filter non-digits from input value.
        this.value = this.value.replace(/\D/g, '');
    }
});

function ValidateFrmProducts() {
    LimpiarSpanMessa();
    var result = false;
    var count = 0;
    var pro_Description = $('#frmCreateProducts #pro_Description');
    var pro_PurchasePrice = $('#frmCreateProducts #pro_PurchasePrice');
    var pro_SalesPrice = $('#frmCreateProducts #pro_SalesPrice');
    var pro_Stock = $('#frmCreateProducts #pro_Stock');
    var pro_ISV = $('#frmCreateProducts #pro_ISV');
    var uni_Id = $('#frmCreateProducts #uni_Id');
    var SelectCategories = $('#frmCreateProducts #SelectCategories');
    var SelectSubcategories = $('#frmCreateProducts #SelectSubcategories');

    var divUnidad = $('#frmCreateProducts #divUnidad');
    var divCategoria = $('#frmCreateProducts #divCategoria');
    var divSubCat = $('#frmCreateProducts #divSubCat');

    var borUni = $('#frmCreateProducts #divUnidad span.select2-selection.select2-selection--single');
    var borCat = $('#frmCreateProducts #divCategoria span.select2-selection.select2-selection--single');
    var borSub = $('#frmCreateProducts #divSubCat span.select2-selection.select2-selection--single');

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
}

$('#frmCreateProducts #pro_Description').on('keypress', function () {
    $('#frmCreateProducts #pro_Description').css("border-color", "#eee");
});
$('#frmCreateProducts #pro_PurchasePrice').on('keypress', function () {
    $('#frmCreateProducts #pro_PurchasePrice').css("border-color", "#eee");
});
$('#frmCreateProducts #pro_SalesPrice').on('keypress', function () {
    $('#frmCreateProducts #pro_SalesPrice').css("border-color", "#eee");
});
$('#frmCreateProducts #pro_Stock').on('keypress', function () {
    $('#frmCreateProducts #pro_Stock').css("border-color", "#eee");
});
$('#frmCreateProducts #pro_ISV').on('keypress', function () {
    $('#frmCreateProducts #pro_ISV').css("border-color", "#eee");
});
$('#frmCreateProducts #SelectSubcategories').on('select2:select', function () {
    $('#frmCreateProducts #divSubCat span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmCreateProducts #SelectCategories').on('select2:select', function () {
    $('#frmCreateProducts #divCategoria span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$('#frmCreateProducts #uni_Id').on('select2:select', function () {
    $('#frmCreateProducts #divUnidad span.select2-selection.select2-selection--single').css("border-color", "#eee");
});
$("#frmCreateProducts #pro_PurchasePrice").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmCreateProducts #pro_SalesPrice").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmCreateProducts #pro_Stock").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})
$("#frmCreateProducts #pro_ISV").on("input", function () {
    if (/^0/.test(this.value)) {
        this.value = this.value.replace(/^0/, "")
    }
})

function GetDropdowns() {
    $('#uni_Id').select2();
    $('#SelectCategories').select2();
    $('#SelectSubcategories').select2();
}

