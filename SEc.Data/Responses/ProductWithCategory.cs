using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SEc.Data.Models.Models;

namespace SEc.Data.Responses
{
    public class ProductWithCategory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }        
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string Image { get; set; }
        public string Descript { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class ROrderDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ROrder
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ROrderDetail> OrderDetails { get; set; }
    }
}
