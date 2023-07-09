using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        [NotMapped]
        public IFormFile? Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false, NullDisplayText = "[N/A]")]
        [DataType(DataType.Currency)]
        public decimal? Price { get { return CostPerUnit * 1.8m; } }
    }

    [ModelMetadataType(typeof(BrandMetadata))]
    public partial class Brand { }

    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer { }

    [ModelMetadataType(typeof(MemberMetadata))]
    public partial class Member { }

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }

    [ModelMetadataType(typeof(OrderProductMetadata))]
    public partial class OrderProduct { }

    [ModelMetadataType(typeof(ProductTypeMetadata))]
    public partial class ProductType { }

    [ModelMetadataType(typeof(SaleMetadata))]
    public partial class Sale { }

    [ModelMetadataType(typeof(SaleProductMetadata))]
    public partial class SaleProduct { }

}

