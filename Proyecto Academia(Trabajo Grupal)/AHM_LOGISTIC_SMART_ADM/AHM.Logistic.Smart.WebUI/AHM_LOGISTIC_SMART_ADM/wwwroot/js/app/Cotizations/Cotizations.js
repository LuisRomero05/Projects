//DECLARACION DE VARIABLES
var Productos = [];
var ProductosDetail = [];
var Precios = [];
var precioUnidad = 0;
var nuevoStock = 0;
var total = 0;
var cont = 0;
var cont2 = 0;
var cont3 = 0;
var cont4 = 0;
var prime = 0;
var Descripcion = "";
var tablaPro = "";
var PantId;
var Pass = 0;
/*-------*/
var Stock = []; 
var Clientes = [];
var DeleteProducts = [];

//formato de fecha
function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}

//Numeros en texto
//$('#Cant').keyup(function (e) {
//    if (e.which >= 37 && e.which <= 40) {
//        e.preventDefault();
//    }

//    $(this).val(function (index, value) {
//        return value
//            .replace(/\D/g, "")
//            .replace(/([0-9])([0-9]{3})$/, '$1,$2')
//            .replace(/\B(?=(\d{3})+(?!\d),?)/g, "")
//            ;
//    });
//});

//Variables de llenado
var cusId = 0;
var fecha;
var id = 0;
var idDet = 0;

//EJECUTAR FUNCIONES EN LA PRIMER CARGA DE LA PAGINA
$(document).ready(function () {
    StartIdDetail();
    //INICIAR FUNCIONES
    GetCotizationList();
    GetCustomersListCreate();
    GetProductsListCreate2();

    //Obtener Id de de detalle
    Productos = [];
});

//INICIAR VARIABLE DE ID DETALLE
function StartIdDetail() {
    var url = window.location.pathname;
    id = url.substring(url.lastIndexOf('/') + 1);
    //console.log(id);
};

function ValidateProducts() {
    if (Productos == 0 && ProductosDetail == 0) {
        tablaPro = "";
    }
}

//OBTENER LA LISTA DE CLIENTES PARA LLENAR EL DROP-DOWN
function GetCustomersListCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Customers/CustomersList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        var valor = $("#valor").val();
        $("#SelectCustomers").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";

        data.data.forEach(function (item) {

            Clientes.push({ id: item.cus_Id, email: item.cus_Email, rtn: item.cus_RTN, cel: item.cus_Phone, dir: item.cus_Address, cus: item.cus_Name });

            if (item.cus_Id == cusId) {
                NewOption += "<option  value=" + item.cus_Id + " selected>" + item.cus_Name + "</option>";
                llenarData();

            }
            else {
                NewOption += "<option  value=" + item.cus_Id + ">" + item.cus_Name + "</option>";
            }
        });



        //Agregar las opciones al dropdownlist
        $("#SelectCustomers").append(NewOption);
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los Clientes");
    });

}

//OBTENER LOS DATOS DE COTIZACION POR ID PARA EL UPDATE
function GetCotizationList() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Quote/CotizationsList",
    }).done(function (data, index) {


        data.data.forEach(function (item) {
            if (item.cot_Id == id) {
                cusId = item.cus_Id;
                fech = item.cot_DateValidUntil;
                fecha = getFormattedDate(fech);
            }
        });
        llenarData();
        GetCustomersListCreate();
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar la Cotizacion");
    });

}

function getFormattedDate(date) {
    var d = new Date(date);
    let year = d.getFullYear();
    let month = (1 + d.getMonth()).toString().padStart(2, '0');
    let day = d.getDate().toString().padStart(2, '0');

    return year + '-' + month + '-' + day;
}

//LLENAR LOS CAMPOS DE INFORMACION GENERAL DEL CLIENTE
function llenarData() {
    var userSelect = $("#SelectCustomers").val();
    Clientes.forEach(function (item) {
        if (item.id == cusId || item.id == userSelect) {
            $("#SelectCustomers").val(item.id);
            $("#PersonEmail").text(item.email);
            $("#PersonRtn").text(item.rtn);
            $("#PersonTelefono").text(item.cel);
            $("#PersonDireccion").text(item.dir);
            $("#fechaexpiracion").val(fecha);
            $("#FechaExpira").text(fecha);
            $("#PersonCustomers").text(item.cus);
        }
    });
    //console.log(fecha);
    //console.log(id);
    //console.log(Clientes);
}

