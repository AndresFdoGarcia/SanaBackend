using SEc.Data.Customers;
using SEc.Data.DTO;
using SEc.Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SEc.Data.Models.Models;

namespace SEc.DataAccess.OrderConfiguration.Contract
{
    public interface IOrderService
    {
        //Task AddOrderAsync(int custId, List<DTO_Order> orderDetails);
        Task<List<ProductWithCategory>> GetAllProductsWithCategories(int? categoryId, decimal? precioMax);
        Task<List<Order>> GetAllOrdersByClient(int custid);
        //Task <Customer> GetCustomer(string custid);
        //Task <OrderDetail> GetOrder();
        Task CreateOrderAsync(ROrder order);
    }
}
