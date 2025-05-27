namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("update-orderitem-product")]
        public async Task<IActionResult> UpdateOrderItemProduct(UpdateOrderItemProductCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
