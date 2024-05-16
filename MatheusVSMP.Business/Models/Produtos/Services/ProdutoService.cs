using MatheusVSMP.Business.Core.Services;
using MatheusVSMP.Business.Models.Produtos.Interfaces;
using MatheusVSMP.Business.Models.Produtos.Validators;
using System;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Produtos.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutorValidator(), produto)) return;

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutorValidator(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }
           

        public async Task Remover(Guid id)
        {

            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
