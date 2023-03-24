using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web_Ecommerce.Controllers
{
    public class CompraUsuarioController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ICompraUsuarioApp _compraUsuarioApp;

        public CompraUsuarioController(UserManager<ApplicationUser> userManager, ICompraUsuarioApp compraUsuarioApp)
        {
            _userManager = userManager;
            _compraUsuarioApp = compraUsuarioApp;
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
    }
}
