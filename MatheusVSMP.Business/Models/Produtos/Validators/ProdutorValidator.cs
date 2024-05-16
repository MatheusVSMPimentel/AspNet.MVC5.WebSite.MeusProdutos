using FluentValidation;
using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;

namespace MatheusVSMP.Business.Models.Produtos.Validators
{
    public class ProdutorValidator : AbstractValidator<Produto>
    {
        public ProdutorValidator()
        {
            RuleFor(e => e.Nome).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 200).WithMessage(LengthValidator);
            RuleFor(e => e.Descricao).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 1000).WithMessage(LengthValidator);
            RuleFor(e => e.Valor).GreaterThan(0).WithMessage(GreaterThenValidator);
        }
    }
}
