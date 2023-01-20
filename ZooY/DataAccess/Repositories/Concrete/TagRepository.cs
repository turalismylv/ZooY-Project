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
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Tag> GetWithProductsAsync(int id)
        {
            return await _context.Tags
                .Include(c => c.ProductTags)
                .ThenInclude(ct => ct.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
