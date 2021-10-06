using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class WrongPortfolioTypeError : IValidationError
    {
        public string Message => "Неверный тип портфеля";
    }
}