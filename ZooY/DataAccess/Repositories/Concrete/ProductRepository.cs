using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllGetCategoryAndGetBrandAsync()
        {
            return await _context.Products.Include(p => p.ProductCategory).Include(b=>b.Brand).ToListAsync();
        }

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.ProductCategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Product>> GetByBrandIdAsync(int brandId)
        {
            return await _context.Products.Where(p => p.BrandId == brandId).ToListAsync();
        }

        //public IQueryable<Product> FilterByTitle(string title)
        //{
        //    return _context.Products.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        //}

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return product;
        }


        public async Task<int> GetPageCountAsync(int take, string title)
        {
            var products = FilterByTitle(title);
            var pagecount = await products.CountAsync();
            return (int)Math.Ceiling((decimal)pagecount / take);

        }

        public async Task<List<Product>> Filter(string title, int page, int take)
        {
            var products = FilterByTitle(title);
            return await PaginateProductsAsync(products, page, take);
        }


        public IQueryable<Product> FilterByTitle(string title)
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }

        public async Task<List<Product>> PaginateProductsAsync(IQueryable<Product> products, int page, int take)
        {
            return await products
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();
        }

        public async Task<Product> GetWithTagsAsync(int id)
        {
            return await _context.Products
                .Include(c => c.ProductTags)
                .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
   
}
