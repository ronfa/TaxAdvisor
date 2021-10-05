using System.Collections.Generic;
using System.Threading.Tasks;
using TaxAdvocate.Business.Model;

namespace TaxAdvocate.Business.Evaluators
{
    public interface IEvaluator
    {
        public Task<List<Data.Model.Client>> ValidateAsync();
        public IEnumerable<ValidationResult> Results { get; set; }
    }
}
