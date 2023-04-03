using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_Ecommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IProdutoApp _IProdutoApp;
        private readonly ICompraUsuarioApp _compraUsuarioApp;
        private IWebHostEnvironment _environment;
        public ProdutosController(IProdutoApp IProductoApp, UserManager<ApplicationUser> userManager, ICompraUsuarioApp compraUsuarioApp, IWebHostEnvironment environment)
        {
            _IProdutoApp = IProductoApp;
            _userManager = userManager;
            _compraUsuarioApp = compraUsuarioApp;
            _environment = environment;
        }
        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _IProdutoApp.ListarProdutosUsuario(idUsuario));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _IProdutoApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();
                produto.UserId = idUsuario;

                await _IProdutoApp.AddProduto(produto);
                if (produto.notificacoes.Any())
                {
                    foreach (var item in produto.notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                    return View("Create", produto);
                }

                await SalvarImagemProduto(produto);
            }
            catch
            {
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _IProdutoApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _IProdutoApp.UpdateProduto(produto);
                if (produto.notificacoes.Any())
                {
                    foreach (var item in produto.notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                }

                ViewBag.Alerta = true;
                ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

                return View("Edit", produto);


            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _IProdutoApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {

                var produtoDeletar = await _IProdutoApp.GetEntityById(id);
                await _IProdutoApp.Delete(produtoDeletar);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        [AllowAnonymous]
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque()
        {
            return Json(await _IProdutoApp.ListarProdutosComEstoque());
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            return View(await _IProdutoApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _IProdutoApp.ObterProdutosCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {

                var produtoDeletar = await _compraUsuarioApp.GetEntityById(id);
                await _compraUsuarioApp.Delete(produtoDeletar);
                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }

        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _IProdutoApp.GetEntityById(produtoTela.Id);

                if (produtoTela.Imagem != null)
                {
                    var webRoot = _environment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProdutos"));
                    permissionSet.AddPermission(writePermission);

                    var Extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);

                    var NomeArquivo = string.Concat(produto.Id.ToString(), Extension);

                    var diretorioArquivoSalvar = string.Concat(webRoot, "\\imgProdutos\\", NomeArquivo);

                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivoSalvar, FileMode.Create));

                    produto.Url = string.Concat("https://localhost:44375", "/imgProdutos/", NomeArquivo);

                    await _IProdutoApp.UpdateProduto(produto);
                }
            }
            catch (Exception erro)
            {

            }
        }

    }
}
