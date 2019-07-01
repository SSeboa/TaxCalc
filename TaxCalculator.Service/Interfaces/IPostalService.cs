using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Model;

namespace TaxCalculator.Service.Interfaces
{
    public interface IPostalService
    {
        Task<List<PostalCodeDetail>> GetPostalCodes();
    }
}
