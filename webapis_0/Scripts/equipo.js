$(function () {
    $('#btnRegistrar').click(
        function () {
            registraEquipo();
        });
});

function registraEquipo() {
    $.ajax(
        {
            type: 'POST',
            url: '/api/equipos',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(obtenEquipo()),
            dataType: 'json',
            success: function (data, estatus, xhr) {
                alert('Se ha registrado el equipo correctamente');
            },
            error: function () {
                alert('Ha fallado la petición');
            }
        });
}

function obtenEquipo() {
    return {
        Nombre: $('#Nombre').val(),
        Fundacion: $('#Fundacion').val(),
        Campeonatos: $('#Campeonatos').val(),
        Apodo: $('#Apodo').val()
    };
}