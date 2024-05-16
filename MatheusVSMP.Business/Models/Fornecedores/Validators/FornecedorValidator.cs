using FluentValidation;
using MatheusVSMP.Business.Validators;
using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;

namespace MatheusVSMP.Business.Models.Fornecedores.Validators
{
    public class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(f => f.Nome).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 200).WithMessage(LengthValidator);

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.Tamanho).WithMessage(EqualLengthValidator);
                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true).WithMessage(InvalidPropertyValidator);
            });
            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.Tamanho).WithMessage(EqualLengthValidator);
                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true).WithMessage(InvalidPropertyValidator);
            });

        }
    }
}
