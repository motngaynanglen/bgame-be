using BG_IMPACT.Command.BookList.Queries;
using BG_IMPACT.Command.Store.Commands;
using BG_IMPACT.Command.Transaction.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [HttpPost("get-online-payment-response")]
        public async Task<IActionResult> Create(GetOnlinePaymentResponseCommand command)
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

        [HttpPost("perform-transaction")]
        public async Task<IActionResult> PerformTransaction(PerformTransactionQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
