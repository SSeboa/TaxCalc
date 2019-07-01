using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Service.CalculatorFactory;

namespace TaxCalculator.Service.Interfaces
{
    public interface ITaxService
    {
        Task<decimal> CalculateTaxValue(decimal salary, string postalCode);
    }
}
