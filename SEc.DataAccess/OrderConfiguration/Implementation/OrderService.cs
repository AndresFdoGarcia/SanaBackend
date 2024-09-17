using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SEc.Data.Customers;
using SEc.Data.DTO;
using SEc.Data.Models;
using SEc.Data.Responses;
using SEc.DataAccess.Context;
using SEc.DataAccess.OrderConfiguration.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SEc.Data.Models.Models;

namespace SEc.DataAccess.OrderConfiguration.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly DBSEcContext _context;
        public OrderService(DBSEcContext context)
        {
            _context = context;
        }

        public async Task<List<ProductWithCategory>> GetAllProductsWithCategories(int? categoryId, decimal? precioMax)
        {

            var query = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId.Value));
            }

            if (precioMax.HasValue)
            {
                query = query.Where(p => p.Price <= precioMax.Value);
            }            

            var products = await query.Select(p => new ProductWithCategory
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,                
                Price = p.Price,
                StockQty = p.StockQty,
                Image = p.Image,
                Descript = p.Descript,
                Categories = p.ProductCategories.Select(pc => new Category
                {
                    CategoryId = pc.CategoryId,
                    CategoryName = pc.Category.CategoryName
                }).ToList()
            }).ToListAsync();

            return products;
        }

        public async Task<List<Order>> GetAllOrdersByClient(int custid)
        {
            var orders = _context.Orders
                .Where(o => o.CustId == custid)
                .ToList();

            return orders;
        }

        public Task<List<Customer>> GetCustomer(Login user)
        {
            throw new NotImplementedException();
        }

        public async Task CreateOrderAsync(ROrder order)
        {
            var orderDetailsTable = new DataTable();
            orderDetailsTable.Columns.Add("productid", typeof(int));
            orderDetailsTable.Columns.Add("quantity", typeof(int));

            foreach (var detail in order.OrderDetails)
            {
                orderDetailsTable.Rows.Add(detail.ProductId, detail.Quantity);
            }

            var customerIdParam = new SqlParameter("@CustomerId", order.CustomerId);
            var totalAmountParam = new SqlParameter("@TotalAmount", order.TotalAmount);
            var orderDetailsParam = new SqlParameter("@OrderDetails", orderDetailsTable)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "OrderDetailsType"
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CreateOrder @CustomerId, @TotalAmount, @OrderDetails",
                customerIdParam, totalAmountParam, orderDetailsParam
            );
        }
    }
}
