using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Model
{
    public class TaxCalculationType
    {
        [Key]
        public int TaxCalculationID { get; set; } // ID (Primary key)
        public string TaxType { get; set; }

        public ICollection<TaxRate> TaxRates { get; set; }        
    }
}
