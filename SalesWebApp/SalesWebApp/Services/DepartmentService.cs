using Microsoft.EntityFrameworkCore;
using SalesWebApp.Data;
using SalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Services
{
    public class DepartmentService
    {
        private readonly SalesWebAppContext _context;

        public DepartmentService(SalesWebAppContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
