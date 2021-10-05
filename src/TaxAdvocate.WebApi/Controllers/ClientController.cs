using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Business.Services;

namespace TaxAdvocate.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;
        private readonly IAdviceService _adviceService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService, IAdviceService adviceService)
        {
            _logger = logger;
            _clientService = clientService;
            _adviceService = adviceService;
        }

        /// <summary>
        /// Get a list of all clients, including mutations
        /// </summary>
        [HttpGet]
        public async Task<List<Client>> GetAll()
        {
            return await _clientService.GetAllAsync();
        }

        /// <summary>
        /// Get a list of all clients valid for a specific advice indicator
        /// </summary>
        [HttpGet]
        [Route("/[controller]/{adviceIndicatorId}")]

        public async Task<List<Client>> GetAllWithAdviceIndicator(string adviceIndicatorId)
        {
            var indicator = _adviceService.ResolveAdviceIndicator(adviceIndicatorId);

            if (indicator == null)
            {
                _logger.LogWarning("{adviceIndicatorId} could not be found");
                return null;
            }

            return await _clientService.GetAllWithAdviceIndicatorAsync(indicator);
        }
    }
}
