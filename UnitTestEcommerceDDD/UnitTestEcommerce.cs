using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrasctruture.Repository.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestEcommerceDDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        [TestMethod]
        public async Task AddProductComSucesso()
        {

            try
            {

                IProduct _IProduct = new RepositoryProduto();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição Test TDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = string.Concat("Nome Test TDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "77f658a4-be5b-4f5a-99e0-f2bc4c09e419"
                };

                await _IServiceProduct.AddProduto(produto);

                Assert.IsFalse(produto.notificacoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduto();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);

                var produto = new Produto
                {

                };

                await _IServiceProduct.AddProduto(produto);

                Assert.IsTrue(produto.notificacoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListarProdutosUsuario()
        {
            try
            {

                IProduct _IProduct = new RepositoryProduto();


                var listaProduto = await _IProduct.ListarProdutosUsuario("77f658a4-be5b-4f5a-99e0-f2bc4c09e419");

                Assert.IsTrue(listaProduto.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduto();

                var listaProduto = await _IProduct.ListarProdutosUsuario("77f658a4-be5b-4f5a-99e0-f2bc4c09e419");
                var produto = await _IProduct.GetEntityById(listaProduto.LastOrDefault().Id);

                Assert.IsTrue(produto != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduto();

                var listaProduto = await _IProduct.ListarProdutosUsuario("77f658a4-be5b-4f5a-99e0-f2bc4c09e419");
                var ultimoProduto = listaProduto.LastOrDefault();

                await _IProduct.Delete(ultimoProduto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
