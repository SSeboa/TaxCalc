using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Service.Interfaces;
using TaxCalculator.Data;
using TaxCalculator.Data.Interfaces;
using TaxCalculator.Service.CalculatorFactory;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaxCalculator.Service
{
    public enum tax_types
    {
        Progressive = 1,
        FlatValue,
        FlatRate
    }
    public class TaxService : ITaxService
    {
        public readonly Context _context;

        public TaxService(Context context)
        {
            _context = context;
        }

        public async Task<decimal> CalculateTaxValue(decimal salary, string pCode)
        {
            Calculator calculator = null;

            // use postalcode to call DB to retrieve taxtype
            var postalCode = await _context.PostalCodeDetails.FirstOrDefaultAsync(x => x.PostalCode.ToLower() == pCode.ToLower());

            int taxType = postalCode.FK_TaxCalculationID;
            decimal taxRate;
            var originalSalary = salary;

            switch (taxType)
            {
                case (int)tax_types.Progressive:
                    var rateValues = (await _context.TaxRates.Where(x => x.TaxCalculationType.TaxCalculationID == (int)tax_types.Progressive).ToListAsync()).Select(x => new RateValue { RateId = x.RatesID, Rate = x.Rate, From = x.From, To = x.To });
                    calculator = new CalculateProgressiveTax(rateValues.ToList());
                    break;
                case (int)tax_types.FlatRate:
                    taxRate = (await _context.TaxRates.FirstOrDefaultAsync(x => x.TaxCalculationType.TaxCalculationID == (int)tax_types.FlatRate))?.Rate ?? 0;
                    calculator = new CalculateFlatTax(taxRate);
                    break;
                case (int)tax_types.FlatValue:
                    var taxRates = await _context.TaxRates.Where(x => x.TaxCalculationType.TaxCalculationID == (int)tax_types.FlatValue).ToListAsync();
                    if(salary > 200000)
                    {
                        taxRate = taxRates.FirstOrDefault(x => x.To == 200000)?.Rate ?? 0;
                        salary = 1;
                    }
                    else
                    {
                        taxRate = taxRates.FirstOrDefault(x => x.From == 200000)?.Rate ?? 0;
                    }
                    calculator = new CalculateFlatValueTax(taxRate);
                    break;
                default:                    
                    break;
            }

            decimal taxDue = calculator.CalculateTax(salary);

            _context.TaxCalculatedValues.Add(new Model.TaxCalculatedValue { AnnualIncome = originalSalary, CreatedDate = DateTime.Now, PostalCode = pCode, TaxCalculation = taxDue });
            await _context.SaveChangesAsync();

            return taxDue;
        }        
    }
}
