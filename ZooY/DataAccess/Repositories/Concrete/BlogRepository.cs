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


    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetOrderByAsync()
        {
            return await _context.Blogs.OrderByDescending(lw => lw.Id).Take(3).ToListAsync();
        }
    }
}
