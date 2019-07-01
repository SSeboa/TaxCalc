using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Service;
using TaxCalculator.Service.Interfaces;

namespace TaxCalculatorTests
{
    [TestFixture]
    public class PostalTest
    {
        private IServiceCollection _services;
        private ServiceProvider _provider;

        public PostalTest()
        {
            _services = new ServiceCollection();

            _services.AddDbContext<Context>(options => options.UseSqlServer("Server=BR7SSEBOA;Database=TaxDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            _services.AddTransient<IPostalService, PostalService>();

            _provider = _services.BuildServiceProvider();
        }

        [Test]
        [TestCase(6, false)]
        [TestCase(4, true)]
        [TestCase(2, false)]
        [TestCase(0, false)]
        public async Task TestPostalGet(int expectedRecords, bool isTrue)
        {
            var postalService = _provider.GetRequiredService<IPostalService>();
            var result = await postalService.GetPostalCodes();

            bool finalResult = result.Count == expectedRecords;
            if (isTrue)
                Assert.True(finalResult);
            else
                Assert.True(!finalResult);
        }
    }
}
