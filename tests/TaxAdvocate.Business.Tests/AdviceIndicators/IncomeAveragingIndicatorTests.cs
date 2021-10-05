using System.Threading.Tasks;
using NUnit.Framework;
using TaxAdvocate.Business.Evaluators;

namespace TaxAdvocate.Business.Tests.AdviceIndicators
{
    [TestFixture]
    public class IncomeAveragingIndicatorTests : BaseServiceTestFixture
    {

        // Needs more work, currently using json files as test source

        [OneTimeSetUp]
        public void Initialize()
        {
        }


        [Test]
        public async Task GetWithAdviceIndicator()
        {
            var indicator = new IncomeAveragingIndicator(_context);

            var result = await indicator.ValidateAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(25, result.Count);
        }


    }
}
