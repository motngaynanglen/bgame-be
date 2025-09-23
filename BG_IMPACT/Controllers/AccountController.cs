using BG_IMPACT.Models;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "CUSTOMER,MANAGER,STAFF,ADMIN")] //Nhớ sửa lại role
        [HttpPost("get-list")]
        public async Task<IActionResult> GetList([FromBody] GetCustomerListQuery query)
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
        //[Authorize(Roles = "CUSTOMER,MANAGER,STAFF,ADMIN")] //Nhớ sửa lại role
        [HttpPost("get-list-by-admin")]
        public async Task<IActionResult> GetListByAdmin([FromBody] GetAccountListByAdminQuery query)
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-list-by-manager")]
        public async Task<IActionResult> GetListByManager([FromBody] GetAccountListByManagerQuery query)
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [Authorize(Roles = "CUSTOMER,MANAGER,STAFF,ADMIN")]
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateCustomerProfileCommand command)
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
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [Authorize(Roles = "MANAGER")]
        [HttpPost("update-staff-profile")]
        public async Task<IActionResult> UpdateStaffProfile([FromBody] UpdateStaffProfileCommand command)
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
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
        [Authorize(Roles = "MANAGER")]
        [HttpPost("reverse-staff-status-for-manager")]
        public async Task<IActionResult> AccountReverseStaffStatus([FromBody] ReverseStaffStatusCommand command)
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
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("reverse-status-for-admin")]
        public async Task<IActionResult> AccountReverseStatus([FromBody] ReverseAccountStatusCommand command)
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
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("search-customer-by-phone-and-email")]
        public async Task<IActionResult> GetListCustomer([FromBody] GetCustomerListByPhoneAndEmailQuery query)
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpGet("get-profile")]
        [Authorize(Roles = "STAFF,MANAGER,CUSTOMER")]
        public async Task<IActionResult> GetProfile([FromQuery] GetAccountProfileQuery query)
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpGet("customer/{Id}")]
        [Authorize(Roles = "STAFF,MANAGER,ADMIN")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid Id)
        {
            try
            {
                GetCustomerByIdQuery query = new GetCustomerByIdQuery();
                query.Id = Id;
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpGet("customer/code/{Code}")]
        [Authorize(Roles = "STAFF,MANAGER,ADMIN")]
        public async Task<IActionResult> GetCustomerByCode([FromRoute] string Code)
        {
            try
            {
                GetCustomerByCodeQuery query = new GetCustomerByCodeQuery();
                query.Code = Code;
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
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }

        [HttpPost("point")]
        [Authorize(Roles = "STAFF,MANAGER,ADMIN")]
        public async Task<IActionResult> AddPointToCustomer([FromQuery] AddPointByAdminCommand command)
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
            catch (Exception ex)
            {
                return NotFound(new ResponseObject { StatusCode = "404", Message = "Chức năng đang bảo trì. Xin vui lòng thử lại sau!" });
            }
        }
    }
}