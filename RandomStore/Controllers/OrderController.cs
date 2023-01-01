using Microsoft.AspNetCore.Mvc;
using RandomStore.Services.Models.OrderModels;
using RandomStore.Services;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostOrder([FromBody] OrderCreateModel order)
        {
            order.OrderDate = DateTime.Now;
            var result = await _service.CreateOrderAsync(order);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("get/all")]
        public IActionResult GetAllOrders()
        {
            var orders = _service.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            var order = await _service.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateModel order, 
            [FromQuery(Name = "day")]int day, [FromQuery(Name = "month")] int month, 
            [FromQuery(Name = "year")] int year, [FromRoute] int id)
        {
            order.OrderDate = new DateTime(year, month, day);
            var result = await _service.UpdateOrderAsync(order, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _service.DeleteOrderAsync(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
