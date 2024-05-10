using MatheusVSMP.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Fornecedores.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
