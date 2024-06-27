using MatheusVSMP.Business.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMapping()
        {
            HasKey(e => e.Id);
            Property(e => e.Logradouro).IsRequired().HasMaxLength(200);
            Property(e => e.Numero).IsRequired().HasMaxLength(50);
            Property(e => e.Cep).IsRequired().HasMaxLength(8).IsFixedLength();
            Property(e => e.Complemento).HasMaxLength(250);
            Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            Property(e => e.Estado).IsRequired().HasMaxLength(100);
            ToTable("Enderecos");
        }
    }
}
