using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Business.Services;

namespace TaxAdvocate.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdviceIndicatorController : ControllerBase
    {
        private readonly ILogger<AdviceIndicatorController> _logger;
        private readonly IAdviceService _adviceService;

        public AdviceIndicatorController(ILogger<AdviceIndicatorController> logger, IAdviceService adviceService)
        {
            _logger = logger;
            _adviceService = adviceService;
        }

        /// <summary>
        /// Get a list of all advice indicators, including name and description
        /// </summary>
        [HttpGet]
        public async Task<List<AdviceIndicator>> GetAll()
        {
            _logger.LogInformation("Getting list of all advice indicators");
            return await _adviceService.GetAllAdviceIndicatorsAsync();
        }
    }
}
