var _persistenConnectionjs_Signal;

$(function () {
    setConnection();
});

function setConnection() {
    _persistenConnectionjs_Signal = $.connection('/Signals/ChatService');

    _persistenConnectionjs_Signal.received(function (datos) {
        $('<p class="rojo"></p>').html(datos).appendTo($('.mensajes'));
    });

    _persistenConnectionjs_Signal.error(function (error) {
        alert('Algo ha salido mal ' + error);
    })

    _persistenConnectionjs_Signal
        .start()
        .done(function () {
            $('.estatusConexion').css('background-color', 'green');
            $('#btnEnviar').click(function () {
                $('<p class="azul"></p>').html('Yo:' + $('#txtMsj').val()).appendTo($('.mensajes'));
                _persistenConnectionjs_Signal.send(
                    $('#txtMsj').val()
                );
            });
        })
        .fail(function () {
            $('.estatusConexion').css('background-color', 'red');
        });
}