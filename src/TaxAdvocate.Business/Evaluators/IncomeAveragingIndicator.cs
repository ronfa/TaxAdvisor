using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Data.Contexts;
using Client = TaxAdvocate.Data.Model.Client;

namespace TaxAdvocate.Business.Evaluators
{
    [DisplayName("Income averaging indicator")]
    [Description("High income flunctuations within 3 years range")]
    public class IncomeAveragingIndicator : IEvaluator
    {
        private readonly InMemoryDatabaseContext _context;
        private const double YearOnYearPercentThreshold = 50;

        public IncomeAveragingIndicator(InMemoryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Model.Client>> ValidateAsync()
        {
            // This query is not efficient for large datasets.
            // It will be more efficient to use SQL here with partitioned queries
            // However, InMemory database does not support raw SQL queries as of now

            var results = _context.Clients
                .Include(c => c.Mutations)
                .ToList()
                .Where(c => c.Mutations.Any(m => m.PreviousYearIncome.HasValue))
                .Distinct()
                .ToList();

            // we have a list of clients with at least 3 mutations with previous year income

            var validations = new List<ValidationResult>();

            foreach (var client in results)
            {
                // Getting list of all mutations with more than 50% difference from year before
                var mutations = client.Mutations
                    .Where(m => Math.Abs(m.YoYPercentDifference.GetValueOrDefault(0)) >= YearOnYearPercentThreshold).ToList();

                if (mutations.Any())
                {
                    // Check each mutation if part of a set of 3 years
                    foreach (var mutation in mutations)
                    {
                        var nextYear = client.Mutations.FirstOrDefault(m => m.Year == mutation.Year + 1);
                        if (nextYear != null)
                        {
                            // Mutation is the middle of the set of 3
                            validations.Add(new ValidationResult
                            {
                                Valid = true,
                                Client = client,
                                ClientId = client.ClientId,
                                Messages = new List<string>
                                {
                                    $"Income average fluctuation in year {mutation.Year} over {YearOnYearPercentThreshold / 100:P} percent.",
                                    $"Income for {mutation.Year - 1} is {mutation.PreviousYearIncome:C}",
                                    $"Income for {mutation.Year} is {mutation.Income:C}.",
                                    $"Income for {nextYear.Year} is {nextYear.Income:C}."
                                }
                            });
                            continue;
                        }

                        var prevYear = client.Mutations.FirstOrDefault(m => m.Year == mutation.Year - 2);

                        if (prevYear != null)
                        {
                            // Mutation is the last of the set of 3
                            validations.Add(new ValidationResult
                            {
                                Valid = true,
                                Client = client,
                                ClientId = client.ClientId,
                                Messages = new List<string>
                                {
                                    $"Income average fluctuation in year {mutation.Year} over {YearOnYearPercentThreshold/100:P} percent.",
                                    $"Income for {prevYear.Year} is {prevYear.Income:C}\n",
                                    $"Income for {mutation.Year - 1} is {mutation.PreviousYearIncome:C}",
                                    $"Income for {mutation.Year} is {mutation.Income:C}."
                                }
                            });
                        }
                    }
                }
            }

            if (validations.Any())
            {
                Results = validations;
                return validations.Select(v => v.Client).Distinct().ToList();
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
