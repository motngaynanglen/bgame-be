using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Command.Product.Queries;
using BG_IMPACT.Command.ProductGroup.Commands;
using BG_IMPACT.Command.ProductGroup.Queries;
using BG_IMPACT.Command.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG_IMPACT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [Authorize(Roles = "MANAGER")]
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

        [Authorize(Roles = "STAFF,MANAGER")]
        [HttpPost("search-store")]
        public async Task<IActionResult> SearchProductListInStore(GetProductListInStoreQuery command)
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

        [HttpPost("search-by-store-id")]
        public async Task<IActionResult> SearchProduct(GetProductListByStoreIdQuery command)
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

        [HttpPost("change-product-to-sales")]
        public async Task<IActionResult> ChangeProductToSales(ChangeProductToSales command)
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

        [HttpPost("change-product-to-rent")]
        public async Task<IActionResult> ChangeProductToRent(ChangeProductToRent command)
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

        [HttpPost("get-by")]
        public async Task<IActionResult> GetBy(GetProductByMultipleOption command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (result.StatusCode == "200")
                {
                    return Ok(result);
                } else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetProductByIdQuery command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (result.StatusCode == "200")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