//OBTENER LA LISTA DE PRODUCTOS PARA LLENAR EL DROP-DOWN
function GetProductsListCreate() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Products/ProductsList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectProduct").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        if (prime == 0) {
            data.data.forEach(function (item) {
                Precios.push({ id: item.pro_Id, descripcion: item.pro_Description, precio: item.pro_SalesPrice, sto: item.pro_Stock })
                Stock.push({ id: item.pro_Id, descripcion: item.pro_Description, precio: item.pro_SalesPrice, sto: item.pro_Stock })
                if (item.scat_Id == 0) {
                    NewOption += "<option  value=" + item.pro_Id + " selected>" + item.pro_Description + " - Lps." + item.pro_SalesPrice + " - Stock: " + item.pro_Stock + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.pro_Id + ">" + item.pro_Description + " - Lps." + item.pro_SalesPrice + " - Stock: " + item.pro_Stock + "</option>";
                }
            })
        } else {
            Stock.forEach(function (item) {
                if (item.id == 0) {
                    NewOption += "<option  value=" + item.id + " selected>" + item.descripcion + " - Lps." + item.precio + " - Stock: " + item.sto + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.id + ">" + item.descripcion + " - Lps." + item.precio+ " - Stock: " + item.sto + "</option>";
                }
            })
        };
        //Agregar las opciones al dropdownlist
        $("#SelectProduct").append(NewOption);
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los Clientes");
    });
}


function GetProductsListCreate2() {
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Products/ProductsList",
    }).done(function (data, index) {
        //Vaciar el dropdownlist
        $("#SelectProduct").empty();
        //variable que almacena las opciones
        var NewOption = "<option value=" + 0 + "> Por favor seleccione una opción... </option>";
        if (prime == 0) {
            data.data.forEach(function (item) {
                Precios.push({ id: item.pro_Id, descripcion: item.pro_Description, precio: item.pro_SalesPrice, sto: item.pro_Stock })
                Stock.push({ id: item.pro_Id, descripcion: item.pro_Description, precio: item.pro_SalesPrice, sto: item.pro_Stock })
                if (item.scat_Id == 0) {
                    NewOption += "<option  value=" + item.pro_Id + " selected>" + item.pro_Description + " - Lps." + item.pro_SalesPrice + " - Stock: " + item.pro_Stock + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.pro_Id + ">" + item.pro_Description + " - Lps." + item.pro_SalesPrice + " - Stock: " + item.pro_Stock + "</option>";
                }
            })
        } else {
            Stock.forEach(function (item) {
                if (item.id == 0) {
                    NewOption += "<option  value=" + item.id + " selected>" + item.descripcion + " - Lps." + item.precio + " - Stock: " + item.sto + "</option>";
                }
                else {
                    NewOption += "<option  value=" + item.id + ">" + item.descripcion + " - Lps." + item.precio + " - Stock: " + item.sto + "</option>";
                }
            })
        };
        //Agregar las opciones al dropdownlist
        $("#SelectProduct").append(NewOption);
        if (PantId == 2) {
            GetCotizationsDetailsList();
        }
        if (PantId == 1) {
            GetCotizationsDetailList();
            tablaPro = " ";
        }
    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los Clientes");
    });
}


