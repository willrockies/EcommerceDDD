using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrasctruture.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasctruture.Repository.Repositories
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduct
    {
    }
}
