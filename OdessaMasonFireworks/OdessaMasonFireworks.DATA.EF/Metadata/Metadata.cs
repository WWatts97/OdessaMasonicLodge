using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    public class BrandMetadata
    {

    }

    public class CustomerMetadata
    {

    }

    public class MemberMetadata
    {

    }

    public class OrderMetadata
    {

    }

    public class OrderProductMetadata
    {

    }

    public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Firework")]
        //[StringLength()]
        public string ProductName { get; set; } = null!;


        public string? Description { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        [Display(Name ="In Stock")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public short? UnitsInStock { get; set; }

        [Display(Name ="Ordered")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public short? UnitsOrdered { get; set; }


        public string? UnitType { get; set; }


        public short? UnitsPerBox { get; set; }
        public short? BoxesPerCase { get; set; }

        [Display(Name = "Cost")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? CostPerUnit { get; set; }

        [Display(Name ="Price")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? PricePerUnit { get; set; }

        [Display(Name ="Image")]
        public string? ProductImage { get; set; }
    }

    public class ProductTypeMetadata
    {

    }

    public class SaleMetadata
    {

    }

    public class SaleProductMetadata
    {

    }
}
