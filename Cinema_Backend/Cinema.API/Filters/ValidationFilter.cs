using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Helpers.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cinema_Backend.Filters;

public class ValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var actionArgument in context.ActionArguments)
        {
            //validate that model is having validator and resolve it
            if (actionArgument.Value is IBaseValidationModel model)
            {
                var modelType = actionArgument.Value.GetType();
                var genericType = typeof(IValidator<>).MakeGenericType(modelType);
                var validator = context.HttpContext.RequestServices.GetService(genericType);

                if (validator != null)
                {
                    // execute validator to validate model
                    Result result = model.Validate(validator, model);

                    if (!result.IsSuccess)
                    {
                        context.Result = new BadRequestObjectResult(result);
                        return;
                    }
                }
            }
        }
        
        base.OnActionExecuting(context);
    }

}