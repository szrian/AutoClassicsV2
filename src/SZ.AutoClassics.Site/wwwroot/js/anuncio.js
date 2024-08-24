$(document).ready(function () {
    $('.select').select2();

    $("#selectEstados").change(function () {
        var estadoId = $("#selectEstados").val();
        var selectCidades = $("#selectCidades");
        var url = window.location.origin + '/Anuncio/ObterCidadesPorEstadoId';

        if (estadoId) {
            $.ajax({
                type: "GET",
                url: url,
                data: { estadoId: estadoId },
                dataType: "json",
                success: function (data) {
                    selectCidades.empty();
                    selectCidades.append('<option value="">Selecione uma cidade</option>');

                    $.each(data, function (index, item) {
                        selectCidades.append('<option value="' + item.value + '">' + item.text + '</option>');
                    });
                }
            });
        }
    })
})