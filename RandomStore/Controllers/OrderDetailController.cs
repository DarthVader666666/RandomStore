using Microsoft.AspNetCore.Mvc;
using RandomStore.Services;
using RandomStore.Services.Models.OrderDetailModels;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(IOrderDetailService service) 
        {
            _service = service;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostOrderDetail([FromQuery(Name = "quant")] int quant,
            [FromQuery(Name = "orderId")] int orderId, [FromQuery(Name = "prodId")] int prodId)
        {
            var orderDetail = new OrderDetailCreateModel
            {
                OrderId = orderId,
                ProductId = prodId,
                Quantity = quant
            };

            var result = await _service.CreateOrderDetailAsync(orderDetail);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("get/all")]
        public IActionResult GetAllOrderDetails()
        {
            var orders = _service.GetAllOrderDetailsAsync();

            return Ok(orders);
        }

        [HttpGet("get/{id:int}")]
        public IActionResult GetOrderDetail([FromRoute] int id)
        {
            var order = _service.GetOrderDetailsByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrderDetail([FromQuery(Name = "quant")] int quant,
            [FromQuery(Name = "orderId")] int orderId, [FromQuery(Name = "prodId")] int prodId)
        {
            var order = new OrderDetailUpdateModel { Quantity = quant };
            var result = await _service.UpdateOrderDetailAsync(order, orderId, prodId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOrderDetail([FromQuery(Name = "orderId")] int ordeId,
            [FromQuery(Name = "prodId")] int prodId)
        {
            var result = await _service.DeleteOrderDetailAsync(ordeId, prodId);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
