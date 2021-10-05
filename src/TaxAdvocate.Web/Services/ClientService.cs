using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Business.Services;

namespace TaxAdvocate.Web.Services
{
    public class ClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientService _clientService;
        private readonly IAdviceService _adviceService;

        public ClientService(ILogger<ClientService> logger, IClientService clientService, IAdviceService adviceService)
        {
            _logger = logger;
            _clientService = clientService;
            _adviceService = adviceService;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clientService.GetAllAsync();
        }

    }
}
