namespace TaxCalculator.Service.CalculatorFactory
{
    public class CalculateFlatTax : Calculator
    {
        private decimal _taxRate;

        public CalculateFlatTax(decimal taxRate)
        {
            _taxRate = taxRate;
        }

        public override decimal CalculateTax(decimal salary)
        {
            // get rate from DB
            decimal taxDue = salary * _taxRate;
            return taxDue;
        }
    }
}
