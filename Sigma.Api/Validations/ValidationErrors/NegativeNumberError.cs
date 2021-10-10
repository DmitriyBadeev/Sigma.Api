using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class NegativeNumberError : IValidationError
    {
        public string Message => "Число не может быть отрицательным";
    }
}