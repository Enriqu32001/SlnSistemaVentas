const MODELO_BASE = {
    idCliente: 0,
    PNySN: "",
    PAySA: "",
    direccion: "",
    telefonoPrincipal: "",
    telefonoSecundario: "",
    numeroIdentificacion: "",
    idTipoIdentificacion: 0,
    esActivo: 1,
}

let tablaData;

$(document).ready(function () {

/*    debugger*/

    fetch("/TipoIdentificacion/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboclientetipodocumento").append(
                        $("<option>").val(item.idTipoIdentificacion).text(item.nombreIdentificacion)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Cliente/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCliente", "visible": false, "searchable": false },
            {"data": "pNySN"},
            { "data": "pAySA" },
            { "data": "direccion" },
            { "data": "telefonoPrincipal" },
            { "data": "telefonoSecundario" },
            { "data": "numeroIdentificacion" },
            { "data": "nombreIdentificacion" },
            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return `<span class="badge badge-info">Activo</span>`;
                    else
                        return `<span class="badge badge-danger">No Activo</span>`;
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Cliente',
                exportOptions: {
                    columns: [1,2, 3, 4, 5, 6,7]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})
function mostrarModal(modelo = MODELO_BASE) {
    $("#txtid").val(modelo.idCliente);
    $("#txtNumeroDocumento").val(modelo.numeroIdentificacion);
    $("#txtNombres").val(modelo.PNySN);
    $("#txtApellidos").val(modelo.PAySA);
    $("#txtDireccion").val(modelo.direccion);
    $("#txtTelefonoPrincipal").val(modelo.telefonoPrincipal)
    $("#txtTelefonoSecundarios").val(modelo.telefonoSecundario)
    $("#cboclientetipodocumento").val(modelo.idTipoIdentificacion == 0 ? $("#cboclientetipodocumento option:first").val() : modelo.idTipoIdentificacion)
    $("#cboEstado").val(modelo.esActivo);

    $("#FormModal").modal("show");

}

$("#btnNuevo").click(function () {
    mostrarModal()
})

$("#btnGuardar").click(async function () {

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo : "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idCliente"] = parseInt($("#txtid").val())
    modelo["numeroIdentificacion"] = $("#txtNumeroDocumento").val()
    modelo["PNySN"] = $("#txtNombres").val()
    modelo["PAySA"] = $("#txtApellidos").val()
    modelo["direccion"] = $("#txtDireccion").val()
    modelo["telefonoPrincipal"] = $("#txtTelefonoPrincipal").val()
    modelo["telefonoSecundario"] = $("#txtTelefonoSecundarios").val()
    modelo["IdTipoIdentificacion"] = $("#cboclientetipodocumento").val()
    modelo["esActivo"] = $("#cboEstado").val()

    //const formData = new FormData();

/*    formData.append("modelo", JSON.stringify(modelo))*/

    $("#FormModal").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idCliente == 0) {
        fetch("/Cliente/Crear", {
            method: "POST",
            headers: { "content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#FormModal").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responsejson => {
                if (responsejson.estado) {
                    tablaData.row.add(responsejson.objeto).draw(false);
                    $("#FormModal").modal("hide");
                    swal("listo", "El cliente fue creado", "success");
                } else {
                    swal("lo sentimos", responsejson.mensaje, "error");

                }
            })
    } else {
        fetch("/Cliente/Editar", {
            method: "PUT",
            headers: { "content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#FormModal").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responsejson => {
                if (responsejson.estado) {
                    tablaData.row(filaSelecionada).data(responsejson.objeto).draw(false);
                    filaSelecionada = null;
                    $("#FormModal").modal("hide");
                    swal("listo", "El cliente fue modificado", "success");
                } else {
                    swal("lo sentimos", responsejson.mensaje, "error");
                }
            })
    }
})

let filaSelecionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSelecionada = $(this).closest("tr").prev();
    } else {
        filaSelecionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSelecionada).data();
    mostrarModal(data);
})


$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Está seguro?",
        text: `Eliminar al cliente "${data.PNySN}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Cliente/Eliminar?IdCliente=${data.idCliente}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo", "El usuario fue eliminado", "success");
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error");
                        }
                    })
            }
        }
    )
})
