using MatheusVSMP.Business.Models.Produtos;
using System.Data.Entity.ModelConfiguration;

namespace MatheusVSMP.Infra.Data.Mappings
{
    public class ProdutoMapping : EntityTypeConfiguration<Produto>
    {
        public ProdutoMapping()
        {
            HasKey(p => p.Id);
            Property(p => p.Descricao).IsRequired().HasMaxLength(1000);
            Property(p => p.Nome).IsRequired().HasMaxLength(200);
            Property(p => p.Imagem).IsRequired().HasMaxLength(300); ;
            HasRequired(p => p.Fornecedor).WithMany(f => f.Produtos).HasForeignKey(p => p.FornecedorId);

            ToTable("Produtos");
        }
    }
}
