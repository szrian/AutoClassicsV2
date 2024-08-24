function SalvarAnuncio() {
    const SalvarAnuncio = {
        AnuncioId: $("#anuncioId").val()
    };

    $.ajax({
        method: 'POST',
        url: 'https://localhost:44329/AnuncioSalvo/SalvarAnuncio',
        data: SalvarAnuncio
    })

    toastr.success("Você salvou este anúncio", "Sucesso");
}