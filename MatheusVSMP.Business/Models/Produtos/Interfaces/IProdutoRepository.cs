using MatheusVSMP.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Produtos.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}
