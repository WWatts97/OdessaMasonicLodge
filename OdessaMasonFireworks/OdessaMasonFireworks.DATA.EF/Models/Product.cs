using System;
using System.Collections.Generic;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            SaleProducts = new HashSet<SaleProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOrdered { get; set; }
        public string? UnitType { get; set; }
        public short? UnitsPerBox { get; set; }
        public short? BoxesPerCase { get; set; }
        public decimal? CostPerUnit { get; set; }
        public decimal? PricePerUnit { get; set; }
        public string? ProductImage { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual ProductType1? Type { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
