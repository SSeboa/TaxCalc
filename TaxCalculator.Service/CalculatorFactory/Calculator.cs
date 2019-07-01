using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Service.CalculatorFactory
{
    public abstract class Calculator
    {
        public abstract decimal CalculateTax(decimal salary);
    }
}
