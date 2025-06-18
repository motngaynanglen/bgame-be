using BG_IMPACT.Models;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [HttpPost("get-online-payment-response")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmOnlinePaymentCommand command)
        {
            try
            {
                ResponseObject result = await _mediator.Send(command);
                return Ok(result);
            }
            catch
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpPost("perform-transaction")]
        public async Task<IActionResult> PerformTransaction(PerformTransactionCommand query)
        {
            try
            {
                ResponseObject result = await _mediator.Send(query);
                if (result.StatusCode == "200")
                {
                    return Ok(result);
                }
                else if (result.StatusCode == "403")
                {
                    return Forbid();
                }
                else if (result.StatusCode == "422")
                {
                    return UnprocessableEntity(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = ex.ToString() });
            }
        }
    }
}
