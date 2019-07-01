using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data;
using TaxCalculator.Data.Interfaces;
using TaxCalculator.Model;
using TaxCalculator.Service.Interfaces;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TaxCalculator.Service
{
    public class PostalService : IPostalService
    {
        public readonly Context _context;

        public PostalService(Context context)
        {
            _context = context;
        }

        public async Task<List<PostalCodeDetail>> GetPostalCodes()
        {
            return await _context.PostalCodeDetails.ToListAsync();
        }
    }
}
