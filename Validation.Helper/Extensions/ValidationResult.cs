namespace Validation.Helper.Extensions
{
    public class ValidationResult
    {
        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public bool IsEmpty { get; set; }

        public ValidationResult()
        {

        }

        public ValidationResult(string? errorMessage, string? errorCode = null)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public static ValidationResult Empty()
        {
            return new ValidationResult() { IsEmpty = true };
        }
    }
}
