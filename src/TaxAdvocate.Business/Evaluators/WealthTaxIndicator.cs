using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Data.Contexts;

namespace TaxAdvocate.Business.Evaluators
{
    [DisplayName("Wealth Tax Indicator")]
    [Description("Total capital is larger than 200,000 (bank balances and stock investments combined)")]
    public class WealthTaxIndicator : IEvaluator
    {
        private const double CapitalThreshold = 200000;
        private readonly InMemoryDatabaseContext _context;

        public WealthTaxIndicator(InMemoryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Model.Client>> ValidateAsync()
        {
            var matches = await _context.Mutations.Include(m => m.Client).Where(m =>
                    m.BankBalanceNational + m.BankbalanceInternational + m.StockInvestments > CapitalThreshold)
                .ToListAsync();

            if (matches.Any())
            {
                Results = matches.Select(c => new ValidationResult
                {
                    Valid = true,
                    Client = c.Client,
                    ClientId = c.ClientId,
                    Year = c.Year,
                    Mutations = new List<Data.Model.Mutation> { c },
                    Messages = new List<string> { $"Capital is larger than {CapitalThreshold:C} for {c.Year}." }
                });
            }
            else
            {
                Results = new List<ValidationResult>
                {
                    new ValidationResult
                    {
                        Valid = false
                    }
                };
            }

            // Return list of distinct clients 
            return matches.Select(m => m.Client).Distinct().ToList();
        }

        public IEnumerable<ValidationResult> Results { get; set; }
    }
}