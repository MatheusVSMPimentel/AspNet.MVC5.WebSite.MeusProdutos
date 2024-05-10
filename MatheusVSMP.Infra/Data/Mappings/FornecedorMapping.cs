using MatheusVSMP.Business.Models.Fornecedores;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace MatheusVSMP.Infra.Data.Mappings
{
    public class FornecedorMapping : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMapping()
        {
            HasKey(x => x.Id);
            Property(f => f.Nome).IsRequired().HasMaxLength(200);
            Property(f => f.Documento).IsRequired().HasMaxLength(14).HasColumnAnnotation("ix_Documento", new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            HasRequired(f => f.Endereco).WithRequiredPrincipal(e => e.Fornecedor);

            ToTable("Fornecedores");
        }
    }
}
