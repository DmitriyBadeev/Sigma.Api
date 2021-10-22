using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class InvalidEnumValueError : IValidationError
    {
        public string Message => "Несуществующее значение переменной";
    }
}