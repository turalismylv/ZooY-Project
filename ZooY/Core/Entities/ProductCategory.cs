using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }
        public string PhotoName { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
