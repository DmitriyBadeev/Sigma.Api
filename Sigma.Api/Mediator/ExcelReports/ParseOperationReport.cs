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
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Api.Mediator.ExcelReports
{
    public class ParseOperationReport<TOperation>
        where TOperation: IOperation
    {
        public record Command(IFile Report, FinanceDbContext Context, IValidationService ValidationService,
            string UserId) : IRequest<List<TOperation>>;

        public class Handler : IRequestHandler<Command, List<TOperation>>
        {
            private readonly IExcelService _excelService;

            public Handler(IExcelService excelService)
            {
                _excelService = excelService;
            }

            public async Task<List<TOperation>> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId) = request;

                await using var excelStream = input.OpenReadStream();
                var isSuccess = _excelService.TryParseReport(excelStream, 
                    out List<TOperation> operations, 
                    out string errorMessage);

                return operations;
            }
        }
    }
}
