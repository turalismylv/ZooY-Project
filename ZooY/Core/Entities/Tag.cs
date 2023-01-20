using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
