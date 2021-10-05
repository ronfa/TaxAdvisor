using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using TaxAdvocate.WebApi.Controllers;

namespace TaxAdvocate.WebApi.Tests.Controllers
{
    public class AdviceControllerTests : BaseWebApiTestFixture
    {
        private AdviceIndicatorController controller;

        public AdviceControllerTests()
        {
            Initialize();
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            controller = new AdviceIndicatorController(new NullLogger<AdviceIndicatorController>(), AdviceService);
        }

        [Test]
        public async Task GetAllIndicators()
        {
            var result = await controller.GetAll();
            Assert.AreEqual(3, result.Count);
        }
    }
}
