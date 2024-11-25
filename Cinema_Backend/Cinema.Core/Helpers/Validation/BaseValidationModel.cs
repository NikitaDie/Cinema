using Cinema.Core.Helpers.UnifiedResponse;
using FluentValidation;

namespace Cinema.Core.Helpers.Validation;

public abstract class BaseValidationModel<T> : IBaseValidationModel
{
    public Result Validate(object validator, IBaseValidationModel modelObj)
    {
        var instance = (IValidator<T>)validator;
        var result = instance.Validate((T)modelObj);
    
        if (!result.IsValid && result.Errors.Any())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            
            return Result.Failure(string.Join(", ", errorMessages));
        }

        return Result.Success();
    }
}