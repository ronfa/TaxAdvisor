using System.Collections.Generic;
using System.Threading.Tasks;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Model;

namespace TaxAdvocate.Business.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllAsync();
        Task<List<Client>> GetAllWithAdviceIndicatorAsync(IEvaluator indicator);
    }
}