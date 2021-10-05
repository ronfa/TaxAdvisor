using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using TaxAdvocate.WebApi.Controllers;

namespace TaxAdvocate.WebApi.Tests.Controllers
{
    public class ClientControllerTests : BaseWebApiTestFixture
    {
        private ClientController controller;

        public ClientControllerTests()
        {
            Initialize();
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            controller = new ClientController(new NullLogger<ClientController>(), ClientService, AdviceService);
        }

        [Test]
        public async Task GetAllClients()
        {
            var result = await controller.GetAll();
            Assert.AreEqual(100, result.Count);
        }


        [Test]
        public async Task GetWithAdviceIndicator()
        {
            var indicators = await AdviceService.GetAllAdviceIndicatorsAsync();

            var result = await controller.GetAllWithAdviceIndicator(indicators.FirstOrDefault()?.AdviceId);

            Assert.IsNotEmpty(result);
        }


    }
}
