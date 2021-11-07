using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sigma.Api.Validations.Interfaces;
using Sigma.Api.Validations.ValidationErrors;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;

namespace Sigma.Api.Validations
{
    public class ValidationService : IValidationService
    {
        private readonly FinanceDbContext _context;
        private readonly List<IValidationError> _validationErrors;

        public ValidationService(IDbContextFactory<FinanceDbContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
            _validationErrors = new List<IValidationError>();
        }

        public IEnumerable<IValidationError> Errors => _validationErrors;

        public IValidationError FirstError => _validationErrors.FirstOrDefault();

        public ValidationService PortfoliosBelongUser(IEnumerable<Guid> portfolioIds, string userId)
        {
            foreach (var portfolioId in portfolioIds)
            {
                PortfolioBelongsUser(portfolioId, userId);
            }

            return this;
        }

        public ValidationService PortfolioBelongsUser(Guid? portfolioId, string userId)
        {
            var portfolio = _context.Portfolios.Find(portfolioId);

            if (portfolio?.UserId != userId)
            {
                _validationErrors.Add(new PortfolioBelongsUserError());
            }
            
            return this;
        }

        public ValidationService CheckExist<T>(Guid id) where T : class, IEntity
        {
            var entity = _context.Set<T>().Find(id);

            if (entity is null)
            {
                _validationErrors.Add( new NotExistError());
            }

            return this;
        }

        public ValidationService CheckExist<T>(IEnumerable<Guid> ids) where T : class, IEntity
        {
            foreach (var id in ids)
            {
                CheckExist<T>(id);
            }

            return this;
        }

        public ValidationService NotNegative(int number)
        {
            if (number < 0)
            {
                _validationErrors.Add(new NegativeNumberError());
            }

            return this;
        }
        
        public ValidationService NotNegative(decimal number)
        {
            if (number < 0)
            {
                _validationErrors.Add(new NegativeNumberError());
            }

            return this;
        }

        public ValidationService CheckEnumValue<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();

            if (!values.Contains(enumValue))
            {
                _validationErrors.Add(new InvalidEnumValueError());
            }

            return this;
        }
    }
}