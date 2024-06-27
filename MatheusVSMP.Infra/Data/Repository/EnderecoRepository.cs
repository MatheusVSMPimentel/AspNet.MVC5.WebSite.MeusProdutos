using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(SqlServerContext db) : base(db)
        {
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await ObterPorId(fornecedorId);
        }
    }
}
