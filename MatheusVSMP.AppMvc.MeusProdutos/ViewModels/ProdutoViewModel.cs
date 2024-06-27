using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using MatheusVSMP.AppMvc.MeusProdutos.Extensions;

namespace MatheusVSMP.AppMvc.MeusProdutos.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Descricao { get; set; }/*
        [DisplayName("Imagem do Produto")]
        public HttpPostedFileBase ImagemUpload { get; set; }*/
        public string Imagem { get; set; }
        [Moeda]
        [Required(ErrorMessage = RequiredDataAnnotations)]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}