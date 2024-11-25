using Cinema.Core.Helpers.UnifiedResponse;

namespace Cinema.Core.Helpers.Validation;

public interface IBaseValidationModel
{
    public Result Validate(object validator, IBaseValidationModel modelObj);
}