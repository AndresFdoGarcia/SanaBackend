using SEc.Data.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SEc.Data.Models
{
    public class Models
    {
        [Table("Categories", Schema = "Production")]
        public class Category
        {
            [Key]
            public int CategoryId { get; set; }
            [Required]
            [MaxLength(100)]
            public string CategoryName { get; set; }
            [JsonIgnore] 
            public ICollection<ProductCategory> ProductCategories { get; set; }
        }

        [Table("Products", Schema = "Production")]
        public class Product
        {
            [Key]
            public int ProductId { get; set; }
            [Required]
            [MaxLength(100)]
            public string ProductName { get; set; }
            [Required]            
            public decimal Price { get; set; }
            public int StockQty { get; set; }
            public string Image { get; set; }
            public string Descript { get; set; }
            public ICollection<ProductCategory> ProductCategories { get; set; }
        }

        [Table("ProductCategories", Schema = "Production")]
        public class ProductCategory
        {
            [Key, Column(Order = 0)] 
            public int ProductId { get; set; }
            public Product Product { get; set; }

            [Key, Column(Order = 1)] 
            public int CategoryId { get; set; }
            public Category Category { get; set; }
        }

        [Table("Orders", Schema = "Sales")]
        public class Order
        {
            [Key]
            public int OrderId { get; set; }
            public int CustId { get; set; }            
            [Required]
            public DateTime OrderDate { get; set; }
            [Required]
            public decimal TotalAmount { get; set; }
            public ICollection<OrderDetail> OrderDetails { get; set; }
        }

        [Table("OrderDetails", Schema = "Sales")]
        public class OrderDetail
        {
            [Key, Column(Order = 0)] 
            public int OrderId { get; set; }
            public Order Order { get; set; }

            [Key, Column(Order = 1)] 
            public int ProductId { get; set; }
            public Product Product { get; set; }
            [Required]
            public int Quantity { get; set; }
            [Required]
            public decimal UnitPrice { get; set; }
        }
    }
}
