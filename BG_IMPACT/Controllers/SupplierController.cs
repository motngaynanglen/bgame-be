﻿using BG_IMPACT.Models;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [Authorize(Roles = "MANAGER")]
        [HttpPost("get-list-supplier")]
        public async Task<IActionResult> GetListSupplier(GetSupplierListQuery command)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("create-supplier")]
        public async Task<IActionResult> CreateSupplier(CreateSupplierCommand command)
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

        [Authorize(Roles = "MANAGER")]
        [HttpPost("update-supplier")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierCommand command)
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
