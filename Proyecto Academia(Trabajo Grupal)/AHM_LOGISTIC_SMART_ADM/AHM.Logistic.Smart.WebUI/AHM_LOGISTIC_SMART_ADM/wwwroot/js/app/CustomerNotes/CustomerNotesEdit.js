var cun_Id = 0;
var pry_Id = 0;
var cus_Id = 0;
function ShowModalEditNotes(id) {
    cun_Id = id;
    GetNoteDetails(id);
    GetPrioritiesListEdit();
    $('#EditNotes').modal('show');
    LimpiarControles();

}

function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}

function GetNoteDetails(id) {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/CustomersNotes/Edit/" + id,
    }).done(function (data) {
        var dateAPI = data.data.cun_ExpirationDate;
        var date = getFormattedDate(dateAPI);
        $("#EditNotes #cun_Descripcion").val(data.data.cun_Descripcion);
        $("#EditNotes #cun_ExpirationDate").val(date);
        pry_Id = data.data.pry_Id;
        $("#prioritiesMdl").val(pry_Id);
        cus_Id = data.data.cus_Id;
    }).fail(function () {
        var message = '<span style="color: red;">No se pudieron cargar las opciones, comuníquese con el encargado.</span>';
        $('#EditNotes #cun_Descripcion').after($(message));
    });

}

function GetPrioritiesListEdit() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Priorities/PrioritiesList",
    }).done(function (data, index) {
        //Vaciar el Dropdawn
        $("#prioritiesMdl").empty();

        //variable que almacena las notas
        var Options = "";

        data.data.forEach(function (item) {
            if (item.pry_Id == pry_Id) {
                Options += "<option value=" + item.pry_Id + " selected>" + item.pry_Descripcion + "</option>";
            }
            else {
                Options += "<option value=" + item.pry_Id + ">" + item.pry_Descripcion + "</option>";
            }
        });

        //Agregar las notas al carrusel
        $("#prioritiesMdl").append(Options);
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar las prioridades");
    });
}


$('#EditNotes #EditNoteConfirm').on('click', function () {
    var result = ValidateEditNotes();
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
                data = [
                    { name: "cun_Id", value: cun_Id },
                    { name: "cun_Descripcion", value: $('#EditNotes #cun_Descripcion').val() },
                    { name: "cun_ExpirationDate", value: $('#EditNotes #cun_ExpirationDate').val() },
                    { name: "pry_Id", value: $('#EditNotes #prioritiesMdl').val() },
                    { name: "cus_Id", value: cus_Id },
                    { name: "cun_IdUserModified", value: TempUserDefault },
                ];

                $.ajax({
                    type: "PUT",
                    url: BaseUrl + "/CustomersNotes/Edit/",
                    data: data,

                }).done(function (data) {
                    location.reload();
                }).fail(function (data) {
                    if (done == false) {
                        NotificationMessage(data.success, "", data.id, data.data, data.type);
                    }
                });
            }
        })
    }
})

function ValidateEditNotes() {
    LimpiarSpanMessa();
    var result = true;
    var count = 0;
    var cun_Descripcion = $('#EditNotes #cun_Descripcion');
    var cun_ExpirationDate = $('#EditNotes #cun_ExpirationDate');
    var pry_Id = $('#EditNotes #prioritiesMdl');
    var divMesa = $('#EditNotes #divPrioridadMdl');

    result = MessagesError(cun_Descripcion, 0, null, 'Descripción');
    if (result == true) { count++; }

    result = MessagesError(cun_ExpirationDate, null, 10, 'Fecha de expiracion');
    if (result == true) { count++; }

    result = MessageErrorDrop(pry_Id, 'prioridad', divMesa);
    if (result == true) { count++; }

    if (count == 3) {
        return result;
    }
    return false;
}

function LimpiarControles() {
    $('#EditNotes #cun_Descripcion').css("border-color", "#eee");
    $('#EditNotes #cun_ExpirationDate').css("border-color", "#eee");
    $('#EditNotes #prioritiesMdl').css("border-color", "#eee");
}

$('#EditNotes #cun_Descripcion').on('keypress', function () {
    $('#EditNotes #cun_Descripcion').css("border-color", "#eee");
});
$('#EditNotes #cun_ExpirationDate').on('keypress', function () {
    $('#EditNotes #cun_ExpirationDate').css("border-color", "#eee");
});
$('#EditNotes #pry_Id').on('keypress', function () {
    $('#EditNotes #prioritiesMdl').css("border-color", "#eee");
});

