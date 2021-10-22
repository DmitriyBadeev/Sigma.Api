using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations.ValidationErrors
{
    public class PortfolioBelongsUserError : IValidationError
    {
        public string Message => "Портфель не принадлежит пользователю";
    }
}