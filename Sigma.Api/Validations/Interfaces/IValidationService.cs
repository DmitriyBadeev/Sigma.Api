using System;
using System.Collections.Generic;

namespace Sigma.Api.Validations.Interfaces
{
    public interface IValidationService
    {
        public IEnumerable<IValidationError> Errors { get; }

        ValidationService PortfolioBelongsUser(Guid portfolioId, string userId);
        ValidationService CheckPortfolioType(Guid typeId);

        ValidationService CheckPortfolioExist(Guid portfolioId);
    }
}