using System;
using System.Collections.Generic;
using Sigma.Core.Interfaces;

namespace Sigma.Api.Validations.Interfaces
{
    public interface IValidationService
    {
        public IEnumerable<IValidationError> Errors { get; }

        IValidationError FirstError { get; }
        ValidationService PortfoliosBelongUser(IEnumerable<Guid> portfolioIds, string userId);
        ValidationService PortfolioBelongsUser(Guid? portfolioId, string userId);
        ValidationService CheckExist<T>(Guid id) where T : class, IEntity;
        ValidationService CheckExist<T>(IEnumerable<Guid> ids) where T : class, IEntity;
        ValidationService NotNegative(int number);
        ValidationService NotNegative(decimal number);
        ValidationService CheckEnumValue<TEnum>(TEnum enumValue) where TEnum : struct, Enum;
    }
}