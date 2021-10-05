using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Services;

namespace TaxAdvocate.Business.Tests.Services
{
    [TestFixture]
    public class ClientServiceTests : BaseServiceTestFixture
    {
        private IClientService _service;

        public ClientServiceTests()
        {
            Initialize();
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            _service = new ClientService(new NullLogger<ClientService>(), _context, _mapper);
        }

        [Test]
        public async Task GetAllClients()
        {
            var result = await _service.GetAllAsync();
            Assert.AreEqual(100, result.Count);
        }


        [Test]
        public async Task GetWithAdviceIndicator()
        {
            var result = await _service.GetAllWithAdviceIndicatorAsync(new WealthTaxIndicator(_context));
            Assert.IsNotEmpty(result);
        }

    }
}
