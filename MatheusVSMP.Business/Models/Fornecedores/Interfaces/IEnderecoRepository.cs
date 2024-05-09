using System;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Fornecedores.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereço> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
