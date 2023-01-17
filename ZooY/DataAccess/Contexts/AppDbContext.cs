using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<HomeMainSlider> HomeMainSliders { get; set; }
        public DbSet<Find> Find { get; set; }
        public DbSet<HotDeal> HotDeal { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Welcome> Welcome { get; set; }
        public DbSet<OurHistory> OurHistory { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

    }
}
