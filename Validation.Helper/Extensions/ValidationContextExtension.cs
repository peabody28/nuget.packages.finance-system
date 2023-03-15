using FluentValidation;
using FluentValidation.Results;

namespace Validation.Helper.Extensions
{
    public static class ValidationContextExtension
    {
        public static void AddFailures<T>(this ValidationContext<T> context, string propertyName, ValidationResult validationResult) 
        {
            if(validationResult != null && !validationResult.IsEmpty)
            {
                context.AddFailure(new ValidationFailure(propertyName, validationResult.ErrorMessage) { ErrorCode = validationResult.ErrorCode });
            }
        }
    }
}
