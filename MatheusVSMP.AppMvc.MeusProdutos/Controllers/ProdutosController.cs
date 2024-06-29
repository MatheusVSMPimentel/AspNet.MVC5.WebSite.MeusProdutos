using AutoMapper;
using MatheusVSMP.AppMvc.MeusProdutos.ViewModels;
using MatheusVSMP.Business.Core.Notificacoes;
using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Business.Models.Produtos;
using MatheusVSMP.Business.Models.Produtos.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MatheusVSMP.AppMvc.MeusProdutos.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, IProdutoRepository produtoRepository, IMapper mapper, IFornecedorRepository fornecedorRepository,
                                      INotificador notificador) : base(notificador)
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
            if (!ModelState.IsValid)
                return View(produtoViewModel);
            
            if(!UploadImagem(produtoViewModel.ImagemUpload, $"{produtoViewModel.Id}_"))
                return View(produtoViewModel);

            produtoViewModel.Imagem = $"{produtoViewModel.Id}_{produtoViewModel.ImagemUpload.FileName}";
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));
            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
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

            if (!ModelState.IsValid)
                return View(produtoViewModel);
            var produtoAtualizacao = await ObterProduto(produtoViewModel.Id);
             produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (produtoViewModel.ImagemUpload != null)
            {
                if (!UploadImagem(produtoViewModel.ImagemUpload, $"{produtoViewModel.Id}_"))
                    return View(produtoViewModel);

                produtoAtualizacao.Imagem = $"{produtoViewModel.Id}_{produtoViewModel.ImagemUpload.FileName}"; 
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;
            produtoAtualizacao.FornecedorId = produtoViewModel.FornecedorId;
            produtoAtualizacao.Fornecedor = produtoViewModel.Fornecedor;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
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
            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private bool UploadImagem(HttpPostedFileBase file, string imgPrefixo)
        {
            if(file is null || file.ContentLength < 1)
            {
                ModelState.AddModelError(string.Empty, "Imagem invalida.");
                return false;
            }

            var path = Path.Combine(HttpContext.Server.MapPath("~/imagens"), imgPrefixo + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Ja existe um arquivo com esse nome.");
                return false;
            }
            file.SaveAs(path);
            return true;
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
           var produto =  _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
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
