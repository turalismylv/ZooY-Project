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
    public class OurHistoryRepository : Repository<OurHistory>, IOurHistoryRepository
    {

        private readonly AppDbContext _context;
        public OurHistoryRepository(AppDbContext context) : base(context)
        {


            _context = context;
        }


        public async Task<OurHistory> GetAsync()
        {
            return await _context.OurHistory.FirstOrDefaultAsync();
        }
    }
}
