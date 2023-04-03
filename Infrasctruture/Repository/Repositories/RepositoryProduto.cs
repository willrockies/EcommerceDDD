using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrasctruture.Configuration;
using Infrasctruture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Repository.Repositories
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryProduto()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var dataBase = new ContextBase(_optionsBuilder))
            {
                return await dataBase.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userId)
        {
            using (var dataBase = new ContextBase(_optionsBuilder))
            {
                var produtosCarrinhoUsuario = await (from p in dataBase.Produtos
                                                     join c in dataBase.CompraUsuario on p.Id equals c.IdProduto
                                                     where c.UserId.Equals(userId) && c.Estado == Entities.Entities.Enums.EnumEstadoCompra.Produto_Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra= c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id,
                                                         Url = p.Url

                                                     }).AsNoTracking().ToListAsync();
                return produtosCarrinhoUsuario;
            }
        }

        public async Task<Produto> ObterProdutosCarrinho(int idProdutoCarrinho)
        {
            using (var dataBase = new ContextBase(_optionsBuilder))
            {
                var produtosCarrinhoUsuario = await (from p in dataBase.Produtos
                                                     join c in dataBase.CompraUsuario on p.Id equals c.IdProduto
                                                     where c.Id.Equals(idProdutoCarrinho) && c.Estado == EnumEstadoCompra.Produto_Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id,
                                                         Url = p.Url

                                                     }).AsNoTracking().FirstOrDefaultAsync();
                return produtosCarrinhoUsuario;
            }
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using (var dataBase = new ContextBase(_optionsBuilder))
            {
                return await dataBase.Produtos.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }


    }
}
