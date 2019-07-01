using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Service.CalculatorFactory
{
    public class CalculateFlatValueTax : Calculator
    {
        private decimal _taxRate;

        public CalculateFlatValueTax(decimal taxRate)
        {
            _taxRate = taxRate;
        }


        public override decimal CalculateTax(decimal salary)
        {
            // get rate from DB
            decimal taxDue;           
            taxDue = salary * _taxRate;
            return taxDue;
        }
    }
}
