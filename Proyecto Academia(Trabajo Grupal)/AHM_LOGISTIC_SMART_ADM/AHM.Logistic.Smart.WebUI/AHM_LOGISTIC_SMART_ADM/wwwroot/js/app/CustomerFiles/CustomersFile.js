$("#btn_add").prop("disabled", true);
$("#danger").show();
$("#success").hide();

$("#btn_addFile").change(function () {
    $file = $(this).val();
    var fileName = document.getElementById("btn_addFile").files[0].name;
    var fileSize = document.getElementById("btn_addFile").files[0].size;
    if (fileSize > 1073741824 && fileSize != null) {
        iziToast.error({
            message: '¡Se produjo un error, el archivo supera el limite de 100MB!',
            displayMode: 'replace'
        });
        $file = null;
        $(this).val('');
        $("#btn_add").prop("disabled", true);
        $("#danger").show();
        $("#success").hide();
    }
    if ($file == "") {
        $("#btn_add").prop("disabled", true);
        $("#danger").show();
        $("#success").hide();
    } else if ($file != null) {
        $("#btn_add").prop("disabled", false);
        $("#success").show();
        $("#danger").hide();
        $('#labelNameFile').html("");
        $('#labelNameFile').html(fileName);
    }
});