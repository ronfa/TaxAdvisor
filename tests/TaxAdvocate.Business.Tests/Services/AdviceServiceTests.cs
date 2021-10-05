using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using TaxAdvocate.Business.Services;

namespace TaxAdvocate.Business.Tests.Services
{
    [TestFixture]
    public class AdviceServiceTests : BaseServiceTestFixture
    {
        private IAdviceService _service;

        public AdviceServiceTests()
        {
            Initialize();
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            _service = new AdviceService(new NullLogger<AdviceService>(), _context);
        }

        [Test]
        public async Task EvaluatorsArePresent()
        {
            var result = await _service.GetAllAdviceIndicatorsAsync();

            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task EvaluatorsNameAndDescriptionArePresent()
        {
            var result = await _service.GetAllAdviceIndicatorsAsync();

            Assert.NotNull(result.FirstOrDefault());
            Assert.IsNotEmpty(result.First().AdviceId);
            Assert.IsNotEmpty(result.First().Description);
        }

        [Test]
        public async Task EvaluatorsIsProperlyResolved()
        {
            var result = await _service.GetAllAdviceIndicatorsAsync();
           
            Assert.NotNull(result.FirstOrDefault());

            var indicator = _service.ResolveAdviceIndicator(result.FirstOrDefault().AdviceId);

            Assert.IsNotNull(indicator);
        }
    }
}
