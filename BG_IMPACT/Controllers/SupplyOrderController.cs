﻿using BG_IMPACT.Business.Command.SupplyOrder.Commands;
using BG_IMPACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyOrderController : ControllerBase
    {
        [Authorize(Roles = "ADMIN,MANAGER")]
        
        [HttpPost("create-supply-order")]
        public async Task<IActionResult> CreateSupplyOrder(CreateSupplyOrderCommand command)
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
    }
}