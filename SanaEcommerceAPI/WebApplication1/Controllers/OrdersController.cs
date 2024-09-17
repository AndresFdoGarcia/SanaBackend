using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEc.Data.Responses;
using SEc.DataAccess.Context;
using SEC.Business.Middleware;
using SEC.Business.SEcBusiness;

namespace SanaEcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SEcOrdersConfigurationBusiness _context;

        public OrdersController(SEcOrdersConfigurationBusiness context)
        {
            _context = context;
        }        

        [HttpGet]
        public async Task<IActionResult> healthcheck()
        {
            return StatusCode(200, "Todo va bien");
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int? category,
            [FromQuery] decimal? price_max)
        {
            var response = _context.GetProducts(category,price_max);
            if (!response.Success)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Data);
            }
        }

        [HttpGet("GetOrders")]
        //[AuthorizationMiddleware]
        public async Task<IActionResult> GetOrdersByClient(
            [FromQuery] int custid)
        {
            var response = _context.GetOrdersByClient(custid);

            if (!response.Success)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Data);
            }
        }

        [HttpPost("CreateOrder")]
        //[AuthorizationMiddleware]
        public async Task<IActionResult> CreateNweOrder(ROrder order)
        {
            var response = await _context.CreateOrderNew(order);
            if (!response || order.OrderDetails.Count <0) return BadRequest();

            return Ok();
        }
    }
}
