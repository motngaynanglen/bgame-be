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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-order-revenue-by-manager")]
        public async Task<IActionResult> GetOrderRevenueByManager(GetOrderRevenueByManagerQuery command)
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
        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-booklist-revenue-by-manager")]
        public async Task<IActionResult> GetBookListRevenueByManager(GetBookListRevenueByManagerQuery command)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-order-count-by-manager")]
        public async Task<IActionResult> GetOrderCountByManagerQuery(GetOrderCountByManagerQuery command)
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
        
        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-booklist-count-by-manager")]
        public async Task<IActionResult> GetBookListCountByManagerQuery(GetBookListCountByManagerQuery command)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-revenue-per-day-by-manager")]
        public async Task<IActionResult> GetRevenuePerDayByMonth(GetRevenuePerDayByMonthQuery command)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-revenue-per-month-by-manager")]
        public async Task<IActionResult> GetRevenuePerDayByMonth(GetRevenuePerMonthQuery command)
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