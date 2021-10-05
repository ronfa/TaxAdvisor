using System.Collections.Generic;

namespace TaxAdvocate.Data.Model
{
    public class Client
    {
        public string ClientId { get; set; }
        public List<Mutation> Mutations { get; set; }
    }
}
