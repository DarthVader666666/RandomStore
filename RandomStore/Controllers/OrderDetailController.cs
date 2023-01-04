using Microsoft.AspNetCore.Mvc;
using RandomStore.Services;
using RandomStore.Services.Models.OrderDetailsModels;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _service;

        public OrderDetailsController(IOrderDetailsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderDetail([FromBody] OrderDetailsCreateModel orderDetail)
        {
            var result = await _service.CreateOrderDetailsAsync(orderDetail);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var orderDetails = _service.GetAllOrderDetailsAsync();

            if (orderDetails == null)
            {
                return BadRequest();
            }

            if (await orderDetails.CountAsync() == 0)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByOrderIdAsync([FromRoute] int id)
        {
            var orderDetails = _service.GetOrderDetailsByIdAsync(id);

            if (orderDetails == null)
            {
                return BadRequest();
            }

            if (await orderDetails.CountAsync() == 0)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailsUpdateModel orderDetailsUpdateModel)
        {
            var result = await _service.UpdateOrderDetailsAsync(orderDetailsUpdateModel);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("order/{orderId:int}/product/{productId:int}")]
        public async Task<IActionResult> DeleteOrderDetail([FromRoute] int ordeId, [FromRoute] int productId)
        {
            var result = await _service.DeleteOrderDetailsAsync(ordeId, productId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
