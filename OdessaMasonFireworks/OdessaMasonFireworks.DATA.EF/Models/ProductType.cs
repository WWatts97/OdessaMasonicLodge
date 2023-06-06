using System;
using System.Collections.Generic;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public partial class ProductType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? TypeDesc { get; set; }
    }
}
