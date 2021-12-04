using System;
using System.Collections.Generic;
using System.Text.Json;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Buildings.Common.Methods;
using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Buildings.Common
{
    public abstract class RequestedBuilder<TRequested, TResponse> : IRequestedBuilder<TRequested, TResponse>
        where TRequested : IRequested
        where TResponse : IResponse
    {
        private readonly Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules;

        protected RequestedBuilder(Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> mapRules)
        {
            _mapRules = mapRules;
        }

        public abstract List<TRequested> BuildRequested(TResponse response, FinanceDbContext context);

        protected TRequested MapRequested(List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var requested = (TRequested)Activator.CreateInstance(typeof(TRequested));
            var requestedType = requested.GetType();

            foreach (var mapRule in _mapRules)
            {
                var requestedProperty = requestedType.GetProperty(mapRule.Value.propertyName);
                if (requestedProperty == null)
                {
                    continue;
                }
                
                var property = mapRule.Value.mapFunc(mapRule.Key, source, columns, context);
                if (property == null)
                {
                    continue;
                }

                requestedProperty.SetValue(requested, property);
            }

            return requested;
        }
    }
}
