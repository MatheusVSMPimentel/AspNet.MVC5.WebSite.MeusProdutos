using FluentValidation;
using FluentValidation.Results;
using MatheusVSMP.Business.Core.Entities;
using MatheusVSMP.Business.Core.Notificacoes;
using static MatheusVSMP.Business.Core.Notificacoes.Notificacao;

namespace MatheusVSMP.Business.Core.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string message) {
            _notificador.Handle(CreateNotificacao(message));
        }

        protected bool ExecutarValidacao<TValidation, TEntity>(TValidation validacao, TEntity entidade)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validacao.Validate(entidade);

            return validator.IsValid;
        }
    }
}
