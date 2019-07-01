using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Service.CalculatorFactory
{
    public class CalculateProgressiveTax : Calculator
    {
        private List<RateValue> _rateValues;

        public CalculateProgressiveTax(List<RateValue> rateValues)
        {
            _rateValues = rateValues;
        }

        public override decimal CalculateTax(decimal salary)
        {
            /*if (salaryWorkingRange > _rateValues.topRateThreshold)
            {
                taxDue += (salaryWorkingRange - _rateValues.topRateThreshold) * _rateValues.topRateFactor;
                salaryWorkingRange = _rateValues.topRateThreshold;
            }
            if (salaryWorkingRange > _rateValues.rate4Threshold)
            {
                taxDue += (salaryWorkingRange - _rateValues.rate4Threshold) * _rateValues.rate5RateFactor;
                salaryWorkingRange = _rateValues.rate4Threshold;
            }
            if (salaryWorkingRange > _rateValues.rate3Threshold)
            {
                taxDue += (salaryWorkingRange - _rateValues.rate3Threshold) * _rateValues.rate4RateFactor;
                salaryWorkingRange = _rateValues.rate3Threshold;
            }
            if (salaryWorkingRange > _rateValues.rate2Threshold)
            {
                taxDue += (salaryWorkingRange - _rateValues.rate2Threshold) * _rateValues.rate3RateFactor;
                salaryWorkingRange = _rateValues.rate2Threshold;
            }
            if (salaryWorkingRange > _rateValues.basicRateThreshold)
            {
                taxDue += (salaryWorkingRange - _rateValues.basicRateThreshold) * _rateValues.rate2RateFactor;
                salaryWorkingRange = _rateValues.basicRateThreshold;
            }
            if (salaryWorkingRange <= _rateValues.basicRateThreshold)
            {
                taxDue += salaryWorkingRange * _rateValues.basicRateFactor;
            }*/

            return AccumulateTax(salary);
        }

        private decimal AccumulateTax(decimal salary, int? currentTop = null)
        {
            decimal taxDue = 0;
            RateValue rateToApply;
            decimal difference;

            if (currentTop == null)
            {
                var maxRate = _rateValues.FirstOrDefault(x => x.To == 0);

                rateToApply = _rateValues.FirstOrDefault(x => x.From <= salary && x.To > 0 && x.To >= salary) ?? maxRate;
                difference = salary < rateToApply.To ? salary : salary - (rateToApply.To == 0 ? rateToApply.From : rateToApply.To);
            }
            else
            {
                rateToApply = _rateValues.FirstOrDefault(x => x.RateId == currentTop - 1);
                difference = rateToApply.To - rateToApply.From;
            }            

            var currentTaxDue = difference * rateToApply.Rate;

            if (difference > 0 && rateToApply.From > 0)
            {
                currentTaxDue += AccumulateTax(difference, rateToApply.RateId);
            }

            taxDue = currentTaxDue;

            return taxDue;
        }
    }

    public class RateValue
    {
        public int RateId { get; set; }
        public decimal Rate { get; set; }
        public int From { get; set;  }
        public int To { get; set; }
    }
}
