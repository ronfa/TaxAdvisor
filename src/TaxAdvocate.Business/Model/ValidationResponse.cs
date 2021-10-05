using System.Collections.Generic;

namespace TaxAdvocate.Business.Model
{
    public class ValidationResponse
    {
        public string ClientId { get; set; }
        public bool Valid { get; set; }
        public int Year { get; set; }
        public List<string> Messages { get; set; }
    }
}
