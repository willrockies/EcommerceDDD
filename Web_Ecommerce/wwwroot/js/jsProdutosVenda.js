var objetoVenda = new Object();

objetoVenda.adicionarCarrinho = function (idProduto) {


    var idNome = $('#nome_' + idProduto).val();
    var idQtd = $('#qtd_' + idProduto).val();

    $.ajax({
        type: 'POST',
        url: '/api/AdicionarProdutoCarrinho',
        dataType: 'JSON',
        cache: false,
        async: true,
        data: {
            'id': idProduto,
            'nome': nome,
            'qtd': qtd
        },
        success: function (data) {


        }
    });
}

objetoVenda.carregaProdutos = function () {
    $.ajax({
        type: 'GET',
        url: '/api/ListarProdutosComEstoque',
        dataType: 'JSON',
        cache: false,
        async: true,
        success: function (data) {
            var htmlConteudo = '';
            data.forEach(function (entities) {
                htmlConteudo += "<div class='col-xs-12 col-sm-4 col-md-4 col-lg-4'>";

                var idNome = 'nome_' + entities.id;
                var idQtd = 'qtd_' + entities.id;

                htmlConteudo += "<label id='" + idNome + "'> Produto: " + entities.nome + "</label></br>";
                htmlConteudo += "<label > Valor: " + entities.valor + "</label></br>";
                htmlConteudo += "Quantidade : <input type'number' value='1' id='" + idQtd + "'>";

                htmlConteudo += "<input type='button' onclick='objetoVenda.adicionarCarrinho(" + entities.id + ")' value='Comprar'> </br>";

                htmlConteudo += "</div>";

                $("#divVenda").html(htmlConteudo);
            });
        }

    });
}

$(function () {
    objetoVenda.carregaProdutos();
});