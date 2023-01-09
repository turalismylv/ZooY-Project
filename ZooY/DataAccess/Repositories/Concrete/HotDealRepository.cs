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
    public class HotDealRepository : Repository<HotDeal>, IHotDealRepository
    {
        private readonly AppDbContext _context;
        public HotDealRepository(AppDbContext context) : base(context)
        {


            _context = context;
        }


        public async Task<HotDeal> GetAsync()
        {
            return await _context.HotDeal.FirstOrDefaultAsync();
        }

    }
}