//AGREGAR LOS PRODUCTOS SELECCIONADOS A LA TABLA
function AddProductos() {
    prime = 1;
    var valor = $("#SelectProduct").val();
    /*var texto = valor2.options[valor2.selectedIndex].text;*/
    var cantidad = $("#Cant").val();
    var tablaProductos = "";
    //obtener el precio, descripcion y la suma de los totales de productos
    Stock.forEach(function (pre) {
        if (valor == pre.id) {
            precioUnidad = pre.precio;
            Descripcion = pre.descripcion;
            if (pre.sto < cantidad) {
                NoStockAlert();
                Pass = 1;
            } else {
                total += parseInt(pre.precio) * parseInt(cantidad);
                nuevoStock = parseInt(pre.sto) - cantidad;
                Stock.forEach(function (pre) {
                    if (pre.id == valor) {
                        pre.sto = pre.sto - cantidad;
                    };
                });
            }
        } 

    });
    if (Pass == 0) {
        if (PantId == 0) {
            //Insertar el primer registro al objeto [Productos]
            if (Productos.length == 0) {
                Productos.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
            }
            else {
                //Sumar las cantidades si encuentra el producto registrado en el objeto [Productos]
                Productos.forEach(function (item) {
                    if (item.id == valor) {
                        item.canti = parseInt(item.canti) + parseInt(cantidad);
                        item.total = parseInt(item.canti) * parseInt(item.precio);
                        cont2++;
                    }
                });
                //Insertar el toda la informacion del producto si no lo encuentra en el objeto [Productos]
                if (cont2 == 0) {
                    Productos.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
                }
            }
        }
        else if (PantId == 1) {
            if (Productos.length == 0) {
                if (ProductosDetail.length == 0) {
                    ProductosDetail.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
                }
                else {
                    ProductosDetail.forEach(function (item) {
                        if (item.id == valor) {
                            item.canti = parseInt(item.canti) + parseInt(cantidad);
                            item.total = parseInt(item.canti) * parseInt(item.precio);
                            cont2++;
                        }
                    });

                    if (cont2 == 0) {
                        ProductosDetail.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
                    }
                }
            }


            Productos.forEach(function (item) {
                if (item.id == valor) {
                    item.canti = parseInt(item.canti) + parseInt(cantidad);
                    item.total = parseInt(item.canti) * parseInt(item.precio);
                    cont3++;
                }
            });

            if (cont3 == 0 && Productos.length != 0) {
                if (ProductosDetail.length == 0) {
                    ProductosDetail.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
                }
                else {
                    ProductosDetail.forEach(function (item) {
                        if (item.id == valor) {
                            item.canti = parseInt(item.canti) + parseInt(cantidad);
                            item.total = parseInt(item.canti) * parseInt(item.precio);
                            cont2++;
                        }
                    });

                    if (cont2 == 0) {
                        ProductosDetail.push({ id: parseInt(valor), canti: parseInt(cantidad), descripcion: Descripcion, precio: precioUnidad, total: parseInt(cantidad) * parseInt(precioUnidad) });
                    }
                }
            }
        }
    }

    //recorrer el objeto [Productos] y agregarlos a la variable
    Productos.forEach(function (item) {
        tablaProductos += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item.id + ")'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a ></div></td><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
    });
    
    ProductosDetail.forEach(function (item) {
        tablaProductos += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item.id + ")'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a ></div></td><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
    });

    console.log(Productos);
    console.log("Detail", ProductosDetail);
    console.log(Precios);
    tablaPro = tablaProductos;
    //Pintar los datos de las variables en la tabla y el total
    document.getElementById("tableContent").innerHTML = tablaProductos;
    document.getElementById("TotalCotization").innerHTML = total;
    $("#Cant").val("");
    $("#SelectProduct").val("0");
    cont2 = 0;
    cont3 = 0;
    cantidad = 0;
    Pass = 0;
    GetProductsListCreate();
}


//formatear fecha
function formatDate(date, esIngresada) {
    let formatted_date = "";
	if (esIngresada) {
        formatted_date = (date.getDate() + 1) + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
    }
	else {
        formatted_date = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
	}
    return formatted_date;
}

//REALIZAR REGISTRO A LA BASE DE DATOS
function InsertarCotizacion() {
    var cus = $("#SelectCustomers").val();
    var date = $("#fechaexpiracion").val();
    let fechaIngresada = new Date(date);
    var InsertPro = [];

    //Fecha actual
    let fechaActual = new Date();

    //console.log("Fecha ingresada", formatDate(fechaIngresada,true));
    //console.log("Fecha generada", formatDate(fechaActual, false));

    //Fecha ingresada

    if (formatDate(fechaIngresada, true) < formatDate(fechaActual, false)){
        iziToast.warning({
                message: '¡La fecha ingresada no es valida!'
            });
      }
    else {
        Productos.forEach(function (pre) {
            InsertPro.push({
                "code_Id": 0,
                "code_Cantidad": parseInt(pre.canti),
                "pro_Id": parseInt(pre.id),
                "code_TotalPrice": pre.total,
            });
        });
        var InsertCoti = {
            "cot_Id": 0,
            "cus_Id": parseInt(cus),
            "cot_DateValidUntil": date,
            "sta_Id": 1,
            "cot_IdUserCreate": 1,
            "cot_IdUserModified": 0,
            "cot_details": InsertPro
        };
        $.ajax({
            type: "POST",
            url: BaseUrl + "/quote/create",
            data: InsertCoti,
            dataType: "json",
        }).done(function (data) {

        }).fail(function () {
            UpdateStock();
            iziToast.success({
                message: '¡Registro creado exitosamente!'
            });
            setTimeout(function () {
                location.assign(BaseUrl + "/quote/index");
            }, 1500)
        });
	}
}

