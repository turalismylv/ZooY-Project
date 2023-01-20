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
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private readonly AppDbContext _context;
        public ContactUsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ContactUs>> GetOrderContactUs()
        {
            return await _context.ContactUs.OrderBy(c=>c.Status).ToListAsync();
        }


    }
}
