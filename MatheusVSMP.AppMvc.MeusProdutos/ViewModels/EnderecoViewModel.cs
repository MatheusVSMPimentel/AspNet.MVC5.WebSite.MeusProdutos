using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;

namespace MatheusVSMP.AppMvc.MeusProdutos.ViewModels
{
    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = StringLegthDataAnnotations)]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(8, ErrorMessage = RequiredStringLegthDataAnnotations)]
        public string Cep { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Bairro { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Cidade { get; set; }
        [Required(ErrorMessage = RequiredDataAnnotations)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = StringLegthDataAnnotations)]
        public string Estado { get; set; }
        [HiddenInput]
        public Guid FornecedorId { get; set; }
    }
}