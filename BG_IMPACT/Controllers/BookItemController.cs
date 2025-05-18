using BG_IMPACT.Command.BookItem.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemController : ControllerBase
    {
        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("update-bookitem-product")]
        public async Task<IActionResult> UpdateBookItemProduct(UpdateBookItemProductCommand command)
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
