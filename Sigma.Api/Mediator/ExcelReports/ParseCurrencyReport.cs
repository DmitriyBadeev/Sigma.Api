using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Api.Mediator.ExcelReports
{
    public class ParseCurrencyReport
    {
        public record Command(IFile report, Guid portfolioId, FinanceDbContext Context, IValidationService ValidationService,
            string UserId) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            private readonly IExcelService excelService;

            public Handler(IExcelService excelService)
            {
                this.excelService = excelService;
            }

            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, portfolioId, context, validationService, userId) = request;

                await using var excelStream = input.OpenReadStream();
                var isSuccess = excelService.TryParseReport(excelStream,
                    out List<CurrencyOperation> operations,
                    out string errorMessage);

                if (!isSuccess)
                {
                    return new DefaultPayload(false, errorMessage);
                }

                operations.ForEach(x => x.PortfolioId = portfolioId);

                await context.CurrencyOperations.AddRangeAsync(operations, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Загрузка операций прошла успешно");
            }
        }
    }
}
