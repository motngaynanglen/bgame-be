using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Command.Product.Queries;
using BG_IMPACT.Command.ProductGroup.Commands;
using BG_IMPACT.Command.ProductGroup.Queries;
using BG_IMPACT.Command.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpPost("create-template")]
        public async Task<IActionResult> CreateTemplate(CreateProductTemplateCommand command)
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

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
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
        [HttpPost("create-product-unknown")]
        public async Task<IActionResult> CreateProductUnknown(CreateProductUnknownCommand command)
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

        [HttpPost("search")]
        public async Task<IActionResult> SearchProduct(GetProductListQuery command)
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
