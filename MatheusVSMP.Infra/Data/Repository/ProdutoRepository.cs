using MatheusVSMP.Business.Models.Produtos;
using MatheusVSMP.Business.Models.Produtos.Interfaces;
using MatheusVSMP.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(SqlServerContext db) : base(db)
        {
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _db.Produtos.AsNoTracking().Include(f => f.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _db.Produtos.AsNoTracking().Include(p => p.Fornecedor).OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId.Equals(fornecedorId));
        }
        public override async Task Remover(Guid id)
        {
            var produto = await ObterPorId(id);
            produto.Ativo = false;
            await Atualizar(produto);
        }
    }
}
