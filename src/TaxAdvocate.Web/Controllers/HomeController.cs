using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Services;
using TaxAdvocate.Data.Contexts;
using TaxAdvocate.Web.Models;

namespace TaxAdvocate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;
        private readonly IAdviceService _adviceService;
        private InMemoryDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, IClientService clientService, IAdviceService adviceService, InMemoryDatabaseContext context)
        {
            _logger = logger;
            _clientService = clientService;
            _adviceService = adviceService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetAllAsync();
            ViewBag.Clients = clients.OrderByDescending(c => c.CurrentMutation.TotalCapital).ToList();

            return View();
        }

        public async Task<IActionResult> Advices()
        {
            var adviceIndicators = await _adviceService.GetAllAdviceIndicatorsAsync();

            var indicatorId = adviceIndicators.First().AdviceId;

            var indicator = _adviceService.ResolveAdviceIndicator(indicatorId);

            var clients = await _clientService.GetAllWithAdviceIndicatorAsync(indicator);
            ViewBag.Clients = clients.OrderByDescending(c => c.CurrentMutation.TotalCapital).ToList();
            ViewBag.IndicatorDescription = adviceIndicators.FirstOrDefault(i => i.AdviceId == indicatorId)?.Description;

            return View(new AdviceModel { AdviceIndicators = adviceIndicators });

        }

        public async Task<IActionResult> AdvicesPost(AdviceModel model)
        {
            var adviceIndicators = await _adviceService.GetAllAdviceIndicatorsAsync();
            var selectedIndicator = _adviceService.ResolveAdviceIndicator(model.AdviceId);
            var clients = await _clientService.GetAllWithAdviceIndicatorAsync(selectedIndicator);

            ViewBag.Clients = clients.OrderByDescending(c => c.CurrentMutation.TotalCapital).ToList();
            ViewBag.IndicatorDescription = adviceIndicators.FirstOrDefault(i => i.AdviceId == model.AdviceId)?.Description;

            model.AdviceIndicators = adviceIndicators;

            return View("Advices", model);
        }


    }
}
