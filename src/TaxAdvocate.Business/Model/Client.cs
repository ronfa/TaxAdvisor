using System.Collections.Generic;
using System.Linq;

namespace TaxAdvocate.Business.Model
{
    public class Client
    {
        public string ClientId { get; set; }
        public IEnumerable<Mutation> Mutations { get; set; }
        public IEnumerable<ValidationResponse> ValidationResults { get; set; }

        public Mutation CurrentMutation => Mutations.OrderByDescending(m => m.Year).FirstOrDefault();
    }
}
