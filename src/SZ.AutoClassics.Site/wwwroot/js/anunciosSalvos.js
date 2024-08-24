function RemoverAnuncio(anuncioId) {
    const SalvarAnuncio = {
        AnuncioId: anuncioId
    };

    $.ajax({
        method: 'POST',
        url: 'https://localhost:44329/AnuncioSalvo/RemoverAnuncioSalvo',
        data: SalvarAnuncio,
        success: function (response) {
            location.reload();
        }
    }).then(function () {
        toastr.warning("Você removeu este anúncio da lista", "Aviso");
    });
}