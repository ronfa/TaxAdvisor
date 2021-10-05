using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Extensions;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Data.Contexts;

namespace TaxAdvocate.Business.Services
{
    public class AdviceService : IAdviceService
    {
        private readonly ILogger<AdviceService> _logger;
        private readonly InMemoryDatabaseContext _context;

        public AdviceService(ILogger<AdviceService> logger, InMemoryDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<AdviceIndicator>> GetAllAdviceIndicatorsAsync()
        {
            var type = typeof(IEvaluator);

            // Scanning all assemblies and find types which implement IEvaluator interface
            // then using extension method to get the display name and description
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.Name != type.Name)
                .Select(t => new AdviceIndicator
                {
                    AdviceId = t.Name, 
                    DisplayName = t.GetDisplayName(),
                    Description = t.GetDescription()
                }).ToList();
        }

        public IEvaluator ResolveAdviceIndicator(string adviceIndicatorId)
        {
            // Trying to find type
            var indicatorType = Type.GetType($"{typeof(IEvaluator).Namespace}.{adviceIndicatorId}");

            if (indicatorType == null)
            {
                return null;
            }

            // Type found, instantiating
            var indicator = (IEvaluator)Activator.CreateInstance(indicatorType, _context);

            return indicator;
        }
    }
}
