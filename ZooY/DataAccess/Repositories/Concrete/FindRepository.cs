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
    public class FindRepository : Repository<Find>, IFindRepsitory
    {

        private readonly AppDbContext _context;
        public FindRepository(AppDbContext context) : base(context) {


            _context = context;
        }


        public async Task<Find> GetAsync()
        {
            return await _context.Find.FirstOrDefaultAsync();
        }
    }
}
