using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface IProdutoApp : IGenericApp<Produto>
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);

        Task<List<Produto>> ListarProdutosUsuario(string userId);
        Task<List<Produto>> ListarProdutosComEstoque();
    }
}
