using BG_IMPACT.Business.Command.Email.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("supplies-send-mail")]
        public async Task<IActionResult> SendSupplyMail(SendSupplyOrderEmailCommand command)
        {
            // Đảm bảo command.SupplyOrderId có giá trị
            if (command.SupplyOrderId == Guid.Empty)
            {
                return BadRequest("SupplyOrderId is required");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}