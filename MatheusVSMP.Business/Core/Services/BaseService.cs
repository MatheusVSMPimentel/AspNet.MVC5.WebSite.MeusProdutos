using FluentValidation;
using MatheusVSMP.Business.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Core.Services
{
    public abstract class BaseService
    {
        protected  bool ExecutarValidacao<TValidation, TEntity>(TValidation validacao, TEntity entidade)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validacao.Validate(entidade);

            return validator.IsValid;
        }
    }
}
