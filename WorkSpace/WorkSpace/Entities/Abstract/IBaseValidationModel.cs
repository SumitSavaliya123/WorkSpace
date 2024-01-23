using Common.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{

    public interface IBaseValidationModel
    {
        public void Validate(object Validator, IBaseValidationModel modelObj);
    }
    public abstract class BaseValidationModel<T> : IBaseValidationModel
    {
        public void Validate(object validator, IBaseValidationModel modelObj)
        {
            var instance = (IValidator<T>)validator;
            var result = instance.Validate((T)modelObj);

            if (!result.IsValid && result.Errors.Any())
            {
                throw new ModelValidationException
                    (result.Errors
                        .Select(error => error.ErrorMessage.Replace("'", string.Empty))
                        .ToList());
            }
        }
    }
}
