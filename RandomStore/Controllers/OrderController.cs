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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] OrderCreateModel order)
        {
            var result = await _service.CreateOrderAsync(order);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = _service.GetAllOrdersAsync();

            if (orders == null)
            { 
                return BadRequest();
            }

            if (await orders.CountAsync() == 0)
            { 
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var order = await _service.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderUpdateModel order,
            [FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await _service.UpdateOrderAsync(order, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await _service.DeleteOrderAsync(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
