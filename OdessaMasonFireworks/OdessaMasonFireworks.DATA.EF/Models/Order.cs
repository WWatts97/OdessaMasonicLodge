using System;
using System.Collections.Generic;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public string? Ponumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderTotal { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
