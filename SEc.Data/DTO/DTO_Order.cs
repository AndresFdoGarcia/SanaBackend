using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEc.Data.DTO
{
    public class DTO_Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
