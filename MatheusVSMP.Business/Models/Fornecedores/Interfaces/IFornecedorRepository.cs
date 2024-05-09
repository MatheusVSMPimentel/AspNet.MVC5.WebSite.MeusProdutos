using MatheusVSMP.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Fornecedores.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}
