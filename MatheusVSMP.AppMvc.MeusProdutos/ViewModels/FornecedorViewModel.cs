using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MatheusVSMP.AppMvc.MeusProdutos.ViewModels
{
    public class FornecedorViewModel
    {
        public FornecedorViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Nome { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(14, MinimumLength = 11, ErrorMessage = StringLegthDataAnnotations)]
        public string Documento { get; set; }
        [DisplayName("Tipo")]
        public int TipoFornecedor { get; set; }
        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}