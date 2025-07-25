using BG_IMPACT.Business.Command.BookTable.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTableController : ControllerBase
    {
        [Authorize(Roles = "CUSTOMER")]
        [HttpPost("create-booktable-by-customer")]
        public async Task<IActionResult> CreateBookTable(CreateBookTableByCustomerCommand command)
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
