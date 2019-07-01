using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Service;
using TaxCalculator.Service.Interfaces;

namespace TaxCalculatorTests
{
    [TestFixture]
    public class TaxCalculatorTest
    {
        private IServiceCollection _services;
        private ServiceProvider _provider;

        public TaxCalculatorTest()
        {
            _services = new ServiceCollection();

            _services.AddDbContext<Context>(options => options.UseSqlServer("Server=BR7SSEBOA;Database=TaxDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            _services.AddTransient<ITaxService, TaxService>();

            _provider = _services.BuildServiceProvider();

        }

        [Test]
        [TestCase(400000, "7441", 117682.640, true)]
        [TestCase(400000, "7441", 217682.640, false)]
        [TestCase(400000, "1000", 117682.640, true)]
        [TestCase(400000, "1000", 217682.640, false)]
        public async Task TestProgressive(decimal salary, string postcode, decimal expectedResult, bool isTrue)
        {
            var taxService = _provider.GetRequiredService<ITaxService>();
            var result = await taxService.CalculateTaxValue(salary, postcode);
            bool finalResult = result == expectedResult;
            if (isTrue)
                Assert.True(finalResult);
            else
                Assert.True(!finalResult);
        }

        [Test]
        [TestCase(400000, "A100", 10000.00, true)]
        [TestCase(400000, "A100", 20000.00, false)]
        public async Task TestFlatValueAbove200000(decimal salary, string postcode, decimal expectedResult, bool isTrue)
        {
            var taxService = _provider.GetRequiredService<ITaxService>();
            var result = await taxService.CalculateTaxValue(salary, postcode);
            bool finalResult = result == expectedResult;
            if (isTrue)
                Assert.True(finalResult);
            else
                Assert.True(!finalResult);
        }

        [Test]
        [TestCase(100000, "A100", 5000.00, true)]
        [TestCase(100000, "A100", 10000.00, false)]
        public async Task TestFlatValueBelow200000(decimal salary, string postcode, decimal expectedResult, bool isTrue)
        {
            var taxService = _provider.GetRequiredService<ITaxService>();
            var result = await taxService.CalculateTaxValue(salary, postcode);

            bool finalResult = result == expectedResult;
            if (isTrue)
                Assert.True(finalResult);
            else
                Assert.True(!finalResult);
        }

        [Test]
        [TestCase(400000, "7000", 70000.00, true)]
        [TestCase(400000, "7000", 7000.00, false)]
        public async Task TestFlatRate(decimal salary, string postcode, decimal expectedResult, bool isTrue)
        {
            var taxService = _provider.GetRequiredService<ITaxService>();
            var result = await taxService.CalculateTaxValue(salary, postcode);

            bool finalResult = result == expectedResult;
            if (isTrue)
                Assert.True(finalResult);
            else
                Assert.True(!finalResult);
        }

    }
}
