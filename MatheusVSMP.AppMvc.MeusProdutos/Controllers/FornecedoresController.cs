using AutoMapper;
using MatheusVSMP.AppMvc.MeusProdutos.Extensions;
using MatheusVSMP.AppMvc.MeusProdutos.ViewModels;
using MatheusVSMP.Business.Core.Notificacoes;
using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static MatheusVSMP.Business.Core.Constants.CommomConstants;

namespace MatheusVSMP.AppMvc.MeusProdutos.Controllers
{
    [Authorize]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;


        public FornecedoresController(IFornecedorService fornecedorService, IFornecedorRepository fornecedorRepository, IMapper mapper,
                                      INotificador notificador) : base(notificador)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-fornecedores")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>((await _fornecedorRepository.ObterTodos())));
        }

        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            return View(fornecedor);
        }
        [ClaimsAuthorize(FornecedorClaim, CreateClaim)]
        [Route("novo-fornecedor")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize(FornecedorClaim, CreateClaim)]
        [Route("novo-fornecedor")]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedor)
        {
            if (!ModelState.IsValid) return View(fornecedor);
            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedor));
            if (!OperacaoValida()) return View(fornecedor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [HttpGet]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            return PartialView("_AtualizarEndereco", new FornecedorViewModel() { Endereco = fornecedor.Endereco });
        }

        [HttpPost]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult> AtualizarEndereco(FornecedorViewModel fornecedor)
        {
            ModelState.Remove(nameof(FornecedorViewModel.Nome));
            ModelState.Remove(nameof(FornecedorViewModel.Documento));

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedor);
            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedor.Endereco));
            if (!OperacaoValida()) return View(fornecedor);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedor.Endereco.FornecedorId });
            return Json(new { success = true, url });
        }

        [ClaimsAuthorize(FornecedorClaim, EditClaim)]
        [HttpGet]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            return View(fornecedor);
        }

        [ClaimsAuthorize(FornecedorClaim, EditClaim)]
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound();
            if (!ModelState.IsValid) return View(fornecedorViewModel);
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);
            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(FornecedorClaim, DeleteClaim)]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            return View(fornecedor);
        }

        [ClaimsAuthorize(FornecedorClaim, DeleteClaim)]
        [HttpPost, ActionName("Delete")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor is null) return HttpNotFound();
            await _fornecedorService.Remover(id);
            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id) => _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id) => _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
    }
}