//RESTAR LA CANTIDAD DE PRODUCTOS SELECCIONADA
function restarProductos(id) {
    var tablaProductos2 = "";
    var cont = 0;
    var cont3 = 0;
    prime = 1;
    $("#tableContent").val("");


    Productos.forEach(function (item) {
        if (item.id == id) {
            item.canti = parseInt(item.canti) - 1;
            item.total = parseInt(item.canti) * parseInt(item.precio);
            Stock.forEach(function (pre) {
                if (pre.id == id) {
                    pre.sto = pre.sto + 1;
                };
            });
        }

        if (item.canti == 0) {
            Productos.splice(cont, 1);
            DeleteProducts.push({ IdProduct: item.id });
        }
       
        cont++;
    });

    ProductosDetail.forEach(function (item) {
        if (item.id == id) {
            item.canti = parseInt(item.canti) - 1;
            item.total = parseInt(item.canti) * parseInt(item.precio);
            Stock.forEach(function (pre) {
                if (pre.id == id) {
                    pre.sto = pre.sto + 1;
                };
            });
        }

        if (item.canti == 0) {
            ProductosDetail.splice(cont4, 1);
        }

        cont4++;
    });
    
    Productos.forEach(function (item2) {

        if (cont3 == 0) {
            total = parseInt(item2.canti) * parseInt(item2.precio);
        }
        else {
            total += parseInt(item2.canti) * parseInt(item2.precio);
        }

        tablaProductos2 += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item2.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item2.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a></div></td><td>" + item2.canti + "</td><td>" + item2.descripcion + "</td><td>" + item2.precio + "</td><td>" + item2.total + "</td></tr>";
        cont3++;
    });

    ProductosDetail.forEach(function (item3) {

        if (cont3 == 0) {
            total = parseInt(item3.canti) * parseInt(item3.precio);
        }
        else {
            total += parseInt(item3.canti) * parseInt(item3.precio);
        }

        tablaProductos2 += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item3.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item3.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a></div></td><td>" + item3.canti + "</td><td>" + item3.descripcion + "</td><td>" + item3.precio + "</td><td>" + item3.total + "</td></tr>";
        cont3++;
    });

    if (Productos.length == 0 && ProductosDetail.length == 0) {
        total = 0;
    }

    document.getElementById("TotalCotization").innerHTML = total;
    document.getElementById("tableContent").innerHTML = tablaProductos2;
    //console.log(Productos);
    ValidateProducts();
    cont = 0;
    cont3 = 0;
    cont4 = 0;
    GetProductsListCreate();
    //console.log("=======Productos Eliminados=======")
    //console.log(DeleteProducts);
}

//ELIMINAR PRODUCTOS SELECCIONADOS
function EliminarProductos(id) {
    var tablaProductos2 = "";
    var cont = 0;
    var cont1 = 0;
    var cont2 = 0;
    var cont3 = 0;
    var cantidad = 0;
    prime = 1;
    Productos.forEach(function (item) {
        if (item.id == id) {
            Productos.splice(cont, 1);
            DeleteProducts.push({ IdProduct: item.id });
            cantidad = item.canti;
            Stock.forEach(function (pre) {
                if (pre.id == id) {
                    pre.sto = pre.sto + cantidad;
                };
            });
        }
        cont++
    });

    ProductosDetail.forEach(function (item1) {
        if (item1.id == id) {
            ProductosDetail.splice(cont1, 1);
            cantidad = item.canti;
            Stock.forEach(function (pre) {
                if (pre.id == id) {
                    pre.sto = pre.sto + cantidad;
                };
            });
        }
        cont1++
    });

    Productos.forEach(function (item2) {
        if (cont2 == 0) {
            total = parseInt(item2.canti) * parseInt(item2.precio);
        }
        else {
            total += parseInt(item2.canti) * parseInt(item2.precio);
        }
        tablaProductos2 += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item2.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item2.id + ");'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a></div></td><td>" + item2.canti + "</td><td>" + item2.descripcion + "</td><td>" + item2.precio + "</td><td>" + item2.total + "</td></tr>";
        cont2++;
    });

    ProductosDetail.forEach(function (item3) {
        total += item3.total;
        tablaProductos2 += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item3.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item3.id + ");'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a></div></td><td>" + item3.canti + "</td><td>" + item3.descripcion + "</td><td>" + item3.precio + "</td><td>" + item3.total + "</td></tr>";
    });

    if (Productos.length == 0 && ProductosDetail.length == 0) {
        total = 0;
    }

    document.getElementById("TotalCotization").innerHTML = total;
    document.getElementById("tableContent").innerHTML = tablaProductos2;
    cont = 0;
    cont1 = 0;
    cont2 = 0;
    cont3 = 0;

    //console.log("=======Productos Eliminados=======")
    ValidateProducts();
    GetProductsListCreate();
    //console.log(DeleteProducts);
    //console.log(ProductosDetail);
}

