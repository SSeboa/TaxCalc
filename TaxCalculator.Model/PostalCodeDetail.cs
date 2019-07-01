using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Model
{
    public class PostalCodeDetail
    {
        [Key]
        public int PostalCodeID { get; set; } // ID (Primary key)
        public string PostalCode { get; set; }
        public int FK_TaxCalculationID { get; set; }
    }
}
