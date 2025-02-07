using BG_IMPACT.Command.ProductGroup.Commands;
using BG_IMPACT.Command.ProductGroup.Queries;
using BG_IMPACT.Command.ProductGroupRef.Commands;
using BG_IMPACT.Command.ProductGroupRef.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupRefController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductGroupRefCommand command)
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

        [HttpPost("get-list")]
        public async Task<IActionResult> GetList(GetProductGroupRefListQuery query)
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
    }
}
