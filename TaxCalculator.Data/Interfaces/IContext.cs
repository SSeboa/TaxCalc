using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaxCalculator.Data.Interfaces
{
    public interface IContext : System.IDisposable
    {

    }
}
