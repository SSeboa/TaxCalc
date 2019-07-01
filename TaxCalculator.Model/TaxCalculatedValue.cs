using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Model
{
    public class TaxCalculatedValue
    {
        [Key]
        public int ID { get; set; } // ID (Primary key)
        public decimal TaxCalculation { get; set; }       
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
