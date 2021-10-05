using System.Linq;

namespace TaxAdvocate.Data.Model
{
    public class Mutation
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public int Year { get; set; }
        public double? Income { get; set; }
        public double? RealEstatePropertyValue { get; set; }
        public double? BankBalanceNational { get; set; }
        public double? BankbalanceInternational { get; set; }
        public double? StockInvestments { get; set; }
        public Client Client { get; set; }
        public double? PreviousYearIncome
        {
            get
            {
                var prevYear = Year - 1;
                var prevMutation = Client.Mutations.FirstOrDefault(m => m.Year == prevYear);
                return prevMutation?.Income;
            }
        }
        public double? YoYPercentDifference
        {
            get
            {
                if (PreviousYearIncome.HasValue && Income.HasValue)
                {
                    return (Income - PreviousYearIncome) / PreviousYearIncome * 100;
                }

                return null;
            }
        }
    }
}
