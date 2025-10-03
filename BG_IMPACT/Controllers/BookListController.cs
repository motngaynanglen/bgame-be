using BG_IMPACT.Models;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookListController : ControllerBase
    {
        [Authorize(Roles = "CUSTOMER")]
        [HttpPost("create-booklist-by-customer")]
        public async Task<IActionResult> CreateBookList(CreateBookListByCustomerCommand command)
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
        [HttpPost("create-booklist-by-staff")]
        public async Task<IActionResult> CreateBookListByStaff(CreateBookListByStaffCommand command)
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

        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("end-booklist")]
        public async Task<IActionResult> EndBookList(EndBookListCommand command)
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

        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("start-booklist")]
        public async Task<IActionResult> StartBookList(StartBookListCommand command)
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

        [Authorize(Roles = "CUSTOMER,STAFF,MANAGER")]
        [HttpPost("get-booklist-by-date")]
        public async Task<IActionResult> GetBookListByDate(GetBookListByDateQuery command)
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

        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("extend-booklist")]
        public async Task<IActionResult> ExtendBookList(ExtendBookListCommand command)
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

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetBookListById([FromBody] GetBookListByIdQuery query)
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

        [Authorize(Roles = "CUSTOMER,STAFF,MANAGER,ADMIN")]
        [HttpPost("get-booklist-paged")]
        public async Task<IActionResult> GetBookListPaged([FromBody] GetBookListPagedQuery query)
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

        [HttpPost("get-booklist-available-slot")]
        public async Task<IActionResult> GetBookListAvailableSlot([FromBody] GetBookListAvailableSlotQuery query)
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

        [HttpPost("get-booklist-available-product")]
        public async Task<IActionResult> GetBookListAvailableProduct([FromBody] GetBookListAvailableProductQuery query)
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
        [HttpPost("get-booklist-timetable-by-date")]
        public async Task<IActionResult> GetBookListTimeTableByDate([FromBody] GetBookListTimeTableByDateQuery query)
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

        [Authorize(Roles = "STAFF,CUSTOMER")]
        [HttpPost("{Id}/public")]
        public async Task<IActionResult> ChangeBookListIsPublicFlag([FromRoute] ChangeBookListIsPublicFlagCommand command)
        {
            try
            {
                ResponseObject result = await _mediator.Send(command);
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
            catch
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpPost("list/public")]
        public async Task<IActionResult> GetBookListPublic([FromBody] GetBookListPublicQuery query)
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
            catch
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
    }
}
