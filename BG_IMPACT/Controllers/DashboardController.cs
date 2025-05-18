using BG_IMPACT.Command.BookList.Queries;
using BG_IMPACT.Command.ConsignmentOrder.Commands;
using BG_IMPACT.Command.Dashboard.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [Authorize(Roles = "STAFF")]
        [HttpPost("get-today-order-revenue")]
        public async Task<IActionResult> GetTodayOrderRevenueByStaff(GetTodayOrderRevenueByStaffQuery command)
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
        [HttpPost("get-today-booklist-revenue-by-staff")]
        public async Task<IActionResult> GetTodayBookListRevenueByStaff(GetTodayBookListRevenueByStaffQuery command)
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
        [HttpPost("get-pending-order-today-by-staff")]
        public async Task<IActionResult> GetPendingOrdersByStaff(GetPendingOrdersCountByStaffQuery command)
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
        [HttpPost("get-active-order-today-by-staff")]
        public async Task<IActionResult> GetActiveOrdersByStaff(GetActiveOrdersCountByStaffQuery command)
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
        [HttpPost("get-peding-booklist-today-by-staff")]
        public async Task<IActionResult> GetPendingBookListByStaff(GetPendingBookListCountByStaff command)
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