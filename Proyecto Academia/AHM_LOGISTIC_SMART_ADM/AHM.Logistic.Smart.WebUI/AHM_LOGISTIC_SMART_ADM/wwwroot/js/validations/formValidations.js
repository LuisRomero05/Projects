//Esta función valida los formularios, retorna un booleano
//Pide los siguientes parámetros el input tal cual sin valor, ejemplo input = $(#ejemplo);
//el min este pueden volverlo un null y no pasará nada el min lo usan para cuantos caracteres minimo ocupan
//el max este pueden volverlo un null y no pasará nada el max lo usan para cuantos caracteres maximo ocupan
//el nameinput es el nombre del campo que quieren, ejemplo 'Descripción' y lo termina guardando.
// Pueden hacer lo asi 
//result = MessagesError(pro_Description, 8, 50, 'Description');
//result = MessagesError(pro_PurchasePrice, null, null, 'Precio de Compra');
function MessagesError(input, min, max, nameInput) {
    var message = "";
    var type = typeof input.val();
    switch (type) {
        case 'string':
            if (input.val() < "1") {
                message = `<span style="color: red;" name="Mesas">El campo ${nameInput} necesita una opción válida</span>`;
                input.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                return false;
            }
            if (input.val() === "" || (input.val().trim() === "")) {
                message = "";
                message = `<span style="color: red;" name="Mesas">El campo ${nameInput} es requerido</span>`;
                input.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                return false;
            }
            if (min != null) {
                if (input.val().length < min) {
                    message = "";                   
                    message = `<span style="color: red;" name="Mesas">El mínimo de caracteres para este campo es de ${min}</span>`;
                    input.after($(message).fadeToggle(3000));
                    input.css("border-color", "red");
                    return false;
                }
            }
            if (max != null) {
                if (input.val().length > max) {
                    message = "";
                    message = `<span style="color: red;" name="Mesas">El máximo de caracteres para este campo es de ${max}</span>`;
                    input.after($(message).fadeToggle(3000));
                    input.css("border-color", "red");
                    return false;
                }
            }
            if (input.val().indexOf('@', 0) == -1 || input.val().indexOf('.', 0) == -1) {
                if (input.val().indexOf('@', 0) == 1) {
                    message = "";
                    message = '<span style="color: red;" name="Mesas">El campo ' + nameInput + ' es inválido</span>';
                    input.after($(message).fadeToggle(3000));
                    input.css("border-color", "red");
                    return false;
                } else if (input.val().indexOf('.', 0) == 1) {
                    message = "";
                    message = '<span style="color: red;" name="Mesas">El campo ' + nameInput + ' es inválido</span>';
                    input.after($(message).fadeToggle(3000));
                    input.css("border-color", "red");
                    return false;
                }
            }
            if (input.val().includes("Lps")) {
                message = "";
                let valueValidation = input.val();
                valueValidation = valueValidation.slice(4, -3);
                valueValidation = valueValidation.replace(/,/g, "");
                if (valueValidation == "") {
                    message = `<span style="color: red;" name="Mesas">El campo ${nameInput} es requerido</span>`;
                    input.after($(message).fadeToggle(3000));
                    input.css("border-color", "red");
                    return false;
                }
                
            }
            break;

        default:
            message = "";
            message = '<span style="color: red;" name="Mesas">El campo ' + nameInput + ' es requerido</span>';
            input.after($(message).fadeToggle(3000));
            input.css("border-color", "red");
            return false;
            break;
    }

    return true;

}



function telMessagesError(input, nameInput, iti, mosMessage) {
    var message = "";
    var isValid = iti.isValidNumber();
    var type = typeof input.val();
    switch (type) {
        case 'string':
            if (input.val() === "") {
                message = `<span style="color: red;" name="Mesas">El campo ${nameInput} es requerido</span>`;
                mosMessage.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                return false;
            }
            if (input.val() === "0") {
                message = `<span style="color: red;" name="Mesas">El campo ${nameInput} necesita una opción válida</span>`;
                mosMessage.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                return false;
            }
            if (isValid === false) {
                message = `<span style="color: red;" name="Mesas">El campo de ${nameInput} no corresponde con al número de área del país seleccionado</span>`;
                mosMessage.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                return false;
            }

            break;

        default:
            message = `<span style="color: red; " name="Mesas">El campo de ${nameInput} es requerido</span>`;
            $(mosMessage).fadeToggle(3000);
            input.css("border-color", "red");
            return false;
            break;
    }

    return true;
};

function MessagesError2(input, min, max, nameInput, mosMessage) {
    var message = "";
    var type = typeof input.val();
    switch (type) {
        case 'string':
            if (input.val() === "0") {
                //message = `<span style="color: red;">El campo ${nameInput} necesita una opción válida</span>`;
                //input.after($(message).fadeToggle(3000));
                //input.css("border-color", "red");
                message = `<span name="Mesas" style="color: red;">El campo ${nameInput} es requerido</span>`;
                mosMessage.after($(message).fadeToggle(3000));
                input.css("border-color", "red");

                return false;
            }
            break;

        default:
            //message = '<span style="color: red;">El campo ' + nameInput + ' es requerido</span>';
            //input.after($(message).fadeToggle(3000));
            //input.css("border-color", "red");
            message = `<span name="Mesas" style="color: red;">El campo ${nameInput} es requerido</span>`;
            mosMessage.after($(message).fadeToggle(3000));
            input.css("border-color", "red");

            return false;
            break;
    }

    return true;

}

function MessageErrorDrop(input, nameInput, mosMessage, borde) {
    var message = "";
    var type = typeof input.val();
    switch (type) {
        case 'string':
            if (input.val() === "" || input.val() === "0") {
                message = `<span style="color: red;" name="Mesas">El campo ${nameInput} es requerido</span>`;
                mosMessage.after($(message).fadeToggle(3000));
                input.css("border-color", "red");
                borde.css("border-color", "red");
                return false;
            }
            break;

        default:
            message = `<span style="color: red; " name="Mesas">El campo de ${nameInput} es requerido</span>`;
            mosMessage.after($(message).fadeToggle(3000));
            input.css("border-color", "red");
            borde.css("border-color", "red");
            return false;
            break;
    }

    return true;
}

function LimpiarSpanMessa() {
    var secciones = document.getElementsByName('Mesas');
    if (secciones.length > 0) {
        var contador = secciones.length;
        for (var i = 0; i < contador; i++) {
            secciones[0].remove();
        }
    }
}
