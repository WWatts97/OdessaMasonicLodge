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
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [StringLength(50)]
        public string BrandName { get; set; } = null!;
    }

    public class CustomerMetadata
    {
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name ="Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(2)]
        public string? State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string? Phone { get; set; }
    }

    public class MemberMetadata
    {
        public int MemberId { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string? Position { get; set; }

        [Display(Name = "Join Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? JoinDate { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(2)]
        public string? State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name ="Phone Number")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }

    public class OrderMetadata
    {
        public int OrderId { get; set; }

        [Required]
        [Display(Name ="Purchase Order ID")]
        [StringLength(50)]
        public string? Ponumber { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true, NullDisplayText = "0.00")]
        [Display(Name = "Order Total")]
        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? OrderTotal { get; set; }
    }

    public class OrderProductMetadata
    {
        public int OrderProductId { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Firework")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name ="Quantity")]
        public short OrderQuantity { get; set; }
    }

    public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Firework")]
        [StringLength(150)]
        public string ProductName { get; set; } = null!;

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }

        [Display(Name ="Firework Type")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public int? TypeId { get; set; }

        [Display(Name ="Brand")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public int? BrandId { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "In Stock")]
        [Range(0, short.MaxValue)]
        public short? UnitsInStock { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Ordered")]
        [Range(0, short.MaxValue)]
        public short? UnitsOrdered { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Unit Type")]
        [StringLength(20)]
        public string? UnitType { get; set; }

        [Display(Name ="Units Per Box")]
        [Range(0, short.MaxValue)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public short? UnitsPerBox { get; set; }

        [Display(Name ="Units Per Case")]
        [Range(0, short.MaxValue)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public short? BoxesPerCase { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false, NullDisplayText = "[N/A]")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Cost Per Unit")]
        public decimal? CostPerUnit { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false, NullDisplayText = "[N/A]")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Price Per Unit")]
        public decimal? PricePerUnit { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Image")]
        [StringLength(100)]
        public string? ProductImage { get; set; }
    }

    public class ProductTypeMetadata
    {
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Type Of Firework")]
        [StringLength(50)]
        public string TypeName { get; set; } = null!;

        [Display(Name ="Description")]
        [StringLength(500)]
        public string? TypeDesc { get; set; }
    }

    public class SaleMetadata
    {
        public int SaleId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Sales Date")]
        public DateTime SaleDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true, NullDisplayText = "0.00")]
        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Sale Total")]
        public decimal? SaleTotal { get; set; }
    }

    public class SaleProductMetadata
    {
        public int SaleProductId { get; set; }

        [Required]
        [Display(Name = "Sale ID")]
        public int SaleId { get; set; }

        [Required]
        [Display(Name = "Firework")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [Range(0, short.MaxValue)]
        public short? SaleQuantity { get; set; }
    }
}
