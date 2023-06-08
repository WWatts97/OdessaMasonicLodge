using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OdessaMasonFireworks.DATA.EF.Models
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
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

