using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {

        Task<List<Blog>> GetOrderByAsync();

        Task<List<Blog>> PaginateBlogsAsync(int page, int take);

        Task<int> GetPageCountAsync(int take);
        
        }
}
