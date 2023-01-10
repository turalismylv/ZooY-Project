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
    public class WelcomeRepository : Repository<Welcome>, IWelcomeRepository
    {

        private readonly AppDbContext _context;
        public WelcomeRepository(AppDbContext context) : base(context)
        {


            _context = context;
        }


        public async Task<Welcome> GetAsync()
        {
            return await _context.Welcome.FirstOrDefaultAsync();
        }
    }
}
