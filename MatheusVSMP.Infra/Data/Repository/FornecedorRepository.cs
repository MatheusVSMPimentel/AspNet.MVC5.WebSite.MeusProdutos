using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await _db.Fornecedores.AsNoTracking().Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await _db.Fornecedores.AsNoTracking().Include(f => f.Endereco).Include(f => f.Produtos).FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task Remover(Guid id)
        {
            var fornecedor = await ObterPorId(id);
            fornecedor.Ativo = false;
            await Atualizar(fornecedor);
        }
    }
}
