using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class PortfolioNotExistError : IValidationError
    {
        public string Message => "Потфель не найден";
    }
}