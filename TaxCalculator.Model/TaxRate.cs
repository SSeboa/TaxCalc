using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Model
{
    public class TaxRate
    {
        [Key]
        public int RatesID { get; set; } // ID (Primary key)
        public decimal Rate { get; set; }  
        public int From { get; set; }
        public int To { get; set; }

        public int FK_TaxCalculationID { get; set; }

        public TaxCalculationType TaxCalculationType { get; set; }
    }
}
