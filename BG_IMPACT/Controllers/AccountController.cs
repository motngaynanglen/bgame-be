using BG_IMPACT.Command.Account.Commands;
using BG_IMPACT.Command.Account.Queries;
using BG_IMPACT.Command.Login.Commands;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //[Authorize(Roles = "CUSTOMER,MANAGER,STAFF,ADMIN")] //Nhớ sửa lại role
        [HttpPost("get-list-by-admin")]
        public async Task<IActionResult> GetListByAdmin([FromBody] GetAccountListByAdminQuery query)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-list-by-manager")]
        public async Task<IActionResult> GetListByManager([FromBody] GetAccountListByManagerQuery query)
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

        [Authorize(Roles = "CUSTOMER,MANAGER,STAFF,ADMIN")]
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateCustomerProfileCommand command)
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
        [HttpPost("update-staff-profile")]
        public async Task<IActionResult> UpdateStaffProfile([FromBody] UpdateStaffProfileCommand command)
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
        [HttpPost("reverse-staff-status-for-manager")]
        public async Task<IActionResult> AccountReverseStaffStatus([FromBody] ReverseStaffStatusCommand command)
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost("reverse-status-for-admin")]
        public async Task<IActionResult> AccountReverseStatus([FromBody] ReverseAccountStatusCommand command)
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