using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class NotExistError : IValidationError
    {
        public string Message => "Объект не найден";
    }
}