var objetoVenda = new Object();

objetoVenda.adicionarCarrinho = function (idProduto) {


    var nome = $('#nome_' + idProduto).val();
    var quantidade = $('#qtd_' + idProduto).val();

    $.ajax({
        type: 'POST',
        url: '/api/AdicionarProdutoCarrinho',
        dataType: 'JSON',
        cache: false,
        async: true,
        data: {
            'id': idProduto,
            'nome': nome,
            'quantidade': quantidade
        },
        success: function (data) {
            if (data.sucesso) {
                objetoAlert.AlertaTela(1, "Produto adicionado no carrinho")
            } else {
                objetoAlert.AlertaTela(2, "Usuário não logado")
            }

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

objetoVenda.carregaQtdCarrinho = function () {
    $('#qtdCarrinho').text('(0)');

    setTimeout(() => {
        objetoVenda.carregaQtdCarrinho
        console.log('teste');
    }, 10000);
}

$(function () {
    objetoVenda.carregaProdutos();
    objetoVenda.carregaQtdCarrinho();
});