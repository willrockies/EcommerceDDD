using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Infrasctruture.Configuration;
using Infrasctruture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities.Enums;

namespace Infrasctruture.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryCompraUsuario() 
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using(var db = new ContextBase(_optionsBuilder))
            {
                return await db.CompraUsuario.CountAsync(
                    c => c.UserId.Equals(userId) && c.Estado == EnumEstadoCompra.Produto_Carrinho);
            }
        }
    }
}
