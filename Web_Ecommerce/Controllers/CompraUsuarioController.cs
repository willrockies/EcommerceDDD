using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web_Ecommerce.Models;

namespace Web_Ecommerce.Controllers
{
    public class CompraUsuarioController : HelpQrCode
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ICompraUsuarioApp _compraUsuarioApp;
        private IWebHostEnvironment _environment;

        public CompraUsuarioController(
            UserManager<ApplicationUser> userManager,
            ICompraUsuarioApp compraUsuarioApp,
            IWebHostEnvironment environment
            )
        {
            _userManager = userManager;
            _compraUsuarioApp = compraUsuarioApp;
            _environment = environment; 
        }

        
        public async Task<IActionResult> FinalizarCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _compraUsuarioApp.CarrinhoCompras(usuario.Id);
            return View(compraUsuario);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _compraUsuarioApp.ProdutosComprados(usuario.Id);

            if (mensagem)
            {
                ViewBag.Sucesso = true;
                ViewBag.Mensagem = "Compra efetivada com sucesso. Pague o boleto para garantir sua compra!";
            }

            return View(compraUsuario);
        }

        public async Task<IActionResult> ConfirmaCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var sucesso = await _compraUsuarioApp.ConfirmaCompraCarrinhoUsuario(usuario.Id);
            if (sucesso)
            {
                return RedirectToAction("MinhasCompras", new { mensagem = true });
            }
            else
            {
                return RedirectToAction("FinalizarCompra");
            }
        }
        public async Task<IActionResult> Imprimir()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _compraUsuarioApp.ProdutosComprados(usuario.Id);

            return await Download(compraUsuario, _environment);
        }

        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string quantidade)
        {

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _compraUsuarioApp.Add(new CompraUsuario
                {
                    IdProduto = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(quantidade),
                    Estado = EnumEstadoCompra.Produto_Carrinho,
                    UserId = usuario.Id
                });
                return Json(new
                {
                    sucesso = true
                });
            }
            return Json(new
            {
                sucesso = false
            });
        }

        [HttpGet("/api/QtdProdutosCarrinho")]
        public async Task<JsonResult> QtdProdutosCarrinho()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var qtd = 0;

            if(usuario != null)
            {
                qtd =  await _compraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new { sucesso = true , qtd }); 
            }

            return Json(new { sucesso = false, qtd });
        }
    }
}
