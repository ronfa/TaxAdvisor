using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxAdvocate.Data.Contexts;
using TaxAdvocate.Data.Model;
using ValidationResult = TaxAdvocate.Business.Model.ValidationResult;

namespace TaxAdvocate.Business.Evaluators
{
    [DisplayName("Real estate value increase indicator")]
    [Description("Property value has increased by 15% or more within the last 3 years")]
    public class RealEstateValueIncreaseIndicator : IEvaluator
    {
        private readonly InMemoryDatabaseContext _context;
        private const double PropertyValuePercentageIncrease = 15;
        private const int PropertyValueComparisonPeriodInYears = 3;

        public RealEstateValueIncreaseIndicator(InMemoryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> ValidateAsync()
        {
            var results = _context.Mutations
                .Where(m => m.Year >= DateTime.Now.Year - PropertyValueComparisonPeriodInYears) // Only mutations within period
                .GroupBy(m => m.ClientId) // Group by client
                .Select(group => new
                {
                    ClientId = group.Key,
                    Min = group.Min(m => m.RealEstatePropertyValue), // Lowest property value in period
                    Max = group.Max(m => m.RealEstatePropertyValue), // Highest property value in period
                })
                // Take only those who had at least 15 percent increase
                .Where(g => ((g.Max - g.Min) / g.Min * 100) > PropertyValuePercentageIncrease)
                .Select(g => g.ClientId)
                .Distinct()
                .ToList();

            if (results.Any())
            {
                var clients = _context.Clients.Where(
                    c => results.Any(r => c.ClientId == r))
                    .Include(c => c.Mutations)
                    .ToList();

                Results = clients.Select(c => new ValidationResult
                {
                    Valid = true,
                    ClientId = c.ClientId,
                    Client = c,
                    Messages = new List<string>{ "Real estate property increase over the last 3 years" }
                });

                return clients;
            }

            Results = new List<ValidationResult>
            {
                new ValidationResult
                {
                    Valid = false
                }
            };

            return Enumerable.Empty<Client>().ToList();
        }

        public IEnumerable<ValidationResult> Results { get; set; }
    }
}
