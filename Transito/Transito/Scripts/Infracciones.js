$(document).ready(function () {
    $("#crearInfraccion").validate({
        rules: {
            id_vehiculo: {
                required: true
            },
            accionador: {
                required: true
            },
            fecha: {
                required: true,
                date: true
            },
            observaciones: {
                required: true,
                maxlength: 100
            },
            //email: {
            //    required: true,
            //    email: true
            //},
        },
        messages: {
            id_vehiculo: {
                required: "El vehículo es requerido"
            },
            accionador: {
                required: "El accionador es requerido"
            },
            fecha: {
                required: "La Fecha infracción es requerida",
                date: "Solo fecha"
            },
            observaciones: {
                required: "El máximo de sdsadas permitido es 100",
                maxlength: "El máximo de caracteres permitido es 100"
            }
        }
    });
});