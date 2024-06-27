using AutoMapper;
using MatheusVSMP.AppMvc.MeusProdutos.ViewModels;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Business.Models.Produtos;
using MatheusVSMP.Business.Models.Produtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MatheusVSMP.AppMvc.MeusProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, IProdutoRepository produtoRepository, IMapper mapper, IFornecedorRepository fornecedorRepository)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        [Route("dados-do-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            ProdutoViewModel produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var produto = await PopularFornecedores(new ProdutoViewModel());
            return View(produto);
        }

        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);
            if (ModelState.IsValid)
            {
                await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {

            ProdutoViewModel produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));
                return RedirectToAction("Index");
            }
            return View(produtoViewModel);
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {

            ProdutoViewModel produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ProdutoViewModel produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            await _produtoService.Remover(id);
            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
           var produto =  _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
            produto = await PopularFornecedores(produto);
            return produto;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _produtoRepository.Dispose();
                _fornecedorRepository.Dispose();
                _produtoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
