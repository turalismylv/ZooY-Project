using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task<List<Brand>> GetAllBrandAsync()
        {
            return await _context.Brands.Include(p => p.Products).ToListAsync();
        }

        public async Task<Brand> GetFirstAsync()
        {
            return await _context.Brands.FirstOrDefaultAsync();
        }
    }
}
