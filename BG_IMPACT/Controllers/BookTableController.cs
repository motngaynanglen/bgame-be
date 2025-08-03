using BG_IMPACT.Business.Command.BookTable.Commands;
using BG_IMPACT.Business.Command.BookTable.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTableController : ControllerBase
    {
        [Authorize(Roles = "CUSTOMER")]
        [HttpPost("create-booktable-by-customer")]
        public async Task<IActionResult> CreateBookTableByCustomer(CreateBookTableByCustomerCommand command)
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
        [Authorize(Roles = "STAFF")]
        [HttpPost("create-booktable-by-staff")]
        public async Task<IActionResult> CreateBookTableByStaff(CreateBookTableByStaffCommand command)
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
        [HttpPost("get-booktable-time-table-by-date")]
        public async Task<IActionResult> GetBookTableTimeTableByDate(GetBookTableByDateQuery command)
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
        [HttpPost("get-booktable-paged")]
        public async Task<IActionResult> GetBookTablePage(GetBookTablePagedQuery command)
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
