using System.Collections.Generic;

namespace TaxAdvocate.Business.Model
{
    public class ValidationResult
    {
        public bool Valid { get; set; }
        public string ClientId { get; set; }
        public Data.Model.Client Client { get; set; }
        public int Year { get; set; }
        public List<string> Messages { get; set; }
        public IEnumerable<Data.Model.Mutation> Mutations { get; set; }
    }
}
