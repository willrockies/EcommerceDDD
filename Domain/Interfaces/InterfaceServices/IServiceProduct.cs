using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task<List<Produto>> ListarProdutosComEstoque();

    }
}
