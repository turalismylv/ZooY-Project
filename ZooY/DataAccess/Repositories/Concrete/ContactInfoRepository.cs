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
    

    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        private readonly AppDbContext _context;
        public ContactInfoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ContactInfo> GetAsync()
        {
            return await _context.ContactInfo.FirstOrDefaultAsync();
        }
    }
}
