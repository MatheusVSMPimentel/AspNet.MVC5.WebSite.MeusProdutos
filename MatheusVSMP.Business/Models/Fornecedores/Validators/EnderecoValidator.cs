using FluentValidation;
using static MatheusVSMP.Business.Core.Constants.MensagensConstantes;

namespace MatheusVSMP.Business.Models.Fornecedores.Validators
{
    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(e => e.Bairro).NotEmpty().WithMessage(NotEmptyValidator).Length(2,100).WithMessage(LengthValidator);
            RuleFor(e => e.Cep).NotEmpty().WithMessage(NotEmptyValidator).Length(8).WithMessage(LengthValidator);
            RuleFor(e => e.Cidade).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 100).WithMessage(LengthValidator);
            RuleFor(e => e.Estado).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 50).WithMessage(LengthValidator);
            RuleFor(e => e.Logradouro).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 100).WithMessage(LengthValidator);
            RuleFor(e => e.Numero).NotEmpty().WithMessage(NotEmptyValidator).Length(2, 25).WithMessage(LengthValidator);
        }
    }
}
