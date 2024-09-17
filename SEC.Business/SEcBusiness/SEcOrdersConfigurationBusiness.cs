using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEc.Data.Responses;
using SEc.DataAccess.OrderConfiguration.Contract;
using static SEc.Data.Models.Models;

namespace SEC.Business.SEcBusiness
{
    public class SEcOrdersConfigurationBusiness
    {
        private readonly IOrderService _orderService;

        public SEcOrdersConfigurationBusiness(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Response<List<ProductWithCategory>> GetProducts(int? categoryId, decimal? precioMax)
        {
            try
            {
                var products = _orderService.GetAllProductsWithCategories(categoryId, precioMax).Result;
                return new Response<List<ProductWithCategory>> { Success = true, Data = products };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductWithCategory>> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public Response<List<Order>> GetOrdersByClient(int custid)
        {
            try
            {
                var orders = _orderService.GetAllOrdersByClient(custid).Result;
                return new Response<List<Order>> { Success = true, Data = orders };
            }
            catch (Exception ex)
            {
                return new Response<List<Order>> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<bool> CreateOrderNew(ROrder order)
        {
            try
            {
                await _orderService.CreateOrderAsync(order);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public class Response<T>
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
            public T? Data { get; set; }
        }     
        
    }
}
