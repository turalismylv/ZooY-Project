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


        public async Task<int> GetPageCountAsync(int take)
        {

            var blogsCount = await _context.Blogs.CountAsync();

            return (int)Math.Ceiling((decimal)blogsCount / take);

        }

        public async Task<List<Blog>> PaginateBlogsAsync(int page, int take)
        {

            return await _context.Blogs
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();

        }
    }
}
