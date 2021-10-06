using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sigma.Api.Validations.Interfaces;
using Sigma.Api.Validations.ValidationErrors;
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

        public ValidationService PortfolioBelongsUser(Guid portfolioId, string userId)
        {
            var portfolio = _context.Portfolios.Find(portfolioId);

            if (portfolio.UserId != userId)
            {
                _validationErrors.Add(new PortfolioBelongsUserError());
            }
            
            return this;
        }

        public ValidationService CheckPortfolioType(Guid typeId)
        {
            var portfolioType = _context.PortfolioTypes.Find(typeId);

            if (portfolioType == null)
            {
                _validationErrors.Add(new WrongPortfolioTypeError());
            }

            return this;
        }

        public ValidationService CheckPortfolioExist(Guid portfolioId)
        {
            var portfolio = _context.Portfolios.Find(portfolioId);
            
            if (portfolio == null)
            {
                _validationErrors.Add( new PortfolioNotExistError());
            }

            return this;
        }
    }
}