//LLENAR DETALLE UPDATE
function GetCotizationsDetailList() {
    var cont = 0;
    var tablaProductos = "";
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Quote/DetailsList?Id=" + id,
    }).done(function (data, index) {

        data.data.forEach(function (item) {
            Stock.forEach(function (pre) {
                if (item.pro_Id == pre.id && item.code_Status == 1 && item.cot_Id == id) {
                    Productos.push({ id: pre.id, idDet: item.code_Id, canti: item.code_Cantidad, descripcion: pre.descripcion, precio: pre.precio, total: item.code_TotalPrice });
                    total += pre.precio * item.code_Cantidad;
                }
            });
        });

        Productos.forEach(function (item) {
            if (cont == 0) {
                tablaProductos = "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item.id + ")'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a ></div></td><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
            }
            else {
                tablaProductos += "<tr><td style='text-align:center; color: #FFF'><div class='flex align-items-lg-start list-user-action'><a class='btn btn-sm btn-icon btn-primary' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick='EliminarProductos(" + item.id + ")'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><path d='M19.3248 9.46826C19.3248 9.46826 18.7818 16.2033 18.4668 19.0403C18.3168 20.3953 17.4798 21.1893 16.1088 21.2143C13.4998 21.2613 10.8878 21.2643 8.27979 21.2093C6.96079 21.1823 6.13779 20.3783 5.99079 19.0473C5.67379 16.1853 5.13379 9.46826 5.13379 9.46826' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M20.708 6.23975H3.75' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path><path d='M17.4406 6.23973C16.6556 6.23973 15.9796 5.68473 15.8256 4.91573L15.5826 3.69973C15.4326 3.13873 14.9246 2.75073 14.3456 2.75073H10.1126C9.53358 2.75073 9.02558 3.13873 8.87558 3.69973L8.63258 4.91573C8.47858 5.68473 7.80258 6.23973 7.01758 6.23973' stroke='currentColor' stroke-width='1.5' stroke-linecap='round' stroke-linejoin='round'></path></svg></span></a> | <a class='btn btn-sm btn-icon btn-dark' data-toggle='tooltip' data-placement='top' data-original-title='Generate' onclick='restarProductos(" + item.id + "); GetProductsListCreate();'><span class='btn-inner'><svg width='20' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg' stroke='currentColor'><line x1='5' y1='12' x2='19' y2='12' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'></line></svg></span></a ></div></td><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
            }
            cont++;
        });
        //Agregar las opciones a la tabla
        document.getElementById("TotalCotization").innerHTML = total;
        document.getElementById("tableContent").innerHTML = tablaProductos;

    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los prouctos");
    });


}
    
//ACTUALIZAR LA COTIZACION
function ActualizarCotizacion() {
    var cus = $("#SelectCustomers").val();
    var date = $("#fechaexpiracion").val();
    let fechaIngresada = new Date(date);
    var InsertPro = [];

    //Fecha actual
    let fechaActual = new Date();

    if (formatDate(fechaIngresada, true) < formatDate(fechaActual, false)) {
        iziToast.warning({
            message: '¡La fecha ingresada no es valida!'
        });
    }
	else {
        Productos.forEach(function (pre) {
            InsertPro.push({
                "code_Id": parseInt(pre.idDet),
                "code_Cantidad": parseInt(pre.canti),
                "pro_Id": parseInt(pre.id),
                "code_TotalPrice": parseInt(pre.total),
            });
        });
        var InsertCoti = {
            "cot_Id": parseInt(id),
            "cus_Id": parseInt(cus),
            "cot_DateValidUntil": date,
            "sta_Id": 1,
            "cot_IdUserCreate": 1,
            "cot_IdUserModified": 1,
            "cot_details": InsertPro
        };

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
                $.ajax({
                    type: "POST",
                    url: BaseUrl + "/quote/edit",
                    data: InsertCoti,
                    dataType: "json",
                }).done(function (data) {
                    iziToast.success({
                        message: '¡Registro actualizado exitosamente!'
                    });
                    setTimeout(function () {
                        location.assign(BaseUrl + "/quote/index");
                    }, 1500)
                    InsertarNewCotizacionDetail();
                    EliminarProducto();
                }).fail(function () {

                    iziToast.success({
                        message: '¡Registro actualizado exitosamente!'
                    });
                    setTimeout(function () {
                        location.assign(BaseUrl + "/quote/index");
                    }, 1500)
                    InsertarNewCotizacionDetail();
                    EliminarProducto();
                });
            }
        })
	}
}

