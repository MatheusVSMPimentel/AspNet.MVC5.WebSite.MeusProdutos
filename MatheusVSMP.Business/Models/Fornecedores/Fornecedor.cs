using MatheusVSMP.Business.Core.Entities;
using MatheusVSMP.Business.Models.Produtos;
using System.Collections.Generic;

namespace MatheusVSMP.Business.Models.Fornecedores
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}

