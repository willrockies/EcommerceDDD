using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrasctruture.Configuration;
using Infrasctruture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using(var dataBase = new ContextBase(_optionsBuilder))
            {
                return await dataBase.Produtos.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }
    }
}