//ELIMINAR PRODUCTO DE COTIZACION EXISTENTE
function EliminarProducto() {

    DeleteProducts.forEach(function (item) {
        var idu = 1;
        $.ajax({
            type: "DELETE",
            url: BaseUrl + "/quote/DeleteDetailProduct?Id=" + parseInt(item.IdProduct) + "&IdCot=" + parseInt(id) + "&Mod=" + idu,
            success: function (response) {
                console.log("Procedimiento realizado exitosamente");
            }
        }).done(function (data) {
            console.log("Procedimiento realizado exitosamente");
        });
        //console.log(item.IdProduct);
        //console.log(id);
    });
};

function InsertarNewCotizacionDetail() {
    var InsertProDetail = [];
    ProductosDetail.forEach(function (pre) {
        InsertProDetail.push({
            "code_Cantidad": parseInt(pre.canti),
            "pro_Id": parseInt(pre.id),
            "code_TotalPrice": pre.total,
            "cot_Id": parseInt(id),
            "code_IdUserCreate": 1,
            "code_IdUserModified": 1,
        });
    });

    var InsertCoti = {
        "cot_details": InsertProDetail,
    };

    $.ajax({
        type: "POST",
        url: BaseUrl + "/quote/InsertCotizationDetail",
        data: InsertCoti,
        dataType: "json",
    }).done(function (data) {



    }).fail(function () {
        /*console.log("AndonyBolo")*/
        //location.reload();

    });

    console.log("InsertProDetail:", InsertProDetail);
}

//LLENAR DETALLE UPDATE SIN BOTONES
function GetCotizationsDetailsList() {
    var cont = 0;
    var tablaProductos = "";
    $.ajax({
        type: "GET",
        url: BaseUrl + "/Quote/DetailsList?Id=" + id,
    }).done(function (data, index) {

        data.data.forEach(function (item) {
            Precios.forEach(function (pre) {
                if (item.pro_Id == pre.id && item.code_Status == 1) {
                    Productos.push({ id: pre.id, idDet: item.code_Id, canti: item.code_Cantidad, descripcion: pre.descripcion, precio: pre.precio, total: item.code_TotalPrice });
                    total += pre.precio * item.code_Cantidad;
                }
            });
        });

        Productos.forEach(function (item) {
            if (cont == 0) {
                tablaProductos = "<tr><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
            }
            else {
                tablaProductos += "<tr><td>" + item.canti + "</td><td>" + item.descripcion + "</td><td>" + item.precio + "</td><td>" + item.total + "</td></tr>";
            }
            cont++;
        });
        //console.log(Productos);
        //console.log(Precios);
        //Agregar las opciones a la tabla
        document.getElementById("TotalCotization").innerHTML = total;
        document.getElementById("tableContent").innerHTML = tablaProductos;

    }).fail(function () {
        //mostrar alerta en caso de error
        console.log("Error al cargar los prouctos");
    });


}

//Alerta de no stock
function NoStockAlert() {
    iziToast.warning({
        title: 'Alerta',
        message: 'La cantidad ingresada supera el stock disponible',
    });
}

function UpdateStock() {
    Stock.forEach(function (pre) {
        var InsertStock = {
            "pro_Id": 0,
            "pro_Stock": parseInt(pre.sto),
            "pro_IdUserModified": 1
        };
        $.ajax({
            type: "POST",
            url: BaseUrl + "/quote/UpdateStock?Id=" + parseInt(pre.id),
            data: InsertStock,
            dataType: "json",
        })
    });
    
}