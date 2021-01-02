using Microsoft.EntityFrameworkCore;
using SalesWebApp.Data;
using SalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebAppContext _context;

        public SalesRecordService(SalesWebAppContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var sales = from sale in _context.SalesRecord select sale;
            
            if (minDate.HasValue)
            {
                sales = sales.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                sales = sales.Where(x => x.Date <= maxDate.Value);
            }

            return await sales
                            .Include(x => x.Seller)
                            .Include(x => x.Seller.Department)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var sales = from sale in _context.SalesRecord select sale;

            if (minDate.HasValue)
            {
                sales = sales.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                sales = sales.Where(x => x.Date <= maxDate.Value);
            }

            return await sales
                            .Include(x => x.Seller)
                            .Include(x => x.Seller.Department)
                            .OrderByDescending(x => x.Date)
                            .GroupBy(x => x.Seller.Department)
                            .ToListAsync();
        }
    }
}
