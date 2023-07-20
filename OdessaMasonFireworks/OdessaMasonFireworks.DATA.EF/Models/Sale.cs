using System;
using System.Collections.Generic;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SaleProducts = new HashSet<SaleProduct>();
        }

        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public decimal? SaleTotal { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
