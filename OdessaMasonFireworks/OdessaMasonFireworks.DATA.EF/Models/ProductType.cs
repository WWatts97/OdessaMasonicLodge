using System;
using System.Collections.Generic;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? TypeDesc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
