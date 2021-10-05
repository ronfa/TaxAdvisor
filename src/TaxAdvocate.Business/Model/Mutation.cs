namespace TaxAdvocate.Business.Model
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

        public double TotalCapital =>
            BankBalanceNational.GetValueOrDefault(0) + BankbalanceInternational.GetValueOrDefault(0) +
            StockInvestments.GetValueOrDefault(0);
    }
}
