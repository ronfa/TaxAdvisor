using System.Collections.Generic;
using System.Threading.Tasks;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Model;

namespace TaxAdvocate.Business.Services
{
    public interface IAdviceService
    {
        public Task<List<AdviceIndicator>> GetAllAdviceIndicatorsAsync();
        public IEvaluator ResolveAdviceIndicator(string adviceIndicatorId);
    }
}