using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Command.Supplier.Commands;
using BG_IMPACT.Command.Supplier.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Authorize(Roles = "MANAGER")]
        [HttpPost("create-supplier")]
        public async Task<IActionResult> CreateSupplier(CreateSupplierCommand command)
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
        [HttpPost("update-supplier")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierCommand command)
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
